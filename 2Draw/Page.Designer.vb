<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Page
    Inherits CleanTab.Tab

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.cms = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAddPoint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemovePoint = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuRemoveSegment = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsCCP = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportAsPNGFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms.SuspendLayout()
        Me.cmsCCP.SuspendLayout()
        Me.SuspendLayout()
        '
        'cms
        '
        Me.cms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddPoint, Me.mnuRemovePoint, Me.ToolStripSeparator1, Me.mnuRemoveSegment})
        Me.cms.Name = "cms"
        Me.cms.Size = New System.Drawing.Size(168, 76)
        '
        'mnuAddPoint
        '
        Me.mnuAddPoint.Enabled = False
        Me.mnuAddPoint.Name = "mnuAddPoint"
        Me.mnuAddPoint.Size = New System.Drawing.Size(167, 22)
        Me.mnuAddPoint.Text = "Add Point"
        '
        'mnuRemovePoint
        '
        Me.mnuRemovePoint.Enabled = False
        Me.mnuRemovePoint.Name = "mnuRemovePoint"
        Me.mnuRemovePoint.Size = New System.Drawing.Size(167, 22)
        Me.mnuRemovePoint.Text = "Remove Point"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(164, 6)
        '
        'mnuRemoveSegment
        '
        Me.mnuRemoveSegment.Name = "mnuRemoveSegment"
        Me.mnuRemoveSegment.Size = New System.Drawing.Size(167, 22)
        Me.mnuRemoveSegment.Text = "Remove Segment"
        '
        'cmsCCP
        '
        Me.cmsCCP.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripMenuItem1, Me.DeleteToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExportAsPNGFileToolStripMenuItem})
        Me.cmsCCP.Name = "cmsCCP"
        Me.cmsCCP.Size = New System.Drawing.Size(168, 148)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(164, 6)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(164, 6)
        '
        'ExportAsPNGFileToolStripMenuItem
        '
        Me.ExportAsPNGFileToolStripMenuItem.Name = "ExportAsPNGFileToolStripMenuItem"
        Me.ExportAsPNGFileToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.ExportAsPNGFileToolStripMenuItem.Text = "Export as PNG file"
        '
        'Page
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Name = "Page"
        Me.Size = New System.Drawing.Size(647, 397)
        Me.cms.ResumeLayout(False)
        Me.cmsCCP.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cms As ContextMenuStrip
    Friend WithEvents mnuAddPoint As ToolStripMenuItem
    Friend WithEvents mnuRemovePoint As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents mnuRemoveSegment As ToolStripMenuItem
    Friend WithEvents cmsCCP As ContextMenuStrip
    Friend WithEvents CutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents ExportAsPNGFileToolStripMenuItem As ToolStripMenuItem
End Class
