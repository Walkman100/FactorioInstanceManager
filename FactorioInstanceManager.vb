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
        lblVersion.Text = "v" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build

        Dim configFileExisted As Boolean = Settings.Init()

        Me.Width = Settings.WindowWidth
        Me.Height = Settings.WindowHeight
        Me.CenterToParent()

        If Settings.WindowMaximised Then Me.WindowState = FormWindowState.Maximized

        scMain.SplitterDistance = Settings.SplitterDistance
        colHeadInstallsPath.Width = Settings.ColumnInstallPathWidth
        colHeadInstallsVersion.Width = Settings.ColumnInstallVersionWidth
        colHeadInstallsActiveInstance.Width = Settings.ColumnInstallInstanceWidth
        colHeadInstancesPath.Width = Settings.ColumnInstancePathWidth
        colHeadInstancesVersion.Width = Settings.ColumnInstanceVersionWidth
        colHeadInstancesIconPath.Width = Settings.ColumnInstanceIconPathWidth

        menuStripToolsTheme.SelectedIndex = Settings.ThemeIndex
        menuStripToolsEnableUpdate.Checked = Not Settings.DisableUpdateCheck
        If Not Settings.DisableUpdateCheck Then
            WalkmanLib.CheckIfUpdateAvailableInBackground("FactorioInstanceManager", My.Application.Info.Version, AddressOf UpdateCheckComplete)
        End If

        lstInstalls.SmallImageList = New ImageList() With {.ColorDepth = ColorDepth.Depth32Bit, .ImageSize = New Drawing.Size(16, 16)}
        lstInstances.SmallImageList = New ImageList() With {.ColorDepth = ColorDepth.Depth32Bit, .ImageSize = New Drawing.Size(16, 16)}

        For Each install As Settings.Install In Settings.Installs
            lstInstalls.Items.Add(CreateInstallItem(install))
        Next
        For Each instance As Settings.Instance In Settings.Instances
            lstInstances.Items.Add(CreateInstanceItem(instance))
        Next

        _settingsLoaded = True

        If Not configFileExisted Then
            ' detect steam install
            menuStripToolsDetectInstall.PerformClick()

            ' set default instance folder
            If Helpers.SelectFolderDialog(Settings.DefaultInstancePath, "Select Default Instance Path", False) = DialogResult.OK Then
                Settings.SaveSettings()

                ' search instances in default folder
                For Each path As String In General.FindInstances(Settings.DefaultInstancePath)
                    Dim instanceVersion As Version = General.GetInstanceVersion(path)
                    lstInstances.Items.Add(CreateInstanceItem(New Settings.Instance With {
                                                                  .Path = path,
                                                                  .Version = instanceVersion
                                                              }))
                Next
                UpdateSettingsItems()
            End If
        End If

        ListViews_ItemSelectionChanged()
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

    Private Sub SetTheme(theme As WalkmanLib.Theme)
        WalkmanLib.ApplyTheme(theme, Me)
        WalkmanLib.ApplyTheme(theme, Me.components.Components)

        If theme = WalkmanLib.Theme.Default Then
            menuStripMain.RenderMode = ToolStripRenderMode.ManagerRenderMode
            ctxMain.RenderMode = ToolStripRenderMode.ManagerRenderMode
        Else
            menuStripMain.RenderMode = ToolStripRenderMode.System
            ctxMain.RenderMode = ToolStripRenderMode.System
        End If
        lblVersion.BackColor = theme.MenuStripBG
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
            Await UpdateInstallInfo(item)
        Next

        For Each item As ListViewItem In lstInstances.Items
            Await UpdateInstanceInfo(item)
        Next

        UpdateSettingsItems()
    End Function

    Private Async Function UpdateInstallInfo(item As ListViewItem) As Task
        Dim install As Settings.Install = GetInstall(item)

        Dim installVersion As Version = Nothing
        Try
            installVersion = Await Task.Run(Function() General.GetInstallVersion(install.Path))
        Catch : End Try
        install.Version = installVersion

        ' UpdateInstallItem runs GetInstallCurrentInstance, which accesses files
        Await Task.Run(Sub() UpdateInstallItem(item, install))
    End Function

    Private Async Function UpdateInstanceInfo(item As ListViewItem) As Task
        Dim instance As Settings.Instance = GetInstance(item)

        Dim instanceVersion As Version = Nothing
        Try
            instanceVersion = Await Task.Run(Function() General.GetInstanceVersion(instance.Path))
        Catch : End Try
        instance.Version = instanceVersion

        ' UpdateInstanceItem gets image from file and resizes
        Await Task.Run(Sub() UpdateInstanceItem(item, instance))
    End Function

