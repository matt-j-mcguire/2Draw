<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
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
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.tsNewFile = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsNewProject = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddNewPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsImportPage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteCurrentPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsOpenFile = New System.Windows.Forms.ToolStripButton()
        Me.tsSaveFile = New System.Windows.Forms.ToolStripSplitButton()
        Me.tsSaveFileAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsPrintFile = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsOptions = New System.Windows.Forms.ToolStripButton()
        Me.tsTools = New System.Windows.Forms.ToolStrip()
        Me.tslblTools = New System.Windows.Forms.ToolStripLabel()
        Me.tsToolMouse = New System.Windows.Forms.ToolStripButton()
        Me.tsToolPan = New System.Windows.Forms.ToolStripButton()
        Me.tsToolRec = New System.Windows.Forms.ToolStripButton()
        Me.tsToolCircle = New System.Windows.Forms.ToolStripButton()
        Me.tsToolTriangle = New System.Windows.Forms.ToolStripButton()
        Me.tsToolLine = New System.Windows.Forms.ToolStripButton()
        Me.tstoolText = New System.Windows.Forms.ToolStripButton()
        Me.tstoolImage = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsFlipHorz = New System.Windows.Forms.ToolStripButton()
        Me.tsFlipVert = New System.Windows.Forms.ToolStripButton()
        Me.tsBringForward = New System.Windows.Forms.ToolStripButton()
        Me.tsToolSendBack = New System.Windows.Forms.ToolStripButton()
        Me.tsToolCloneItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsToolGroup = New System.Windows.Forms.ToolStripButton()
        Me.tsToolUngroup = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsToolRotation = New System.Windows.Forms.ToolStripButton()
        Me.tsToolMoveHandle = New System.Windows.Forms.ToolStripButton()
        Me.tsToolGrid = New System.Windows.Forms.ToolStripButton()
        Me.tsToolCrossHairs = New System.Windows.Forms.ToolStripButton()
        Me.tsToolNormalWidth = New System.Windows.Forms.ToolStripButton()
        Me.PropPanel = New System.Windows.Forms.Panel()
        Me.ccSymbols = New _2Draw.CleanContainer()
        Me.symLibList = New _2Draw.LibraryView()
        Me.lblMouseOverSymbol = New System.Windows.Forms.Label()
        Me.symboltoolstrip = New System.Windows.Forms.ToolStrip()
        Me.tsAddNewSymbol = New System.Windows.Forms.ToolStripButton()
        Me.tsSelectLibrary = New System.Windows.Forms.ToolStripComboBox()
        Me.tsEditLibraries = New System.Windows.Forms.ToolStripButton()
        Me.ccAlignment = New _2Draw.CleanContainer()
        Me.propSpacing = New _2Draw.DispSpacing()
        Me.PropSizing = New _2Draw.DispSizing()
        Me.propAlignment = New _2Draw.DispAlignment()
        Me.ccFont = New _2Draw.CleanContainer()
        Me.PropFontStyle = New _2Draw.FontStyles()
        Me.propFontJust = New _2Draw.FontJustified()
        Me.propFontNameSize = New _2Draw.FontCombo()
        Me.ccShapeOptions = New _2Draw.CleanContainer()
        Me.propShapHatch = New _2Draw.FillHatchingCombo()
        Me.propShapeFill = New _2Draw.ColorPickerCombobox()
        Me.ccLineOptions = New _2Draw.CleanContainer()
        Me.propLineStyle = New _2Draw.LineStyled()
        Me.PropLineWidth = New _2Draw.LineWidth()
        Me.PropLineColor = New _2Draw.ColorPickerCombobox()
        Me.ccLayers = New _2Draw.CleanContainer()
        Me.lbLayers = New System.Windows.Forms.ListBox()
        Me.tsLayers = New System.Windows.Forms.ToolStrip()
        Me.tsLayerAdd = New System.Windows.Forms.ToolStripButton()
        Me.tsLayerRemove = New System.Windows.Forms.ToolStripButton()
        Me.tsLayerClone = New System.Windows.Forms.ToolStripButton()
        Me.MergeLayerDown = New System.Windows.Forms.ToolStripButton()
        Me.CreateLayerFromSelected = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsLayerUP = New System.Windows.Forms.ToolStripButton()
        Me.tsLayerDown = New System.Windows.Forms.ToolStripButton()
        Me.tsLayerRename = New System.Windows.Forms.ToolStripButton()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlPageZoom = New System.Windows.Forms.Panel()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.lblZoom = New System.Windows.Forms.Label()
        Me.tmrAutoSave = New System.Windows.Forms.Timer(Me.components)
        Me.mainTabs = New _2Draw.CleanTab()
        Me.cmsPageTabMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RenamePageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClonePageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToNewProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteThisPageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAbout = New System.Windows.Forms.ToolStripButton()
        Me.tsMain.SuspendLayout()
        Me.tsTools.SuspendLayout()
        Me.PropPanel.SuspendLayout()
        Me.ccSymbols.SuspendLayout()
        Me.symboltoolstrip.SuspendLayout()
        Me.ccAlignment.SuspendLayout()
        Me.ccFont.SuspendLayout()
        Me.ccShapeOptions.SuspendLayout()
        Me.ccLineOptions.SuspendLayout()
        Me.ccLayers.SuspendLayout()
        Me.tsLayers.SuspendLayout()
        Me.pnlPageZoom.SuspendLayout()
        Me.cmsPageTabMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsMain
        '
        Me.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsNewFile, Me.tsOpenFile, Me.tsSaveFile, Me.ToolStripSeparator1, Me.tsPrintFile, Me.ToolStripSeparator2, Me.tsOptions, Me.btnAbout})
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsMain.Size = New System.Drawing.Size(1008, 25)
        Me.tsMain.TabIndex = 0
        Me.tsMain.Text = "tsMain"
        '
        'tsNewFile
        '
        Me.tsNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsNewFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsNewProject, Me.ToolStripSeparator3, Me.AddNewPageToolStripMenuItem, Me.tsImportPage, Me.ToolStripSeparator7, Me.DeleteCurrentPageToolStripMenuItem})
        Me.tsNewFile.Image = Global._2Draw.My.Resources.Resources.newfile
        Me.tsNewFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsNewFile.Name = "tsNewFile"
        Me.tsNewFile.Size = New System.Drawing.Size(29, 22)
        Me.tsNewFile.Text = "Project"
        '
        'tsNewProject
        '
        Me.tsNewProject.Image = Global._2Draw.My.Resources.Resources.newfile
        Me.tsNewProject.Name = "tsNewProject"
        Me.tsNewProject.Size = New System.Drawing.Size(252, 22)
        Me.tsNewProject.Text = "New Project"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(249, 6)
        '
        'AddNewPageToolStripMenuItem
        '
        Me.AddNewPageToolStripMenuItem.Image = Global._2Draw.My.Resources.Resources.PageAdd
        Me.AddNewPageToolStripMenuItem.Name = "AddNewPageToolStripMenuItem"
        Me.AddNewPageToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.AddNewPageToolStripMenuItem.Text = "Add New Page"
        '
        'tsImportPage
        '
        Me.tsImportPage.Image = Global._2Draw.My.Resources.Resources.pageImport
        Me.tsImportPage.Name = "tsImportPage"
        Me.tsImportPage.Size = New System.Drawing.Size(252, 22)
        Me.tsImportPage.Text = "Import Page from another Project"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(249, 6)
        '
        'DeleteCurrentPageToolStripMenuItem
        '
        Me.DeleteCurrentPageToolStripMenuItem.Image = Global._2Draw.My.Resources.Resources.PageRemove
        Me.DeleteCurrentPageToolStripMenuItem.Name = "DeleteCurrentPageToolStripMenuItem"
        Me.DeleteCurrentPageToolStripMenuItem.Size = New System.Drawing.Size(252, 22)
        Me.DeleteCurrentPageToolStripMenuItem.Text = "Delete Current Page"
        '
        'tsOpenFile
        '
        Me.tsOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsOpenFile.Image = Global._2Draw.My.Resources.Resources.Openfile
        Me.tsOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsOpenFile.Name = "tsOpenFile"
        Me.tsOpenFile.Size = New System.Drawing.Size(23, 22)
        Me.tsOpenFile.Text = "Open File"
        '
        'tsSaveFile
        '
        Me.tsSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsSaveFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsSaveFileAs})
        Me.tsSaveFile.Image = Global._2Draw.My.Resources.Resources.Save
        Me.tsSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsSaveFile.Name = "tsSaveFile"
        Me.tsSaveFile.Size = New System.Drawing.Size(32, 22)
        Me.tsSaveFile.Text = "Save File"
        Me.tsSaveFile.ToolTipText = "Save File (ctrl. + S)"
        '
        'tsSaveFileAs
        '
        Me.tsSaveFileAs.Name = "tsSaveFileAs"
        Me.tsSaveFileAs.Size = New System.Drawing.Size(123, 22)
        Me.tsSaveFileAs.Text = "Save As..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tsPrintFile
        '
        Me.tsPrintFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsPrintFile.Image = Global._2Draw.My.Resources.Resources.printfile
        Me.tsPrintFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsPrintFile.Name = "tsPrintFile"
        Me.tsPrintFile.Size = New System.Drawing.Size(23, 22)
        Me.tsPrintFile.Text = "Print"
        Me.tsPrintFile.ToolTipText = "Print (ctrl. + P)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsOptions
        '
        Me.tsOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsOptions.Image = Global._2Draw.My.Resources.Resources.Options
        Me.tsOptions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsOptions.Name = "tsOptions"
        Me.tsOptions.Size = New System.Drawing.Size(23, 22)
        Me.tsOptions.Text = "Preferences"
        '
        'tsTools
        '
        Me.tsTools.Dock = System.Windows.Forms.DockStyle.Left
        Me.tsTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslblTools, Me.tsToolMouse, Me.tsToolPan, Me.tsToolRec, Me.tsToolCircle, Me.tsToolTriangle, Me.tsToolLine, Me.tstoolText, Me.tstoolImage, Me.ToolStripButton1, Me.tsFlipHorz, Me.tsFlipVert, Me.tsBringForward, Me.tsToolSendBack, Me.tsToolCloneItem, Me.ToolStripSeparator6, Me.tsToolGroup, Me.tsToolUngroup, Me.ToolStripSeparator5, Me.tsToolRotation, Me.tsToolMoveHandle, Me.tsToolGrid, Me.tsToolCrossHairs, Me.tsToolNormalWidth})
        Me.tsTools.Location = New System.Drawing.Point(0, 25)
        Me.tsTools.Name = "tsTools"
        Me.tsTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsTools.Size = New System.Drawing.Size(36, 566)
        Me.tsTools.TabIndex = 1
        Me.tsTools.Text = "Tools"
        '
        'tslblTools
        '
        Me.tslblTools.Name = "tslblTools"
        Me.tslblTools.Size = New System.Drawing.Size(33, 15)
        Me.tslblTools.Text = "Tools"
        '
        'tsToolMouse
        '
        Me.tsToolMouse.Checked = True
        Me.tsToolMouse.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsToolMouse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolMouse.Image = Global._2Draw.My.Resources.Resources.ToolMouse
        Me.tsToolMouse.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolMouse.Name = "tsToolMouse"
        Me.tsToolMouse.Size = New System.Drawing.Size(33, 20)
        Me.tsToolMouse.Tag = "TOOL"
        Me.tsToolMouse.Text = "ToolStripButton1"
        Me.tsToolMouse.ToolTipText = "Mouse (ctrl. + 1 or ESC)"
        '
        'tsToolPan
        '
        Me.tsToolPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolPan.Image = Global._2Draw.My.Resources.Resources.ToolHand
        Me.tsToolPan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolPan.Name = "tsToolPan"
        Me.tsToolPan.Size = New System.Drawing.Size(33, 20)
        Me.tsToolPan.Tag = "TOOL"
        Me.tsToolPan.Text = "Pan"
        Me.tsToolPan.ToolTipText = "Pan (ctrl. + 2)"
        '
        'tsToolRec
        '
        Me.tsToolRec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolRec.Image = Global._2Draw.My.Resources.Resources.ToolRec
        Me.tsToolRec.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolRec.Name = "tsToolRec"
        Me.tsToolRec.Size = New System.Drawing.Size(33, 20)
        Me.tsToolRec.Tag = "TOOL"
        Me.tsToolRec.Text = "Rectangle Tool"
        Me.tsToolRec.ToolTipText = "Rectangle Tool (ctrl. + 3)"
        '
        'tsToolCircle
        '
        Me.tsToolCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolCircle.Image = Global._2Draw.My.Resources.Resources.toolCircle
        Me.tsToolCircle.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolCircle.Name = "tsToolCircle"
        Me.tsToolCircle.Size = New System.Drawing.Size(33, 20)
        Me.tsToolCircle.Tag = "TOOL"
        Me.tsToolCircle.Text = "Circle Tool"
        Me.tsToolCircle.ToolTipText = "Circle Tool (ctrl. + 4)"
        '
        'tsToolTriangle
        '
        Me.tsToolTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolTriangle.Image = Global._2Draw.My.Resources.Resources.ToolTriangle
        Me.tsToolTriangle.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolTriangle.Name = "tsToolTriangle"
        Me.tsToolTriangle.Size = New System.Drawing.Size(33, 20)
        Me.tsToolTriangle.Tag = "TOOL"
        Me.tsToolTriangle.Text = "Triangle Tool"
        Me.tsToolTriangle.ToolTipText = "Triangle Tool (ctrl. + 5)"
        '
        'tsToolLine
        '
        Me.tsToolLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolLine.Image = Global._2Draw.My.Resources.Resources.ToolLine
        Me.tsToolLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolLine.Name = "tsToolLine"
        Me.tsToolLine.Size = New System.Drawing.Size(33, 20)
        Me.tsToolLine.Tag = "TOOL"
        Me.tsToolLine.Text = "Line Tool"
        Me.tsToolLine.ToolTipText = "Line Tool (ctrl. + 6)"
        '
        'tstoolText
        '
        Me.tstoolText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tstoolText.Image = Global._2Draw.My.Resources.Resources.toolText
        Me.tstoolText.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tstoolText.Name = "tstoolText"
        Me.tstoolText.Size = New System.Drawing.Size(33, 20)
        Me.tstoolText.Tag = "TOOL"
        Me.tstoolText.Text = "Text Tool"
        Me.tstoolText.ToolTipText = "Text Tool (ctrl. + 7)"
        '
        'tstoolImage
        '
        Me.tstoolImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tstoolImage.Image = Global._2Draw.My.Resources.Resources.toolImage
        Me.tstoolImage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tstoolImage.Name = "tstoolImage"
        Me.tstoolImage.Size = New System.Drawing.Size(33, 20)
        Me.tstoolImage.Tag = "TOOL"
        Me.tstoolImage.Text = "Image Tool"
        Me.tstoolImage.ToolTipText = "Image Tool (ctrl. + 8)"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(33, 6)
        '
        'tsFlipHorz
        '
        Me.tsFlipHorz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsFlipHorz.Image = Global._2Draw.My.Resources.Resources.flipHorz
        Me.tsFlipHorz.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsFlipHorz.Name = "tsFlipHorz"
        Me.tsFlipHorz.Size = New System.Drawing.Size(33, 20)
        Me.tsFlipHorz.Text = "Flip Selected Items Horizontally"
        Me.tsFlipHorz.ToolTipText = "Flip Selected Items Horizontally (ctrl. + left/right arrow)"
        '
        'tsFlipVert
        '
        Me.tsFlipVert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsFlipVert.Image = Global._2Draw.My.Resources.Resources.flipVert
        Me.tsFlipVert.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsFlipVert.Name = "tsFlipVert"
        Me.tsFlipVert.Size = New System.Drawing.Size(33, 20)
        Me.tsFlipVert.Text = "Flip Selected Items Vertically"
        Me.tsFlipVert.ToolTipText = "Flip Selected Items Vertically (ctrl. + up/down arrow)"
        '
        'tsBringForward
        '
        Me.tsBringForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsBringForward.Image = Global._2Draw.My.Resources.Resources.BringForward
        Me.tsBringForward.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBringForward.Name = "tsBringForward"
        Me.tsBringForward.Size = New System.Drawing.Size(33, 20)
        Me.tsBringForward.Text = "Bring Item Forward"
        Me.tsBringForward.ToolTipText = "Bring Item Forward (ctrl. + Page Up)"
        '
        'tsToolSendBack
        '
        Me.tsToolSendBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolSendBack.Image = Global._2Draw.My.Resources.Resources.ToolSendBack
        Me.tsToolSendBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolSendBack.Name = "tsToolSendBack"
        Me.tsToolSendBack.Size = New System.Drawing.Size(33, 20)
        Me.tsToolSendBack.Text = "Send Item Back"
        Me.tsToolSendBack.ToolTipText = "Send Item Back (ctrl. + Page Down)"
        '
        'tsToolCloneItem
        '
        Me.tsToolCloneItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolCloneItem.Image = Global._2Draw.My.Resources.Resources.ToolClone1
        Me.tsToolCloneItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolCloneItem.Name = "tsToolCloneItem"
        Me.tsToolCloneItem.Size = New System.Drawing.Size(33, 20)
        Me.tsToolCloneItem.Text = "Clone Item(s)"
        Me.tsToolCloneItem.ToolTipText = "Clone Item(s) (ctrl. + Q)"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(33, 6)
        '
        'tsToolGroup
        '
        Me.tsToolGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolGroup.Image = Global._2Draw.My.Resources.Resources.ToolGroup
        Me.tsToolGroup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolGroup.Name = "tsToolGroup"
        Me.tsToolGroup.Size = New System.Drawing.Size(33, 20)
        Me.tsToolGroup.Text = "Group Selected Items"
        Me.tsToolGroup.ToolTipText = "Group Selected Items (ctrl. + G)"
        '
        'tsToolUngroup
        '
        Me.tsToolUngroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolUngroup.Image = Global._2Draw.My.Resources.Resources.ToolUngroup
        Me.tsToolUngroup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolUngroup.Name = "tsToolUngroup"
        Me.tsToolUngroup.Size = New System.Drawing.Size(33, 20)
        Me.tsToolUngroup.Text = "Ungroup Selected Item"
        Me.tsToolUngroup.ToolTipText = "Ungroup Selected Item (ctrl. + U)"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(33, 6)
        '
        'tsToolRotation
        '
        Me.tsToolRotation.CheckOnClick = True
        Me.tsToolRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolRotation.Image = Global._2Draw.My.Resources.Resources.ToolRotate
        Me.tsToolRotation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolRotation.Name = "tsToolRotation"
        Me.tsToolRotation.Size = New System.Drawing.Size(33, 20)
        Me.tsToolRotation.Text = "Show Rotation Tool"
        '
        'tsToolMoveHandle
        '
        Me.tsToolMoveHandle.CheckOnClick = True
        Me.tsToolMoveHandle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolMoveHandle.Image = Global._2Draw.My.Resources.Resources.move16
        Me.tsToolMoveHandle.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolMoveHandle.Name = "tsToolMoveHandle"
        Me.tsToolMoveHandle.Size = New System.Drawing.Size(33, 20)
        Me.tsToolMoveHandle.Text = "Show Item Movement Tool"
        '
        'tsToolGrid
        '
        Me.tsToolGrid.CheckOnClick = True
        Me.tsToolGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolGrid.Image = Global._2Draw.My.Resources.Resources.ToolGrid
        Me.tsToolGrid.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolGrid.Name = "tsToolGrid"
        Me.tsToolGrid.Size = New System.Drawing.Size(33, 20)
        Me.tsToolGrid.Text = "Show Grid"
        '
        'tsToolCrossHairs
        '
        Me.tsToolCrossHairs.CheckOnClick = True
        Me.tsToolCrossHairs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolCrossHairs.Image = Global._2Draw.My.Resources.Resources.toolcross
        Me.tsToolCrossHairs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolCrossHairs.Name = "tsToolCrossHairs"
        Me.tsToolCrossHairs.Size = New System.Drawing.Size(33, 20)
        Me.tsToolCrossHairs.Text = "Use Cross Hairs"
        '
        'tsToolNormalWidth
        '
        Me.tsToolNormalWidth.CheckOnClick = True
        Me.tsToolNormalWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsToolNormalWidth.Image = Global._2Draw.My.Resources.Resources.NormalSize
        Me.tsToolNormalWidth.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsToolNormalWidth.Name = "tsToolNormalWidth"
        Me.tsToolNormalWidth.Size = New System.Drawing.Size(33, 20)
        Me.tsToolNormalWidth.Text = "Show Lines At Normal Width"
        '
        'PropPanel
        '
        Me.PropPanel.AutoScroll = True
        Me.PropPanel.Controls.Add(Me.ccSymbols)
        Me.PropPanel.Controls.Add(Me.ccAlignment)
        Me.PropPanel.Controls.Add(Me.ccFont)
        Me.PropPanel.Controls.Add(Me.ccShapeOptions)
        Me.PropPanel.Controls.Add(Me.ccLineOptions)
        Me.PropPanel.Controls.Add(Me.ccLayers)
        Me.PropPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.PropPanel.Location = New System.Drawing.Point(787, 25)
        Me.PropPanel.Name = "PropPanel"
        Me.PropPanel.Padding = New System.Windows.Forms.Padding(2)
        Me.PropPanel.Size = New System.Drawing.Size(221, 566)
        Me.PropPanel.TabIndex = 2
        '
        'ccSymbols
        '
        Me.ccSymbols.AutoFill = True
        Me.ccSymbols.AutoScroll = True
        Me.ccSymbols.BackColor = System.Drawing.SystemColors.Control
        Me.ccSymbols.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccSymbols.Controls.Add(Me.symLibList)
        Me.ccSymbols.Controls.Add(Me.lblMouseOverSymbol)
        Me.ccSymbols.Controls.Add(Me.symboltoolstrip)
        Me.ccSymbols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ccSymbols.Location = New System.Drawing.Point(2, 408)
        Me.ccSymbols.Name = "ccSymbols"
        Me.ccSymbols.Open = True
        Me.ccSymbols.OpenHeight = 200
        Me.ccSymbols.Padding = New System.Windows.Forms.Padding(2, 20, 2, 2)
        Me.ccSymbols.Size = New System.Drawing.Size(217, 156)
        Me.ccSymbols.TabIndex = 5
        Me.ccSymbols.Text = "Symbol Library"
        '
        'symLibList
        '
        Me.symLibList.AutoScroll = True
        Me.symLibList.AutoScrollMinSize = New System.Drawing.Size(0, 64)
        Me.symLibList.BackColor = System.Drawing.Color.White
        Me.symLibList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.symLibList.Location = New System.Drawing.Point(2, 45)
        Me.symLibList.Name = "symLibList"
        Me.symLibList.Size = New System.Drawing.Size(211, 94)
        Me.symLibList.TabIndex = 2
        '
        'lblMouseOverSymbol
        '
        Me.lblMouseOverSymbol.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblMouseOverSymbol.Location = New System.Drawing.Point(2, 139)
        Me.lblMouseOverSymbol.Name = "lblMouseOverSymbol"
        Me.lblMouseOverSymbol.Size = New System.Drawing.Size(211, 13)
        Me.lblMouseOverSymbol.TabIndex = 3
        '
        'symboltoolstrip
        '
        Me.symboltoolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.symboltoolstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsAddNewSymbol, Me.tsSelectLibrary, Me.tsEditLibraries})
        Me.symboltoolstrip.Location = New System.Drawing.Point(2, 20)
        Me.symboltoolstrip.Name = "symboltoolstrip"
        Me.symboltoolstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.symboltoolstrip.Size = New System.Drawing.Size(211, 25)
        Me.symboltoolstrip.TabIndex = 1
        Me.symboltoolstrip.Text = "ToolStrip2"
        '
        'tsAddNewSymbol
        '
        Me.tsAddNewSymbol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsAddNewSymbol.Image = Global._2Draw.My.Resources.Resources.layerAdd
        Me.tsAddNewSymbol.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsAddNewSymbol.Name = "tsAddNewSymbol"
        Me.tsAddNewSymbol.Size = New System.Drawing.Size(23, 22)
        Me.tsAddNewSymbol.Text = "Add New Symbol to current library"
        '
        'tsSelectLibrary
        '
        Me.tsSelectLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tsSelectLibrary.Name = "tsSelectLibrary"
        Me.tsSelectLibrary.Size = New System.Drawing.Size(121, 25)
        '
        'tsEditLibraries
        '
        Me.tsEditLibraries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsEditLibraries.Image = Global._2Draw.My.Resources.Resources.Options
        Me.tsEditLibraries.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsEditLibraries.Name = "tsEditLibraries"
        Me.tsEditLibraries.Size = New System.Drawing.Size(23, 22)
        Me.tsEditLibraries.Text = "Manage Libaries"
        '
        'ccAlignment
        '
        Me.ccAlignment.AutoFill = False
        Me.ccAlignment.AutoScroll = True
        Me.ccAlignment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccAlignment.Controls.Add(Me.propSpacing)
        Me.ccAlignment.Controls.Add(Me.PropSizing)
        Me.ccAlignment.Controls.Add(Me.propAlignment)
        Me.ccAlignment.Dock = System.Windows.Forms.DockStyle.Top
        Me.ccAlignment.Location = New System.Drawing.Point(2, 318)
        Me.ccAlignment.Name = "ccAlignment"
        Me.ccAlignment.Open = True
        Me.ccAlignment.OpenHeight = 90
        Me.ccAlignment.Padding = New System.Windows.Forms.Padding(2)
        Me.ccAlignment.Size = New System.Drawing.Size(217, 90)
        Me.ccAlignment.TabIndex = 4
        Me.ccAlignment.Text = "Alignment & Sizing"
        '
        'propSpacing
        '
        Me.propSpacing.Location = New System.Drawing.Point(9, 26)
        Me.propSpacing.Name = "propSpacing"
        Me.propSpacing.Size = New System.Drawing.Size(165, 23)
        Me.propSpacing.TabIndex = 2
        '
        'PropSizing
        '
        Me.PropSizing.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropSizing.Location = New System.Drawing.Point(153, 55)
        Me.PropSizing.Name = "PropSizing"
        Me.PropSizing.Size = New System.Drawing.Size(60, 23)
        Me.PropSizing.TabIndex = 1
        '
        'propAlignment
        '
        Me.propAlignment.Location = New System.Drawing.Point(9, 55)
        Me.propAlignment.Name = "propAlignment"
        Me.propAlignment.Size = New System.Drawing.Size(122, 23)
        Me.propAlignment.TabIndex = 0
        '
        'ccFont
        '
        Me.ccFont.AutoFill = False
        Me.ccFont.AutoScroll = True
        Me.ccFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccFont.Controls.Add(Me.PropFontStyle)
        Me.ccFont.Controls.Add(Me.propFontJust)
        Me.ccFont.Controls.Add(Me.propFontNameSize)
        Me.ccFont.Dock = System.Windows.Forms.DockStyle.Top
        Me.ccFont.Location = New System.Drawing.Point(2, 230)
        Me.ccFont.Name = "ccFont"
        Me.ccFont.Open = True
        Me.ccFont.OpenHeight = 88
        Me.ccFont.Padding = New System.Windows.Forms.Padding(2)
        Me.ccFont.Size = New System.Drawing.Size(217, 88)
        Me.ccFont.TabIndex = 3
        Me.ccFont.Text = "Font"
        '
        'PropFontStyle
        '
        Me.PropFontStyle.FontStyle = System.Drawing.FontStyle.Regular
        Me.PropFontStyle.Location = New System.Drawing.Point(9, 54)
        Me.PropFontStyle.Name = "PropFontStyle"
        Me.PropFontStyle.Size = New System.Drawing.Size(80, 23)
        Me.PropFontStyle.TabIndex = 2
        '
        'propFontJust
        '
        Me.propFontJust.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propFontJust.fontAlignment = System.Drawing.StringAlignment.Near
        Me.propFontJust.Location = New System.Drawing.Point(146, 54)
        Me.propFontJust.Name = "propFontJust"
        Me.propFontJust.Size = New System.Drawing.Size(60, 23)
        Me.propFontJust.TabIndex = 1
        '
        'propFontNameSize
        '
        Me.propFontNameSize.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propFontNameSize.Location = New System.Drawing.Point(9, 25)
        Me.propFontNameSize.Name = "propFontNameSize"
        Me.propFontNameSize.SelectedItem = "Consolas"
        Me.propFontNameSize.selectedSize = 16
        Me.propFontNameSize.Size = New System.Drawing.Size(197, 23)
        Me.propFontNameSize.TabIndex = 0
        '
        'ccShapeOptions
        '
        Me.ccShapeOptions.AutoFill = False
        Me.ccShapeOptions.AutoScroll = True
        Me.ccShapeOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccShapeOptions.Controls.Add(Me.propShapHatch)
        Me.ccShapeOptions.Controls.Add(Me.propShapeFill)
        Me.ccShapeOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.ccShapeOptions.Location = New System.Drawing.Point(2, 140)
        Me.ccShapeOptions.Name = "ccShapeOptions"
        Me.ccShapeOptions.Open = True
        Me.ccShapeOptions.OpenHeight = 90
        Me.ccShapeOptions.Padding = New System.Windows.Forms.Padding(2)
        Me.ccShapeOptions.Size = New System.Drawing.Size(217, 90)
        Me.ccShapeOptions.TabIndex = 2
        Me.ccShapeOptions.Text = "Shape Fill Options"
        '
        'propShapHatch
        '
        Me.propShapHatch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propShapHatch.HatchValue = System.Drawing.Drawing2D.HatchStyle.Horizontal
        Me.propShapHatch.isHatch = False
        Me.propShapHatch.Location = New System.Drawing.Point(9, 54)
        Me.propShapHatch.Name = "propShapHatch"
        Me.propShapHatch.Size = New System.Drawing.Size(197, 23)
        Me.propShapHatch.TabIndex = 10
        '
        'propShapeFill
        '
        Me.propShapeFill.AllowTransparent = True
        Me.propShapeFill.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propShapeFill.HasBoarder = False
        Me.propShapeFill.Location = New System.Drawing.Point(9, 25)
        Me.propShapeFill.Name = "propShapeFill"
        Me.propShapeFill.SelectedItem = System.Drawing.Color.Black
        Me.propShapeFill.Size = New System.Drawing.Size(197, 23)
        Me.propShapeFill.TabIndex = 9
        Me.propShapeFill.Text = "lcLineColor"
        '
        'ccLineOptions
        '
        Me.ccLineOptions.AutoFill = False
        Me.ccLineOptions.AutoScroll = True
        Me.ccLineOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccLineOptions.Controls.Add(Me.propLineStyle)
        Me.ccLineOptions.Controls.Add(Me.PropLineWidth)
        Me.ccLineOptions.Controls.Add(Me.PropLineColor)
        Me.ccLineOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.ccLineOptions.Location = New System.Drawing.Point(2, 22)
        Me.ccLineOptions.Name = "ccLineOptions"
        Me.ccLineOptions.Open = True
        Me.ccLineOptions.OpenHeight = 118
        Me.ccLineOptions.Padding = New System.Windows.Forms.Padding(2)
        Me.ccLineOptions.Size = New System.Drawing.Size(217, 118)
        Me.ccLineOptions.TabIndex = 1
        Me.ccLineOptions.Text = "Line Options"
        '
        'propLineStyle
        '
        Me.propLineStyle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.propLineStyle.EndCap = _2Draw.endCapType.capType.None
        Me.propLineStyle.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.propLineStyle.Location = New System.Drawing.Point(9, 86)
        Me.propLineStyle.Name = "propLineStyle"
        Me.propLineStyle.Size = New System.Drawing.Size(197, 23)
        Me.propLineStyle.StartCap = _2Draw.endCapType.capType.None
        Me.propLineStyle.TabIndex = 7
        '
        'PropLineWidth
        '
        Me.PropLineWidth.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropLineWidth.LineWidth = 1.0!
        Me.PropLineWidth.Location = New System.Drawing.Point(9, 57)
        Me.PropLineWidth.Name = "PropLineWidth"
        Me.PropLineWidth.Size = New System.Drawing.Size(197, 23)
        Me.PropLineWidth.TabIndex = 5
        '
        'PropLineColor
        '
        Me.PropLineColor.AllowTransparent = False
        Me.PropLineColor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropLineColor.HasBoarder = False
        Me.PropLineColor.Location = New System.Drawing.Point(9, 28)
        Me.PropLineColor.Name = "PropLineColor"
        Me.PropLineColor.SelectedItem = System.Drawing.Color.Black
        Me.PropLineColor.Size = New System.Drawing.Size(197, 23)
        Me.PropLineColor.TabIndex = 3
        Me.PropLineColor.Text = "lcLineColor"
        '
        'ccLayers
        '
        Me.ccLayers.AutoFill = False
        Me.ccLayers.AutoScroll = True
        Me.ccLayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ccLayers.Controls.Add(Me.lbLayers)
        Me.ccLayers.Controls.Add(Me.tsLayers)
        Me.ccLayers.Dock = System.Windows.Forms.DockStyle.Top
        Me.ccLayers.Location = New System.Drawing.Point(2, 2)
        Me.ccLayers.Name = "ccLayers"
        Me.ccLayers.Open = False
        Me.ccLayers.OpenHeight = 174
        Me.ccLayers.Padding = New System.Windows.Forms.Padding(2, 20, 2, 2)
        Me.ccLayers.Size = New System.Drawing.Size(217, 20)
        Me.ccLayers.TabIndex = 0
        Me.ccLayers.Text = "Layers"
        '
        'lbLayers
        '
        Me.lbLayers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbLayers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbLayers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lbLayers.FormattingEnabled = True
        Me.lbLayers.IntegralHeight = False
        Me.lbLayers.ItemHeight = 18
        Me.lbLayers.Location = New System.Drawing.Point(2, 45)
        Me.lbLayers.Name = "lbLayers"
        Me.lbLayers.Size = New System.Drawing.Size(211, 0)
        Me.lbLayers.TabIndex = 1
        '
        'tsLayers
        '
        Me.tsLayers.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsLayers.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsLayerAdd, Me.tsLayerRemove, Me.tsLayerClone, Me.MergeLayerDown, Me.CreateLayerFromSelected, Me.ToolStripSeparator4, Me.tsLayerUP, Me.tsLayerDown, Me.tsLayerRename})
        Me.tsLayers.Location = New System.Drawing.Point(2, 20)
        Me.tsLayers.Name = "tsLayers"
        Me.tsLayers.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.tsLayers.Size = New System.Drawing.Size(194, 25)
        Me.tsLayers.TabIndex = 0
        Me.tsLayers.Text = "ToolStrip2"
        '
        'tsLayerAdd
        '
        Me.tsLayerAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsLayerAdd.Image = Global._2Draw.My.Resources.Resources.layerAdd
        Me.tsLayerAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsLayerAdd.Name = "tsLayerAdd"
        Me.tsLayerAdd.Size = New System.Drawing.Size(23, 22)
        Me.tsLayerAdd.Text = "Add a new layer"
        '
        'tsLayerRemove
        '
        Me.tsLayerRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsLayerRemove.Image = Global._2Draw.My.Resources.Resources.layerRemove
        Me.tsLayerRemove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsLayerRemove.Name = "tsLayerRemove"
        Me.tsLayerRemove.Size = New System.Drawing.Size(23, 22)
        Me.tsLayerRemove.Text = "Remove selected layer"
        '
        'tsLayerClone
        '
        Me.tsLayerClone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsLayerClone.Image = Global._2Draw.My.Resources.Resources.layerClone
        Me.tsLayerClone.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsLayerClone.Name = "tsLayerClone"
        Me.tsLayerClone.Size = New System.Drawing.Size(23, 22)
        Me.tsLayerClone.Text = "Clone selected layer"
        '
        'MergeLayerDown
        '
        Me.MergeLayerDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MergeLayerDown.Image = Global._2Draw.My.Resources.Resources.LayerMergeDown
        Me.MergeLayerDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MergeLayerDown.Name = "MergeLayerDown"
        Me.MergeLayerDown.Size = New System.Drawing.Size(23, 22)
        Me.MergeLayerDown.Text = "Merge Layer Down"
        '
        'CreateLayerFromSelected
        '
        Me.CreateLayerFromSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CreateLayerFromSelected.Image = Global._2Draw.My.Resources.Resources.LayerFromSelectedItems
        Me.CreateLayerFromSelected.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CreateLayerFromSelected.Name = "CreateLayerFromSelected"
        Me.CreateLayerFromSelected.Size = New System.Drawing.Size(23, 22)
        Me.CreateLayerFromSelected.Text = "Create Layer From Selected"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'tsLayerUP
        '
        Me.tsLayerUP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsLayerUP.Image = Global._2Draw.My.Resources.Resources.LayerUp
        Me.tsLayerUP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsLayerUP.Name = "tsLayerUP"
        Me.tsLayerUP.Size = New System.Drawing.Size(23, 22)
        Me.tsLayerUP.Text = "Bump selected layer up"
        '
        'tsLayerDown
        '
        Me.tsLayerDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsLayerDown.Image = Global._2Draw.My.Resources.Resources.LayerDown
        Me.tsLayerDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsLayerDown.Name = "tsLayerDown"
        Me.tsLayerDown.Size = New System.Drawing.Size(23, 22)
        Me.tsLayerDown.Text = "Bump selected layer down"
        '
        'tsLayerRename
        '
        Me.tsLayerRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsLayerRename.Image = Global._2Draw.My.Resources.Resources.layerRename
        Me.tsLayerRename.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsLayerRename.Name = "tsLayerRename"
        Me.tsLayerRename.Size = New System.Drawing.Size(23, 22)
        Me.tsLayerRename.Text = "Rename selected layer"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(777, 25)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(10, 566)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'pnlPageZoom
        '
        Me.pnlPageZoom.Controls.Add(Me.lblLocation)
        Me.pnlPageZoom.Controls.Add(Me.lblZoom)
        Me.pnlPageZoom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPageZoom.Location = New System.Drawing.Point(36, 566)
        Me.pnlPageZoom.Name = "pnlPageZoom"
        Me.pnlPageZoom.Size = New System.Drawing.Size(741, 25)
        Me.pnlPageZoom.TabIndex = 5
        '
        'lblLocation
        '
        Me.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLocation.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblLocation.Location = New System.Drawing.Point(102, 2)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(162, 23)
        Me.lblLocation.TabIndex = 1
        '
        'lblZoom
        '
        Me.lblZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZoom.Location = New System.Drawing.Point(0, 2)
        Me.lblZoom.Name = "lblZoom"
        Me.lblZoom.Size = New System.Drawing.Size(100, 23)
        Me.lblZoom.TabIndex = 0
        '
        'tmrAutoSave
        '
        Me.tmrAutoSave.Enabled = True
        Me.tmrAutoSave.Interval = 300000
        '
        'mainTabs
        '
        Me.mainTabs.AllowCloseOfTabs = False
        Me.mainTabs.AllowDrop = True
        Me.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainTabs.EmptyBackColor = System.Drawing.Color.DarkGray
        Me.mainTabs.Location = New System.Drawing.Point(36, 25)
        Me.mainTabs.Menu = Nothing
        Me.mainTabs.MenuIcon = Nothing
        Me.mainTabs.Name = "mainTabs"
        Me.mainTabs.Padding = New System.Windows.Forms.Padding(0, 25, 0, 0)
        Me.mainTabs.SelectedBackColor = System.Drawing.Color.White
        Me.mainTabs.SelectedForeColor = System.Drawing.Color.Black
        Me.mainTabs.SelectedTab = Nothing
        Me.mainTabs.Size = New System.Drawing.Size(741, 541)
        Me.mainTabs.TabIndex = 4
        Me.mainTabs.TabRclickMenu = Me.cmsPageTabMenu
        '
        'cmsPageTabMenu
        '
        Me.cmsPageTabMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RenamePageToolStripMenuItem, Me.ClonePageToolStripMenuItem, Me.ExportToNewProjectToolStripMenuItem, Me.ToolStripSeparator8, Me.DeleteThisPageToolStripMenuItem})
        Me.cmsPageTabMenu.Name = "cmsPageTabMenu"
        Me.cmsPageTabMenu.Size = New System.Drawing.Size(189, 98)
        '
        'RenamePageToolStripMenuItem
        '
        Me.RenamePageToolStripMenuItem.Image = Global._2Draw.My.Resources.Resources.PageRename
        Me.RenamePageToolStripMenuItem.Name = "RenamePageToolStripMenuItem"
        Me.RenamePageToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.RenamePageToolStripMenuItem.Text = "Rename Page"
        '
        'ClonePageToolStripMenuItem
        '
        Me.ClonePageToolStripMenuItem.Image = Global._2Draw.My.Resources.Resources.ToolClone
        Me.ClonePageToolStripMenuItem.Name = "ClonePageToolStripMenuItem"
        Me.ClonePageToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ClonePageToolStripMenuItem.Text = "Clone Page"
        '
        'ExportToNewProjectToolStripMenuItem
        '
        Me.ExportToNewProjectToolStripMenuItem.Image = Global._2Draw.My.Resources.Resources.PageExport
        Me.ExportToNewProjectToolStripMenuItem.Name = "ExportToNewProjectToolStripMenuItem"
        Me.ExportToNewProjectToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ExportToNewProjectToolStripMenuItem.Text = "Export to New Project"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(185, 6)
        '
        'DeleteThisPageToolStripMenuItem
        '
        Me.DeleteThisPageToolStripMenuItem.Image = Global._2Draw.My.Resources.Resources.PageRemove
        Me.DeleteThisPageToolStripMenuItem.Name = "DeleteThisPageToolStripMenuItem"
        Me.DeleteThisPageToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.DeleteThisPageToolStripMenuItem.Text = "Delete This Page"
        '
        'btnAbout
        '
        Me.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(23, 22)
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 591)
        Me.Controls.Add(Me.mainTabs)
        Me.Controls.Add(Me.pnlPageZoom)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.PropPanel)
        Me.Controls.Add(Me.tsTools)
        Me.Controls.Add(Me.tsMain)
        Me.DoubleBuffered = True
        Me.Icon = Global._2Draw.My.Resources.Resources.app
        Me.KeyPreview = True
        Me.Name = "Main"
        Me.Text = "2Draw"
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.tsTools.ResumeLayout(False)
        Me.tsTools.PerformLayout()
        Me.PropPanel.ResumeLayout(False)
        Me.ccSymbols.ResumeLayout(False)
        Me.ccSymbols.PerformLayout()
        Me.symboltoolstrip.ResumeLayout(False)
        Me.symboltoolstrip.PerformLayout()
        Me.ccAlignment.ResumeLayout(False)
        Me.ccFont.ResumeLayout(False)
        Me.ccShapeOptions.ResumeLayout(False)
        Me.ccLineOptions.ResumeLayout(False)
        Me.ccLayers.ResumeLayout(False)
        Me.ccLayers.PerformLayout()
        Me.tsLayers.ResumeLayout(False)
        Me.tsLayers.PerformLayout()
        Me.pnlPageZoom.ResumeLayout(False)
        Me.cmsPageTabMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tsMain As ToolStrip
    Friend WithEvents tsOpenFile As ToolStripButton
    Friend WithEvents tsSaveFile As ToolStripSplitButton
    Friend WithEvents tsSaveFileAs As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsTools As ToolStrip
    Friend WithEvents tsToolRec As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tsOptions As ToolStripButton
    Friend WithEvents tsToolCircle As ToolStripButton
    Friend WithEvents tsToolTriangle As ToolStripButton
    Friend WithEvents tsToolLine As ToolStripButton
    Friend WithEvents tstoolText As ToolStripButton
    Friend WithEvents tstoolImage As ToolStripButton
    Friend WithEvents tslblTools As ToolStripLabel
    Friend WithEvents PropPanel As Panel
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents mainTabs As CleanTab
    Friend WithEvents ccLayers As CleanContainer
    Friend WithEvents ccLineOptions As CleanContainer
    Friend WithEvents pnlPageZoom As Panel
    Friend WithEvents lblZoom As Label
    Friend WithEvents tsLayers As ToolStrip
    Friend WithEvents tsLayerAdd As ToolStripButton
    Friend WithEvents tsLayerRemove As ToolStripButton
    Friend WithEvents tsLayerClone As ToolStripButton
    Friend WithEvents tsLayerUP As ToolStripButton
    Friend WithEvents tsLayerDown As ToolStripButton
    Friend WithEvents tsLayerRename As ToolStripButton
    Friend WithEvents lbLayers As ListBox
    Friend WithEvents tmrAutoSave As Timer
    Friend WithEvents tsNewFile As ToolStripDropDownButton
    Friend WithEvents tsNewProject As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents AddNewPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteCurrentPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tsToolMouse As ToolStripButton
    Friend WithEvents ToolStripButton1 As ToolStripSeparator
    Friend WithEvents tsFlipHorz As ToolStripButton
    Friend WithEvents tsFlipVert As ToolStripButton
    Friend WithEvents tsToolPan As ToolStripButton
    Friend WithEvents ccShapeOptions As CleanContainer
    Friend WithEvents PropLineColor As ColorPickerCombobox
    Friend WithEvents PropLineWidth As LineWidth
    Friend WithEvents propLineStyle As LineStyled
    Friend WithEvents propShapeFill As ColorPickerCombobox
    Friend WithEvents ccFont As CleanContainer
    Friend WithEvents propFontNameSize As FontCombo
    Friend WithEvents propFontJust As FontJustified
    Friend WithEvents PropFontStyle As FontStyles
    Friend WithEvents ccAlignment As CleanContainer
    Friend WithEvents propAlignment As DispAlignment
    Friend WithEvents PropSizing As DispSizing
    Friend WithEvents propSpacing As DispSpacing
    Friend WithEvents ccSymbols As CleanContainer
    Friend WithEvents MergeLayerDown As ToolStripButton
    Friend WithEvents CreateLayerFromSelected As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents symboltoolstrip As ToolStrip
    Friend WithEvents tsAddNewSymbol As ToolStripButton
    Friend WithEvents tsSelectLibrary As ToolStripComboBox
    Friend WithEvents tsEditLibraries As ToolStripButton
    Friend WithEvents symLibList As LibraryView
    Friend WithEvents lblLocation As Label
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents tsToolRotation As ToolStripButton
    Friend WithEvents tsToolMoveHandle As ToolStripButton
    Friend WithEvents tsToolGrid As ToolStripButton
    Friend WithEvents tsToolCrossHairs As ToolStripButton
    Friend WithEvents tsToolSendBack As ToolStripButton
    Friend WithEvents tsBringForward As ToolStripButton
    Friend WithEvents tsToolCloneItem As ToolStripButton
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents tsToolGroup As ToolStripButton
    Friend WithEvents tsToolUngroup As ToolStripButton
    Friend WithEvents tsPrintFile As ToolStripButton
    Friend WithEvents tsToolNormalWidth As ToolStripButton
    Friend WithEvents lblMouseOverSymbol As Label
    Friend WithEvents tsImportPage As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents cmsPageTabMenu As ContextMenuStrip
    Friend WithEvents RenamePageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClonePageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportToNewProjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents DeleteThisPageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents propShapHatch As FillHatchingCombo
    Friend WithEvents btnAbout As ToolStripButton
End Class
