Imports _2Draw

Public Class Symbol_2D
    Implements Type_2D

    Private _totalRotate As Double
    Private _center As PointF
    Private _size As SizeF

    Public Sub New()
        Items = New List(Of Type_2D)
        DrawingOpts = Globals.current.clone
        _UID = DisplayItem.CreateUID
        _ItemType = ItemType.Symbol
        _center = New PointF(0, 0)
        _size = New SizeF(100, 100)
    End Sub

    Public Sub New(thenode As XmlNode)
        Items = New List(Of Type_2D)
        ItemType = ItemType.Symbol
        DrawingOpts = drawingOpts.Load(thenode)
        _UID = XHelper.Get(thenode, "UID")
        _totalRotate = XHelper.Get(thenode, "rotate", _totalRotate)
        _center = XHelper.Get(thenode, "center", New PointF(0, 0))
        _size = XHelper.Get(thenode, "size", New SizeF(100, 100))
        _ItemType = ItemType.Symbol

        'create sub child items
        For Each l As XmlNode In thenode.ChildNodes
            If l.NodeType = XmlNodeType.Element Then
                Items.Add(DisplayItem.CreateType(l))
            End If
        Next
    End Sub

    Public Sub Save(useNode As XmlNode) Implements Type_2D.Save
        DrawingOpts.Save(useNode)
        XHelper.Set(useNode, "UID", UID)
        XHelper.Set(useNode, "rotate", Me._totalRotate)
        XHelper.Set(useNode, "center", _center)
        XHelper.Set(useNode, "size", _size)

        'save the items back to the xml
        For Each t As Type_2D In Items
            DisplayItem.SaveAppendNode(useNode, t)
        Next
    End Sub

    Public Sub AddItem(item As Type_2D, boundingRec As RectangleF)
        item.ApplyOffset(New PointF(-boundingRec.X, -boundingRec.Y))
        Items.Add(item)
        item.Selected = False
        _size = GetOrigionalRec.Size
    End Sub

    Public Sub AddItems(item As Type_2D(), boundingRec As RectangleF)
        For i As Integer = 0 To item.Length - 1
            Dim c As Type_2D = item(i).Clone
            c.ApplyOffset(New PointF(-boundingRec.X, -boundingRec.Y))
            Items.Add(c)
            c.Selected = False
        Next
        _size = GetOrigionalRec.Size
    End Sub

    Public Function GetItems() As Type_2D()
        Return Items.ToArray
    End Function

    Private Property Items() As List(Of Type_2D)

    Public Property DrawingOpts As drawingOpts Implements Type_2D.DrawingOpts

    Public ReadOnly Property ItemType As ItemType Implements Type_2D.ItemType

    Public Property Selected As Boolean Implements Type_2D.Selected

    Public Property UID() As String Implements Type_2D.UID

    Public Sub Draw(G As Graphics, GO As GraphicsOpts) Implements Type_2D.Draw
        Dim rec As RectangleF = Geometry.RectangleFromCenter(_center, _size)
        Dim org As RectangleF = Me.GetOrigionalRec
        Dim szdiff As New SizeF(rec.Width / org.Width, rec.Height / org.Height)

        G.TranslateTransform(rec.X * GO.ZoomLevel, rec.Y * GO.ZoomLevel, MatrixOrder.Append)
        Drawlib.RotateAround(G, _center.X - rec.X, _center.Y - rec.Y, _totalRotate)

        For i As Integer = 0 To _Items.Count - 1
            _Items(i).Draw(G, GO)
        Next

        Drawlib.RotateAround(G, _center.X - rec.X, _center.Y - rec.Y, -_totalRotate)
        G.TranslateTransform(-rec.X * GO.ZoomLevel, -rec.Y * GO.ZoomLevel, MatrixOrder.Append)

    End Sub

    ''' <summary>
    ''' this creates a square thumbnail 
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateThumbNail(thumbsize As Integer) As Bitmap
        Dim rec As RectangleF = Me.GetOrigionalRec
        Dim ret As New Bitmap(thumbsize, thumbsize, PixelFormat.Format32bppArgb)
        Dim wrk As New SizeF(thumbsize - 4, thumbsize - 4)

        Dim sclx As Single = (rec.Width + 5) / wrk.Width
        Dim scly As Single = (rec.Height + 5) / wrk.Height
        Dim scl As Single = Math.Max(sclx, scly)

        Dim inverse As Single = 1 / scl



        Using gg As Graphics = Graphics.FromImage(ret)
            gg.Clear(Color.Transparent)
            gg.SmoothingMode = SmoothingMode.AntiAlias
            gg.TranslateTransform(2 + (wrk.Width - (rec.Width * inverse)) / 2, 2+(wrk.Height - (rec.Height * inverse)) / 2)
            gg.ScaleTransform(inverse, inverse)

            Dim go As GraphicsOpts
            go.InverseZoom = scl
            go.ScrollOffset = New PointF
            go.ZoomLevel = inverse

            'draw all the origional items
            For i As Integer = 0 To _Items.Count - 1
                _Items(i).Draw(gg, go)
            Next




            'Dim bgs As Double = Math.Max(rec.Width, rec.Height)
            'Dim per As Double = thumbsize / bgs
            ''center the image
            'Dim sz As New SizeF(per * rec.Width, per * rec.Height)
            'Dim pt As New PointF(thumbsize / 2 - sz.Width / 2, thumbsize / 2 - sz.Height / 2)

            'gg.DrawImage(fsp, New RectangleF(pt, sz))
        End Using



        Return ret
    End Function

    Public Shadows Sub Rotate(center As PointF, angle As Double) Implements Type_2D.Rotate
        Dim pts() As PointF = getrotatedrecpoints()
        pts = pts.AtAngle(center, angle)

        're-adjust the numbers
        _center = Geometry.GetBoundingRecf(pts).Center
        _totalRotate += angle 'Geometry.FindDegrees(_center, pts(0))
    End Sub

    Public Sub ApplyOffset(pointoffset As PointF) Implements Type_2D.ApplyOffset

        _center = _center.Add(pointoffset) ' = GetRecf.Center
    End Sub

    Public Sub OffsetZero()
        _center = New PointF(0, 0)
    End Sub

    Public Shadows Function HitTest(pt As PointF, zoomScale As Single) As Boolean Implements Type_2D.HitTest
        Dim pts() As PointF = getrotatedrecpoints()
        Return Geometry.WithInPolygon(pt, pts)
    End Function



    ''' <summary>
    ''' this returns what the full size of the origional items would have been
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrigionalRec() As RectangleF
        Dim ret As Rectangle = Rectangle.Empty

        For i As Integer = 0 To _Items.Count - 1
            ret = Rectangle.Union(ret, _Items(i).GetRecf.ToRec)
        Next

        Return ret
    End Function

    Public Function GetRecf() As RectangleF Implements Type_2D.GetRecf
        Dim pts() As PointF = getrotatedrecpoints()


        Dim mx As PointF = pts(0)
        Dim mn As PointF = mx

        For i As Integer = 0 To pts.Length - 1
            'check for the minimum point
            If pts(i).X < mn.X Then mn.X = pts(i).X
            If pts(i).Y < mn.Y Then mn.Y = pts(i).Y
            'check for the maximum point
            If pts(i).X > mx.X Then mx.X = pts(i).X
            If pts(i).Y > mx.Y Then mx.Y = pts(i).Y
        Next

        'return a new rectangle
        Dim r As New RectangleF(mn.X, mn.Y, mx.X - mn.X, mx.Y - mn.Y)
        Return r
    End Function

    Private Function getrotatedrecpoints() As PointF()
        Dim c As PointF = _center
        Dim s As SizeF = _size.Scale(0.5)
        Dim pts() As PointF = {New PointF(c.X - s.Width, c.Y - s.Height),
                                New PointF(c.X + s.Width, c.Y - s.Height),
                                New PointF(c.X + s.Width, c.Y + s.Height),
                                New PointF(c.X - s.Width, c.Y + s.Height)}


        Return pts.AtAngle(c, _totalRotate)
    End Function

    Public Sub Resize(newBoundingRecSize As RectangleF) Implements Type_2D.Resize

        'do nothing untill can figure out scaleing of screen

        'Dim oldr As RectangleF = GetRecf()
        ''find out the scale change
        'Dim w As Double = 1 - (oldr.Width / newBoundingRecSize.Width) + 1
        'Dim h As Double = 1 - (oldr.Height / newBoundingRecSize.Height) + 1

        ''get and scale the points
        'Dim pts() As PointF = getrotatedrecpoints()
        'pts = pts.Scale(w, h)

        ''get the new width and height mesurements
        'w = Geometry.DistanceBetween(pts(0), pts(1))
        'h = Geometry.DistanceBetween(pts(0), pts(3))
        '_size = New SizeF(w, h)

        ''move the _center to the new center
        '_center = newBoundingRecSize.Center
    End Sub

    Public Shadows Sub FlipVert(BoundingRec As RectangleF) Implements Type_2D.FlipVert
        Dim oldr As RectangleF = GetOrigionalRec()

        For i As Integer = 0 To _Items.Count - 1
            _Items(i).FlipVert(oldr)
        Next

        Dim pt As New PointF(_center.X, BoundingRec.Bottom - _center.Y)
        _center = pt
    End Sub

    Public Shadows Sub FlipHorz(BoundingRec As RectangleF) Implements Type_2D.FlipHorz
        Dim oldr As RectangleF = GetOrigionalRec()

        For i As Integer = 0 To _Items.Count - 1
            _Items(i).FlipHorz(oldr)
        Next

        Dim pt As New PointF(BoundingRec.Right - _center.X, _center.Y)
        _center = pt
    End Sub

    Public Function Clone() As Type_2D Implements Type_2D.Clone
        Dim ret As Type_2D = Nothing

        Dim b As XmlNode = Nothing
        Dim d As XmlDocument = XHelper.CreateNewDocument("b", b)
        Dim n As XmlNode = DisplayItem.SaveAppendNode(b, Me)

        If n IsNot Nothing Then
            ret = DisplayItem.CreateType(n)
            ret.UID = DisplayItem.CreateUID
        End If
        Return ret
    End Function

End Class

