Imports System
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms

Public Class FactorioInstanceManager
    Private _settingsLoaded As Boolean = False
    Private Sub FactorioInstanceManager_Load() Handles Me.Shown
        lstInstalls.DoubleBuffered(True)
        lstInstances.DoubleBuffered(True)

        Settings.Init()

        Me.Width = Settings.WindowWidth
        Me.Height = Settings.WindowHeight
        Me.CenterToParent()

        If Settings.WindowMaximised Then Me.WindowState = FormWindowState.Maximized

        menuStripToolsEnableUpdate.Checked = Not Settings.DisableUpdateCheck
        If Not Settings.DisableUpdateCheck Then
            WalkmanLib.CheckIfUpdateAvailableInBackground("FactorioInstanceManager", My.Application.Info.Version,
                                                          New ComponentModel.RunWorkerCompletedEventHandler(AddressOf UpdateCheckComplete))
        End If

        For Each install As Settings.Install In Settings.Installs
            lstInstalls.Items.Add(CreateInstallItem(install))
        Next
        For Each instance As Settings.Instance In Settings.Instances
            lstInstances.Items.Add(CreateInstanceItem(instance))
        Next

        _settingsLoaded = True

        lstItemSelectionChanged()
        Dim unused = UpdateInfo()
    End Sub

    Private Sub UpdateCheckComplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
        If Not Settings.DisableUpdateCheck Then
            If e.Error Is Nothing Then
                If DirectCast(e.Result, Boolean) Then
                    Select Case WalkmanLib.CustomMsgBox("An update is available!", "Update Check", "Go to Download page", "Disable Update Check",
                                                        "Ignore", MessageBoxIcon.Information, ownerForm:=Me)
                        Case "Go to Download page"
                            Diagnostics.Process.Start("https://github.com/Walkman100/FactorioInstanceManager/releases/latest")
                        Case "Disable Update Check"
                            menuStripToolsEnableUpdate.Checked = False
                    End Select
                End If
            Else
                If WalkmanLib.CustomMsgBox("Update check failed!" & Environment.NewLine & e.Error.Message, "Update Check", "OK",
                                           "Disable Update Check", style:=MessageBoxIcon.Exclamation, ownerForm:=Me) = "Disable Update Check" Then
                    menuStripToolsEnableUpdate.Checked = False
                End If
            End If
        End If
    End Sub

    Private Sub UpdateSettingsItems()
        Settings.Installs.Clear()
        Settings.Installs.AddRange(lstInstalls.Items.Cast(Of ListViewItem).Select(AddressOf GetInstall))

        Settings.Instances.Clear()
        Settings.Instances.AddRange(lstInstances.Items.Cast(Of ListViewItem).Select(AddressOf GetInstance))

        Settings.SaveSettings()
    End Sub

    Private Sub FactorioInstanceManager_Resize() Handles MyBase.Resize
        If _settingsLoaded Then
            If (Me.WindowState = FormWindowState.Maximized AndAlso Not Settings.WindowMaximised) OrElse
                    (Me.WindowState = FormWindowState.Normal AndAlso Settings.WindowMaximised) Then
                ' ResizeEnd event below doesn't fire when window is Maximised, so save if the state has changed
                Settings.WindowMaximised = (Me.WindowState = FormWindowState.Maximized)
                Settings.SaveSettings()
            End If

            If Me.WindowState <> FormWindowState.Maximized Then
                Settings.WindowWidth = Me.Width
                Settings.WindowHeight = Me.Height
            End If
        End If
    End Sub
    Private Sub FactorioInstanceManager_ResizeEnd() Handles MyBase.ResizeEnd
        Settings.SaveSettings()
    End Sub

    Private Async Function UpdateInfo() As Task
        For Each item As ListViewItem In lstInstalls.Items
            Dim install As Settings.Install = GetInstall(item)

            Dim installVersion As Version = Nothing
            Try
                installVersion = Await Task.Run(Function() General.GetInstallVersion(install.Path))
            Catch : End Try
            install.Version = installVersion

            ' UpdateInstallItem runs GetInstallCurrentInstance, which accesses files
            Await Task.Run(Sub() UpdateInstallItem(item, install))
        Next

        For Each item As ListViewItem In lstInstances.Items
            Dim instance As Settings.Instance = GetInstance(item)

            Dim instanceVersion As Version = Nothing
            Try
                instanceVersion = Await Task.Run(Function() General.GetInstanceVersion(instance.Path))
            Catch : End Try
            instance.Version = instanceVersion

            UpdateInstanceItem(item, instance)
        Next

        UpdateSettingsItems()
    End Function

