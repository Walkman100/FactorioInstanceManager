<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FactorioInstanceManager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.menuStripMain = New System.Windows.Forms.MenuStrip()
        Me.menuStripFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripFileAddInstance = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripFileAddInstall = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripFileRemoveSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripFileSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuStripFileCreateInstance = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripFileDeleteInstance = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripFileSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuStripFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripEditSetInstallInstance = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripEditSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuStripEditSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripEditDeselectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripEditInvertSelection = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsScan = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsDetectInstall = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuStripToolsUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuStripToolsTheme = New System.Windows.Forms.ToolStripComboBox()
        Me.menuStripToolsSetDefaultInstancePath = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsEnableUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.lstInstalls = New System.Windows.Forms.ListView()
        Me.colHeadInstallsPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstallsVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstallsActiveInstance = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstInstances = New System.Windows.Forms.ListView()
        Me.colHeadInstancesPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstancesVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstancesIconPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ctxMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctxMainOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainRun = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainSetSame = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainSetSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ctxMainSetMajor = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainSetSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ctxMainSetAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ctxMainSetIcon = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainReplace = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxMainRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.menuStripMain.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.ctxMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuStripMain
        '
        Me.menuStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStripFile, Me.menuStripEdit, Me.menuStripTools})
        Me.menuStripMain.Location = New System.Drawing.Point(0, 0)
        Me.menuStripMain.Name = "menuStripMain"
        Me.menuStripMain.Size = New System.Drawing.Size(725, 24)
        Me.menuStripMain.TabIndex = 0
        Me.menuStripMain.Text = "MenuStrip1"
        '
        'menuStripFile
        '
        Me.menuStripFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStripFileAddInstance, Me.menuStripFileAddInstall, Me.menuStripFileRemoveSelected, Me.menuStripFileSeparator1, Me.menuStripFileCreateInstance, Me.menuStripFileDeleteInstance, Me.menuStripFileSeparator2, Me.menuStripFileExit})
        Me.menuStripFile.Name = "menuStripFile"
        Me.menuStripFile.Size = New System.Drawing.Size(37, 20)
        Me.menuStripFile.Text = "&File"
        '
        'menuStripFileAddInstance
        '
        Me.menuStripFileAddInstance.Name = "menuStripFileAddInstance"
        Me.menuStripFileAddInstance.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.menuStripFileAddInstance.Size = New System.Drawing.Size(220, 22)
        Me.menuStripFileAddInstance.Text = "Add Instance"
        '
        'menuStripFileAddInstall
        '
        Me.menuStripFileAddInstall.Name = "menuStripFileAddInstall"
        Me.menuStripFileAddInstall.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.menuStripFileAddInstall.Size = New System.Drawing.Size(220, 22)
        Me.menuStripFileAddInstall.Text = "Add Install"
        '
        'menuStripFileRemoveSelected
        '
        Me.menuStripFileRemoveSelected.Name = "menuStripFileRemoveSelected"
        Me.menuStripFileRemoveSelected.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.menuStripFileRemoveSelected.Size = New System.Drawing.Size(220, 22)
        Me.menuStripFileRemoveSelected.Text = "Remove Selected Items"
        '
        'menuStripFileSeparator1
        '
        Me.menuStripFileSeparator1.Name = "menuStripFileSeparator1"
        Me.menuStripFileSeparator1.Size = New System.Drawing.Size(217, 6)
        '
        'menuStripFileCreateInstance
        '
        Me.menuStripFileCreateInstance.Name = "menuStripFileCreateInstance"
        Me.menuStripFileCreateInstance.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.menuStripFileCreateInstance.Size = New System.Drawing.Size(220, 22)
        Me.menuStripFileCreateInstance.Text = "Create Instance"
        '
        'menuStripFileDeleteInstance
        '
        Me.menuStripFileDeleteInstance.Name = "menuStripFileDeleteInstance"
        Me.menuStripFileDeleteInstance.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.menuStripFileDeleteInstance.Size = New System.Drawing.Size(220, 22)
        Me.menuStripFileDeleteInstance.Text = "Delete Instance"
        '
        'menuStripFileSeparator2
        '
        Me.menuStripFileSeparator2.Name = "menuStripFileSeparator2"
        Me.menuStripFileSeparator2.Size = New System.Drawing.Size(217, 6)
        '
        'menuStripFileExit
        '
        Me.menuStripFileExit.Name = "menuStripFileExit"
        Me.menuStripFileExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.menuStripFileExit.Size = New System.Drawing.Size(220, 22)
        Me.menuStripFileExit.Text = "Exit"
        '
        'menuStripEdit
        '
        Me.menuStripEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStripEditSetInstallInstance, Me.menuStripEditSeparator1, Me.menuStripEditSelectAll, Me.menuStripEditDeselectAll, Me.menuStripEditInvertSelection})
        Me.menuStripEdit.Name = "menuStripEdit"
        Me.menuStripEdit.Size = New System.Drawing.Size(39, 20)
        Me.menuStripEdit.Text = "&Edit"
        '
        'menuStripEditSetInstallInstance
        '
        Me.menuStripEditSetInstallInstance.Name = "menuStripEditSetInstallInstance"
        Me.menuStripEditSetInstallInstance.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.menuStripEditSetInstallInstance.Size = New System.Drawing.Size(253, 22)
        Me.menuStripEditSetInstallInstance.Text = "Set Install's active instance"
        '
        'menuStripEditSeparator1
        '
        Me.menuStripEditSeparator1.Name = "menuStripEditSeparator1"
        Me.menuStripEditSeparator1.Size = New System.Drawing.Size(250, 6)
        '
        'menuStripEditSelectAll
        '
        Me.menuStripEditSelectAll.Name = "menuStripEditSelectAll"
        Me.menuStripEditSelectAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuStripEditSelectAll.Size = New System.Drawing.Size(253, 22)
        Me.menuStripEditSelectAll.Text = "Select All"
        '
        'menuStripEditDeselectAll
        '
        Me.menuStripEditDeselectAll.Name = "menuStripEditDeselectAll"
        Me.menuStripEditDeselectAll.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuStripEditDeselectAll.Size = New System.Drawing.Size(253, 22)
        Me.menuStripEditDeselectAll.Text = "Deselect All"
        '
        'menuStripEditInvertSelection
        '
        Me.menuStripEditInvertSelection.Name = "menuStripEditInvertSelection"
        Me.menuStripEditInvertSelection.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.menuStripEditInvertSelection.Size = New System.Drawing.Size(253, 22)
        Me.menuStripEditInvertSelection.Text = "Invert Selection"
        '
        'menuStripTools
        '
        Me.menuStripTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStripToolsScan, Me.menuStripToolsDetectInstall, Me.menuStripToolsSeparator1, Me.menuStripToolsUpdate, Me.menuStripToolsSeparator2, Me.menuStripToolsTheme, Me.menuStripToolsSetDefaultInstancePath, Me.menuStripToolsEnableUpdate})
        Me.menuStripTools.Name = "menuStripTools"
        Me.menuStripTools.Size = New System.Drawing.Size(46, 20)
        Me.menuStripTools.Text = "&Tools"
        '
        'menuStripToolsScan
        '
        Me.menuStripToolsScan.Name = "menuStripToolsScan"
        Me.menuStripToolsScan.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.menuStripToolsScan.Size = New System.Drawing.Size(242, 22)
        Me.menuStripToolsScan.Text = "Scan for Instances"
        '
        'menuStripToolsDetectInstall
        '
        Me.menuStripToolsDetectInstall.Name = "menuStripToolsDetectInstall"
        Me.menuStripToolsDetectInstall.Size = New System.Drawing.Size(242, 22)
        Me.menuStripToolsDetectInstall.Text = "Detect Steam/Standalone Install"
        '
        'menuStripToolsSeparator1
        '
        Me.menuStripToolsSeparator1.Name = "menuStripToolsSeparator1"
        Me.menuStripToolsSeparator1.Size = New System.Drawing.Size(239, 6)
        '
        'menuStripToolsUpdate
        '
        Me.menuStripToolsUpdate.Name = "menuStripToolsUpdate"
        Me.menuStripToolsUpdate.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.menuStripToolsUpdate.Size = New System.Drawing.Size(242, 22)
        Me.menuStripToolsUpdate.Text = "Update Versions"
        '
        'menuStripToolsSeparator2
        '
        Me.menuStripToolsSeparator2.Name = "menuStripToolsSeparator2"
        Me.menuStripToolsSeparator2.Size = New System.Drawing.Size(239, 6)
        '
        'menuStripToolsTheme
        '
        Me.menuStripToolsTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.menuStripToolsTheme.Items.AddRange(New Object() {"Theme: Default", "Theme: System Dark", "Theme: Dark", "Theme: Inverted", "Theme: Test"})
        Me.menuStripToolsTheme.Name = "menuStripToolsTheme"
        Me.menuStripToolsTheme.Size = New System.Drawing.Size(121, 23)
        '
        'menuStripToolsSetDefaultInstancePath
        '
        Me.menuStripToolsSetDefaultInstancePath.Name = "menuStripToolsSetDefaultInstancePath"
        Me.menuStripToolsSetDefaultInstancePath.Size = New System.Drawing.Size(242, 22)
        Me.menuStripToolsSetDefaultInstancePath.Text = "Set Default Instance Path"
        '
        'menuStripToolsEnableUpdate
        '
        Me.menuStripToolsEnableUpdate.CheckOnClick = True
        Me.menuStripToolsEnableUpdate.Name = "menuStripToolsEnableUpdate"
        Me.menuStripToolsEnableUpdate.Size = New System.Drawing.Size(242, 22)
        Me.menuStripToolsEnableUpdate.Text = "Enable Update Check"
        '
        'scMain
        '
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.Location = New System.Drawing.Point(0, 24)
        Me.scMain.Name = "scMain"
        Me.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.lstInstalls)
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.lstInstances)
        Me.scMain.Size = New System.Drawing.Size(725, 394)
        Me.scMain.SplitterDistance = 131
        Me.scMain.TabIndex = 1
        '
        'lstInstalls
        '
        Me.lstInstalls.AllowColumnReorder = True
        Me.lstInstalls.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colHeadInstallsPath, Me.colHeadInstallsVersion, Me.colHeadInstallsActiveInstance})
        Me.lstInstalls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstInstalls.FullRowSelect = True
        Me.lstInstalls.GridLines = True
        Me.lstInstalls.HideSelection = False
        Me.lstInstalls.Location = New System.Drawing.Point(0, 0)
        Me.lstInstalls.Name = "lstInstalls"
        Me.lstInstalls.Size = New System.Drawing.Size(725, 131)
        Me.lstInstalls.TabIndex = 0
        Me.lstInstalls.UseCompatibleStateImageBehavior = False
        Me.lstInstalls.View = System.Windows.Forms.View.Details
        '
        'colHeadInstallsPath
        '
        Me.colHeadInstallsPath.Text = "Install Path"
        Me.colHeadInstallsPath.Width = 300
        '
        'colHeadInstallsVersion
        '
        Me.colHeadInstallsVersion.Text = "Version"
        Me.colHeadInstallsVersion.Width = 100
        '
        'colHeadInstallsActiveInstance
        '
        Me.colHeadInstallsActiveInstance.Text = "Active Instance Path"
        Me.colHeadInstallsActiveInstance.Width = 300
        '
        'lstInstances
        '
        Me.lstInstances.AllowColumnReorder = True
        Me.lstInstances.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colHeadInstancesPath, Me.colHeadInstancesVersion, Me.colHeadInstancesIconPath})
        Me.lstInstances.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstInstances.FullRowSelect = True
        Me.lstInstances.GridLines = True
        Me.lstInstances.HideSelection = False
        Me.lstInstances.Location = New System.Drawing.Point(0, 0)
        Me.lstInstances.Name = "lstInstances"
        Me.lstInstances.Size = New System.Drawing.Size(725, 259)
        Me.lstInstances.TabIndex = 0
        Me.lstInstances.UseCompatibleStateImageBehavior = False
        Me.lstInstances.View = System.Windows.Forms.View.Details
        '
        'colHeadInstancesPath
        '
        Me.colHeadInstancesPath.Text = "Instance Path"
        Me.colHeadInstancesPath.Width = 300
        '
        'colHeadInstancesVersion
        '
        Me.colHeadInstancesVersion.Text = "Last Used Version"
        Me.colHeadInstancesVersion.Width = 100
        '
        'colHeadInstancesIconPath
        '
        Me.colHeadInstancesIconPath.Text = "Icon Path"
        Me.colHeadInstancesIconPath.Width = 300
        '
        'ctxMain
        '
        Me.ctxMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctxMainOpenFolder, Me.ctxMainRun, Me.ctxMainDelete, Me.ctxMainSet, Me.ctxMainSeparator1, Me.ctxMainSetIcon, Me.ctxMainReplace, Me.ctxMainUpdate, Me.ctxMainRemove})
        Me.ctxMain.Name = "ctxMain"
        Me.ctxMain.Size = New System.Drawing.Size(143, 186)
        '
        'ctxMainOpenFolder
        '
        Me.ctxMainOpenFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ctxMainOpenFolder.Name = "ctxMainOpenFolder"
        Me.ctxMainOpenFolder.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainOpenFolder.Text = "Open Folder"
        '
        'ctxMainRun
        '
        Me.ctxMainRun.Name = "ctxMainRun"
        Me.ctxMainRun.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainRun.Text = "Run"
        '
        'ctxMainDelete
        '
        Me.ctxMainDelete.Name = "ctxMainDelete"
        Me.ctxMainDelete.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainDelete.Text = "Delete"
        '
        'ctxMainSet
        '
        Me.ctxMainSet.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctxMainSetSame, Me.ctxMainSetSeparator1, Me.ctxMainSetMajor, Me.ctxMainSetSeparator2, Me.ctxMainSetAll})
        Me.ctxMainSet.Name = "ctxMainSet"
        Me.ctxMainSet.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainSet.Text = "Set Instance"
        '
        'ctxMainSetSame
        '
        Me.ctxMainSetSame.Enabled = False
        Me.ctxMainSetSame.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ctxMainSetSame.Name = "ctxMainSetSame"
        Me.ctxMainSetSame.Size = New System.Drawing.Size(184, 22)
        Me.ctxMainSetSame.Text = "Same Version"
        '
        'ctxMainSetSeparator1
        '
        Me.ctxMainSetSeparator1.Name = "ctxMainSetSeparator1"
        Me.ctxMainSetSeparator1.Size = New System.Drawing.Size(181, 6)
        '
        'ctxMainSetMajor
        '
        Me.ctxMainSetMajor.Enabled = False
        Me.ctxMainSetMajor.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ctxMainSetMajor.Name = "ctxMainSetMajor"
        Me.ctxMainSetMajor.Size = New System.Drawing.Size(184, 22)
        Me.ctxMainSetMajor.Text = "Same Major Version"
        '
        'ctxMainSetSeparator2
        '
        Me.ctxMainSetSeparator2.Name = "ctxMainSetSeparator2"
        Me.ctxMainSetSeparator2.Size = New System.Drawing.Size(181, 6)
        '
        'ctxMainSetAll
        '
        Me.ctxMainSetAll.Enabled = False
        Me.ctxMainSetAll.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ctxMainSetAll.Name = "ctxMainSetAll"
        Me.ctxMainSetAll.Size = New System.Drawing.Size(184, 22)
        Me.ctxMainSetAll.Text = "All Instances"
        '
        'ctxMainSeparator1
        '
        Me.ctxMainSeparator1.Name = "ctxMainSeparator1"
        Me.ctxMainSeparator1.Size = New System.Drawing.Size(139, 6)
        '
        'ctxMainSetIcon
        '
        Me.ctxMainSetIcon.Name = "ctxMainSetIcon"
        Me.ctxMainSetIcon.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainSetIcon.Text = "Set Icon"
        '
        'ctxMainReplace
        '
        Me.ctxMainReplace.Name = "ctxMainReplace"
        Me.ctxMainReplace.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainReplace.Text = "Replace"
        '
        'ctxMainUpdate
        '
        Me.ctxMainUpdate.Name = "ctxMainUpdate"
        Me.ctxMainUpdate.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainUpdate.Text = "Update"
        '
        'ctxMainRemove
        '
        Me.ctxMainRemove.Name = "ctxMainRemove"
        Me.ctxMainRemove.Size = New System.Drawing.Size(142, 22)
        Me.ctxMainRemove.Text = "Remove"
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(695, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(30, 9)
        Me.lblVersion.Text = "v0.0.0"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FactorioInstanceManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 418)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.menuStripMain)
        Me.Icon = Global.My.Resources.Resources.factorio_instance_manager
        Me.MainMenuStrip = Me.menuStripMain
        Me.Name = "FactorioInstanceManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Factorio Instance Manager"
        Me.menuStripMain.ResumeLayout(False)
        Me.menuStripMain.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.ctxMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents menuStripMain As System.Windows.Forms.MenuStrip
    Friend WithEvents menuStripFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripFileCreateInstance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripFileDeleteInstance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripFileSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuStripFileAddInstance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripFileAddInstall As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripFileRemoveSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripFileSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuStripFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripToolsScan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripToolsDetectInstall As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripToolsSetDefaultInstancePath As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripEditSetInstallInstance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents lstInstalls As System.Windows.Forms.ListView
    Friend WithEvents lstInstances As System.Windows.Forms.ListView
    Friend WithEvents colHeadInstallsPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadInstallsVersion As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadInstallsActiveInstance As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadInstancesPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadInstancesVersion As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeadInstancesIconPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents menuStripToolsUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripToolsSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuStripToolsSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuStripToolsEnableUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripEditSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuStripEditSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripEditDeselectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuStripEditInvertSelection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMain As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ctxMainOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainRun As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ctxMainUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainReplace As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainSetIcon As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainSet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainSetSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ctxMainSetMajor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainSetSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ctxMainSetSame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxMainSetAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents menuStripToolsTheme As System.Windows.Forms.ToolStripComboBox
End Class
