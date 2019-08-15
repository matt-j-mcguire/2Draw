
Imports _2Draw

Public Class Shape_2D
    Inherits LinkedPoints
    Implements Type_2D

    Public Sub New()
        MyBase.New()
        DrawingOpts = Globals.current.clone
        _UID = DisplayItem.CreateUID
        _ItemType = ItemType.Shape
    End Sub

    Public Sub New(existing As LinkedPoints, Opts As drawingOpts)
        MyBase.New(existing)
        DrawingOpts = Opts
        _UID = DisplayItem.CreateUID
        _ItemType = ItemType.Shape
    End Sub

    Public Sub New(thenode As XmlNode)
        MyBase.New()
        DrawingOpts = drawingOpts.Load(thenode)
        _UID = XHelper.Get(thenode, "UID")
        _ItemType = ItemType.Shape
        Me.LoadFromString(XHelper.Get(thenode, "points"))
    End Sub

    Public Sub Save(useNode As XmlNode) Implements Type_2D.Save
        DrawingOpts.Save(useNode)
        XHelper.Set(useNode, "points", Me.SaveToString)
        XHelper.Set(useNode, "UID", UID)
    End Sub


    Public Property DrawingOpts As drawingOpts Implements Type_2D.DrawingOpts

    Public ReadOnly Property ItemType As ItemType Implements Type_2D.ItemType

    Public Property Selected As Boolean Implements Type_2D.Selected

    Public Property UID() As String Implements Type_2D.UID

    Public Sub Draw(G As Graphics, GO As GraphicsOpts) Implements Type_2D.Draw
        Dim pts() As PointF = Me.Points
        Dim wd As Single = If(ShowNormalLineWidth, 1.0, GO.InverseZoom)

        Using p As New Pen(DrawingOpts.LineColor, DrawingOpts.LineWidth * wd)
            p.DashStyle = DrawingOpts.LineDash

            Using s As New GraphicsPath()
                s.AddLines(pts)
                If ClosedFigure Then s.CloseFigure()
                If DrawingOpts.FillColor <> Color.Transparent Then
                    If DrawingOpts.FillColor <> Color.Transparent Then
                        If DrawingOpts.FillHatch > -1 Then
                            Using br As New HatchBrush(DrawingOpts.FillHatch, DrawingOpts.FillColor, Color.Transparent)
                                G.FillPath(br, s)
                            End Using
                        Else
                            Using br As New SolidBrush(DrawingOpts.FillColor)
                                G.FillPath(br, s)
                            End Using
                        End If

                    End If

                End If
                G.DrawPath(p, s)


            End Using


            If Selected Then Drawlib.DrawAllPoints(G, pts, p, 5 * GO.InverseZoom)
        End Using
    End Sub

    Public Shadows Function HitTest(pt As PointF, zoomScale As Single) As Boolean Implements Type_2D.HitTest
        Return MyBase.HittestSCL(pt, zoomScale).IsBetween(1, -1)
    End Function

    <DebuggerStepThrough()> Public Function GetRecf() As RectangleF Implements Type_2D.GetRecf
        Return MyBase.GetBoundingRecf
    End Function

    Private Sub Type_2D_Rotate1(center As PointF, angle As Double) Implements Type_2D.Rotate
        MyBase.Rotate(center, angle)
    End Sub

    Private Sub Type_2D_ApplyOffset1(pointoffset As PointF) Implements Type_2D.ApplyOffset
        MyBase.ApplyOffset(pointoffset.ToPoint)
    End Sub

    Public Sub Resize(newBoundingRecSize As RectangleF) Implements Type_2D.Resize
        Dim oldr As RectangleF = GetRecf()
        If newBoundingRecSize.Width < 1 Then newBoundingRecSize.Width = 1
        If newBoundingRecSize.Height < 1 Then newBoundingRecSize.Height = 1
        'find out the scale change
        Dim w As Double = 1 - (oldr.Width / newBoundingRecSize.Width) + 1
        Dim h As Double = 1 - (oldr.Height / newBoundingRecSize.Height) + 1

        MyBase.ScalePoints(oldr.Location, w, h)
    End Sub

    Public Shadows Sub FlipVert(BoundingRec As RectangleF) Implements Type_2D.FlipVert
        MyBase.FlipVert(BoundingRec)
    End Sub

    Public Shadows Sub FlipHorz(BoundingRec As RectangleF) Implements Type_2D.FlipHorz
        MyBase.FlipHorz(BoundingRec)
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
