Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization
Imports System.Windows.Forms

Namespace General
    Module Installs
        Private Const installVersionInfoJson As String = "info.json"
        Function GetInstallVersion(installPath As String) As Version
            Dim infoPath As String = Path.Combine(installPath, "data", "base", installVersionInfoJson)

            If Not File.Exists(infoPath) Then
                Throw New FileNotFoundException("Invalid Install! Missing " & installVersionInfoJson, infoPath)
            End If

            Dim jsonObject As String = File.ReadAllText(infoPath)

            ' https://stackoverflow.com/a/38944715/2999220
            Dim jss As New JavaScriptSerializer()
            Dim jsonObjectDict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(jsonObject)

            If jsonObjectDict Is Nothing OrElse Not jsonObjectDict.ContainsKey("version") Then
                Throw New InvalidDataException(installVersionInfoJson & " missing version key!")
            End If
            Dim version As Version = Nothing
            If Not Version.TryParse(DirectCast(jsonObjectDict("version"), String), version) Then
                Throw New InvalidDataException(installVersionInfoJson & " version key not convertable to Version!")
            End If

            Return version
        End Function

        Friend Function GetInstallImage(instancePath As String, imageSize As Integer) As Drawing.Image
            Dim installIconPath As String = Path.Combine(instancePath, "data", "core", "graphics", "factorio-icon.png")

            If Not File.Exists(installIconPath) Then
                Throw New FileNotFoundException("Invalid Install! Missing factorio-icon.png", installIconPath)
            End If

            Return Helpers.ResizeImage(Drawing.Image.FromFile(installIconPath), imageSize)
        End Function

        Private Function ReplaceFactorioVars(factorioPath As String, installPath As String) As String
            If factorioPath.StartsWith("__PATH__executable__") Then
                factorioPath = factorioPath.Replace("__PATH__executable__", Path.Combine(installPath, "bin", "x64"))
            End If
            If factorioPath.StartsWith("__PATH__system-write-data__") Then
                If Helpers.GetOS() = OS.Windows Then
                    factorioPath = factorioPath.Replace("__PATH__system-write-data__", Path.Combine(Environment.GetEnvironmentVariable("AppData"), "Factorio"))
                ElseIf Helpers.GetOS = OS.Linux Then
                    factorioPath = factorioPath.Replace("__PATH__system-write-data__", Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".factorio"))
                End If
            End If
            Return factorioPath
        End Function

        Private Const installInstanceConfigCfg As String = "config-path.cfg"
        Private Const installInstanceConfigIni As String = "config.ini"
        Function GetInstallCurrentInstance(installPath As String) As String
            Dim configPath As String = Path.Combine(installPath, installInstanceConfigCfg)
            If Not File.Exists(configPath) Then
                Throw New FileNotFoundException("Invalid Install! Missing " & installInstanceConfigCfg, configPath)
            End If

            Dim instanceConfigPath As String = Nothing
            Using sr As New StreamReader(configPath)
                While Not sr.EndOfStream
                    Dim line As String = sr.ReadLine()
                    If line.StartsWith("config-path=") Then
                        instanceConfigPath = line.Substring(12)
                        Exit While
                    End If
                End While
            End Using

            If instanceConfigPath Is Nothing Then
                Throw New InvalidDataException(installInstanceConfigCfg & " missing config-path key!")
            End If

            instanceConfigPath = ReplaceFactorioVars(instanceConfigPath, installPath)
            instanceConfigPath = Path.Combine(Path.GetFullPath(instanceConfigPath), installInstanceConfigIni)
            If Not File.Exists(instanceConfigPath) Then
                Throw New FileNotFoundException("Invalid Instance Config! Missing " & installInstanceConfigIni, instanceConfigPath)
            End If

            Dim instancePath As String = Nothing
            Using sr As New StreamReader(instanceConfigPath)
                Dim isInSection As Boolean = False
                While Not sr.EndOfStream
                    Dim line As String = sr.ReadLine()
                    If isInSection AndAlso line.StartsWith("write-data=") Then
                        instancePath = line.Substring(11)
                        Exit While
                    ElseIf isInSection AndAlso line.StartsWith("[") AndAlso line.EndsWith("]") Then
                        ' found next section. just exit, as throwing error is handled after
                        Exit While
                    ElseIf line = "[path]" Then
                        isInSection = True
                    End If
                End While
            End Using

            If instancePath Is Nothing Then
                Throw New InvalidDataException(installInstanceConfigIni & " missing write-data key!")
            End If

            instancePath = ReplaceFactorioVars(instancePath, installPath)
            instancePath = Path.GetFullPath(instancePath)

            Return instancePath
        End Function

        Sub SetInstallCurrentInstance(installPath As String, instancePath As String)
            Dim configPath As String = Path.Combine(installPath, installInstanceConfigCfg)
            If Not File.Exists(configPath) Then
                Throw New FileNotFoundException("Invalid Install! Missing " & installInstanceConfigCfg, configPath)
            End If

            Dim instanceConfigPath As String = Path.GetFullPath(Path.Combine(instancePath, "config"))
            Dim configContents As String() = File.ReadAllLines(configPath)
            Dim setPath As Boolean = False
            For i = 0 To configContents.Length - 1
                If configContents(i).StartsWith("config-path=") Then
                    configContents(i) = "config-path=" & instanceConfigPath
                    setPath = True
                    Exit For
                End If
            Next

            If Not setPath Then
                Throw New InvalidDataException(installInstanceConfigCfg & " missing config-path key!")
            End If
            Helpers.WriteAllLines(configPath, configContents, Microsoft.VisualBasic.vbLf)

            instanceConfigPath = Path.Combine(instanceConfigPath, installInstanceConfigIni)
            If Not File.Exists(instanceConfigPath) Then
                Throw New FileNotFoundException("Invalid Instance Config! Missing " & installInstanceConfigIni, instanceConfigPath)
            End If

            configContents = File.ReadAllLines(instanceConfigPath)
            setPath = False
            Dim isInSection As Boolean = False
            For i = 0 To configContents.Length - 1
                Dim line As String = configContents(i)
                If isInSection AndAlso line.StartsWith("write-data=") Then
                    configContents(i) = "write-data=" & instancePath
                    setPath = True
                    Exit For
                ElseIf isInSection AndAlso line.StartsWith("[") AndAlso line.EndsWith("]") Then
                    ' found next section. just exit, as throwing error is handled after
                    Exit For
                ElseIf line = "[path]" Then
                    isInSection = True
                End If
            Next

            If Not setPath Then
                Throw New InvalidDataException(installInstanceConfigIni & " missing write-data key!")
            End If
            Helpers.WriteAllLines(instanceConfigPath, configContents, Microsoft.VisualBasic.vbLf)
        End Sub

        Function FindInstallExe(installPath As String) As String
            Dim winExePath64 As String = Path.Combine(installPath, "bin", "x64", "factorio.exe")
            Dim winExePath32 As String = Path.Combine(installPath, "bin", "x86", "factorio.exe")
            Dim linExePath64 As String = Path.Combine(installPath, "bin", "x64", "factorio")
            Dim linExePath32 As String = Path.Combine(installPath, "bin", "x86", "factorio")
            Dim macExePath As String = Path.Combine(installPath, "MacOS", "factorio")

            Select Case Helpers.GetOS()
                Case OS.Windows
                    If File.Exists(winExePath64) Then Return winExePath64
                    If File.Exists(winExePath32) Then Return winExePath32
                Case OS.Linux
                    If File.Exists(linExePath64) Then Return linExePath64
                    If File.Exists(linExePath32) Then Return linExePath32
                Case OS.Other
                    If File.Exists(macExePath) Then Return macExePath
                    ' check Linux paths anyway, just in case
                    If File.Exists(linExePath64) Then Return linExePath64
                    If File.Exists(linExePath32) Then Return linExePath32
            End Select

            Throw New FileNotFoundException("Executable not found!")
        End Function

        Function FindSteamInstall() As String
            'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 427520\InstallLocation
            Dim keyPath As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 427520"

            Dim localKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(
                    Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Default)
            localKey = localKey.OpenSubKey(keyPath)

            If localKey IsNot Nothing AndAlso localKey.GetValue("InstallLocation") IsNot Nothing Then
                Dim rtn As String = localKey.GetValue("InstallLocation").ToString()
                If Directory.Exists(rtn) Then
                    Return Path.GetFullPath(rtn)
                End If
            End If

            Return Nothing
        End Function

        Function FindStandaloneInstall() As String
            'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Factorio_is1\InstallLocation
            Dim keyPath As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Factorio_is1"

            Dim localKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(
                    Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Default)
            localKey = localKey.OpenSubKey(keyPath)

            If localKey IsNot Nothing AndAlso localKey.GetValue("InstallLocation") IsNot Nothing Then
                Dim rtn As String = localKey.GetValue("InstallLocation").ToString()
                If Directory.Exists(rtn) Then
                    Return Path.GetFullPath(rtn)
                End If
            End If

            Return Nothing
        End Function
    End Module

    Module Instances
        Private Const instanceVersionDataJson As String = "player-data.json"
        Function GetInstanceVersion(instancePath As String) As Version
            Dim dataPath As String = Path.Combine(instancePath, instanceVersionDataJson)
            If Not File.Exists(dataPath) Then
                Throw New FileNotFoundException("Invalid Instance! Missing " & instanceVersionDataJson, dataPath)
            End If

            Dim jsonObject As String = File.ReadAllText(dataPath)
            Dim jss As New JavaScriptSerializer()
            Dim jsonObjectDict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(jsonObject)

            If Not jsonObjectDict.ContainsKey("last-played-version") Then
                Throw New InvalidDataException(instanceVersionDataJson & " missing last-played-version key!")
            End If
            Dim lastPlayedVersion As Dictionary(Of String, Object) = TryCast(jsonObjectDict("last-played-version"), Dictionary(Of String, Object))
            If lastPlayedVersion Is Nothing Then
                Throw New InvalidDataException(instanceVersionDataJson & " last-played-version key not convertable to array!")
            End If

            If Not lastPlayedVersion.ContainsKey("game_version") Then
                Throw New InvalidDataException(instanceVersionDataJson & " missing last-played-version.game_version key!")
            End If
            Dim version As Version = Nothing
            If Not Version.TryParse(DirectCast(lastPlayedVersion("game_version"), String), version) Then
                Throw New InvalidDataException(instanceVersionDataJson & " last-played-version.game_version key not convertable to Version!")
            End If

            Return version
        End Function

        Sub CreateInstance(instancePath As String)
            If WalkmanLib.IsFileOrDirectory(instancePath).HasFlag(PathEnum.Exists) Then
                Throw New InvalidOperationException(instancePath & " already exists!")
            End If

            Directory.CreateDirectory(Path.Combine(instancePath, "config"))
            Dim configFilePath As String = Path.Combine(instancePath, "config", "config.ini")

            Dim fileContents As String() = { ' default config.ini header
                                               "; version=9",
                                               "; This is INI file : https://en.wikipedia.org/wiki/INI_file#Format",
                                               "; Semicolons (;) at the beginning of the line indicate a comment. Comment lines are ignored.",
                                               "[path]",
                                               "read-data=" & Path.Combine("__PATH__executable__", "..", "..", "data"),
                                               "write-data=" & instancePath
                                           }

            Helpers.WriteAllLines(configFilePath, fileContents, Microsoft.VisualBasic.vbLf)
        End Sub

        Async Function DeleteInstance(instancePath As String) As Task(Of Boolean)
            If MessageBox.Show($"Are you sure you want to delete ""{instancePath}""?", "Deleting Instance",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                Await Task.Run(Sub() Directory.Delete(instancePath, recursive:=True))
                Return True
            Else
                Return False
            End If
        End Function

        Iterator Function FindInstances(rootPath As String) As IEnumerable(Of String)
            For Each dir As String In Directory.EnumerateDirectories(rootPath)
                Try
                    GetInstanceVersion(dir)
                    Yield dir
                Catch : End Try
            Next
        End Function
    End Module

    Module Sorting
        Public Sub Sort(sender As ListView, items As ListView.ListViewItemCollection, columnIndex As Integer, sortOrder As SortOrder)
            Dim itemInfos As IOrderedEnumerable(Of ListViewItem) = items.Cast(Of ListViewItem).OrderBy(Function(x) x.Text)

            If sortOrder = SortOrder.Ascending Then
                itemInfos = itemInfos.OrderBy(Function(x) keySelector(x, sender, columnIndex))
                itemInfos = itemInfos.ThenBy(Function(x) keySelector(x, sender, 0))
            ElseIf sortOrder = SortOrder.Descending Then
                itemInfos = itemInfos.OrderByDescending(Function(x) keySelector(x, sender, columnIndex))
                itemInfos = itemInfos.ThenByDescending(Function(x) keySelector(x, sender, 0))
            Else
                Return
            End If

            Dim itemArr As ListViewItem() = itemInfos.ToArray()

            sender.BeginUpdate()
            items.Clear()
            items.AddRange(itemArr)
            sender.EndUpdate()
        End Sub

        Private Function keySelector(item As ListViewItem, sender As ListView, columnIndex As Integer) As Object
            If sender Is FactorioInstanceManager.lstInstalls Then
                Dim install As Settings.Install = FactorioInstanceManager.GetInstall(item)
                Select Case columnIndex
                    Case 0
                        Return install.Path
                    Case 1
                        Return install.Version
                    Case 2
                        Return item.SubItems(2).Text
                    Case Else
                        Throw New InvalidOperationException($"Invalid columnIndex! ({columnIndex})")
                End Select
            ElseIf sender Is FactorioInstanceManager.lstInstances Then
                Dim instance As Settings.Instance = FactorioInstanceManager.GetInstance(item)
                Select Case columnIndex
                    Case 0
                        Return instance.Path
                    Case 1
                        Return instance.Version
                    Case 2
                        Return instance.IconPath
                    Case Else
                        Throw New InvalidOperationException($"Invalid columnIndex! ({columnIndex})")
                End Select
            Else
                Throw New InvalidOperationException($"Invalid Sender! ({sender?.ToString()})")
            End If
        End Function
    End Module
End Namespace