#Region "Install Helpers"
    Public Shared Function GetInstall(item As ListViewItem) As Settings.Install
        Return DirectCast(item.Tag, Settings.Install)
    End Function

    Public Function CreateInstallItem(install As Settings.Install) As ListViewItem
        Return UpdateInstallItem(New ListViewItem({"", "", ""}), install)
    End Function

    Public Function UpdateInstallItem(item As ListViewItem, install As Settings.Install) As ListViewItem
        item.SubItems(0).Text = install.Path
        item.SubItems(1).Text = install.Version?.ToString()
        Try : item.SubItems(2).Text = General.GetInstallCurrentInstance(install.Path)
        Catch ex As Exception : item.SubItems(2).Text = "Error: " & ex.Message
        End Try
        Try
            lstInstalls.SmallImageList.Images.Add(install.Path, General.GetInstallImage(install.Path, lstInstalls.SmallImageList.ImageSize.Width))
            item.ImageKey = install.Path
        Catch : End Try
        item.Tag = install
        Return item
    End Function
#End Region

#Region "Instance Helpers"
    Public Shared Function GetInstance(item As ListViewItem) As Settings.Instance
        Return DirectCast(item.Tag, Settings.Instance)
    End Function

    Public Function CreateInstanceItem(instance As Settings.Instance) As ListViewItem
        Return UpdateInstanceItem(New ListViewItem({"", "", ""}), instance)
    End Function

    Public Function UpdateInstanceItem(item As ListViewItem, instance As Settings.Instance) As ListViewItem
        item.SubItems(0).Text = instance.Path
        item.SubItems(1).Text = instance.Version?.ToString()
        item.SubItems(2).Text = instance.IconPath
        Try
            Dim img As Drawing.Image = Drawing.Image.FromFile(instance.IconPath)
            img = Helpers.ResizeImage(img, lstInstances.SmallImageList.ImageSize.Width)

            lstInstances.SmallImageList.Images.Add(instance.Path, img)
            item.ImageKey = instance.Path
        Catch : End Try
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
        Dim steamInstallPath As String = General.FindSteamInstall()
        Dim standaloneInstallPath As String = General.FindStandaloneInstall()

        If steamInstallPath IsNot Nothing Then
            Dim installVersion As Version = Nothing
            Try
                installVersion = General.GetInstallVersion(steamInstallPath)
            Catch : End Try

            lstInstalls.Items.Add(CreateInstallItem(New Settings.Install With {
                                                        .Path = steamInstallPath,
                                                        .Version = installVersion
                                                    }))
        End If
        If standaloneInstallPath IsNot Nothing Then
            Dim installVersion As Version = Nothing
            Try
                installVersion = General.GetInstallVersion(standaloneInstallPath)
            Catch : End Try

            lstInstalls.Items.Add(CreateInstallItem(New Settings.Install With {
                                                        .Path = standaloneInstallPath,
                                                        .Version = installVersion
                                                    }))
        End If

        If steamInstallPath Is Nothing AndAlso standaloneInstallPath Is Nothing Then
            MessageBox.Show("Could not find Factorio Steam install or installed Standalone install!")
        Else
            UpdateSettingsItems()
        End If
    End Sub
    Private Async Sub menuStripToolsUpdate_Click() Handles menuStripToolsUpdate.Click
        Await UpdateInfo()
    End Sub
    Private Sub menuStripToolsTheme_SelectedIndexChanged() Handles menuStripToolsTheme.SelectedIndexChanged
        Select Case menuStripToolsTheme.SelectedIndex
            Case 0
                SetTheme(WalkmanLib.Theme.Default)
            Case 1
                SetTheme(WalkmanLib.Theme.SystemDark)
            Case 2
                SetTheme(WalkmanLib.Theme.Dark)
            Case 3
                SetTheme(WalkmanLib.Theme.Inverted)
            Case 4
                SetTheme(WalkmanLib.Theme.Test)
            Case Else
                SetTheme(WalkmanLib.Theme.Default)
                Return
        End Select
        If _settingsLoaded Then
            Settings.ThemeIndex = menuStripToolsTheme.SelectedIndex
            Settings.SaveSettings()
        End If
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
    Private Sub ListViews_ItemSelectionChanged() Handles lstInstalls.ItemSelectionChanged, lstInstances.ItemSelectionChanged
        menuStripFileRemoveSelected.Enabled = lstInstalls.SelectedItems.Count > 0 OrElse lstInstances.SelectedItems.Count > 0
        menuStripFileDeleteInstance.Enabled = lstInstances.SelectedItems.Count > 0
        menuStripEditSetInstallInstance.Enabled = lstInstalls.SelectedItems.Count > 0
    End Sub

    Private Sub lstInstalls_ItemActivate() Handles lstInstalls.ItemActivate
        If lstInstalls.SelectedItems.Count > 0 Then
            For Each item As Settings.Install In lstInstalls.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstall)
                Try
                    Helpers.OpenFolder(item.Path)
                Catch ex As Exception
                    MessageBox.Show(ex.Message & Environment.NewLine, "Error Opening Install Folder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If
    End Sub

    Private Sub lstInstances_ItemActivate() Handles lstInstances.ItemActivate
        If lstInstances.SelectedItems.Count > 0 Then
            For Each item As Settings.Instance In lstInstances.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstance)
                Try
                    Helpers.OpenFolder(item.Path)
                Catch ex As Exception
                    MessageBox.Show(ex.Message & Environment.NewLine, "Error Opening Instance Folder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If
    End Sub

    Private Sub scMain_SplitterMoved() Handles scMain.SplitterMoved
        If _settingsLoaded Then
            Settings.SplitterDistance = scMain.SplitterDistance
            Settings.SaveSettings()
        End If
    End Sub

    Private lastInstallSort As Collections.Generic.KeyValuePair(Of Integer, SortOrder)
    Private Sub lstInstalls_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstInstalls.ColumnClick
        If e.Column = lastInstallSort.Key Then
            lastInstallSort = New Collections.Generic.KeyValuePair(Of Integer, SortOrder)(e.Column,
                If(lastInstallSort.Value = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending))
        Else
            lastInstallSort = New Collections.Generic.KeyValuePair(Of Integer, SortOrder)(e.Column, SortOrder.Ascending)
        End If
        General.Sort(lstInstalls, lstInstalls.Items, lastInstallSort.Key, lastInstallSort.Value)
        UpdateSettingsItems()
    End Sub

    Private lastInstanceSort As Collections.Generic.KeyValuePair(Of Integer, SortOrder)
    Private Sub lstInstances_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstInstances.ColumnClick
        If e.Column = lastInstanceSort.Key Then
            lastInstanceSort = New Collections.Generic.KeyValuePair(Of Integer, SortOrder)(e.Column,
                If(lastInstanceSort.Value = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending))
        Else
            lastInstanceSort = New Collections.Generic.KeyValuePair(Of Integer, SortOrder)(e.Column, SortOrder.Ascending)
        End If
        General.Sort(lstInstances, lstInstances.Items, lastInstanceSort.Key, lastInstanceSort.Value)
        UpdateSettingsItems()
    End Sub

    Private Sub ListViews_ColumnResized() Handles lstInstalls.ColumnWidthChanged, lstInstances.ColumnWidthChanged
        If _settingsLoaded Then
            Settings.ColumnInstallPathWidth = colHeadInstallsPath.Width
            Settings.ColumnInstallVersionWidth = colHeadInstallsVersion.Width
            Settings.ColumnInstallInstanceWidth = colHeadInstallsActiveInstance.Width
            Settings.ColumnInstancePathWidth = colHeadInstancesPath.Width
            Settings.ColumnInstanceVersionWidth = colHeadInstancesVersion.Width
            Settings.ColumnInstanceIconPathWidth = colHeadInstancesIconPath.Width
            Settings.SaveSettings()
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
        ctxMainSet.Visible = sender Is lstInstalls
        ctxMainSetIcon.Visible = sender Is lstInstances
        If sender Is lstInstalls Then UpdateContextSetMenu()
        ctxMain.Show(sender, location)
    End Sub
#End Region

#Region "Context Menu Events"
    Private Sub ctxMainOpenFolder_Click() Handles ctxMainOpenFolder.Click
        If ctxMain.SourceControl Is lstInstalls Then
            For Each item As Settings.Install In lstInstalls.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstall)
                Try
                    Helpers.OpenFolder(item.Path)
                Catch ex As Exception
                    MessageBox.Show(ex.Message & Environment.NewLine, "Error Opening Install Folder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If

        If ctxMain.SourceControl Is lstInstances Then
            For Each item As Settings.Instance In lstInstances.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstance)
                Try
                    Helpers.OpenFolder(item.Path)
                Catch ex As Exception
                    MessageBox.Show(ex.Message & Environment.NewLine, "Error Opening Instance Folder", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If
    End Sub
    Private Sub ctxMainRun_Click() Handles ctxMainRun.Click
        If ctxMain.SourceControl Is lstInstalls Then
            For Each item As Settings.Install In lstInstalls.SelectedItems.Cast(Of ListViewItem).Select(AddressOf GetInstall)
                Try
                    Diagnostics.Process.Start(General.FindInstallExe(item.Path))
                Catch ex As Exception
                    MessageBox.Show($"Error launching install at {item.Path}:{Environment.NewLine}{ex.Message}",
                                    "Error Launching Factorio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            Next
        End If
    End Sub
    Private Sub ctxMainDelete_Click() Handles ctxMainDelete.Click
        menuStripFileDeleteInstance.PerformClick()
    End Sub
    Private Sub ctxMainSetIcon_Click() Handles ctxMainSetIcon.Click
        If ctxMain.SourceControl Is lstInstances Then
            For Each item As ListViewItem In lstInstances.SelectedItems
                Dim instance As Settings.Instance = GetInstance(item)

                ' Path.Get* doesn't like empty string paths, but null is fine
                Dim iconPath As String = If(String.IsNullOrWhiteSpace(instance.IconPath), Nothing, instance.IconPath)

                Dim ofd As OpenFileDialog = New OpenFileDialog() With {
                    .Title = "Select Icon File",
                    .Filter = "Images|*.png;*.jpg;*.bmp;*.ico;*.gif|All Items|*.*",
                    .InitialDirectory = If(Path.GetDirectoryName(iconPath), instance.Path),
                    .FileName = Path.GetFileName(iconPath)
                }

                If ofd.ShowDialog() = DialogResult.OK Then
                    instance.IconPath = ofd.FileName
                    UpdateInstanceItem(item, instance)
                End If
            Next
            UpdateSettingsItems()
        End If
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
        If ctxMain.SourceControl Is lstInstalls Then
            For Each item As ListViewItem In lstInstalls.SelectedItems
                Await UpdateInstallInfo(item)
            Next
            UpdateSettingsItems()
        End If

        If ctxMain.SourceControl Is lstInstances Then
            For Each item As ListViewItem In lstInstances.SelectedItems
                Await UpdateInstanceInfo(item)
            Next
            UpdateSettingsItems()
        End If
    End Sub
    Private Sub ctxMainRemove_Click() Handles ctxMainRemove.Click
        If TypeOf ctxMain.SourceControl Is ListView Then
            For Each item As ListViewItem In DirectCast(ctxMain.SourceControl, ListView).SelectedItems
                item.Remove()
            Next
            UpdateSettingsItems()
        End If
    End Sub

    Private Function CreateToolStripMenuItem(instance As Settings.Instance) As ToolStripMenuItem
        Dim item As ToolStripMenuItem = New ToolStripMenuItem(instance.Path, Nothing, AddressOf ctxMainSet_SubItem_Click)

        Try
            Dim img As Drawing.Image = Drawing.Image.FromFile(instance.IconPath)
            img = Helpers.ResizeImage(img, ctxMain.ImageScalingSize.Width)

            item.Image = img
        Catch : End Try

        Return item
    End Function
    Private Sub UpdateContextSetMenu()
        ' clear menu
        For Each item As ToolStripMenuItem In ctxMainSet.DropDownItems.OfType(Of ToolStripMenuItem).ToList() ' so we can remove items
            If item IsNot ctxMainSetSame AndAlso
                    item IsNot ctxMainSetMajor AndAlso
                    item IsNot ctxMainSetAll Then
                ctxMainSet.DropDownItems.Remove(item)
            End If
        Next

        If lstInstalls.SelectedItems.Count <> 1 Then Exit Sub
        Dim install As Settings.Install = GetInstall(lstInstalls.SelectedItems(0))
        If install.Version Is Nothing Then Exit Sub

        ' 0.17.79: Major.Minor.Build

        Dim InstancesSameVersion = Settings.Instances.Where(Function(i) install.Version = i.Version)
        Dim InstancesSameMajor = Settings.Instances.Where(Function(i) install.Version <> i.Version).
            Where(Function(i)
                      If i.Version Is Nothing Then Return False
                      If install.Version.Major = 0 Then
                          Return i.Version.Major = 0 AndAlso install.Version.Minor = i.Version.Minor
                      Else
                          Return install.Version.Major = i.Version.Major
                      End If
                  End Function)
        Dim InstancesAllVersions = Settings.Instances.Where(Function(i) install.Version <> i.Version).
            Where(Function(i)
                      If i.Version Is Nothing Then Return True
                      If install.Version.Major = 0 Then
                          Return i.Version.Major <> 0 OrElse install.Version.Minor <> i.Version.Minor
                      Else
                          Return install.Version.Major <> i.Version.Major
                      End If
                  End Function)

        Dim sameVersionIndex As Integer = ctxMainSet.DropDownItems.IndexOf(ctxMainSetSame)
        For i = 0 To InstancesSameVersion.Count - 1
            Dim instance As Settings.Instance = InstancesSameVersion(i)
            ctxMainSet.DropDownItems.Insert(sameVersionIndex + i + 1, CreateToolStripMenuItem(instance))
        Next

        Dim sameMajorVersionIndex As Integer = ctxMainSet.DropDownItems.IndexOf(ctxMainSetMajor)
        For i = 0 To InstancesSameMajor.Count - 1
            Dim instance As Settings.Instance = InstancesSameMajor(i)
            ctxMainSet.DropDownItems.Insert(sameMajorVersionIndex + i + 1, CreateToolStripMenuItem(instance))
        Next

        Dim allVersionsIndex As Integer = ctxMainSet.DropDownItems.IndexOf(ctxMainSetAll)
        For i = 0 To InstancesAllVersions.Count - 1
            Dim instance As Settings.Instance = InstancesAllVersions(i)
            ctxMainSet.DropDownItems.Insert(allVersionsIndex + i + 1, CreateToolStripMenuItem(instance))
        Next
    End Sub

    Private Async Sub ctxMainSet_SubItem_Click(sender As Object, e As EventArgs)
        If lstInstalls.SelectedItems.Count <> 1 Then Exit Sub
        Dim install As Settings.Install = GetInstall(lstInstalls.SelectedItems(0))

        Dim item As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)

        Try
            Await Task.Run(Sub() General.SetInstallCurrentInstance(install.Path, item.Text))
        Catch ex As FileNotFoundException
            MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                "Error Setting Install Instance", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            WalkmanLib.ErrorDialog(ex, "Error Setting Install Instance!" & Environment.NewLine)
        End Try

        Await UpdateInfo()
    End Sub
#End Region
End Class
