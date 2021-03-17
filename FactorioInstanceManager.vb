Imports System
Imports System.Windows.Forms

Public Class FactorioInstanceManager
    Private Sub FactorioInstanceManager_Load() Handles Me.Shown
        lstInstalls.DoubleBuffered(True)
        lstInstances.DoubleBuffered(True)

        Settings.Init()
    End Sub

    Private Sub FactorioInstanceManager_Resize() Handles MyBase.Resize

    End Sub

#Region "Install Helpers"
    Public Shared Function GetInstall(item As ListViewItem) As Settings.Install

    End Function

    Public Shared Function CreateInstallItem(install As Settings.Install) As ListViewItem

    End Function

    Public Shared Function UpdateInstallItem(item As ListViewItem, install As Settings.Install) As ListViewItem

    End Function
#End Region

#Region "Instance Helpers"
    Public Shared Function GetInstance(item As ListViewItem) As Settings.Instance

    End Function

    Public Shared Function CreateInstanceItem(instance As Settings.Instance) As ListViewItem

    End Function

    Public Shared Function UpdateInstanceItem(item As ListViewItem, instance As Settings.Instance) As ListViewItem

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
