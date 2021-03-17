Imports System
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

    End Sub
    Private Sub menuStripFileAddInstall_Click() Handles menuStripFileAddInstall.Click

    End Sub
    Private Sub menuStripFileRemoveSelected_Click() Handles menuStripFileRemoveSelected.Click

    End Sub
    Private Sub menuStripFileCreateInstance_Click() Handles menuStripFileCreateInstance.Click

    End Sub
    Private Sub menuStripFileDeleteInstance_Click() Handles menuStripFileDeleteInstance.Click

    End Sub
    Private Sub menuStripFileExit_Click() Handles menuStripFileExit.Click
        Application.Exit()
    End Sub

    Private Sub menuStripEditSetInstallInstance_Click() Handles menuStripEditSetInstallInstance.Click

    End Sub

    Private Sub menuStripToolsScan_Click() Handles menuStripToolsScan.Click

    End Sub
    Private Sub menuStripToolsDetectInstall_Click() Handles menuStripToolsDetectInstall.Click

    End Sub
    Private Sub menuStripToolsSetDefaultInstancePath_Click() Handles menuStripToolsSetDefaultInstancePath.Click

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
