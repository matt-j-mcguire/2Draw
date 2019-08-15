
Imports _2Draw

Public Class Line_2D
    Inherits LinkedPoints
    Implements Type_2D

    Public Class Connector

        ''' <summary>
        ''' creates a new instance of a connector
        ''' </summary>
        ''' <param name="ndata"></param>
        ''' <returns>will return nothing if not valid</returns>
        Public Shared Function create(ndata As String)
            Dim ret As Connector = Nothing
            If Not String.IsNullOrEmpty(ndata) Then
                Dim s() As String = ndata.Split("|")
                If s.Length = 3 Then
                    ret = New Connector
                    ret.otherUID = s(0)
                    ret.NearestPoint = CInt(s(1))
                    ret.DistancefromPT = CDbl(s(2))
                End If
            End If

            Return ret
        End Function

        ''' <summary>
        ''' for saveing the connector info back to file
        ''' </summary>
        ''' <returns></returns>
        Public Function savestring() As String
            Return $"{otherUID}|{NearestPoint}|{DistancefromPT}"
        End Function

        ''' <summary>
        ''' what the other items connection might be
        ''' </summary>
        Public otherUID As String
        ''' <summary>
        ''' what the closest point to the connection is
        ''' </summary>
        Public NearestPoint As Integer
        ''' <summary>
        ''' percentage from the point (+ is after point, - in befor point)
        ''' </summary>
        Public DistancefromPT As Double
    End Class

    Public Sub New()
        MyBase.New()
        DrawingOpts = Globals.current.clone
        _UID = DisplayItem.CreateUID
        _ItemType = ItemType.Line
    End Sub

    Public Sub New(existing As LinkedPoints, Opts As drawingOpts, startconn As Connector, endconn As Connector)
        MyBase.New(existing)
        DrawingOpts = Opts
        _UID = DisplayItem.CreateUID
        _ItemType = ItemType.Line
        StartConnector = startconn
        EndConnector = EndConnector
    End Sub

    Public Sub New(thenode As XmlNode)
        MyBase.New()
        DrawingOpts = drawingOpts.Load(thenode)
        _UID = XHelper.Get(thenode, "UID")
        _ItemType = ItemType.Line
        Me.LoadFromString(XHelper.Get(thenode, "points"))
        StartConnector = Connector.create(XHelper.Get(thenode, "startconn", ""))
        EndConnector = Connector.create(XHelper.Get(thenode, "endconn", ""))
    End Sub

    Public Sub Save(useNode As XmlNode) Implements Type_2D.Save
        DrawingOpts.Save(useNode)
        XHelper.Set(useNode, "points", Me.SaveToString)
        XHelper.Set(useNode, "UID", UID)
        If StartConnector IsNot Nothing Then XHelper.Set(useNode, "startconn", StartConnector.savestring)
        If EndConnector IsNot Nothing Then XHelper.Set(useNode, "endconn", EndConnector.savestring)
    End Sub

    ''' <summary>
    ''' gets or sets the start connector to link the line
    ''' </summary>
    ''' <returns>returns nothing if not set</returns>
    Public Property StartConnector() As Connector

    ''' <summary>
    ''' gets or sets the end connector to link the line
    ''' </summary>
    ''' <returns>returns nothing if not set</returns>
    Public Property EndConnector() As Connector

    Public Property DrawingOpts As drawingOpts Implements Type_2D.DrawingOpts

    Public ReadOnly Property ItemType As ItemType Implements Type_2D.ItemType

    Public Property Selected As Boolean Implements Type_2D.Selected

    Public Property UID() As String Implements Type_2D.UID

    Public Sub Draw(G As Graphics, GO As GraphicsOpts) Implements Type_2D.Draw
        Dim pts() As PointF = Me.Points
        Dim wd As Single = If(ShowNormalLineWidth, 1.0, GO.InverseZoom)

        Using p As New Pen(DrawingOpts.LineColor, DrawingOpts.LineWidth * wd), sc As CustomLineCap = endCapType.GetCap(DrawingOpts.LineStartCap), ec As CustomLineCap = endCapType.GetCap(DrawingOpts.LineEndCap)
            p.DashStyle = DrawingOpts.LineDash
            p.StartCap = LineCap.Custom
            p.CustomStartCap = sc
            p.LineJoin = LineJoin.Round
            p.EndCap = LineCap.Custom
            p.CustomEndCap = ec

            If ClosedFigure Then
                Using s As New GraphicsPath()
                    s.AddLines(pts)
                    s.CloseFigure()
                    G.DrawPath(p, s)
                End Using
            Else
                Try
                    If pts.Length > 1 Then
                        G.DrawLines(p, pts)
                    End If
                Catch ex As Exception
                End Try
            End If


            If Selected Then Drawlib.DrawAllPoints(G, pts, p, 5 * GO.InverseZoom)
        End Using
    End Sub

    Public Shadows Function HitTest(pt As PointF, zoomScale As Single) As Boolean Implements Type_2D.HitTest
        Return MyBase.HittestSCL(pt, zoomScale).IsBetween(1, -1)
    End Function

    Public Function GetRecf() As RectangleF Implements Type_2D.GetRecf
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
