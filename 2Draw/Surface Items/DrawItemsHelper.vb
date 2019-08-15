
Imports _2Draw

Public Class DrawItemsHelper
    Implements iMouseHandler

    Private _owner As Page
    Private _pt1 As PointF
    Private _pt2 As PointF
    Private _drawItem As Type_2D

    Public Sub New(owner As Page)
        _owner = owner
    End Sub

    Public Sub ZoomChanged() Implements iMouseHandler.ZoomChanged
        'do nothing
    End Sub

    ''' <summary>
    ''' event showing that we are done drawing the item, and it needs to be added to the page
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasMouse As Boolean Implements iMouseHandler.HasMouse


    Public Sub MouseDown(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseDown
        If Globals.CurrDrawingTool <> SelectedDrawingTool.Mouse AndAlso e.Button = MouseButtons.Left Then
            _HasMouse = True
            _pt1 = New Point(e.X, e.Y)
            '  Debug.WriteLine($"pt1={_pt1}")
            If CurrDrawingTool = SelectedDrawingTool.Line Then
                'if this is the first time the mouse went down
                'creat a new line object
                If _drawItem Is Nothing Then
                    _drawItem = New Line_2D
                End If
                With DirectCast(_drawItem, Line_2D)
                    If Globals.CtrlKey AndAlso .Count > 0 Then
                        'only drop the next point within 10 degrees
                        Dim ang As Single = FindDegrees(.Last.Pointf, _pt1)
                        Dim ang2 As Single = Geometry.FindClosest(ang, 5.0)
                        _pt1 = _pt1.AtAngle(.Last.Pointf, ang2 - ang)
                    End If
                    .Add(_pt1)

                End With
            ElseIf CurrDrawingTool = SelectedDrawingTool.Pan Then
                'do nothing
            End If
        Else
            _HasMouse = False
            _drawItem = Nothing
        End If
    End Sub

    Public Sub MouseMove(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseMove
        If Globals.CurrDrawingTool <> SelectedDrawingTool.Mouse AndAlso _HasMouse Then
            _pt2 = New Point(e.X, e.Y)


            Select Case CurrDrawingTool
                Case SelectedDrawingTool.Rectangle
                    Dim r As RectangleF = Geometry.GetRecFrmPts(_pt1, _pt2, Globals.CtrlKey)

                    Dim lx As New Shape_2D
                    lx.Add(r.X, r.Y)
                    lx.Add(r.Right, r.Y)
                    lx.Add(r.Right, r.Bottom)
                    lx.Add(r.X, r.Bottom)
                    lx.ClosedFigure = True
                    _drawItem = lx
                Case SelectedDrawingTool.Circle
                    Dim lx As New Shape_2D
                    Dim r As RectangleF = Geometry.GetRecFrmPts(_pt1, _pt2, Globals.CtrlKey)
                    Dim c As PointF = r.Center
                    Dim ang = 360 / Globals.CircleDefPoints
                    lx.AddRange(Geometry.GetpointsAroundEllipse(r, Globals.CircleDefPoints))

                    lx.ClosedFigure = True
                    _drawItem = lx
                Case SelectedDrawingTool.Triangle
                    Dim r As RectangleF = Geometry.GetRecFrmPts(_pt1, _pt2, Globals.CtrlKey)
                    Dim lx As New Shape_2D
                    lx.Add(r.X + r.Width / 2, r.Y)
                    lx.Add(r.Right, r.Bottom)
                    lx.Add(r.X, r.Bottom)
                    lx.ClosedFigure = True
                    _drawItem = lx
                Case SelectedDrawingTool.Line 'special (may continue without mousedown)
                    If e.Button = MouseButtons.Left Then
                        'mouse is down, record all the points
                        DirectCast(_drawItem, Line_2D).Add(New PointF(e.X, e.Y))
                    End If
                Case SelectedDrawingTool.Text 'special (will not resize)
                    Dim t As New Text_2D()
                    Dim r As RectangleF = Geometry.GetRecFrmPts(_pt1, _pt2)
                    Dim c As PointF = r.Center
                    t.SetCenter(c)
                    _drawItem = t
                Case SelectedDrawingTool.Image
                    Dim r As RectangleF = Geometry.GetRecFrmPts(_pt1, _pt2, Globals.CtrlKey)
                    Dim c As PointF = r.Center
                    Dim px As New Image_2D
                    px.SetCenterAndSize(c, r.Size)
                    _drawItem = px
                Case SelectedDrawingTool.Pan
                    Dim p As PointF = _owner.ScrollOffset
                    Dim zp As Double = _owner.ZoomPerecentage

                    p.X += (e.X - _pt1.X) * zp
                    p.Y += (e.Y - _pt1.Y) * zp

                    _owner.ScrollOffset = p
                    _pt1 = _owner.TranslatePointToPageCoord(_owner.PointToClient(Control.MousePosition))
                    ' Debug.WriteLine($" p={p}  so={_owner.ScrollOffset}  e={e.Location}   pt1={_pt1}  tme={Now.ToString("mm:ss:f")}")
            End Select
        Else
            _HasMouse = False
            _drawItem = Nothing
        End If
    End Sub

    Public Sub MouseUp(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseUp
        If Globals.CurrDrawingTool <> SelectedDrawingTool.Mouse AndAlso CurrDrawingTool <> SelectedDrawingTool.Pan AndAlso _drawItem IsNot Nothing Then

            Select Case CurrDrawingTool
                Case SelectedDrawingTool.Rectangle, SelectedDrawingTool.Circle, SelectedDrawingTool.Triangle, SelectedDrawingTool.Text, SelectedDrawingTool.Image
                    If e.Button = MouseButtons.Left Then
                        _owner.SelectedLayer.items.Add(_drawItem)

                        _drawItem = Nothing
                        _HasMouse = False
                    End If
                Case SelectedDrawingTool.Line
                    'special (may continue without mousedown) untill the double click happens
                Case SelectedDrawingTool.Pan
                    If e.Button = MouseButtons.Left Then
                        _drawItem = Nothing
                        _HasMouse = False
                    End If
            End Select


        Else
            _HasMouse = False
            _drawItem = Nothing
        End If
    End Sub

    Public Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean) Implements iMouseHandler.MouseDoubleClick
        If Globals.CurrDrawingTool = SelectedDrawingTool.Line AndAlso _HasMouse AndAlso _drawItem IsNot Nothing Then
            'find out if this will still be a line or if the end 
            'and we need to convert this to a shape instead
            Dim l As Line_2D = _drawItem


            Dim r1 As RectangleF = New Point(e.X, e.Y).toRec(10).ToRecf
            Dim r2 As RectangleF = l.First.toRecf(10)
            If r1.IntersectsWith(r2) Then
                'convert to shape
                Dim pts() As PointF = l.Points
                Dim s As New Shape_2D()
                s.AddRange(pts)
                s.ClosedFigure = True
                _owner.SelectedLayer.items.Add(s)
            Else
                ''remove the last point
                'If CtrlKey AndAlso l.Count > 3 Then
                '    'usualy because of adjustment, an extra one will be added
                '    'this does not happen on non-controlkey lines.
                '    l.Remove(l.Last)
                'End If

                _owner.SelectedLayer.items.Add(l)
                End If
                _drawItem = Nothing
            _HasMouse = False
            Handled = True
        Else
            Handled = False
        End If


    End Sub


    Public Sub Draw(g As Graphics) Implements iMouseHandler.Draw
        Dim r As Rectangle = GetRecFrmPts(_pt1, _pt2).ToRec
        If r.IsEmpty = False Then
            Using p As New Pen(Globals.Highlight, 1 * _owner.InverseZoom), fnt As New Font(_owner.Font.Name, _owner.Font.Size * _owner.InverseZoom)
                If _HasMouse AndAlso Globals.CurrDrawingTool <> SelectedDrawingTool.Mouse Then
                    If CurrDrawingTool <> SelectedDrawingTool.Line AndAlso CurrDrawingTool <> SelectedDrawingTool.Pan Then
                        p.DashStyle = DashStyle.Dot
                        g.DrawRectangle(p, r)
                    End If

                End If

                If _drawItem IsNot Nothing Then
                    _drawItem.Draw(g, _owner.GetGraphicsOpts)
                    If TypeOf _drawItem Is Line_2D Then
                        Dim l As Line_2D = _drawItem
                        Dim px As PointF = l.Last.Point
                        Dim ang As Single = FindDegrees(px, _pt2)
                        If CtrlKey Then
                            Dim ang2 As Single = Geometry.FindClosest(ang, 5.0)
                            _pt2 = _pt2.AtAngle(px, ang2 - ang)
                            g.DrawString($"{CInt(ang2)}°", fnt, p.Brush, _pt2.X + 5, _pt2.Y + 5)
                        Else
                            g.DrawString($"{ang:f1}°", fnt, p.Brush, _pt2.X + 5, _pt2.Y + 5)
                        End If
                        g.DrawLine(p, _pt2, px)
                    End If
                End If
            End Using
        End If
    End Sub



End Class