#Region "Install Helpers"
    Public Shared Function GetInstall(item As ListViewItem) As Settings.Install
        Return DirectCast(item.Tag, Settings.Install)
    End Function

    Public Shared Function CreateInstallItem(install As Settings.Install) As ListViewItem
        Return UpdateInstallItem(New ListViewItem({"", "", ""}), install)
    End Function

    Public Shared Function UpdateInstallItem(item As ListViewItem, install As Settings.Install) As ListViewItem
        item.SubItems(0).Text = install.Path
        item.SubItems(1).Text = install.Version?.ToString()
        Try : item.SubItems(2).Text = General.GetInstallCurrentInstance(install.Path)
        Catch ex As Exception : item.SubItems(2).Text = "Error: " & ex.Message
        End Try
        item.Tag = install
        Return item
    End Function
#End Region

#Region "Instance Helpers"
    Public Shared Function GetInstance(item As ListViewItem) As Settings.Instance
        Return DirectCast(item.Tag, Settings.Instance)
    End Function

    Public Shared Function CreateInstanceItem(instance As Settings.Instance) As ListViewItem
        Return UpdateInstanceItem(New ListViewItem({"", "", ""}), instance)
    End Function

    Public Shared Function UpdateInstanceItem(item As ListViewItem, instance As Settings.Instance) As ListViewItem
        item.SubItems(0).Text = instance.Path
        item.SubItems(1).Text = instance.Version?.ToString()
        item.SubItems(2).Text = instance.IconPath
        item.Tag = instance
        Return item
    End Function
#End Region

