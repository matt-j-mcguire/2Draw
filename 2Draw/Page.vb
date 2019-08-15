





Public Class Page
    Private Const PAGE_NODE As String = "Page"
    Private Const NODE_NAME As String = "name"
    Private Const LAYER_NODE As String = "Layer"
    Private Const PAGE_SIZE As String = "size"
    Private Const PAGE_OREN As String = "landscape"
    Private Const ZOOMSIZE As String = "zoomsize"

    Public Event ZoomChanged As EventHandler
    Public Event MouseMoved(sender As Object, e As PointF)

    ''' <summary>
    ''' to tell the main program that something has changed
    ''' </summary>
    Public Event FileChanged As EventHandler
    ''' <summary>
    ''' a list of layers for this page
    ''' </summary>
    Public Layers As List(Of Layer)
    ''' <summary>
    ''' xml document that holds this page
    ''' </summary>
    Public WithEvents Doc As XmlDocument
    ''' <summary>
    ''' how big this page size is
    ''' </summary>
    Public PageSize As PageSizes


    Public RectPage As RectangleF
    Public RectInnerPage As RectangleF
    ' Public PageOffset As Point


    '---------------------------------------------
    'the following are helper classes to work with
    'the items on the screen
    '---------------------------------------------
    Private WithEvents _cc As RotationHandle
    Private WithEvents _mm As MoveHandle
    Private WithEvents _pp As pointHelper
    Private WithEvents _LL As LineHelper
    Private WithEvents _SH As SelectionHelper
    Private WithEvents _RH As ResizeHelper
    Private WithEvents _DH As DrawItemsHelper
    Private _helpers As List(Of iMouseHandler)


    ''' <summary>
    ''' loads an existing page
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="name"></param>
    Public Sub New(xml As XmlDocument, name As String)
        InitializeComponent()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint, True)

        Doc = xml
        ScrollOffset = New PointF(0, 0)
        Me.AllowDrop = True

        '---get root settings---
        Dim root As XmlNode = Doc.SelectSingleNode(PAGE_NODE)
        ZoomPerecentage = XHelper.Get(root, ZOOMSIZE, 1.0)
        PageSize = New PageSizes(XHelper.Get(root, PAGE_SIZE, PageSizes.PS_LETT), XHelper.Get(root, PAGE_OREN, False))
        Me.Name = name 'object name
        Me.Text = XHelper.Get(root, NODE_NAME, name) 'friendly title name

        '---load up layers---
        Layers = New List(Of Layer)
        Dim xs As XmlNodeList = root.SelectNodes(LAYER_NODE)
        For Each l As XmlNode In xs
            Layers.Add(New Layer(l, Me))
        Next
        Layers.Last.Selected = True

        '---set up the logical bounds---
        RectPage = New RectangleF(New PointF(0, 0), PageSize.getInitalSize) 'create the inital size of the page
        RectInnerPage = PageSize.GetPageBoundry



        If ShowCrossHairs Then
            Me.Cursor = BlankCursor
        Else
            Me.Cursor = Cursors.Default
        End If

        _cc = New RotationHandle(Me)
        _mm = New MoveHandle(Me)
        _pp = New pointHelper(Me)
        _LL = New LineHelper(Me)
        _RH = New ResizeHelper(Me)
        _SH = New SelectionHelper(Me)
        _DH = New DrawItemsHelper(Me)
        _helpers = New List(Of iMouseHandler)({_DH, _cc, _mm, _pp, _LL, _RH, _SH}) 'order is very important!
    End Sub

    ''' <summary>
    ''' creates a new page from scratch
    ''' </summary>
    ''' <param name="name">the name of the page</param>
    ''' <param name="Landscape">if it will be in landscape or portrate</param>
    ''' <param name="PS_size">must be one of the pageSizes.PS_ constants</param>
    ''' <returns></returns>
    Public Shared Function CreatePage(name As String, PS_size As String, Landscape As Boolean) As Page
        Dim r As XmlNode = Nothing
        Dim d As XmlDocument = XHelper.CreateNewDocument(PAGE_NODE, r)
        XHelper.Set(r, NODE_NAME, name)
        XHelper.Set(r, PAGE_SIZE, PS_size)
        XHelper.Set(r, PAGE_OREN, Landscape)
        r = XHelper.NodeAppendNew(r, LAYER_NODE)
        XHelper.Set(r, NODE_NAME, LAYER_NODE)
        XHelper.Set(r, ZOOMSIZE, 1.0)

        Dim p As New Page(d, XHelper.GetSafeName(name))
        Return p
    End Function

    Public Overloads Sub Save()
        Dim root As XmlNode = Doc.SelectSingleNode(PAGE_NODE)
        root.RemoveAll()

        XHelper.Set(root, NODE_NAME, Me.Text)
        XHelper.Set(root, PAGE_SIZE, PageSize.PS_Size)
        XHelper.Set(root, PAGE_OREN, PageSize.Landscape)
        XHelper.Set(root, ZOOMSIZE, ZoomPerecentage)

        For Each l As Layer In Layers
            Dim ln As XmlNode = XHelper.NodeAppendNew(root, LAYER_NODE)
            l.save(ln)
        Next
    End Sub

    Public Function Clone() As Page
        Dim nd As XmlNode = Nothing
        Dim d As XmlDocument = XHelper.CreateNewDocument(PAGE_NODE, nd)


        XHelper.Set(nd, NODE_NAME, Me.Text)
        XHelper.Set(nd, PAGE_SIZE, PageSize.PS_Size)
        XHelper.Set(nd, PAGE_OREN, PageSize.Landscape)

        For Each l As Layer In Layers
            Dim n As XmlNode = l.CloneNode(d)
            nd.AppendChild(n)
        Next

        Return New Page(d, Me.Text)
    End Function

    Public Function GetResourcesNames() As String()
        Dim ret As New List(Of String)

        For Each l As Layer In Layers
            For Each i As Type_2D In l.items
                If TypeOf i Is Image_2D Then
                    Dim v As String = DirectCast(i, Image_2D).Image
                    If Not ret.Contains(v) Then ret.Add(v)
                ElseIf TypeOf i Is Symbol_2D Then
                    Dim s As Symbol_2D = i
                    For Each j As Type_2D In s.GetItems
                        If TypeOf j Is Image_2D Then
                            Dim v As String = DirectCast(j, Image_2D).Image
                            If Not ret.Contains(v) Then ret.Add(v)
                        End If

                    Next
                End If
            Next
        Next

        Return ret.ToArray
    End Function

    Public Overrides Function ToString() As String
        Return Me.Text
    End Function


#Region "page header info"

    'Private Sub doc_NodeChanged(sender As Object, e As XmlNodeChangedEventArgs) Handles Doc.NodeChanged, Doc.NodeInserted, Doc.NodeRemoved


    'End Sub

    Public Sub ShowSomethingChanged()
        RaiseEvent FileChanged(Me, New EventArgs)
        Me.DirtyBit = True
    End Sub

    Friend Sub TheFileWasSaved()
        Me.DirtyBit = False
    End Sub

#End Region

    ''' <summary>
    ''' returns the currently selected layer
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SelectedLayer() As Layer
        <DebuggerStepThrough()> Get
            Dim ret As Layer = Nothing
            For i As Integer = 0 To Layers.Count - 1
                If Layers(i).Selected Then
                    ret = Layers(i)
                    Exit For
                End If
            Next
            Return ret
        End Get
    End Property


#Region "display properties"

    Private _grid As Boolean
    ''' <summary>
    ''' if to show the grid
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowGrid() As Boolean
        <DebuggerStepThrough()> Get
            Return _grid
        End Get
        Set(value As Boolean)
            _grid = value
            CauseRedraw()
        End Set
    End Property

    Private _zoom As Double
    Public Property ZoomPerecentage() As Double
        <DebuggerStepThrough()> Get
            Return _zoom
        End Get
        Set(value As Double)
            If value < 0.15 Then value = 0.15
            If value > 10.0 Then value = 10.0
            _zoom = value
            ScrollOffset = _Scoff 'force the scroll to re-evaulate
            If _helpers IsNot Nothing Then
                For i As Integer = 0 To _helpers.Count - 1
                    _helpers(i).ZoomChanged()
                Next
            End If

            CauseRedraw()
            RaiseEvent ZoomChanged(Me, New EventArgs)
        End Set
    End Property

    Public ReadOnly Property InverseZoom() As Double
        <DebuggerStepThrough()> Get
            Return 1 / _zoom
        End Get
    End Property

    Public ReadOnly Property BoundingRecAdjustedToZoomAndOffset() As Rectangle
        Get
            Dim b As Rectangle = Me.DisplayRectangle
            Dim p As PointF = Me.ScrollOffset
            b.Location = New Point(-p.X, -p.Y)
            b.Width *= InverseZoom
            b.Height *= InverseZoom

            Return b

        End Get
    End Property

    Dim _Scoff As PointF
    ''' <summary>
    ''' scroll offset can only live in the negative values
    ''' </summary>
    ''' <returns></returns>
    Public Property ScrollOffset() As PointF
        <DebuggerStepThrough()> Get
            Return _Scoff
        End Get
        Set(value As PointF)
            Dim mw As Double = -((RectPage.Width * ZoomPerecentage) - Me.Width)
            Dim mh As Double = -((RectPage.Height * ZoomPerecentage) - Me.Height)
            value.X = Limits(value.X, mw, 0)
            value.Y = Limits(value.Y, mh, 0)

            _Scoff = value
        End Set
    End Property

    Private Sub Page_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'force the scroll to re-evaulate the page position
        ScrollOffset = _Scoff
    End Sub

#End Region

#Region "mouse and keyboard"

    Private Sub me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Dim ed As DoubleMouseEventArgs = TranslatePointToPageCoord(e)
        For Each p As iMouseHandler In _helpers
            p.MouseDown(sender, ed)
            If p.HasMouse Then Debug.WriteLine($"helper: {p.GetType.Name}")
            If p.HasMouse Then Exit For
        Next
    End Sub

    Private Sub me_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Dim ed As DoubleMouseEventArgs = TranslatePointToPageCoord(e)
        For Each p As iMouseHandler In _helpers
            p.MouseMove(sender, ed)
            If p.HasMouse Then Exit For
        Next
        Me.Invalidate()
        RaiseEvent MouseMoved(Me, New PointF(ed.X, ed.Y))
    End Sub

    Private Sub me_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Dim ed As DoubleMouseEventArgs = TranslatePointToPageCoord(e)
        ' Dim handled As Boolean = False
        For Each p As iMouseHandler In _helpers
            p.MouseUp(sender, ed)
            If ed.Handled Then
                ShowSomethingChanged()
                Exit For
            End If
        Next

        If e.Button = MouseButtons.Right AndAlso Not ed.Handled Then
            Dim l() As Type_2D = SelectedLayer.SelectedItems
            Dim en As Boolean = If(l.Length > 0, True, False)
            CutToolStripMenuItem.Enabled = en
            CopyToolStripMenuItem.Enabled = en
            PasteToolStripMenuItem.Enabled = Clipboard.ContainsData("xmldoc")
            DeleteToolStripMenuItem.Enabled = en
            cmsCCP.Show(Me, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub Page_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        Dim Handled As Boolean = False
        For Each p As iMouseHandler In _helpers
            p.MouseDoubleClick(sender, TranslatePointToPageCoord(e), Handled)
            If Handled Then
                ShowSomethingChanged()
                Exit For
            End If
        Next
        Dim iv As Single = Me.InverseZoom

        If Not Handled Then
            Dim l As Layer = SelectedLayer
            If l IsNot Nothing Then
                Dim gx As PointF = TranslatePointToPageCoord(New PointF(e.X, e.Y))
                For i As Integer = 0 To l.items.Count - 1
                    If l.items(i).HitTest(gx, iv) Then
                        If TypeOf l.items(i) Is Image_2D Then
                            HandleImageEdit(l.items(i))
                        ElseIf TypeOf l.items(i) Is Text_2D Then
                            HandleLabelEdit(l.items(i))
                        ElseIf TypeOf l.items(i) Is Symbol_2D Then
                            With DirectCast(l.items(i), Symbol_2D)
                                Dim r As RectangleF = .GetRecf
                                Dim tp As New PointF(gx.X - r.X, gx.Y - r.Y)
                                Dim itms() As Type_2D = .GetItems
                                For j As Integer = 0 To itms.Length - 1
                                    If itms(j).HitTest(tp, iv) Then
                                        If TypeOf itms(j) Is Image_2D Then
                                            HandleImageEdit(itms(j))
                                        ElseIf TypeOf itms(j) Is Text_2D Then
                                            HandleLabelEdit(itms(j))
                                        End If
                                    End If
                                Next
                            End With
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub HandleImageEdit(img As Image_2D)
        'launch Image selector
        Dim fl As New ImageItemEdit(img)
        fl.ShowDialog(Me.FindForm)
        ShowSomethingChanged()
    End Sub

    Private Sub HandleLabelEdit(lbl As Text_2D)
        Dim fl As New TextItemEdit(lbl)
        fl.ShowDialog(Me.FindForm)
        If String.IsNullOrEmpty(lbl.Text) Then lbl.Text = "Text"
        ShowSomethingChanged()
    End Sub

    Private Sub Page_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel

        Dim dp As PointF = TranslatePointToPageCoord(e).Location
        Dim sc As Single = 0.0
        If e.Delta > 0 Then
            sc = +0.05
        Else
            sc = -0.05
        End If
        Me.ZoomPerecentage += sc
        Dim szp As PointF = TranslatePointToPageCoord(e).Location


        _Scoff.X += (szp.X - dp.X) * ZoomPerecentage
        _Scoff.Y += (szp.Y - dp.Y) * ZoomPerecentage
        ScrollOffset = _Scoff


    End Sub

#End Region

#Region "Point Translation"

    ''' <summary>
    ''' translates the current control coord to virtual page coords
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <returns>calls the translatepointtopagecoord(pointf) call internally</returns>
    Private Function TranslatePointToPageCoord(pt As MouseEventArgs) As DoubleMouseEventArgs
        Dim ret As New DoubleMouseEventArgs(pt)
        ret.Location = TranslatePointToPageCoord(New PointF(pt.X, pt.Y))
        Return ret
    End Function

    ''' <summary>
    ''' translates the current control coord to virtual page coords
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <returns></returns>
    Public Function TranslatePointToPageCoord(pt As PointF) As PointF
        Dim pSize As SizeF = PageSize.getInitalSize
        Dim pto As PointF = ScrollOffset
        Dim ret As New Point(pt.X, pt.Y)

        ret.X -= pto.X
        ret.Y -= pto.Y
        ret.X /= ZoomPerecentage
        ret.Y /= ZoomPerecentage

        Return ret
    End Function


#End Region

#Region "Tools"

    Private Sub _SH_SelectionChanged(sender As Object, e As EventArgs) Handles _SH.SelectionChanged
        Dim cnt As Integer = 0
        For i As Integer = 0 To SelectedLayer.items.Count - 1
            If SelectedLayer.items(i).Selected Then cnt += 1
        Next

        Dim show As Boolean = cnt > 0
        Dim r As Rectangle = GetBoundingRecFromSelected()
        _cc.Enabled = show
        _cc.Center = r.Center
        _mm.Enabled = show
        _mm.UpdatePoints(r)
    End Sub

    Public Function GetBoundingRecFromSelected() As Rectangle
        Dim ret As Rectangle = Rectangle.Empty

        For i As Integer = 0 To SelectedLayer.items.Count - 1
            If SelectedLayer.items(i).Selected Then
                If ret.IsEmpty Then
                    ret = SelectedLayer.items(i).GetRecf.ToRec
                Else
                    ret = Rectangle.Union(ret, SelectedLayer.items(i).GetRecf.ToRec)
                End If
            End If
        Next

        Return ret
    End Function

    Private Sub _LL_LineMoved(sender As Object, e As EventArgs) Handles _LL.LineMoved, _pp.PointMoved
        _mm.UpdatePoints(GetBoundingRecFromSelected)
        ShowSomethingChanged()
    End Sub

    Private Sub _cc_Rotation(Origin As PointF, Angle As Double) Handles _cc.Rotation
        For j As Integer = 0 To SelectedLayer.items.Count - 1
            If SelectedLayer.items(j).Selected Then
                SelectedLayer.items(j).Rotate(_cc.Center, Angle)
            End If
        Next
        _mm.UpdatePoints(GetBoundingRecFromSelected)
        ShowSomethingChanged()
    End Sub

    Private Sub _mm_Move(sender As Object, pointOffset As Point) Handles _mm.Move
        For j As Integer = 0 To SelectedLayer.items.Count - 1
            If SelectedLayer.items(j).Selected Then
                SelectedLayer.items(j).ApplyOffset(pointOffset)
            End If
        Next
        _cc.Center = GetBoundingRecFromSelected().Center
        ShowSomethingChanged()
    End Sub

    Private Sub _RH_Resize(sender As Object, Resized As ResizeHelper.ResizeCord) Handles _RH.Resize
        For j As Integer = 0 To SelectedLayer.items.Count - 1
            Dim d As Type_2D = SelectedLayer.items(j)
            If d.Selected Then

                Dim b As RectangleF = d.GetRecf
                If Resized.left <> 0 Then
                    b.Width -= Resized.left
                    d.Resize(b)
                    d.ApplyOffset(New Point(Resized.left, 0))
                End If
                If Resized.right <> 0 Then
                    b.Width += Resized.right
                    d.Resize(b)
                End If
                If Resized.top <> 0 Then
                    b.Height -= Resized.top
                    d.Resize(b)
                    d.ApplyOffset(New Point(0, Resized.top))
                End If
                If Resized.bottom <> 0 Then
                    b.Height += Resized.bottom
                    d.Resize(b)
                End If
            End If
        Next
        _cc.Center = GetBoundingRecFromSelected().Center
        ShowSomethingChanged()
    End Sub

    Private Sub _LL_ShowAddPT(sender As Object, ap As LineInfo) Handles _LL.ShowLineOpts
        mnuAddPoint.Enabled = True
        mnuRemoveSegment.Enabled = True
        mnuAddPoint.Tag = ap
        cms.Show(Cursor.Position)
    End Sub

    Private Sub mnuAddPoint_Click(sender As Object, e As EventArgs) Handles mnuAddPoint.Click
        Dim ap As LineInfo = mnuAddPoint.Tag
        If ap IsNot Nothing AndAlso TypeOf ap.item Is LinkedPoints Then
            DirectCast(ap.item, LinkedPoints).InsertAfter(ap.newPt, ap.Pt1)
        End If
        mnuAddPoint.Enabled = False
        mnuRemoveSegment.Enabled = False
        ShowSomethingChanged()
    End Sub

    Private Sub mnuRemoveSegment_Click(sender As Object, e As EventArgs) Handles mnuRemoveSegment.Click
        Dim ap As LineInfo = mnuAddPoint.Tag
        If ap IsNot Nothing AndAlso TypeOf ap.item Is LinkedPoints Then
            Dim l As LinkedPoints = DirectCast(ap.item, LinkedPoints).RemoveLink(ap.Pt1, ap.Pt2)
            If l IsNot Nothing AndAlso l.Count > 1 Then
                If TypeOf ap.item Is Shape_2D Then
                    Dim ll As New Shape_2D(l, ap.item.DrawingOpts)
                    SelectedLayer.items.Add(ll)
                    ll.Selected = True
                ElseIf TypeOf l Is Line_2D Then
                    Dim ll As New Line_2D(l, ap.item.DrawingOpts, Nothing, Nothing)
                    SelectedLayer.items.Add(ll)
                    ll.Selected = True
                End If

            End If
            If DirectCast(ap.item, LinkedPoints).Count < 2 Then SelectedLayer.items.Remove(ap.item)
        End If

        mnuAddPoint.Enabled = False
        mnuRemoveSegment.Enabled = False
        ShowSomethingChanged()
    End Sub

    Private Sub _pp_RemovePt(sender As Object, r As removePt) Handles _pp.RemovePt
        mnuRemovePoint.Enabled = True
        mnuRemovePoint.Tag = r
        cms.Show(Cursor.Position)
        ShowSomethingChanged()
    End Sub

    Private Sub mnuRemovePoint_Click(sender As Object, e As EventArgs) Handles mnuRemovePoint.Click
        Dim r As removePt = mnuRemovePoint.Tag
        If r IsNot Nothing AndAlso TypeOf r.item Is LinkedPoints Then
            DirectCast(r.item, LinkedPoints).Remove(r.Pt)
        End If
        mnuRemovePoint.Enabled = False
        ShowSomethingChanged()
    End Sub

    ''' <summary>
    ''' tells the selected items to flip verical by the bounding rectangle
    ''' </summary>
    Public Sub FlipSelectedVertical()
        Dim sel() As Type_2D = SelectedLayer.SelectedItems
        Dim r As Rectangle = GetBoundingRecFromSelected()

        For i As Integer = 0 To sel.Length - 1
            sel(i).FlipVert(r)
        Next
        Me.Invalidate()
        ShowSomethingChanged()
    End Sub

    ''' <summary>
    ''' tells the selected items to flip horizontal by the bounding rectangle
    ''' </summary>
    Public Sub FlipSelectedHorizonal()
        Dim sel() As Type_2D = SelectedLayer.SelectedItems
        Dim r As Rectangle = GetBoundingRecFromSelected()

        For i As Integer = 0 To sel.Length - 1
            sel(i).FlipHorz(r)
        Next
        Me.Invalidate()
        ShowSomethingChanged()
    End Sub

    Public Sub CancelGrabandRotationHandles()
        For i As Integer = 0 To Layers.Count - 1
            Layers(i).CancelSelectedItems()
        Next
        _cc.Enabled = False
        _mm.Enabled = False
        Me.Invalidate()
    End Sub

    Public Sub SendItemBack()
        Dim items() As Type_2D = SelectedLayer.SelectedItems
        If items.Length > 0 Then
            SelectedLayer.MoveBeforeAny(items(0))
        End If
        ShowSomethingChanged()
    End Sub

    Public Sub BringItemForward()
        Dim items() As Type_2D = SelectedLayer.SelectedItems
        If items.Length > 0 Then
            SelectedLayer.MoveAfterAny(items(0))
        End If
        ShowSomethingChanged()
    End Sub

    Public Sub DeleteItem()
        Dim itms() As Type_2D = SelectedLayer.SelectedItems
        For i As Integer = 0 To itms.Length - 1
            SelectedLayer.items.Remove(itms(i))
        Next
        _cc.Enabled = False
        _mm.Enabled = False
        Me.Invalidate()
        ShowSomethingChanged()
    End Sub

    Public Sub ClearSelected()
        SelectedLayer.ClearSelectedItems()
        _cc.Enabled = False
        _mm.Enabled = False
    End Sub

    Public Sub CloneItem()
        Dim items() As Type_2D = SelectedLayer.SelectedItems


        SelectedLayer.ClearSelectedItems()
        For i As Integer = 0 To items.Length - 1
            Dim td As Type_2D = items(i).Clone
            td.Selected = True
            SelectedLayer.items.Add(td)
            td.ApplyOffset(New PointF(5, 5))
        Next
        Me.Invalidate()
        ShowSomethingChanged()
    End Sub

    Public Sub CutItem()
        Dim items() As Type_2D = SelectedLayer.SelectedItems
        If items.Length > 0 Then
            Dim nd As XmlNode = Nothing
            Dim out As XmlDocument = XHelper.CreateNewDocument("Base", nd)
            For i As Integer = 0 To items.Length - 1
                DisplayItem.SaveAppendNode(nd, items(i))
                SelectedLayer.items.Remove(items(i))
            Next
            Clipboard.SetData("xmldoc", out.OuterXml)
            Me.Invalidate()
        End If
        ShowSomethingChanged()
    End Sub

    Public Sub CopyItem()
        Dim items() As Type_2D = SelectedLayer.SelectedItems
        If items.Length > 0 Then
            Dim nd As XmlNode = Nothing
            Dim out As XmlDocument = XHelper.CreateNewDocument("Base", nd)
            For i As Integer = 0 To items.Length - 1
                Dim t As Type_2D = items(i).Clone
                t.UID = DisplayItem.CreateUID
                DisplayItem.SaveAppendNode(nd, t)
            Next
            Clipboard.SetData("xmldoc", out.OuterXml)
        End If
        ShowSomethingChanged()

    End Sub

    Public Sub PasteItem()
        If Clipboard.ContainsData("xmldoc") Then
            SelectedLayer.ClearSelectedItems()
            Dim d As New XmlDocument
            d.LoadXml(Clipboard.GetData("xmldoc"))
            Dim nd As XmlNode = d.SelectSingleNode("Base")
            For Each x As XmlNode In nd
                Dim t As Type_2D = DisplayItem.CreateType(x)
                If t IsNot Nothing Then
                    t.Selected = True
                    SelectedLayer.items.Add(t)
                End If
            Next
            _mm.UpdatePoints(GetBoundingRecFromSelected)
            _mm.Enabled = True
            _cc.Enabled = True
            Me.Invalidate()
        End If
        ShowSomethingChanged()
    End Sub

    Public Sub GroupSelected()
        Dim sl As Layer = SelectedLayer
        Dim r As RectangleF = GetBoundingRecFromSelected()
        Dim t() As Type_2D = sl.SelectedItems

        Dim ex As New Symbol_2D()
        ex.AddItems(t, r)
        ex.ApplyOffset(r.Center)
        For i As Integer = 0 To t.Length - 1
            sl.items.Remove(t(i))
        Next
        sl.items.Add(ex)
        ex.Selected = True
        Me.Refresh()
        ShowSomethingChanged()
    End Sub

    Public Sub UnGroupSelected()
        Dim sl As Layer = SelectedLayer
        Dim t() As Type_2D = sl.SelectedItems
        If t.Length > 0 Then
            If Warnings.Show(AppWarnings.UNGROUP) Then
                For i As Integer = 0 To t.Length - 1
                    If TypeOf t(i) Is Symbol_2D Then
                        Dim r As RectangleF = t(i).GetRecf
                        Dim items() As Type_2D = DirectCast(t(i), Symbol_2D).GetItems
                        sl.items.Remove(t(i))
                        For j As Integer = 0 To items.Length - 1
                            items(j).ApplyOffset(r.Location)
                            sl.items.Add(items(j))
                            items(j).Selected = True
                        Next
                    End If
                Next
            End If
            Me.Refresh()
        End If
        ShowSomethingChanged()
    End Sub

#End Region

#Region "drawing"

    ''' <summary>
    ''' just temporary until the revalidate works again
    ''' </summary>
    Private Sub CauseRedraw()
        Me.Invalidate()
    End Sub

    Public Function GetGraphicsOpts() As GraphicsOpts
        Dim go As New GraphicsOpts
        go.InverseZoom = InverseZoom
        go.ZoomLevel = ZoomPerecentage
        go.ScrollOffset = ScrollOffset
        Return go
    End Function

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        'draw the background area
        e.Graphics.Clear(Color.DimGray) 'background area

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        e.Graphics.TranslateTransform(ScrollOffset.X, ScrollOffset.Y)
        e.Graphics.ScaleTransform(ZoomPerecentage, ZoomPerecentage)


        e.Graphics.FillRectangle(Brushes.White, RectPage) 'page area
        e.Graphics.DrawRectangle(Pens.Gainsboro, RectInnerPage.ToRec) 'light boarder to show printing bounds

        If ShowToolGrid Then DrawtheGrid(e.Graphics)

        Dim go As GraphicsOpts = GetGraphicsOpts()

        'draw the lines
        For j As Integer = 0 To Layers.Count - 1
            If Layers(j).Visible Then
                For i As Integer = 0 To Layers(j).items.Count - 1
                    Layers(j).items(i).Draw(e.Graphics, go)
                Next
            End If
        Next

        Using ex As New Region(RectPage), br As New SolidBrush(Color.FromArgb(200, Color.White))
            ex.Xor(RectInnerPage)
            e.Graphics.FillRegion(br, ex)
        End Using


        'draw the crosshairs for the mouse
        If ShowCrossHairs Then
            Using p As New Pen(Color.FromArgb(100, Globals.CrossHairColor), 1 * InverseZoom)
                Dim pt As PointF = TranslatePointToPageCoord(Me.PointToClient(MousePosition).ToPointf)
                Dim maxs As SizeF = PageSize.getInitalSize
                e.Graphics.DrawLine(p, 0, pt.Y, -ScrollOffset.X + maxs.Width + 1000, pt.Y)
                e.Graphics.DrawLine(p, pt.X, 0, pt.X, -ScrollOffset.Y + maxs.Height + 1000)
            End Using
        End If

        'update the tools
        For i As Integer = 0 To _helpers.Count - 1
            _helpers(i).Draw(e.Graphics)
        Next

        e.Graphics.ResetTransform()
    End Sub

    ''' <summary>
    ''' draw the grid on the screen
    ''' </summary>
    ''' <param name="g"></param>
    Private Sub DrawtheGrid(g As Graphics)
        Dim sp As Integer = Globals.GridSize
        Dim r As RectangleF = RectInnerPage
        Using p As New Pen(Color.FromArgb(125, Globals.GridColor), 1 * InverseZoom)
            For i As Integer = 0 To r.Width / sp
                g.DrawLine(p, r.X + sp * i, r.Y, r.X + sp * i, r.Bottom)
            Next
            For i As Integer = 0 To r.Height / sp
                g.DrawLine(p, r.X, r.Y + sp * i, r.Right, r.Y + sp * i)
            Next
        End Using

    End Sub

    Public Sub printToPNGFile()
        Dim sz As SizeF = PageSize.getInitalSize
        Using p As Bitmap = DrawAsBitmap()

            Dim file As String = Globals.ThisProject.GetFileName & "-" & Me.Text & ".png"
            p.Save(file, ImageFormat.Png)
            Process.Start(file)
        End Using
    End Sub

    Public Function DrawAsBitmap() As Bitmap
        Dim sz As SizeF = PageSize.getInitalSize
        Dim p As New Bitmap(sz.Width, sz.Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(p)
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.FillRectangle(Brushes.White, 0, 0, sz.Width, sz.Height)
            Dim go As New GraphicsOpts
            go.InverseZoom = 1
            go.ScrollOffset = New PointF
            go.ZoomLevel = 1.0


            For j As Integer = 0 To Layers.Count - 1
                If Layers(j).Visible Then
                    For i As Integer = 0 To Layers(j).items.Count - 1
                        Layers(j).items(i).Draw(g, go)
                    Next
                End If
            Next
        End Using
        Return p
    End Function

    Public Sub Print(g As PrintPageEventArgs)
        g.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        ' Dim sz As SizeF = PageSize.getInitalSize
        ' Using p As New Bitmap(sz.Width, sz.Height, PixelFormat.Format32bppArgb)
        ' Using gx As Graphics = Graphics.FromImage(p)
        Dim box As Rectangle = g.PageBounds
        Dim sw As Double = box.Width / RectPage.Width
        '    Dim sh As Double = RectPage.Height / box.Height


        g.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        g.Graphics.Clear(Color.White)
        Dim go As New GraphicsOpts
        go.InverseZoom = 1 / sw
        go.ScrollOffset = New PointF(0, 0)
        go.ZoomLevel = sw
        g.Graphics.ScaleTransform(sw, sw)

        For j As Integer = 0 To Layers.Count - 1
            If Layers(j).Visible Then
                For i As Integer = 0 To Layers(j).items.Count - 1
                    Layers(j).items(i).Draw(g.Graphics, go)
                Next
            End If
        Next
        ' gx.ResetTransform()
        '  End Using
        ' g.Graphics.DrawImage(p, 0, 0, sz.Width, sz.Height)
        ' End Using
    End Sub

#End Region

#Region "Drop data"

    Private Sub Page_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        e.Effect = DragDropEffects.Copy


    End Sub

    Private Sub me_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragDrop
        ' Ensures that the list item index is contained in the data. 

        If e.Data.GetDataPresent(DataFormats.StringFormat) Then

            Dim item As String = CType(e.Data.GetData(GetType(System.String)), System.String)

            ' Perform drag-and-drop, depending upon the effect. 
            If (e.Effect = DragDropEffects.Copy) Then
                Dim sym As Symbol_2D = Globals.SymbolLibs.GetItemByUID(item).Clone
                If sym IsNot Nothing Then
                    Dim ptc As PointF = Me.PointToClient(New Point(e.X, e.Y)).ToPointf
                    Dim cp As PointF = TranslatePointToPageCoord(ptc)
                    Dim rx As RectangleF = sym.GetOrigionalRec
                    cp.X += rx.Width / 2
                    cp.Y += rx.Height / 2
                    sym.ApplyOffset(cp)
                    Dim sl As Layer = SelectedLayer
                    sl.items.Add(sym)
                    ShowSomethingChanged()
                ElseIf Not String.IsNullOrEmpty(item) Then
                    'maybe text was drug to the screen?
                    'todo: setup for draging other things to the screen.
                End If
            End If
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) Then

            'maybe an image to put on the screen?
            'todo: setup for image drop


            '        Dim draggedfiles As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            '        For Each x As String In draggedfiles
            '            Dim xfile As New FileInfo(x)
            '            Dim ext As String = xfile.Extension.ToLower
            '            If ext = ".bmp" OrElse ext = ".gif" OrElse ext = ".jpg" OrElse ext = ".jpeg" OrElse ext = ".png" Then
            '                _BackPic.Picture = xfile.FullName
            '                _BackPic.ViewChanged()
            '                Me.BackgroundImage = _BackPic.Image
            '                _BackPic.SaveChanges()
            '                Exit For
            '            End If
            '        Next


        End If
    End Sub

#End Region

#Region "context menu = cut copy paste delete"

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        Me.CutItem()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Me.CopyItem()
    End Sub

    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        Me.PasteItem()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Me.DeleteItem()
    End Sub

    Private Sub ExportAsPNGFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportAsPNGFileToolStripMenuItem.Click
        printToPNGFile()
    End Sub

    Private Sub cms_Opening(sender As Object, e As CancelEventArgs) Handles cms.Opening, cmsCCP.Opening
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cms_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles cms.Closing, cmsCCP.Closing
        If ShowCrossHairs Then
            Me.Cursor = BlankCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub


#End Region

End Class
