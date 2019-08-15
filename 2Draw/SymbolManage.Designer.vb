<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SymbolManage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSymbolLocation = New System.Windows.Forms.TextBox()
        Me.ccItemList = New _2Draw.CleanContainerSolid()
        Me.lbLibraries = New System.Windows.Forms.ListBox()
        Me.tsLibraries = New System.Windows.Forms.ToolStrip()
        Me.tsAddLibrary = New System.Windows.Forms.ToolStripButton()
        Me.tsRemoveLibrary = New System.Windows.Forms.ToolStripButton()
        Me.ccItems = New _2Draw.CleanContainerSolid()
        Me.lvItems = New _2Draw.LibraryView()
        Me.cmsItems = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteSymbolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsImportsymbols = New System.Windows.Forms.ToolStripButton()
        Me.ccItemList.SuspendLayout()
        Me.tsLibraries.SuspendLayout()
        Me.ccItems.SuspendLayout()
        Me.cmsItems.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Symbol file Location:"
        '
        'txtSymbolLocation
        '
        Me.txtSymbolLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSymbolLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSymbolLocation.Location = New System.Drawing.Point(123, 10)
        Me.txtSymbolLocation.Name = "txtSymbolLocation"
        Me.txtSymbolLocation.ReadOnly = True
        Me.txtSymbolLocation.Size = New System.Drawing.Size(905, 20)
        Me.txtSymbolLocation.TabIndex = 1
        '
        'ccItemList
        '
        Me.ccItemList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ccItemList.AutoScroll = True
        Me.ccItemList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccItemList.Controls.Add(Me.lbLibraries)
        Me.ccItemList.Controls.Add(Me.tsLibraries)
        Me.ccItemList.Location = New System.Drawing.Point(12, 36)
        Me.ccItemList.Name = "ccItemList"
        Me.ccItemList.Padding = New System.Windows.Forms.Padding(0, 22, 0, 0)
        Me.ccItemList.Size = New System.Drawing.Size(161, 476)
        Me.ccItemList.TabIndex = 2
        Me.ccItemList.Text = "Libraries"
        '
        'lbLibraries
        '
        Me.lbLibraries.AllowDrop = True
        Me.lbLibraries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbLibraries.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lbLibraries.FormattingEnabled = True
        Me.lbLibraries.Location = New System.Drawing.Point(0, 47)
        Me.lbLibraries.Name = "lbLibraries"
        Me.lbLibraries.Size = New System.Drawing.Size(159, 427)
        Me.lbLibraries.TabIndex = 1
        '
        'tsLibraries
        '
        Me.tsLibraries.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsLibraries.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsAddLibrary, Me.tsRemoveLibrary, Me.tsImportsymbols})
        Me.tsLibraries.Location = New System.Drawing.Point(0, 22)
        Me.tsLibraries.Name = "tsLibraries"
        Me.tsLibraries.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsLibraries.Size = New System.Drawing.Size(159, 25)
        Me.tsLibraries.TabIndex = 0
        Me.tsLibraries.Text = "ToolStrip1"
        '
        'tsAddLibrary
        '
        Me.tsAddLibrary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsAddLibrary.Image = Global._2Draw.My.Resources.Resources.layerAdd
        Me.tsAddLibrary.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsAddLibrary.Name = "tsAddLibrary"
        Me.tsAddLibrary.Size = New System.Drawing.Size(23, 22)
        Me.tsAddLibrary.Text = "ToolStripButton1"
        '
        'tsRemoveLibrary
        '
        Me.tsRemoveLibrary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsRemoveLibrary.Image = Global._2Draw.My.Resources.Resources.layerRemove
        Me.tsRemoveLibrary.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsRemoveLibrary.Name = "tsRemoveLibrary"
        Me.tsRemoveLibrary.Size = New System.Drawing.Size(23, 22)
        Me.tsRemoveLibrary.Text = "ToolStripButton2"
        '
        'ccItems
        '
        Me.ccItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ccItems.AutoScroll = True
        Me.ccItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccItems.Controls.Add(Me.lvItems)
        Me.ccItems.Location = New System.Drawing.Point(179, 36)
        Me.ccItems.Name = "ccItems"
        Me.ccItems.Padding = New System.Windows.Forms.Padding(0, 22, 0, 0)
        Me.ccItems.Size = New System.Drawing.Size(849, 476)
        Me.ccItems.TabIndex = 3
        Me.ccItems.Text = "Items"
        '
        'lvItems
        '
        Me.lvItems.AutoScroll = True
        Me.lvItems.AutoScrollMinSize = New System.Drawing.Size(0, 64)
        Me.lvItems.BackColor = System.Drawing.Color.White
        Me.lvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvItems.Location = New System.Drawing.Point(0, 22)
        Me.lvItems.Name = "lvItems"
        Me.lvItems.Size = New System.Drawing.Size(847, 452)
        Me.lvItems.TabIndex = 0
        '
        'cmsItems
        '
        Me.cmsItems.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteSymbolToolStripMenuItem})
        Me.cmsItems.Name = "cmsItems"
        Me.cmsItems.Size = New System.Drawing.Size(151, 26)
        '
        'DeleteSymbolToolStripMenuItem
        '
        Me.DeleteSymbolToolStripMenuItem.Image = Global._2Draw.My.Resources.Resources.layerRemove
        Me.DeleteSymbolToolStripMenuItem.Name = "DeleteSymbolToolStripMenuItem"
        Me.DeleteSymbolToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.DeleteSymbolToolStripMenuItem.Text = "Delete Symbol"
        '
        'tsImportsymbols
        '
        Me.tsImportsymbols.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsImportsymbols.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsImportsymbols.Image = Global._2Draw.My.Resources.Resources.pageImport
        Me.tsImportsymbols.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsImportsymbols.Name = "tsImportsymbols"
        Me.tsImportsymbols.Size = New System.Drawing.Size(23, 22)
        Me.tsImportsymbols.Text = "Import Symbols from another file"
        '
        'SymbolManage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1040, 524)
        Me.Controls.Add(Me.ccItems)
        Me.Controls.Add(Me.ccItemList)
        Me.Controls.Add(Me.txtSymbolLocation)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "SymbolManage"
        Me.Text = "Symbol Manager"
        Me.ccItemList.ResumeLayout(False)
        Me.ccItemList.PerformLayout()
        Me.tsLibraries.ResumeLayout(False)
        Me.tsLibraries.PerformLayout()
        Me.ccItems.ResumeLayout(False)
        Me.cmsItems.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtSymbolLocation As TextBox
    Friend WithEvents ccItemList As CleanContainerSolid
    Friend WithEvents ccItems As CleanContainerSolid
    Friend WithEvents lvItems As LibraryView
    Friend WithEvents lbLibraries As ListBox
    Friend WithEvents tsLibraries As ToolStrip
    Friend WithEvents tsAddLibrary As ToolStripButton
    Friend WithEvents tsRemoveLibrary As ToolStripButton
    Friend WithEvents cmsItems As ContextMenuStrip
    Friend WithEvents DeleteSymbolToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsImportsymbols As ToolStripButton
End Class
