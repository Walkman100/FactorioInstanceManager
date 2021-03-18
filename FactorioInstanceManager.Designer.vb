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
        Me.menuStripTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsScan = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsDetectInstall = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsSetDefaultInstancePath = New System.Windows.Forms.ToolStripMenuItem()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.lstInstalls = New System.Windows.Forms.ListView()
        Me.colHeadInstallsPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstallsVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstallsActiveInstance = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstInstances = New System.Windows.Forms.ListView()
        Me.colHeadInstancesPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstancesVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeadInstancesIconPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.menuStripToolsUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuStripToolsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuStripToolsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuStripMain.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuStripMain
        '
        Me.menuStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStripFile, Me.menuStripEdit, Me.menuStripTools})
        Me.menuStripMain.Location = New System.Drawing.Point(0, 0)
        Me.menuStripMain.Name = "menuStripMain"
        Me.menuStripMain.Size = New System.Drawing.Size(801, 24)
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
        Me.menuStripFileAddInstance.Size = New System.Drawing.Size(196, 22)
        Me.menuStripFileAddInstance.Text = "Add Instance"
        '
        'menuStripFileAddInstall
        '
        Me.menuStripFileAddInstall.Name = "menuStripFileAddInstall"
        Me.menuStripFileAddInstall.Size = New System.Drawing.Size(196, 22)
        Me.menuStripFileAddInstall.Text = "Add Install"
        '
        'menuStripFileRemoveSelected
        '
        Me.menuStripFileRemoveSelected.Name = "menuStripFileRemoveSelected"
        Me.menuStripFileRemoveSelected.Size = New System.Drawing.Size(196, 22)
        Me.menuStripFileRemoveSelected.Text = "Remove Selected Items"
        '
        'menuStripFileSeparator1
        '
        Me.menuStripFileSeparator1.Name = "menuStripFileSeparator1"
        Me.menuStripFileSeparator1.Size = New System.Drawing.Size(193, 6)
        '
        'menuStripFileCreateInstance
        '
        Me.menuStripFileCreateInstance.Name = "menuStripFileCreateInstance"
        Me.menuStripFileCreateInstance.Size = New System.Drawing.Size(196, 22)
        Me.menuStripFileCreateInstance.Text = "Create Instance"
        '
        'menuStripFileDeleteInstance
        '
        Me.menuStripFileDeleteInstance.Name = "menuStripFileDeleteInstance"
        Me.menuStripFileDeleteInstance.Size = New System.Drawing.Size(196, 22)
        Me.menuStripFileDeleteInstance.Text = "Delete Instance"
        '
        'menuStripFileSeparator2
        '
        Me.menuStripFileSeparator2.Name = "menuStripFileSeparator2"
        Me.menuStripFileSeparator2.Size = New System.Drawing.Size(193, 6)
        '
        'menuStripFileExit
        '
        Me.menuStripFileExit.Name = "menuStripFileExit"
        Me.menuStripFileExit.Size = New System.Drawing.Size(196, 22)
        Me.menuStripFileExit.Text = "Exit"
        '
        'menuStripEdit
        '
        Me.menuStripEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStripEditSetInstallInstance})
        Me.menuStripEdit.Name = "menuStripEdit"
        Me.menuStripEdit.Size = New System.Drawing.Size(39, 20)
        Me.menuStripEdit.Text = "&Edit"
        '
        'menuStripEditSetInstallInstance
        '
        Me.menuStripEditSetInstallInstance.Name = "menuStripEditSetInstallInstance"
        Me.menuStripEditSetInstallInstance.Size = New System.Drawing.Size(213, 22)
        Me.menuStripEditSetInstallInstance.Text = "Set Install's active instance"
        '
        'menuStripTools
        '
        Me.menuStripTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuStripToolsScan, Me.menuStripToolsDetectInstall, Me.menuStripToolsSeparator1, Me.menuStripToolsUpdate, Me.menuStripToolsSeparator2, Me.menuStripToolsSetDefaultInstancePath})
        Me.menuStripTools.Name = "menuStripTools"
        Me.menuStripTools.Size = New System.Drawing.Size(46, 20)
        Me.menuStripTools.Text = "&Tools"
        '
        'menuStripToolsScan
        '
        Me.menuStripToolsScan.Name = "menuStripToolsScan"
        Me.menuStripToolsScan.Size = New System.Drawing.Size(205, 22)
        Me.menuStripToolsScan.Text = "Scan for Instances"
        '
        'menuStripToolsDetectInstall
        '
        Me.menuStripToolsDetectInstall.Name = "menuStripToolsDetectInstall"
        Me.menuStripToolsDetectInstall.Size = New System.Drawing.Size(205, 22)
        Me.menuStripToolsDetectInstall.Text = "Detect Steam Install"
        '
        'menuStripToolsSetDefaultInstancePath
        '
        Me.menuStripToolsSetDefaultInstancePath.Name = "menuStripToolsSetDefaultInstancePath"
        Me.menuStripToolsSetDefaultInstancePath.Size = New System.Drawing.Size(205, 22)
        Me.menuStripToolsSetDefaultInstancePath.Text = "Set Default Instance Path"
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
        Me.scMain.Size = New System.Drawing.Size(801, 456)
        Me.scMain.SplitterDistance = 152
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
        Me.lstInstalls.Size = New System.Drawing.Size(801, 152)
        Me.lstInstalls.TabIndex = 0
        Me.lstInstalls.UseCompatibleStateImageBehavior = False
        Me.lstInstalls.View = System.Windows.Forms.View.Details
        '
        'colHeadInstallsPath
        '
        Me.colHeadInstallsPath.Text = "Path"
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
        Me.lstInstances.Size = New System.Drawing.Size(801, 300)
        Me.lstInstances.TabIndex = 0
        Me.lstInstances.UseCompatibleStateImageBehavior = False
        Me.lstInstances.View = System.Windows.Forms.View.Details
        '
        'colHeadInstancesPath
        '
        Me.colHeadInstancesPath.Text = "Path"
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
        'menuStripToolsUpdate
        '
        Me.menuStripToolsUpdate.Name = "menuStripToolsUpdate"
        Me.menuStripToolsUpdate.Size = New System.Drawing.Size(205, 22)
        Me.menuStripToolsUpdate.Text = "Update Versions"
        '
        'menuStripToolsSeparator1
        '
        Me.menuStripToolsSeparator1.Name = "menuStripToolsSeparator1"
        Me.menuStripToolsSeparator1.Size = New System.Drawing.Size(202, 6)
        '
        'menuStripToolsSeparator2
        '
        Me.menuStripToolsSeparator2.Name = "menuStripToolsSeparator2"
        Me.menuStripToolsSeparator2.Size = New System.Drawing.Size(202, 6)
        '
        'FactorioInstanceManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 480)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.menuStripMain)
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
End Class