#Region "MenuStrip Events"
    Private Sub menuStripFileAddInstance_Click() Handles menuStripFileAddInstance.Click
        Dim selectedPath As String = Settings.DefaultInstancePath & Path.DirectorySeparatorChar
        If Helpers.SelectFolderDialog(selectedPath, "Select Instance Folder", False) = DialogResult.OK Then
            Try
                Dim instanceVersion As Version = General.GetInstanceVersion(selectedPath)
                lstInstances.Items.Add(CreateInstanceItem(New Settings.Instance With {
                                                              .Path = selectedPath,
                                                              .Version = instanceVersion
                                                          }))
                UpdateSettingsItems()
            Catch ex As FileNotFoundException
                MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                "Error Adding Instance", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                WalkmanLib.ErrorDialog(ex, "Error Adding Instance!" & Environment.NewLine)
            End Try
        End If
    End Sub
    Private Sub menuStripFileAddInstall_Click() Handles menuStripFileAddInstall.Click
        Dim selectedPath As String = ""
        If Helpers.SelectFolderDialog(selectedPath, "Select Install Folder", False) = DialogResult.OK Then
            Try
                Dim installVersion As Version = General.GetInstallVersion(selectedPath)
                lstInstalls.Items.Add(CreateInstallItem(New Settings.Install With {
                                                            .Path = selectedPath,
                                                            .Version = installVersion
                                                        }))
                UpdateSettingsItems()
            Catch ex As FileNotFoundException
                MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                "Error Adding Install", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                WalkmanLib.ErrorDialog(ex, "Error Adding Install!" & Environment.NewLine)
            End Try
        End If
    End Sub
    Private Sub menuStripFileRemoveSelected_Click() Handles menuStripFileRemoveSelected.Click
        For Each item As ListViewItem In lstInstalls.SelectedItems
            item.Remove()
        Next

        For Each item As ListViewItem In lstInstances.SelectedItems
            item.Remove()
        Next

        UpdateSettingsItems()
    End Sub
    Private Sub menuStripFileCreateInstance_Click() Handles menuStripFileCreateInstance.Click
        Dim parentPath As String = Settings.DefaultInstancePath
        If Helpers.SelectFolderDialog(parentPath, "Select Instance Parent Folder", True) <> DialogResult.OK Then
            Exit Sub
        End If

        Dim instanceName As String = ""
        If Helpers.GetInput(instanceName, "Creating Instance", "Enter Instance Name") <> DialogResult.OK Then
            Exit Sub
        End If

        Dim instancePath As String = Path.Combine(parentPath, instanceName)

        General.CreateInstance(instancePath)
        lstInstances.Items.Add(CreateInstanceItem(New Settings.Instance With {
                                                      .Path = instancePath,
                                                      .Version = Nothing
                                                  }))
        UpdateSettingsItems()
    End Sub
    Private Async Sub menuStripFileDeleteInstance_Click() Handles menuStripFileDeleteInstance.Click
        Dim items As Collections.Generic.List(Of ListViewItem) = lstInstances.SelectedItems.Cast(Of ListViewItem).ToList()

        For Each item As ListViewItem In items
            item.ForeColor = Drawing.SystemColors.GrayText
            item.Selected = False
        Next

        For Each item As ListViewItem In items
            Dim instance As Settings.Instance = GetInstance(item)
            If Await General.DeleteInstance(instance.Path) Then
                item.Remove()
            Else ' restore default color
                item.ForeColor = Drawing.SystemColors.WindowText
            End If
        Next
        UpdateSettingsItems()
    End Sub
    Private Sub menuStripFileExit_Click() Handles menuStripFileExit.Click
        Application.Exit()
    End Sub

    Private Async Sub menuStripEditSetInstallInstance_Click() Handles menuStripEditSetInstallInstance.Click
        If lstInstances.SelectedItems.Count <> 1 Then
            MessageBox.Show("Select One Instance to apply!", "Error Setting Instance", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim instancePath As String = GetInstance(lstInstances.SelectedItems(0)).Path
        For Each install As Settings.Install In lstInstalls.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstall)
            Try
                Await Task.Run(Sub() General.SetInstallCurrentInstance(install.Path, instancePath))
            Catch ex As FileNotFoundException
                MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                "Error Setting Install Instance", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                WalkmanLib.ErrorDialog(ex, "Error Setting Install Instance!" & Environment.NewLine)
            End Try
        Next

        Await UpdateInfo()
    End Sub
    Private Sub menuStripEditSelectAll_Click() Handles menuStripEditSelectAll.Click
        For Each item As ListViewItem In lstInstalls.Items
            item.Selected = True
        Next
        For Each item As ListViewItem In lstInstances.Items
            item.Selected = True
        Next
    End Sub
    Private Sub menuStripEditDeselectAll_Click() Handles menuStripEditDeselectAll.Click
        For Each item As ListViewItem In lstInstalls.Items
            item.Selected = False
        Next
        For Each item As ListViewItem In lstInstances.Items
            item.Selected = False
        Next
    End Sub
    Private Sub menuStripEditInvertSelection_Click() Handles menuStripEditInvertSelection.Click
        For Each item As ListViewItem In lstInstalls.Items
            item.Selected = Not item.Selected
        Next
        For Each item As ListViewItem In lstInstances.Items
            item.Selected = Not item.Selected
        Next
    End Sub

    Private Sub menuStripToolsScan_Click() Handles menuStripToolsScan.Click
        Dim selectedPath As String = Settings.DefaultInstancePath
        If Helpers.SelectFolderDialog(selectedPath, "Select Root Folder to scan", False) = DialogResult.OK Then
            For Each path As String In General.FindInstances(selectedPath)
                Dim instanceVersion As Version = General.GetInstanceVersion(path)
                lstInstances.Items.Add(CreateInstanceItem(New Settings.Instance With {
                                                              .Path = path,
                                                              .Version = instanceVersion
                                                          }))
            Next
            UpdateSettingsItems()
        End If
    End Sub
    Private Sub menuStripToolsDetectInstall_Click() Handles menuStripToolsDetectInstall.Click
        Dim installPath As String = General.FindSteamInstall()

        If installPath IsNot Nothing Then
            Dim installVersion As Version = Nothing
            Try
                installVersion = General.GetInstallVersion(installPath)
            Catch : End Try

            lstInstalls.Items.Add(CreateInstallItem(New Settings.Install With {
                                                        .Path = installPath,
                                                        .Version = installVersion
                                                    }))
            UpdateSettingsItems()
        Else
            MessageBox.Show("Could not find Factorio Steam install!")
        End If
    End Sub
    Private Async Sub menuStripToolsUpdate_Click() Handles menuStripToolsUpdate.Click
        Await UpdateInfo()
    End Sub
    Private Sub menuStripToolsSetDefaultInstancePath_Click() Handles menuStripToolsSetDefaultInstancePath.Click
        If Helpers.SelectFolderDialog(Settings.DefaultInstancePath, "Select Default Instance Path", False) = DialogResult.OK Then
            Settings.SaveSettings()
        End If
    End Sub
    Private Sub menuStripToolsEnableUpdate_CheckedChanged() Handles menuStripToolsEnableUpdate.CheckedChanged
        If _settingsLoaded Then
            Settings.DisableUpdateCheck = Not menuStripToolsEnableUpdate.Checked
            Settings.SaveSettings()
        End If
    End Sub
#End Region

