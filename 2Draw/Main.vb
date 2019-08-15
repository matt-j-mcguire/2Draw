


Imports _2Draw

Public Class Main

    ''' <summary>
    ''' used for closing the project, not the app!
    ''' </summary>
    Private _Flag_closing As Boolean

    Public Const NEWFILE As String = "_NEW_FILE"

#Region "App start/closed"

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load

        For Each s As String In My.Application.CommandLineArgs
            If File.Exists(s) Then
                OpenFile(s)
            ElseIf s = NEWFILE Then
                NewProject()
            Else
                'don't know what this is
            End If
        Next


        Globals.Load()
        myStartupLocation = Application.StartupPath
        PropLineColor.SelectedItem = Globals.current.LineColor
        PropLineWidth.LineWidth = Globals.current.LineWidth
        propLineStyle.StartCap = Globals.current.LineStartCap
        propLineStyle.LineStyle = Globals.current.LineDash
        propLineStyle.EndCap = Globals.current.LineEndCap
        propShapeFill.SelectedItem = Globals.current.FillColor
        propFontNameSize.SelectedItem = Globals.current.FontName
        propFontNameSize.selectedSize = Globals.current.FontSize
        propFontJust.fontAlignment = Globals.current.FontAlignment
        PropFontStyle.FontStyle = Globals.current.FontStyle
        LoadSymbols()
        tsToolRotation.Checked = True
        tsToolMoveHandle.Checked = True
        tsToolGrid.Checked = True
        tsToolCrossHairs.Checked = True

        Dim ex As New Threading.Thread(AddressOf CheckonUpdates)
        ex.Start() 'have to use a seperate thread to jump out and come back in to shut down the form (app)
    End Sub

    Private Sub Main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Me.CloseFile(False)
        Globals.Save()
        Globals.SymbolLibs.Save()
    End Sub

#End Region

#Region "Main tool Bar, open save close....menus"

    ''' <summary>
    ''' for creating a new Project
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsNewProject_Click(sender As Object, e As EventArgs) Handles tsNewProject.Click
        NewProject()
    End Sub

    Public Sub NewProject()
        If ThisProject IsNot Nothing Then
            Shell(Application.ExecutablePath & " """ & NEWFILE & """", AppWinStyle.NormalFocus)
        Else
            Dim t As New PageAdd()
            If t.ShowDialog(Me) = DialogResult.OK Then
                ThisProject = New Project(t.PageName, t.PageSize, t.PageLandscape)
                LoadCurrentProjectTabs()
            End If
        End If

    End Sub

    Private Sub AddNewPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewPageToolStripMenuItem.Click
        If ThisProject IsNot Nothing Then
            Dim t As New PageAdd()
            If t.ShowDialog(Me) = DialogResult.OK Then
                Dim p As Page = ThisProject.AddPage(t.PageName, t.PageSize, t.PageLandscape)
                mainTabs.AddTab(p)
                mainTabs.SelectedTab = p
                AddHandler p.ZoomChanged, AddressOf ZoomChanged
                AddHandler p.MouseMoved, AddressOf MouseMoved

            End If
        End If
    End Sub

    Private Sub DeleteCurrentPageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteCurrentPageToolStripMenuItem.Click, DeleteThisPageToolStripMenuItem.Click
        If ThisProject IsNot Nothing Then
            If ThisProject.Pages.Count > 1 Then
                If TypeOf mainTabs.SelectedTab Is Page Then
                    If MessageBox.Show("Delete the current page from the project?", "Remove Page?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Dim p As Page = mainTabs.SelectedTab
                        mainTabs.RemoveTab(p)
                        ThisProject.RemovePage(p)
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' opens an existing file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsOpenFile_Click(sender As Object, e As EventArgs) Handles tsOpenFile.Click
        Dim openinother As Boolean = False
        If ThisProject IsNot Nothing Then openinother = True


        Dim fl As New OpenFileDialog
        fl.AddExtension = True
        fl.Filter = $"2Draw Project Files|*{Project.FILE_EXTENTION}"
        If fl.ShowDialog(Me) = DialogResult.OK Then
            If openinother Then
                'opens file in a new instance of the program
                Shell(Application.ExecutablePath & " """ & fl.FileName & """", AppWinStyle.NormalFocus)
            Else
                'checks for a newer version in the temporary files
                ThisProject = New Project(fl.FileName)
                Dim str As String = ThisProject.DoesBackupExistAndisNewer
                If Not String.IsNullOrEmpty(str) Then
                    Dim fb As New foundBackup()
                    Dim dlr As DialogResult = fb.ShowDialog
                    If dlr = DialogResult.Yes Then 'restore
                        File.Copy(str, fl.FileName, True)
                        ThisProject = New Project(fl.FileName)
                    ElseIf dlr = DialogResult.Retry Then 'open both
                        Shell(Application.ExecutablePath & " """ & str & """", AppWinStyle.NormalFocus)
                    End If
                End If
                LoadCurrentProjectTabs()
            End If
        End If
    End Sub

    Private Sub OpenFile(file As String)
        If ThisProject IsNot Nothing Then
            Shell(Application.ExecutablePath & " """ & file & """", AppWinStyle.NormalFocus)
        Else
            ThisProject = New Project(file)
            LoadCurrentProjectTabs()
        End If

    End Sub

    ''' <summary>
    ''' loads up all the tabs and any of the other background process that may need to happen
    ''' </summary>
    Private Sub LoadCurrentProjectTabs()
        For Each p As Page In ThisProject.Pages
            Me.mainTabs.AddTab(p)
            AddHandler p.ZoomChanged, AddressOf ZoomChanged
            AddHandler p.MouseMoved, AddressOf MouseMoved
            If ThisProject.Settings.ND(Project.SETS_MAIN).Get(Project.SETS_SELECTEDPAGE, String.Empty) = p.Text Then
                mainTabs.SelectedTab = p
            End If
        Next
    End Sub

    Private Sub tsSaveFile_ButtonClick(sender As Object, e As EventArgs) Handles tsSaveFile.ButtonClick
        If ThisProject IsNot Nothing Then
            If ThisProject.FileName = "" Then
                Dim ex As New SaveFileDialog
                ex.AddExtension = True
                ex.Filter = $"2Draw Project Files|*{Project.FILE_EXTENTION}"
                If ex.ShowDialog(Me) = DialogResult.OK Then
                    ThisProject.Save(ex.FileName)
                End If
            Else
                ThisProject.Save()
            End If
        End If
    End Sub

    Private Sub tmrAutoSave_Tick(sender As Object, e As EventArgs) Handles tmrAutoSave.Tick
        If ThisProject IsNot Nothing Then
            ThisProject.SaveBackupTemp()
        End If
    End Sub

    Private Sub tsSaveFileAs_Click(sender As Object, e As EventArgs) Handles tsSaveFileAs.Click
        If ThisProject IsNot Nothing Then
            Dim ex As New SaveFileDialog
            ex.AddExtension = True
            ex.Filter = $"2Draw Project Files|*{Project.FILE_EXTENTION}"
            If ex.ShowDialog(Me) = DialogResult.OK Then
                ThisProject.Save(ex.FileName)
            End If
        End If
    End Sub

    'Private Sub tsCloseFile_Click(sender As Object, e As EventArgs) Handles tsCloseFile.Click
    '    CloseFile(False)
    'End Sub

    Public Sub CloseFile(ignoreSave As Boolean)
        _Flag_closing = True
        If ThisProject IsNot Nothing Then
            If Not ignoreSave Then
                If ThisProject.DirtyFlag Then
                    Dim ret As DialogResult = MessageBox.Show("Do you wish to Save this current project before closing?", "Save?", MessageBoxButtons.YesNo)
                    If ret = DialogResult.Yes Then
                        tsSaveFile_ButtonClick(tsSaveFile, New EventArgs)
                    End If
                End If
            End If


            For Each t As CleanTab.Tab In mainTabs.Items
                If TypeOf t Is Page Then
                    RemoveHandler DirectCast(t, Page).ZoomChanged, AddressOf ZoomChanged
                    RemoveHandler DirectCast(t, Page).MouseMoved, AddressOf MouseMoved
                End If
            Next
            mainTabs.CloseAll()
            ThisProject = Nothing
        End If
        _Flag_closing = False
    End Sub

    ''' <summary>
    ''' shows the options dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsOptions_Click(sender As Object, e As EventArgs) Handles tsOptions.Click
        Dim ex As New AppOptions
        ex.ShowDialog(Me)
    End Sub

    Private Sub MouseMoved(sender As Object, pt As PointF)
        If TypeOf sender Is Page Then
            lblLocation.Text = $"Location: {CInt(pt.X)},{CInt(pt.Y)}"
            lblLocation.Refresh()
        End If

    End Sub

    Private Sub ZoomChanged(sender As Object, e As EventArgs)
        If TypeOf sender Is Page Then
            Dim t As Page = sender
            lblZoom.Text = $"Zoom:{CInt(t.ZoomPerecentage * 100)}%"
            lblZoom.Refresh()
        Else
            lblZoom.Text = ""
        End If

    End Sub


    Private Sub RenamePageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenamePageToolStripMenuItem.Click
        If ThisProject IsNot Nothing Then
            If TypeOf mainTabs.SelectedTab Is Page Then
                With DirectCast(mainTabs.SelectedTab, Page)
                    Dim st As String = InputBox("Rename page", "Rename", .Text)
                    If Not String.IsNullOrEmpty(st) Then
                        .Text = st
                        mainTabs.Invalidate()
                    End If
                End With
            End If
        End If
    End Sub


    Private Sub ClonePageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClonePageToolStripMenuItem.Click
        If ThisProject IsNot Nothing Then
            If TypeOf mainTabs.SelectedTab Is Page Then
                Dim p As Page = DirectCast(mainTabs.SelectedTab, Page)
                Dim np As Page = p.Clone

                np.Text &= " Cloned"
                ThisProject.Pages.Add(np)
                Me.mainTabs.AddTab(np)
                AddHandler np.ZoomChanged, AddressOf ZoomChanged
                AddHandler np.MouseMoved, AddressOf MouseMoved
                mainTabs.SelectedTab = np
            End If
        End If
    End Sub

    Private Sub tsImportPage_Click(sender As Object, e As EventArgs) Handles tsImportPage.Click
        If ThisProject IsNot Nothing Then
            Dim ip As New ImportPage
            If ip.ShowDialog = DialogResult.OK Then
                Dim pro As Project = Nothing
                Dim p() As Page = Nothing
                ip.GetSelectedProjectPages(pro, p)

                For pi As Integer = 0 To p.Length - 1
                    Dim res() As String = p(pi).GetResourcesNames
                    For i As Integer = 0 To res.Length - 1
                        If ThisProject.GetPicByName(res(i)) Is Nothing Then
                            Dim xp As Project.XPic = pro.GetPicByName(res(i))
                            ThisProject.Images.Add(xp.clone)
                        End If
                    Next

                    Dim np As Page = p(pi).Clone
                    ThisProject.Pages.Add(np)
                    Me.mainTabs.AddTab(np)
                    AddHandler np.ZoomChanged, AddressOf ZoomChanged
                    AddHandler np.MouseMoved, AddressOf MouseMoved
                Next
            End If
        End If
    End Sub

    Private Sub ExportToNewProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToNewProjectToolStripMenuItem.Click
        If ThisProject IsNot Nothing Then
            If TypeOf mainTabs.SelectedTab Is Page Then
                Dim sv As New SaveFileDialog
                sv.Filter = $"*{Project.FILE_EXTENTION}|*{Project.FILE_EXTENTION}"
                If sv.ShowDialog = DialogResult.OK Then
                    Dim fl As String = sv.FileName
                    Dim p As Page = DirectCast(mainTabs.SelectedTab, Page).Clone
                    Dim x As New Project(fl, p)
                    Dim res() As String = p.GetResourcesNames
                    For i As Integer = 0 To res.Length - 1
                        Dim pic As Project.XPic = ThisProject.GetPicByName(res(i))
                        If pic IsNot Nothing Then
                            x.Images.Add(pic.clone)
                        End If
                    Next
                    x.Save()
                    Shell(Application.ExecutablePath & " """ & fl & """", AppWinStyle.NormalFocus)
                End If
            End If
        End If
    End Sub


#End Region



#Region "keyboard"

    Private Sub Main_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'saved in globals
        CtrlKey = e.Control
        ShiftKey = e.Shift
   '     Debug.WriteLine("dn")
    End Sub

    Private Sub Main_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub Main_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Dim p As Page = GetActivePage()
        If p IsNot Nothing Then
            If e.KeyCode = Keys.Delete Then
                p.DeleteItem()
            ElseIf CtrlKey Then
                Select Case e.KeyCode
                    Case Keys.C
                        p.CopyItem()
                    Case Keys.X
                        p.CutItem()
                    Case Keys.V
                        p.PasteItem()
                    Case Keys.S
                        tsSaveFile_ButtonClick(tsSaveFile, New EventArgs)
                    Case Keys.P
                        tsPrintFile_Click(tsPrintFile, New EventArgs)
                    Case Keys.D1
                        tsToolMouse_Click(tsToolMouse, New EventArgs)
                    Case Keys.D2
                        tsToolMouse_Click(tsToolPan, New EventArgs)
                    Case Keys.D3
                        tsToolMouse_Click(tsToolRec, New EventArgs)
                    Case Keys.D4
                        tsToolMouse_Click(tsToolCircle, New EventArgs)
                    Case Keys.D5
                        tsToolMouse_Click(tsToolTriangle, New EventArgs)
                    Case Keys.D6
                        tsToolMouse_Click(tsToolLine, New EventArgs)
                    Case Keys.D7
                        tsToolMouse_Click(tstoolText, New EventArgs)
                    Case Keys.D8
                        tsToolMouse_Click(tstoolImage, New EventArgs)
                    Case Keys.D9
                        'reserved for now
                    Case Keys.Left, Keys.Right
                        tsFlipHorz_Click(tsFlipHorz, New EventArgs)
                    Case Keys.Up, Keys.Down
                        tsFlipVert_Click(tsFlipVert, New EventArgs)
                    Case Keys.PageUp
                        tsBringForward_Click(tsBringForward, New EventArgs)
                    Case Keys.PageDown
                        tsToolSendBack_Click(tsToolSendBack, New EventArgs)
                    Case Keys.Q
                        tsToolCloneItem_Click(tsToolCloneItem, New EventArgs)
                    Case Keys.G
                        tsToolGroup_Click(tsToolGroup, New EventArgs)
                    Case Keys.U
                        tsToolUngroup_Click(tsToolUngroup, New EventArgs)
                End Select

            End If
        End If

        'saved in globals
        CtrlKey = e.Control
        ShiftKey = e.Shift
        If e.KeyCode = Keys.Escape Then CapturedEscapeButton()

        'stop




    End Sub

#End Region

#Region "Layers"

    ''' <summary>
    ''' load up the layers for this page
    ''' </summary>
    ''' <param name="t"></param>
    ''' <param name="e"></param>
    Private Sub mainTabs_SelectedTabChanged(t As CleanTab.Tab, e As EventArgs) Handles mainTabs.SelectedTabChanged
        lbLayers.Items.Clear()

        If t IsNot Nothing AndAlso TypeOf t Is Page AndAlso Not _Flag_closing Then
            'set this as the last active tab page
            ThisProject.Settings.ND(Project.SETS_MAIN).Set(Project.SETS_SELECTEDPAGE, t.Text)
            Dim p As Page = t
            lbLayers.Items.AddRange(p.Layers.ToArray)
            lbLayers.SelectedIndex = lbLayers.Items.Count - 1
        End If
        ZoomChanged(t, New EventArgs)

    End Sub

    ''' <summary>
    ''' draws the layers in the box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lbLayers_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lbLayers.DrawItem
        If e.Index > -1 Then
            Dim f, r As Color
            If e.State And DrawItemState.Selected Then
                r = titleBack
                f = titleFore
            Else
                r = innerBack
                f = innerFore
            End If

            Dim x As Layer = lbLayers.Items(e.Index)
            Using fc As New SolidBrush(f), bc As New SolidBrush(r), strf As New StringFormat
                e.Graphics.FillRectangle(bc, e.Bounds)
                e.Graphics.DrawImage(If(x.Visible, My.Resources.Checked, My.Resources.CheckedUn), New Point(e.Bounds.X + 2, e.Bounds.Y + 1))
                Dim rec As New Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height)
                strf.LineAlignment = StringAlignment.Center
                e.Graphics.DrawString(x.Name, Me.Font, fc, rec, strf)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' changing the index of the slected layer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lbLayers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbLayers.SelectedIndexChanged
        lbLayers.Invalidate()

        Dim inx As Integer = lbLayers.SelectedIndex
        If inx > -1 Then
            For i As Integer = 0 To lbLayers.Items.Count - 1
                DirectCast(lbLayers.Items(i), Layer).Selected = If(inx = i, True, False)
            Next
            DirectCast(lbLayers.SelectedItem, Layer).ParentPage.CancelGrabandRotationHandles()
        End If

    End Sub

    ''' <summary>
    ''' changes the visible prop of the layer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lbLayers_MouseUp(sender As Object, e As MouseEventArgs) Handles lbLayers.MouseUp
        For i As Integer = 0 To lbLayers.Items.Count - 1
            Dim r As Rectangle = lbLayers.GetItemRectangle(i)
            r.Width = 18
            If r.Contains(e.X, e.Y) Then
                Dim l As Layer = DirectCast(lbLayers.Items(i), Layer)
                l.Visible = Not l.Visible
                l.ParentPage.Invalidate()
                lbLayers.Invalidate()
            End If
        Next

    End Sub

    ''' <summary>
    ''' adds a new layer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsLayerAdd_Click(sender As Object, e As EventArgs) Handles tsLayerAdd.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim p As Page = mainTabs.SelectedTab
            Dim t As String = InputBox("Name for new layer", "Add a layer", $"Layer {lbLayers.Items.Count + 1}")
            If Not String.IsNullOrEmpty(t) Then
                Dim l As New Layer(p)
                l.Name = t
                p.Layers.Add(l)
                lbLayers.Items.Add(l)
                lbLayers.SelectedItem = l
            End If
        End If
    End Sub

    ''' <summary>
    ''' removes the currently selected layer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsLayerRemove_Click(sender As Object, e As EventArgs) Handles tsLayerRemove.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim p As Page = mainTabs.SelectedTab
            If p.Layers.Count > 1 Then
                Dim l As Layer = lbLayers.SelectedItem
                Dim index As Integer = lbLayers.SelectedIndex
                If MessageBox.Show($"Delete {l.Name} from this page?", "Delete?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    lbLayers.Items.Remove(l)
                    p.Layers.Remove(l)
                    If index = 0 Then
                        lbLayers.SelectedIndex = 0
                    Else
                        lbLayers.SelectedIndex = index - 1
                    End If

                End If
            End If

        End If
    End Sub


    Private Sub MergeLayerDown_Click(sender As Object, e As EventArgs) Handles MergeLayerDown.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim p As Page = mainTabs.SelectedTab

            Dim index As Integer = lbLayers.SelectedIndex
            If index > -1 AndAlso index < lbLayers.Items.Count - 1 Then
                Dim l As Layer = lbLayers.SelectedItem
                Dim items() As Type_2D = l.items.ToArray

                Dim nl As Layer = lbLayers.Items(index + 1)
                nl.items.AddRange(items)
                p.Layers.Remove(l)
                lbLayers.Items.Remove(l)
                lbLayers.SelectedItem = nl
            End If
        End If
    End Sub

    Private Sub CreateLayerFromSelected_Click(sender As Object, e As EventArgs) Handles CreateLayerFromSelected.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim p As Page = mainTabs.SelectedTab

            Dim l As Layer = lbLayers.SelectedItem
            If l IsNot Nothing Then
                Dim items() As Type_2D = l.SelectedItems
                If items.Length > 0 Then
                    For i As Integer = 0 To items.Length - 1
                        l.items.Remove(items(i))
                    Next

                    Dim nl As New Layer(p)
                    nl.Name = l.Name & " (selected)"
                    nl.items.AddRange(items)
                    p.Layers.Add(nl)
                    lbLayers.Items.Insert(lbLayers.SelectedIndex, nl)
                    lbLayers.SelectedItem = nl
                End If

            End If
        End If
    End Sub

    Private Sub tsLayerClone_Click(sender As Object, e As EventArgs) Handles tsLayerClone.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim p As Page = mainTabs.SelectedTab
            Dim oldl As Layer = lbLayers.SelectedItem

            Dim t As String = InputBox("Name for cloned layer", "Add a layer", $"{oldl.Name} copy")
            If Not String.IsNullOrEmpty(t) Then
                Dim l As New Layer(oldl.CloneNode(p.Doc), p)
                l.Name = t
                p.Layers.Add(l)
                lbLayers.Items.Add(l)
            End If

        End If
    End Sub

    Private Sub tsLayerUP_Click(sender As Object, e As EventArgs) Handles tsLayerUP.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim p As Page = mainTabs.SelectedTab

            Dim index As Integer = lbLayers.SelectedIndex
            If index > 0 Then
                Dim l As Layer = lbLayers.SelectedItem
                lbLayers.Items(index) = lbLayers.Items(index - 1)
                p.Layers(index) = p.Layers(index - 1)
                lbLayers.Items(index - 1) = l
                p.Layers(index - 1) = l
                lbLayers.SelectedIndex = index - 1
            End If
        End If
    End Sub

    Private Sub tsLayerDown_Click(sender As Object, e As EventArgs) Handles tsLayerDown.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim p As Page = mainTabs.SelectedTab

            Dim index As Integer = lbLayers.SelectedIndex
            If index < lbLayers.Items.Count - 1 Then
                Dim l As Layer = lbLayers.SelectedItem
                lbLayers.Items(index) = lbLayers.Items(index + 1)
                p.Layers(index) = p.Layers(index + 1)
                lbLayers.Items(index + 1) = l
                p.Layers(index + 1) = l
                lbLayers.SelectedIndex = index + 1
            End If

        End If
    End Sub

    Private Sub tsLayerRename_Click(sender As Object, e As EventArgs) Handles tsLayerRename.Click
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim l As Layer = lbLayers.SelectedItem
            Dim t As String = InputBox("New Name for selected layer", "Rename", l.Name)
            If Not String.IsNullOrEmpty(t) Then l.Name = t
            lbLayers.Invalidate()
        End If
    End Sub

#End Region

#Region "drawing Tools"

    Private Const ISTOOL As String = "TOOL"

    Private Sub tsToolMouse_Click(sender As Object, e As EventArgs) Handles tsToolMouse.Click,
        tsToolRec.Click, tsToolCircle.Click, tsToolTriangle.Click,
        tsToolLine.Click, tstoolText.Click, tstoolImage.Click, tsToolPan.Click
        If ThisProject IsNot Nothing Then

            For Each t As ToolStripItem In tsTools.Items
                If TypeOf t Is ToolStripButton Then
                    With DirectCast(t, ToolStripButton)
                        If .Tag = ISTOOL Then .Checked = sender Is t
                    End With
                End If
            Next

            If sender Is tsToolMouse Then
                CurrDrawingTool = SelectedDrawingTool.Mouse
            ElseIf sender Is tsToolPan Then
                CurrDrawingTool = SelectedDrawingTool.Pan
            ElseIf sender Is tsToolRec Then
                CurrDrawingTool = SelectedDrawingTool.Rectangle
            ElseIf sender Is tsToolCircle Then
                CurrDrawingTool = SelectedDrawingTool.Circle
            ElseIf sender Is tsToolTriangle Then
                CurrDrawingTool = SelectedDrawingTool.Triangle
            ElseIf sender Is tsToolLine Then
                CurrDrawingTool = SelectedDrawingTool.Line
            ElseIf sender Is tstoolText Then
                CurrDrawingTool = SelectedDrawingTool.Text
            ElseIf sender Is tstoolImage Then
                CurrDrawingTool = SelectedDrawingTool.Image
            End If
        End If

        If TypeOf mainTabs.SelectedTab Is Page AndAlso CurrDrawingTool > SelectedDrawingTool.Pan Then
            DirectCast(mainTabs.SelectedTab, Page).CancelGrabandRotationHandles()
        End If

    End Sub

    Private Sub CapturedEscapeButton()
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                DirectCast(mtb, Page).ClearSelected()
            End If
        End If
        tsToolMouse_Click(tsToolMouse, New EventArgs)
    End Sub

    Private Sub tsFlipHorz_Click(sender As Object, e As EventArgs) Handles tsFlipHorz.Click
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                DirectCast(mtb, Page).FlipSelectedHorizonal()
            End If
        End If
    End Sub

    Private Sub tsFlipVert_Click(sender As Object, e As EventArgs) Handles tsFlipVert.Click
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                DirectCast(mtb, Page).FlipSelectedVertical()
            End If
        End If
    End Sub

    Private Sub tsToolNormalWidth_Click(sender As Object, e As EventArgs) Handles tsToolNormalWidth.CheckedChanged
        ShowNormalLineWidth = tsToolNormalWidth.Checked

        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            mtb.Refresh()
        End If
    End Sub

    Private Sub tsToolRotation_Click(sender As Object, e As EventArgs) Handles tsToolRotation.CheckedChanged
        ShowToolRotation = tsToolRotation.Checked

        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            mtb.Refresh()
        End If
    End Sub

    Private Sub tsToolMoveHandle_Click(sender As Object, e As EventArgs) Handles tsToolMoveHandle.CheckedChanged
        ShowToolMoveHandle = tsToolMoveHandle.Checked
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            mtb.Refresh()
        End If
    End Sub

    Private Sub tsToolGrid_Click(sender As Object, e As EventArgs) Handles tsToolGrid.CheckedChanged
        ShowToolGrid = tsToolGrid.Checked
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            mtb.Refresh()
        End If
    End Sub

    Private Sub tsToolCrossHairs_Click(sender As Object, e As EventArgs) Handles tsToolCrossHairs.CheckedChanged
        ShowCrossHairs = tsToolCrossHairs.Checked
        If ThisProject IsNot Nothing Then
            For Each mtb As CleanTab.Tab In mainTabs.Items
                If TypeOf mtb Is Page Then
                    If ShowCrossHairs Then
                        mtb.Cursor = BlankCursor
                    Else
                        mtb.Cursor = Cursors.Default
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub tsToolSendBack_Click(sender As Object, e As EventArgs) Handles tsToolSendBack.Click
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                DirectCast(mtb, Page).SendItemBack()
                mtb.Refresh()
            End If
        End If
    End Sub

    Private Sub tsBringForward_Click(sender As Object, e As EventArgs) Handles tsBringForward.Click
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                DirectCast(mtb, Page).BringItemForward()
                mtb.Refresh()
            End If
        End If
    End Sub

    Private Sub tsToolCloneItem_Click(sender As Object, e As EventArgs) Handles tsToolCloneItem.Click
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                DirectCast(mtb, Page).CloneItem()
                mtb.Refresh()
            End If
        End If
    End Sub

#End Region

#Region "Drawing Props"

    Private Sub PropLineColor_ValueChanged(sender As Object, e As EventArgs) Handles PropLineColor.ValueChanged
        current.LineColor = PropLineColor.SelectedItem

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.LineColor = current.LineColor
        Next
        ForcePageRedraw()
    End Sub

    Private Sub PropLineWidth_ValueChanged(sender As Object, e As EventArgs) Handles PropLineWidth.ValueChanged
        current.LineWidth = PropLineWidth.LineWidth

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.LineWidth = current.LineWidth
        Next
        ForcePageRedraw()
    End Sub

    Private Sub propLineStyle_startchanged(sender As Object, e As EventArgs) Handles propLineStyle.StartChanged
        current.LineStartCap = propLineStyle.StartCap

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.LineStartCap = current.LineStartCap
        Next
        ForcePageRedraw()
    End Sub

    Private Sub propLineStyle_endchanged(sender As Object, e As EventArgs) Handles propLineStyle.EndChanged
        current.LineEndCap = propLineStyle.EndCap

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.LineEndCap = current.LineEndCap
        Next
        ForcePageRedraw()
    End Sub

    Private Sub propLineStyle_linechanged(sender As Object, e As EventArgs) Handles propLineStyle.LineChanged
        current.LineDash = propLineStyle.LineStyle

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.LineDash = current.LineDash
        Next
        ForcePageRedraw()
    End Sub

    Private Sub propShapeFill_ValueChanged(sender As Object, e As EventArgs) Handles propShapeFill.ValueChanged
        current.FillColor = propShapeFill.SelectedItem
        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.FillColor = current.FillColor
        Next
        ForcePageRedraw()
    End Sub

    Private Sub FillHatchingCombo1_valuchanged(sender As Object, e As EventArgs) Handles propShapHatch.ValueChanged
        current.FillHatch = If(propShapHatch.isHatch, propShapHatch.HatchValue, -1)
        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.FillHatch = current.FillHatch
        Next
        ForcePageRedraw()
    End Sub

    Private Sub propFontNameSize_FontNameChanged(sender As Object, e As EventArgs) Handles propFontNameSize.FontNameChanged
        current.FontName = propFontNameSize.SelectedItem

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.FontName = current.FontName
        Next
        ForcePageRedraw()
    End Sub

    Private Sub propFontNameSize_FontSizeChanged(sender As Object, e As EventArgs) Handles propFontNameSize.FontSizeChanged
        current.FontSize = propFontNameSize.selectedSize

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.FontSize = current.FontSize
        Next
        ForcePageRedraw()
    End Sub

    Private Sub PropFontStyle_FontSyleChanged(sender As Object, e As EventArgs) Handles PropFontStyle.FontSyleChanged
        current.FontStyle = PropFontStyle.FontStyle

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.FontStyle = current.FontStyle
        Next
        ForcePageRedraw()
    End Sub

    Private Sub propFontJust_FontAlignmentChanged(sender As Object, e As EventArgs) Handles propFontJust.FontAlignmentChanged
        current.FontAlignment = propFontJust.fontAlignment

        Dim itms() As Type_2D = GetSelectedItems()
        For i As Integer = 0 To itms.Length - 1
            itms(i).DrawingOpts.FontAlignment = current.FontAlignment
        Next
        ForcePageRedraw()
    End Sub


#End Region

    ''' <summary>
    ''' trys to get the active layer from active page.
    ''' </summary>
    ''' <returns>returns nothing if not found</returns>
    Private Function GetActiveLayerFromActivePage() As Layer
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Dim l As Layer = lbLayers.SelectedItem
            Return l
        Else
            Return Nothing
        End If
    End Function

    Private Function GetActivePage() As Page
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            Return mainTabs.SelectedTab
        Else
            Return Nothing
        End If
    End Function

    Private Sub ForcePageRedraw()
        If ThisProject IsNot Nothing AndAlso TypeOf mainTabs.SelectedTab Is Page Then
            mainTabs.SelectedTab.Invalidate()
        End If
    End Sub

    ''' <summary>
    ''' trys to get the current screen items
    ''' </summary>
    ''' <returns></returns>
    Private Function GetSelectedItems() As Type_2D()
        Dim ret(-1) As Type_2D
        Dim l As Layer = GetActiveLayerFromActivePage()
        If l IsNot Nothing Then
            ret = l.SelectedItems
        End If
        Return ret
    End Function

#Region "Alignment and Spacing and Size controls"

    Private Sub propSpacing_SpacingClicked(sender As Object, e As DisplaySpacing) Handles propSpacing.SpacingClicked
        Dim l As Layer = GetActiveLayerFromActivePage()
        If l IsNot Nothing Then
            l.SetSelectedSpacing(e)
        End If
    End Sub

    Private Sub propAlignment_AlignmentClicked(sender As Object, e As DisplayAlignment) Handles propAlignment.AlignmentClicked
        Dim l As Layer = GetActiveLayerFromActivePage()
        If l IsNot Nothing Then
            l.setSelectedAlignment(e)
        End If
    End Sub

    Private Sub PropSizing_SizingClicked(sender As Object, e As DisplaySizing) Handles PropSizing.SizingClicked
        Dim l As Layer = GetActiveLayerFromActivePage()
        If l IsNot Nothing Then
            l.setSelectedSizing(e)
        End If
    End Sub

#End Region

#Region "Symbols"

    Private Sub LoadSymbols()
        tsSelectLibrary.Items.AddRange(SymbolLibs.GetLibraryNames)
        tsSelectLibrary.SelectedIndex = 0
    End Sub

    Private Sub tsSelectLibrary_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tsSelectLibrary.SelectedIndexChanged
        'clear the panel and re-add the items to the list
        symLibList.clear()
        If tsSelectLibrary.SelectedIndex > -1 Then
            For i As Integer = 0 To SymbolLibs.Librarys.Count - 1
                If SymbolLibs.Librarys(i).Name = CStr(tsSelectLibrary.SelectedItem) Then
                    symLibList.addRange(SymbolLibs.Librarys(i).Items.ToArray)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub tsAddNewSymbol_Click(sender As Object, e As EventArgs) Handles tsAddNewSymbol.Click
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                Dim t() As Type_2D = DirectCast(mtb, Page).SelectedLayer.SelectedItems
                If t.Length > 0 Then
                    Dim name As String = InputBox("Name for new symbol", "New Symbol", "")
                    If Not String.IsNullOrEmpty(name) Then

                        Dim libr As SymbolLib.Library = SymbolLibs.LibraryByName(CStr(tsSelectLibrary.SelectedItem))
                        If t.Length = 1 AndAlso TypeOf t(0) Is Symbol_2D Then
                            'there was only one item and it was already a symbol
                            'so just add it to the list
                            Dim ex As Symbol_2D = t(0).Clone
                            ex.OffsetZero()
                            libr.Items.Add(New SymbolLib.Library.Symbol(ex, name))
                        Else
                            'multiple items are added together to form one symbol
                            'and then added it to the library
                            Dim b As RectangleF = DirectCast(mtb, Page).GetBoundingRecFromSelected
                            Dim ex As New Symbol_2D()
                            ex.AddItems(t, b)
                            libr.Items.Add(New SymbolLib.Library.Symbol(ex, name))
                            tsSelectLibrary_SelectedIndexChanged(tsSelectLibrary, New EventArgs)
                        End If
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub tsEditLibraries_Click(sender As Object, e As EventArgs) Handles tsEditLibraries.Click
        Dim ex As New SymbolManage
        ex.ShowDialog(Me)
        tsSelectLibrary.Items.Clear()
        tsSelectLibrary.Items.AddRange(SymbolLibs.GetLibraryNames)
        tsSelectLibrary.SelectedIndex = 0
    End Sub

    Private Sub symLibList_MouseOverItem(sender As Object, item As SymbolLib.Library.Symbol) Handles symLibList.MouseOverItem
        Dim rec As RectangleF = item.item.GetOrigionalRec
        lblMouseOverSymbol.Text = $"{item.Name} [{rec.Width} x {rec.Height}] {item.item.GetItems.Count} sub items"
    End Sub







#End Region

#Region "Grouping"

    Private Sub tsToolGroup_Click(sender As Object, e As EventArgs) Handles tsToolGroup.Click
        If ThisProject IsNot Nothing Then
            Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
            If TypeOf mtb Is Page Then
                DirectCast(mtb, Page).GroupSelected()
            End If
        End If
    End Sub

    Private Sub tsToolUngroup_Click(sender As Object, e As EventArgs) Handles tsToolUngroup.Click
        Dim mtb As CleanTab.Tab = mainTabs.SelectedTab
        If TypeOf mtb Is Page Then
            DirectCast(mtb, Page).UnGroupSelected()
        End If
    End Sub

#End Region

#Region "sync program and data"

    Private Sub CheckonUpdates()
        Dim d As New DirectoryInfo(ProgramUpdatePath)
        If d.Exists Then
            Dim fl As New FileInfo(d.FullName & "\" & Application.ProductName & ".exe")
            If fl.Exists Then
                Dim curr As New FileInfo(Application.ExecutablePath)
                If fl.LastWriteTime > curr.LastWriteTime Then
                    LaunchSync()
                End If
            End If
        End If
    End Sub

    Delegate Sub LaunchSyncCallBack()
    Private Sub LaunchSync()
        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        Try
            If Me.InvokeRequired Then
                Dim d As New LaunchSyncCallBack(AddressOf LaunchSync)
                Me.Invoke(d, New Object() {})
            Else
                checkUpdates()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub checkUpdates()
        Dim wasupdated As Boolean = False
        Dim curr As New FileInfo(Application.ExecutablePath)
        Dim org As String = curr.FullName

        'do the program update first
        Dim d As New DirectoryInfo(ProgramUpdatePath)
        If d.Exists Then
            Dim fl As New FileInfo(d.FullName & "\" & Application.ProductName & ".exe")
            If fl.Exists Then

                If fl.LastWriteTime > curr.LastWriteTime Then

                    curr.MoveTo(curr.FullName.Replace(Application.ProductName & curr.Extension, "old.exe"))
                    fl.CopyTo(org)
                    wasupdated = True
                End If
            Else
                MessageBox.Show(Application.ProductName & " Was not found at this location", "Khan!!!!")
            End If
        Else
            MessageBox.Show("Unable to update the program, could not find the folder", "you poor bastard")
        End If

        If wasupdated Then
            MessageBox.Show("This program was updated.", "Ooh! Shiny")
            Shell(org, AppWinStyle.NormalFocus)
            Me.Close()
        End If

    End Sub

#End Region

#Region "printing"

    Private _currPage As Integer
    Private _printfilename As String

    Private Sub tsPrintFile_Click(sender As Object, e As EventArgs) Handles tsPrintFile.Click
        If ThisProject IsNot Nothing Then
            Dim p As New PrintDocument
            ' Save the document... And start up the default pdf viewer
            _printfilename = ThisProject.GetFileName & ".pdf"
            If File.Exists(_printfilename) Then
                Try
TRYDEL:             File.Delete(_printfilename)
                Catch ex As Exception
                    If MessageBox.Show("The PDF may be already open, Close the file and try again", "Print", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        GoTo TRYDEL
                    Else
                        MessageBox.Show("Printing was canceled")
                        Exit Sub
                    End If
                End Try
            End If
            p.PrinterSettings.PrinterName = My.Settings.CurrentPDF ' "Microsoft Print to PDF"
            p.PrinterSettings.PrintFileName = _printfilename
            p.PrinterSettings.PrintToFile = True
            AddHandler p.QueryPageSettings, AddressOf QueryPageSettings
            AddHandler p.PrintPage, AddressOf printpage
            AddHandler p.EndPrint, AddressOf printdone
            _currPage = 0
            p.Print()
        End If
    End Sub

    Private Sub printdone(sender As Object, e As PrintEventArgs)
        Dim cb As New Threading.TimerCallback(AddressOf EndPrint)
        Dim t As New Threading.Timer(cb, New Object, 2000, Threading.Timeout.Infinite)
    End Sub


    Private Sub EndPrint()
        If File.Exists(_printfilename) Then
            Process.Start(_printfilename)
        End If
    End Sub

    Public Sub QueryPageSettings(sender As Object, e As System.Drawing.Printing.QueryPageSettingsEventArgs)
        Dim t As CleanTab.Tab = mainTabs.Items(_currPage)
        If TypeOf t Is Page Then
            With DirectCast(t, Page)
                Dim sz As Size = .PageSize.GetPrintSize
                Dim epy As New PaperSize(.PageSize.getPaperSizeName, sz.Width, sz.Height)
                e.PageSettings.PaperSize = epy
                e.PageSettings.Landscape = .PageSize.Landscape
            End With
        End If
    End Sub

    Public Sub printpage(sender As Object, p As PrintPageEventArgs)
        Dim t As CleanTab.Tab = mainTabs.Items(_currPage)
        If TypeOf t Is Page Then
            DirectCast(t, Page).Print(p)
        End If

        _currPage += 1
        If _currPage > mainTabs.Items.Count - 1 Then
            p.HasMorePages = False
        Else
            p.HasMorePages = True
        End If
    End Sub

#End Region

    Private Sub mainTabs_TabOrderChanged(sender As Object, e As EventArgs) Handles mainTabs.TabOrderChanged
        If ThisProject IsNot Nothing Then
            Dim lst As New List(Of Page)
            For Each t As CleanTab.Tab In mainTabs.Items
                If TypeOf t Is Page Then
                    lst.Add(t)
                End If
            Next
            ThisProject.Pages.Clear()
            ThisProject.Pages.AddRange(lst.ToArray)
        End If
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        MessageBox.Show("designed and built by Matt McGuire", ":)")
    End Sub



    '''' <summary>
    '''' tuple tests. use the NuGet under tools to install the ValueTuple package
    '''' </summary>
    'Public Sub zz()
    '    Dim ff As (p As Integer, z As Integer)
    '    ff = xy(5, 5)
    'End Sub

    'Public Function xy(x As Integer, y As Integer) As (p As Integer, Z As Integer)
    '    Return (x, y)
    'End Function

End Class






