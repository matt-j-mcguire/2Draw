﻿
Imports _2Draw

Public Class Text_2D
    Implements Type_2D

    Private _totalRotate As Double
    Private _center As PointF
    Private _Size As SizeF

    Public Sub New()
        DrawingOpts = Globals.current.clone
        _UID = DisplayItem.CreateUID
        _ItemType = ItemType.Text
        Text = "Text"
        _Size = New SizeF(16, 16)
        _center = New Point(8, 8)
    End Sub

    Public Sub New(thenode As XmlNode)
        DrawingOpts = drawingOpts.Load(thenode)
        _UID = XHelper.Get(thenode, "UID")
        _ItemType = ItemType.Text
        _totalRotate = XHelper.Get(thenode, "rotate", _totalRotate)
        Text = XHelper.Get(thenode, "text", "Text")
        _center = XHelper.Get(thenode, "center", New PointF(8, 8))
        _Size = XHelper.Get(thenode, "size", New SizeF(16, 16))
    End Sub

    Public Sub Save(useNode As XmlNode) Implements Type_2D.Save
        DrawingOpts.Save(useNode)
        XHelper.Set(useNode, "text", Text)
        XHelper.Set(useNode, "UID", UID)
        XHelper.Set(useNode, "rotate", Me._totalRotate)
        XHelper.Set(useNode, "center", Me._center)
        XHelper.Set(useNode, "size", _Size)
    End Sub

    Public Property Text() As String

    Public Property DrawingOpts As drawingOpts Implements Type_2D.DrawingOpts

    Public ReadOnly Property ItemType As ItemType Implements Type_2D.ItemType

    Public Property Selected As Boolean Implements Type_2D.Selected

    Public Property UID() As String Implements Type_2D.UID

    Public Sub Draw(G As Graphics, GO As GraphicsOpts) Implements Type_2D.Draw
        'grab all the points, find the center and 
        'rotate the world round the center
        ' Dim pts() As PointF = Me.Points
        Dim c As PointF = _center
        Drawlib.RotateAround(G, c.X, c.Y, _totalRotate)
        Using f As New Font(DrawingOpts.FontName, DrawingOpts.FontSize, DrawingOpts.FontStyle), strf As New StringFormat(), fc As New SolidBrush(DrawingOpts.LineColor)
            _Size = G.MeasureString(Me.Text, f)
            _Size = _Size.Scale(1.1)

            'draw the current text with the current options
            Dim r As New Rectangle(c.X - _Size.Width / 2, c.Y - _Size.Height / 2, _Size.Width, _Size.Height)

            strf.Alignment = DrawingOpts.FontAlignment
            G.DrawString(Me.Text, f, fc, r, strf)


            'if the item is still selected draw a highlight box
            'around the item
            If Selected Then
                Using fx As New Pen(Globals.Highlight)
                    G.DrawRectangle(fx, r)
                End Using
            End If
        End Using

        'rotate the world back around the item
        Drawlib.RotateAround(G, c.X, c.Y, -_totalRotate)
    End Sub

    Public Shadows Function HitTest(pt As PointF, zoomScale As Single) As Boolean Implements Type_2D.HitTest
        Dim pts() As PointF = getrotatedrecpoints()
        Return Geometry.WithInPolygon(pt, pts)
    End Function

    ''' <summary>
    ''' this is the bounding rectangle that fits the whole inner rectangle no matter what angle
    ''' </summary>
    ''' <returns></returns>
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
        'Dim c As PointF = _center
        'Dim s As SizeF = _Size.Scale(0.5)
        'Dim pts() As PointF = {New PointF(c.X - s.Width, c.Y - s.Height),
        '                        New PointF(c.X + s.Width, c.Y - s.Height),
        '                        New PointF(c.X + s.Width, c.Y + s.Height),
        '                        New PointF(c.X - s.Width, c.Y + s.Height)}
        Dim r As RectangleF = Geometry.GetRecFrmCntrPt(_center, _Size)
        Dim pts() As PointF = r.ToPoints

        Return pts.AtAngle(_center, _totalRotate)
    End Function

    Private Sub Type_2D_Rotate(center As PointF, angle As Double) Implements Type_2D.Rotate
        Dim pts() As PointF = getrotatedrecpoints()
        pts = pts.AtAngle(center, angle)

        're-adjust the numbers
        _center = Geometry.GetBoundingRecf(pts).Center
        _totalRotate += angle 'Geometry.FindDegrees(_center, pts(0))
    End Sub

    Private Sub Type_2D_ApplyOffset(pointoffset As PointF) Implements Type_2D.ApplyOffset
        _center.X += pointoffset.X
        _center.Y += pointoffset.Y
    End Sub

    Public Sub Resize(newBoundingRecSize As RectangleF) Implements Type_2D.Resize
        'this does nothing for text
    End Sub

    Public Sub SetCenter(c As PointF)
        _center = c
    End Sub


    Public Shadows Sub FlipVert(BoundingRec As RectangleF) Implements Type_2D.FlipVert
        Dim pt As New PointF(_center.X, BoundingRec.Bottom - _center.Y)
        _center = pt
    End Sub

    Public Shadows Sub FlipHorz(BoundingRec As RectangleF) Implements Type_2D.FlipHorz
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