#Region "ListView Events"
    Private Sub lstItemSelectionChanged() Handles lstInstalls.ItemSelectionChanged, lstInstances.ItemSelectionChanged
        menuStripFileRemoveSelected.Enabled = lstInstalls.SelectedItems.Count > 0 OrElse lstInstances.SelectedItems.Count > 0
        menuStripFileDeleteInstance.Enabled = lstInstances.SelectedItems.Count > 0
    End Sub

    Private Sub lstInstalls_ItemActivate() Handles lstInstalls.ItemActivate
        If lstInstalls.SelectedItems.Count > 0 Then
            For Each item As Settings.Install In lstInstalls.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstall)
                Diagnostics.Process.Start(item.Path)
            Next
        End If
    End Sub

    Private Sub lstInstances_ItemActivate() Handles lstInstances.ItemActivate
        If lstInstances.SelectedItems.Count > 0 Then
            For Each item As Settings.Instance In lstInstances.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstance)
                Diagnostics.Process.Start(item.Path)
            Next
        End If
    End Sub

    Private Sub ListViews_MouseUp(sender As Object, e As MouseEventArgs) Handles lstInstalls.MouseUp, lstInstances.MouseUp
        If e.Button = MouseButtons.Right AndAlso DirectCast(sender, ListView).SelectedItems.Count > 0 Then
            ShowContext(DirectCast(sender, ListView), e.Location)
        End If
    End Sub

    Private Sub ListViews_KeyUp(sender As Object, e As KeyEventArgs) Handles lstInstalls.KeyUp, lstInstances.KeyUp
        If e.KeyData = (Keys.Shift Or Keys.F10) OrElse e.KeyData = Keys.Apps Then
            If DirectCast(sender, ListView).SelectedItems.Count > 0 Then
                ShowContext(DirectCast(sender, ListView), New Drawing.Point(0, 0))
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub ShowContext(sender As ListView, location As Drawing.Point)
        ctxMainRun.Visible = sender Is lstInstalls
        ctxMainDelete.Visible = sender Is lstInstances
        ctxMain.Show(sender, location)
    End Sub
#End Region

#Region "Context Menu Events"
    Private Sub ctxMainOpenFolder_Click() Handles ctxMainOpenFolder.Click
        If ctxMain.SourceControl Is lstInstalls Then
            For Each item As Settings.Install In lstInstalls.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstall)
                Diagnostics.Process.Start(item.Path)
            Next
        End If

        If ctxMain.SourceControl Is lstInstances Then
            For Each item As Settings.Instance In lstInstances.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstance)
                Diagnostics.Process.Start(item.Path)
            Next
        End If
    End Sub
    Private Sub ctxMainRun_Click() Handles ctxMainRun.Click

    End Sub
    Private Sub ctxMainReplace_Click() Handles ctxMainReplace.Click
        If ctxMain.SourceControl Is lstInstalls Then
            For Each item As ListViewItem In lstInstalls.SelectedItems
                Dim install As Settings.Install = GetInstall(item)

                Dim selectedPath As String = install.Path
                If Helpers.SelectFolderDialog(selectedPath, "Select Install Folder", False) = DialogResult.OK Then
                    Try
                        install.Version = General.GetInstallVersion(selectedPath)
                        install.Path = selectedPath
                        UpdateInstallItem(item, install)
                    Catch ex As FileNotFoundException
                        MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                        "Error Updating Install", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        WalkmanLib.ErrorDialog(ex, "Error Updating Install!" & Environment.NewLine)
                    End Try
                End If
            Next
            UpdateSettingsItems()
        End If

        If ctxMain.SourceControl Is lstInstances Then
            For Each item As ListViewItem In lstInstances.SelectedItems
                Dim instance As Settings.Instance = GetInstance(item)

                Dim selectedPath As String = instance.Path
                If Helpers.SelectFolderDialog(selectedPath, "Select Instance Folder", False) = DialogResult.OK Then
                    Try
                        instance.Version = General.GetInstanceVersion(selectedPath)
                        instance.Path = selectedPath
                        UpdateInstanceItem(item, instance)
                    Catch ex As FileNotFoundException
                        MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                        "Error Updating Instance", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        WalkmanLib.ErrorDialog(ex, "Error Updating Instance!" & Environment.NewLine)
                    End Try
                End If
            Next
            UpdateSettingsItems()
        End If
    End Sub
    Private Async Sub ctxMainUpdate_Click() Handles ctxMainUpdate.Click

    End Sub
    Private Sub ctxMainRemove_Click() Handles ctxMainRemove.Click

    End Sub
    Private Sub ctxMainDelete_Click() Handles ctxMainDelete.Click

    End Sub
#End Region
End Class
