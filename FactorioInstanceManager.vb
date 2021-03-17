Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Public Class FactorioInstanceManager
    Private Sub FactorioInstanceManager_Load() Handles Me.Shown
        lstInstalls.DoubleBuffered(True)
        lstInstances.DoubleBuffered(True)

        Settings.Init()

        Me.Width = Settings.WindowWidth
        Me.Height = Settings.WindowHeight
        Me.CenterToParent()

        If Settings.WindowMaximised Then Me.WindowState = FormWindowState.Maximized

        For Each install As Settings.Install In Settings.Installs
            lstInstalls.Items.Add(CreateInstallItem(install))
        Next
        For Each instance As Settings.Instance In Settings.Instances
            lstInstances.Items.Add(CreateInstanceItem(instance))
        Next
    End Sub

    Private Sub FactorioInstanceManager_Resize() Handles MyBase.Resize

    End Sub

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
            Catch ex As FileNotFoundException
                MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                "Error Adding Instance", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Adding Instance", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Catch ex As FileNotFoundException
                MessageBox.Show(ex.Message & Environment.NewLine & "File path: " & ex.FileName,
                                "Error Adding Install", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Adding Install", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    End Sub
    Private Sub menuStripFileExit_Click() Handles menuStripFileExit.Click
        Application.Exit()
    End Sub

    Private Sub menuStripEditSetInstallInstance_Click() Handles menuStripEditSetInstallInstance.Click

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
        End If
    End Sub
    Private Sub menuStripToolsSetDefaultInstancePath_Click() Handles menuStripToolsSetDefaultInstancePath.Click
        Helpers.SelectFolderDialog(Settings.DefaultInstancePath, "Select Default Instance Path", False)
    End Sub
#End Region

#Region "ListView Events"
    Private Sub lstItemSelectionChanged() Handles lstInstalls.ItemSelectionChanged, lstInstances.ItemSelectionChanged

    End Sub

    Private Sub lstInstalls_ItemActivate() Handles lstInstalls.ItemActivate

    End Sub

    Private Sub lstInstances_ItemActivate() Handles lstInstances.ItemActivate

    End Sub
#End Region
End Class
