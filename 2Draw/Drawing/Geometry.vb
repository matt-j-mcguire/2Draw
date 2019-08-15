

Public Module Geometry

    ''' <summary>
    ''' turns two points coords into a recangle
    ''' </summary>
    ''' <param name="pt1">any given point</param>
    ''' <param name="pt2">any other given point</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecFrmPts(ByVal pt1 As Point, ByVal pt2 As Point) As Rectangle
        Dim x, y, w, h As Integer

        If pt1.X < pt2.X Then
            x = pt1.X
            w = pt2.X - pt1.X
        Else
            x = pt2.X
            w = pt1.X - pt2.X
        End If

        If pt1.Y < pt2.Y Then
            y = pt1.Y
            h = pt2.Y - pt1.Y
        Else
            y = pt2.Y
            h = pt1.Y - pt2.Y
        End If


        Return New Rectangle(x, y, w, h)
    End Function


    Public Function GetRecFrmCntrPt(center As PointF, size As SizeF) As RectangleF
        Return New RectangleF(center.X - size.Width / 2, center.Y - size.Height / 2, size.Width, size.Height)
    End Function

    ''' <summary>
    ''' turns two points coords into a recangle
    ''' </summary>
    ''' <param name="pt1">any given point</param>
    ''' <param name="pt2">any other given point</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecFrmPts(ByVal pt1 As PointF, ByVal pt2 As PointF, Optional PerfectRec As Boolean = False) As RectangleF
        Dim x, y, w, h As Integer

        If pt1.X < pt2.X Then
            x = pt1.X
            w = pt2.X - pt1.X
        Else
            x = pt2.X
            w = pt1.X - pt2.X
        End If

        If pt1.Y < pt2.Y Then
            y = pt1.Y
            h = pt2.Y - pt1.Y
        Else
            y = pt2.Y
            h = pt1.Y - pt2.Y
        End If

        If PerfectRec Then
            Dim min As Single = Math.Min(w, h)
            w = min
            h = min
        End If

        Return New RectangleF(x, y, w, h)
    End Function

    ''' <summary>
    ''' finds the closest value to the incrementer
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="incrementer"></param>
    ''' <returns></returns>
    Public Function FindClosest(value As Single, incrementer As Single) As Integer
        Dim b As Integer = (value \ incrementer)
        Dim min As Integer = b * incrementer
        Dim max As Integer = (b + 1) * incrementer
        If Math.Abs(value - min) < Math.Abs(max - value) Then
            Return min
        Else
            Return max
        End If
    End Function

    Public Function FindSlope(pt1 As Point, pt2 As Point) As Double
        Dim y As Double = pt2.Y - pt1.Y
        Dim x As Double = pt2.X - pt1.X
        If y = 0 Then
            Return 0
        ElseIf x = 0 Then
            Return Double.MaxValue
        Else
            Return y / x
        End If


    End Function

    Public Function FindSlope(pt1 As PointF, pt2 As PointF) As Double
        Dim y As Double = pt2.Y - pt1.Y
        Dim x As Double = pt2.X - pt1.X
        If y = 0 Then
            Return 0
        ElseIf x = 0 Then
            Return Double.MaxValue
        Else
            Return y / x
        End If


    End Function

    ''' <summary>
    ''' returns the angle that the points are
    ''' </summary>
    ''' <param name="pt1">origin point</param>
    ''' <param name="pt2">point to map to</param>
    ''' <returns>andgle in degrees</returns>
    Public Function FindDegrees(pt1 As PointF, pt2 As PointF) As Double
        Dim dy As Double = pt1.Y - pt2.Y
        Dim dx As Double = pt1.X - pt2.X

        Return Math.Atan2(dy, dx) * (180 / Math.PI)
    End Function

    'Public Function CheckWithin(p1 As PointF, p2 As PointF, find As PointF, deadband As Double) As Boolean
    '    Dim v As Double = (p1.Y - p2.Y) * (p1.X - find.X) - (p1.Y - find.Y) * (p1.X - p2.X)

    '    If v <= deadband AndAlso v >= -deadband Then
    '        Dim d As Double = DistanceBetween(p1, p2)
    '        Dim d1 As Double = DistanceBetween(p1, find)
    '        Dim d2 As Double = DistanceBetween(p2, find)

    '        If d1 < d AndAlso d2 < d Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Else
    '        Return False
    '    End If
    'End Function

    Public Const FINDNOTFOUND As Single = Single.MaxValue

    ''' <summary>
    ''' closer to 0 the better, returns single.maxvalue if not where close
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <param name="p2"></param>
    ''' <param name="find"></param>
    ''' <returns></returns>
    Public Function CheckWithinSCL(p1 As PointF, p2 As PointF, find As PointF) As Single
        Dim v As Double = (p1.Y - p2.Y) * (p1.X - find.X) - (p1.Y - find.Y) * (p1.X - p2.X)
        Dim d As Double = DistanceBetween(p1, p2)
        Dim d1 As Double = DistanceBetween(p1, find)
        Dim d2 As Double = DistanceBetween(p2, find)

        If d1 <= d AndAlso d2 <= d Then
            Return d - (d1 + d2)
        Else
            Return FINDNOTFOUND
        End If
    End Function


    Public Function GetpointsAroundEllipse(rec As RectangleF, count As Integer) As PointF()
        Dim ret(count - 1) As PointF
        Dim a As Double = rec.Width / 2
        Dim b As Double = rec.Height / 2
        Dim c As PointF = rec.Center

        For i As Integer = 0 To count - 1
            Dim r As Double = i * (Math.PI * 2) / count
            ret(i).X = a * Math.Cos(r)
            ret(i).Y = b * Math.Sin(r)
            ret(i) = ret(i).Add(c)
        Next



        Return ret
    End Function

    Public Function WithInPolygon(pt As PointF, pts() As PointF) As Boolean
        Dim ret As Boolean = False
        Dim j As Integer = pts.Length - 1

        For i As Integer = 0 To pts.Length - 1
            If (pts(i).Y < pt.Y AndAlso pts(j).Y >= pt.Y OrElse pts(j).Y < pt.Y AndAlso pts(i).Y >= pt.Y) AndAlso (pts(i).X <= pt.X OrElse pts(j).X <= pt.X) Then
                If (pts(i).X + (pt.Y - pts(i).Y) / (pts(j).Y - pts(i).Y) * (pts(j).X - pts(i).X) < pt.X) Then
                    ret = Not ret
                End If
            End If
            j = i
        Next


        Return ret
    End Function

    Public Function RectangleFromCenter(center As PointF, size As SizeF) As RectangleF
        Return New RectangleF(center.X - size.Width / 2, center.Y - size.Height / 2, size.Width, size.Height)
    End Function


#Region "rectangle extentions"

    ''' <summary>
    ''' scales the dementions of a rectange, in all directions
    ''' </summary>
    ''' <param name="rec">the rect to scale</param>
    ''' <param name="scaleby"> decimal value to work with 1.0 = 100%</param>
    ''' <returns></returns>
    <Extension()> Public Function Scale(rec As Rectangle, scaleby As Double) As Rectangle
        Return New Rectangle(rec.X * scaleby, rec.Y * scaleby, rec.Width * scaleby, rec.Height * scaleby)
    End Function

    ''' <summary>
    ''' returns true if width or height = 0
    ''' </summary>
    ''' <param name="rec"></param>
    ''' <returns></returns>
    <Extension()> Public Function Invalid(rec As Rectangle) As Boolean
        Return rec.Width = 0 OrElse rec.Height = 0
    End Function

    ''' <summary>
    ''' returns true if width or height = 0
    ''' </summary>
    ''' <param name="rec"></param>
    ''' <returns></returns>
    <Extension()> Public Function Invalid(rec As RectangleF) As Boolean
        Return rec.Width = 0.0 OrElse rec.Height = 0.0
    End Function

    ''' <summary>
    ''' scales the dementions of a rectange, in all directions
    ''' </summary>
    ''' <param name="rec">the rect to scale</param>
    ''' <param name="scaleby"> decimal value to work with 1.0 = 100%</param>
    ''' <returns></returns>
    <Extension()> Public Function Scale(rec As RectangleF, scaleby As Double) As RectangleF
        Return New RectangleF(rec.X * scaleby, rec.Y * scaleby, rec.Width * scaleby, rec.Height * scaleby)
    End Function

    <Extension(), DebuggerStepThrough()> Public Function ToRec(rectf As RectangleF) As Rectangle
        Return New Rectangle(rectf.X, rectf.Y, rectf.Width, rectf.Height)
    End Function

    <Extension()> Public Function ToRecf(rect As Rectangle) As RectangleF
        Return New Rectangle(rect.X, rect.Y, rect.Width, rect.Height)
    End Function

    <Extension()> Public Function Center(rect As RectangleF) As PointF
        Return New PointF((rect.X + rect.Right) / 2, (rect.Y + rect.Bottom) / 2)
    End Function

    <Extension()> Public Function ToPoints(rect As RectangleF) As PointF()
        Dim p(3) As PointF
        p(0) = New PointF(rect.X, rect.Y)
        p(1) = New PointF(rect.Right, rect.Y)
        p(2) = New PointF(rect.Right, rect.Bottom)
        p(3) = New PointF(rect.X, rect.Bottom)
        Return p
    End Function

    <Extension()> Public Function ToPoints(rect As Rectangle) As Point()
        Dim p(3) As Point
        p(0) = New Point(rect.X, rect.Y)
        p(1) = New Point(rect.Right, rect.Y)
        p(2) = New Point(rect.Right, rect.Bottom)
        p(3) = New Point(rect.X, rect.Bottom)
        Return p
    End Function

    <Extension()> Public Function Center(rect As Rectangle) As Point
        Return New Point((rect.X + rect.Right) / 2, (rect.Y + rect.Bottom) / 2)
    End Function


    Public Enum RecSide
        none = 0
        left = 1
        right = 2
        top = 4
        bottom = 8
        topleft = top Or left
        topright = top Or right
        bottomright = bottom Or right
        bottomleft = bottom Or left
    End Enum

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="r">rectangle instance</param>
    ''' <param name="pt">point to find</param>
    ''' <param name="inflate">how much to inflate sides to see if point is close enought + and - (value of 3 is 6 total) </param>
    ''' <returns>what side or sides the point may be on</returns>
    <Extension()> Public Function PointTouches(r As Rectangle, pt As Point, inflate As Double) As RecSide
        Dim ls As New RectangleF(r.X - inflate, r.Y - inflate, inflate * 2, r.Height + inflate * 2)
        Dim rs As New Rectangle(r.Right - inflate, r.Y - inflate, inflate * 2, r.Height + inflate * 2)
        Dim ts As New Rectangle(r.X - inflate, r.Y - inflate, r.Width + inflate * 2, inflate * 2)
        Dim bs As New Rectangle(r.X - inflate, r.Bottom - inflate, r.Width + inflate * 2, inflate * 2)

        Dim ret As RecSide = RecSide.none
        If ls.Contains(pt) Then ret = ret Or RecSide.left
        If rs.Contains(pt) Then ret = ret Or RecSide.right
        If ts.Contains(pt) Then ret = ret Or RecSide.top
        If bs.Contains(pt) Then ret = ret Or RecSide.bottom
        Return ret
    End Function


    ''' <summary>
    ''' scales to fit and center one rectangle in to this one
    ''' </summary>
    ''' <param name="rec"></param>
    ''' <param name="recToScale"></param>
    ''' <returns></returns>
    <Extension> Public Function ScaleRecToFitCentered(rec As Rectangle, recToScale As Rectangle) As Rectangle
        Dim scale As SizeF
        scale.Width = rec.Width / recToScale.Width
        scale.Height = rec.Height / recToScale.Height

        'use the smaller of the two scales
        Dim sc As Single = If(scale.Width < scale.Height, scale.Width, scale.Height)
        recToScale.Width *= sc
        recToScale.Height *= sc

        'center the one rec to the other
        Dim offset As New Point(rec.Width / 2 - recToScale.Width / 2, rec.Height / 2 - recToScale.Height / 2)
        Return New Rectangle(rec.X + offset.X, rec.Y + offset.Y, recToScale.Width, recToScale.Height)
    End Function

    ''' <summary>
    ''' scales to fit and center one rectangle in to this one
    ''' </summary>
    ''' <param name="rec"></param>
    ''' <param name="recToScale"></param>
    ''' <returns></returns>
    <Extension> Public Function ScaleRecToFitCentered(rec As RectangleF, recToScale As RectangleF) As RectangleF
        Dim scale As SizeF
        scale.Width = rec.Width / recToScale.Width
        scale.Height = rec.Height / recToScale.Height

        'use the smaller of the two scales
        Dim sc As Single = If(scale.Width < scale.Height, scale.Width, scale.Height)
        recToScale.Width *= sc
        recToScale.Height *= sc

        'center the one rec to the other
        Dim offset As New PointF(rec.Width / 2 - recToScale.Width / 2, rec.Height / 2 - recToScale.Height / 2)
        Return New RectangleF(rec.X + offset.X, rec.Y + offset.Y, recToScale.Width, recToScale.Height)
    End Function

#End Region

#Region "point extentions"

    <Extension()> Public Function Scale(pt As Point, scaleby As Double) As Point
        Return New Point(pt.X * scaleby, pt.Y * scaleby)
    End Function

    <Extension()> Public Function Scale(pt As PointF, scaleby As Double) As PointF
        Return New PointF(pt.X * scaleby, pt.Y * scaleby)
    End Function

    <Extension()> Public Function ToPoint(pf As PointF) As Point
        Return New Point(pf.X, pf.Y)
    End Function

    <Extension()> Public Function ToPointf(p As Point) As PointF
        Return New PointF(p.X, p.Y)
    End Function

    <Extension(), DebuggerStepThrough()> Public Function toRecf(pt As PointF, size As Double) As RectangleF
        Return toRecf(pt, New Size(size, size))
    End Function

    <Extension()> Public Function toRecf(pt As PointF, size As SizeF) As RectangleF
        Return New RectangleF(pt.X - size.Width / 2, pt.Y - size.Height / 2, size.Width, size.Height)
    End Function

    <Extension()> Public Function toRec(pt As Point, size As Double) As Rectangle
        Return toRec(pt, New Size(size, size))
    End Function

    <Extension()> Public Function toRec(pt As Point, size As SizeF) As Rectangle
        Return New Rectangle(pt.X - size.Width / 2, pt.Y - size.Height / 2, size.Width, size.Height)
    End Function

    <Extension()> Public Function IsWithin(pt As PointF, size As Double, MatchPt As PointF) As Boolean
        Return IsWithin(pt, New SizeF(size, size), MatchPt)
    End Function

    <Extension()> Public Function IsWithin(pt As PointF, size As SizeF, MatchPt As PointF) As Boolean
        Dim s As New SizeF(size.Width / 2, size.Height / 2)
        Dim ret As Boolean = False
        If pt.X - s.Width <= MatchPt.X AndAlso pt.X + s.Width >= MatchPt.X AndAlso pt.Y - s.Height <= MatchPt.Y AndAlso pt.Y + s.Height >= MatchPt.Y Then ret = True
        Return ret
    End Function

    <Extension()> Public Function IsWithin(pt As Point, size As Double, MatchPt As Point) As Boolean
        Return IsWithin(pt, New Size(size, size), MatchPt)
    End Function

    <Extension()> Public Function IsWithin(pt As Point, size As SizeF, MatchPt As Point) As Boolean
        Dim s As New Size(size.Width / 2, size.Height / 2)
        Dim ret As Boolean = False
        If pt.X - s.Width <= MatchPt.X AndAlso pt.X + s.Width >= MatchPt.X AndAlso pt.Y - s.Height <= MatchPt.Y AndAlso pt.Y + s.Height >= MatchPt.Y Then ret = True
        Return ret
    End Function

    <Extension()> Public Function Add(pt As PointF, addr As PointF) As PointF
        Dim ret As New PointF(pt.X + addr.X, pt.Y + addr.Y)
        Return ret
    End Function

    <Extension()> Public Function Subtract(pt As PointF, remove As PointF) As PointF
        Dim ret As New PointF(pt.X - remove.X, pt.Y - remove.Y)
        Return ret
    End Function

    <Extension()> Public Function Add(pt As Point, addr As Point) As Point
        Dim ret As New Point(pt.X + addr.X, pt.Y + addr.Y)
        Return ret
    End Function

    <Extension()> Public Function Subtract(pt As Point, remove As Point) As Point
        Dim ret As New Point(pt.X - remove.X, pt.Y - remove.Y)
        Return ret
    End Function

    ''' <summary>
    ''' caculates the distance between two points
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <param name="p2"></param>
    ''' <returns></returns>
    <Extension()> Public Function DistanceBetween(p1 As PointF, p2 As PointF) As Double
        Return Math.Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2)
    End Function

    ''' <summary>
    ''' caculates the distance between two points
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <param name="p2"></param>
    ''' <returns></returns>
    <Extension()> Public Function DistanceBetween(p1 As Point, p2 As Point) As Integer
        Return Math.Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2)
    End Function

    ''' <summary>
    ''' caculates a point in it's new location because of a rotation
    ''' </summary>
    ''' <param name="P">location of the origional point</param>
    ''' <param name="cent">location of the center of the rotation</param>
    ''' <param name="angle">angle of the rotation</param>
    ''' <returns></returns>
    <Extension()> Public Function AtAngle(P As PointF, cent As PointF, angle As Double) As PointF
        Dim theta As Double = angle * Math.PI / 180

        Dim t As PointF = P
        Dim r As PointF
        t.X -= cent.X
        t.Y -= cent.Y

        r.X = t.X * Math.Cos(theta) - t.Y * Math.Sin(theta)
        r.Y = t.X * Math.Sin(theta) + t.Y * Math.Cos(theta)

        r.X += cent.X
        r.Y += cent.Y

        Return r
    End Function

    <Extension()> Public Sub Rotate(ByRef p As PointF, cent As PointF, angle As Double)
        p = AtAngle(p, cent, angle)
    End Sub

    ''' <summary>
    ''' caculates a point in it's new location because of a rotation
    ''' </summary>
    ''' <param name="P">location of the origional point</param>
    ''' <param name="cent">location of the center of the rotation</param>
    ''' <param name="angle">angle of the rotation</param>
    ''' <returns></returns>
    <Extension()> Public Function AtAngle(P As Point, cent As Point, angle As Double) As Point
        Dim theta As Double = angle * Math.PI / 180

        Dim t As PointF = P
        Dim r As PointF
        t.X -= cent.X
        t.Y -= cent.Y

        r.X = t.X * Math.Cos(theta) - t.Y * Math.Sin(theta)
        r.Y = t.X * Math.Sin(theta) + t.Y * Math.Cos(theta)

        r.X += cent.X
        r.Y += cent.Y

        Return r.ToPoint
    End Function
    <Extension()> Public Sub Rotate(ByRef p As Point, cent As Point, angle As Double)
        p = AtAngle(p, cent, angle)
    End Sub

    ''' <summary>
    ''' converts a group of points to the new location
    ''' </summary>
    ''' <param name="p"></param>
    ''' <param name="cent"></param>
    ''' <param name="angle"></param>
    ''' <returns></returns>
    <Extension()> Public Function AtAngle(p() As PointF, cent As PointF, angle As Double) As PointF()
        Dim pf(p.Length - 1) As PointF

        For i As Integer = 0 To p.Length - 1
            pf(i) = p(i).AtAngle(cent, angle)
        Next
        Return pf
    End Function

    <Extension()> Public Sub Rotate(ByRef p() As PointF, cent As PointF, angle As Double)
        p = AtAngle(p, cent, angle)
    End Sub

    ''' <summary>
    ''' converts a group of points to the new location
    ''' </summary>
    ''' <param name="p"></param>
    ''' <param name="cent"></param>
    ''' <param name="angle"></param>
    ''' <returns></returns>
    <Extension()> Public Function AtAngle(p() As Point, cent As Point, angle As Double) As Point()
        Dim pf(p.Length - 1) As Point

        For i As Integer = 0 To p.Length - 1
            pf(i) = p(i).AtAngle(cent, angle)
        Next
        Return pf
    End Function

    <Extension()> Public Sub Rotate(ByRef p() As Point, cent As Point, angle As Double)
        p = AtAngle(p, cent, angle)
    End Sub

    <Extension()> Public Function GetBoundingRecf(p() As PointF) As RectangleF
        Dim mx As PointF = p(0)
        Dim mn As PointF = p(0)

        For i As Integer = 1 To p.Length - 1
            'check for the minimum point
            If p(i).X < mn.X Then mn.X = p(i).X
            If p(i).Y < mn.Y Then mn.Y = p(i).Y
            'check for the maximum point
            If p(i).X > mx.X Then mx.X = p(i).X
            If p(i).Y > mx.Y Then mx.Y = p(i).Y
        Next

        'return a new rectangle
        Dim r As New RectangleF(mn.X, mn.Y, mx.X - mn.X, mx.Y - mn.Y)
        Return r

    End Function

    <Extension()> Public Function GetBoundingRecf(p() As Point) As Rectangle
        Dim mx As Point = p(0)
        Dim mn As Point = p(0)

        For i As Integer = 1 To p.Length - 1
            'check for the minimum point
            If p(i).X < mn.X Then mn.X = p(i).X
            If p(i).Y < mn.Y Then mn.Y = p(i).Y
            'check for the maximum point
            If p(i).X > mx.X Then mx.X = p(i).X
            If p(i).Y > mx.Y Then mx.Y = p(i).Y
        Next

        'return a new rectangle
        Dim r As New Rectangle(mn.X, mn.Y, mx.X - mn.X, mx.Y - mn.Y)
        Return r

    End Function

    <Extension()> Public Function Scale(P() As PointF, scaleX As Double, scaleY As Double) As PointF()
        Dim ret(P.Length - 1) As PointF
        For i As Integer = 0 To P.Length - 1
            ret(i) = New PointF(P(i).X * scaleX, P(i).Y * scaleY)
        Next
        Return ret
    End Function

    <Extension()> Public Function Scale(P As PointF, scaleX As Double, scaleY As Double) As PointF
        Return New PointF(P.X * scaleX, P.Y * scaleY)
    End Function



#End Region

#Region "size extentions"

    <Extension()> Public Function Scale(sz As Size, scaleby As Double) As Size
        Return New Size(sz.Width * scaleby, sz.Height * scaleby)
    End Function

    <Extension()> Public Function Scale(sz As SizeF, scaleby As Double) As SizeF
        Return New SizeF(sz.Width * scaleby, sz.Height * scaleby)
    End Function

    <Extension()> Public Function ToSize(szf As SizeF) As Size
        Return New Size(szf.Width, szf.Height)
    End Function

    <Extension()> Public Function ToSizef(sz As Size) As SizeF
        Return New SizeF(sz.Width, sz.Height)
    End Function

    <Extension()> Public Function IsLessThan(sz As Size, otherSzie As Size) As Boolean
        Return sz.Width < otherSzie.Width AndAlso sz.Height < otherSzie.Height
    End Function

    <Extension()> Public Function IsLessThan(sz As SizeF, otherSzie As SizeF) As Boolean
        Return sz.Width < otherSzie.Width AndAlso sz.Height < otherSzie.Height
    End Function

    <Extension()> Public Function IsGreaterThan(sz As Size, otherSzie As Size) As Boolean
        Return sz.Width > otherSzie.Width AndAlso sz.Height > otherSzie.Height
    End Function

    <Extension()> Public Function IsGreaterThan(sz As SizeF, otherSzie As SizeF) As Boolean
        Return sz.Width > otherSzie.Width AndAlso sz.Height > otherSzie.Height
    End Function

    ''' <summary>
    ''' this will return the upper corner of one rec centered in the other rec
    ''' </summary>
    ''' <param name="sz">larger rec</param>
    ''' <param name="othersize">smaller rec</param>
    ''' <returns></returns>
    <Extension()> Public Function ToCenterifRecs(sz As Size, othersize As Size) As Point
        Dim x As Integer = sz.Width / 2 - othersize.Width / 2
        Dim y As Integer = sz.Height / 2 - othersize.Height / 2
        Return New Point(x, y)
    End Function

    ''' <summary>
    ''' this will return the upper corner of one rec centered in the other rec
    ''' </summary>
    ''' <param name="sz">larger rec</param>
    ''' <param name="othersize">smaller rec</param>
    ''' <returns></returns>
    <Extension()> Public Function ToCenterifRecs(sz As SizeF, othersize As SizeF) As PointF
        Dim x As Single = sz.Width / 2 - othersize.Width / 2
        Dim y As Single = sz.Height / 2 - othersize.Height / 2
        Return New PointF(x, y)
    End Function

#End Region

    <Extension()> Public Function IsBetween(v As Single, high As Single, low As Single) As Boolean
        Return v <= high AndAlso v >= low
    End Function

    <Extension()> Public Function IsBetween(v As Double, high As Double, low As Double) As Boolean
        Return v <= high AndAlso v >= low
    End Function

    <Extension()> Public Function IsBetween(v As Integer, high As Integer, low As Integer) As Boolean
        Return v <= high AndAlso v >= low
    End Function

#Region "other math"

    <DebuggerStepThrough()> Public Function Limits(value As Point, min As Point, max As Point) As Point
        value.X = Limits(value.X, min.X, max.X)
        value.Y = Limits(value.Y, min.Y, max.Y)
        Return value
    End Function

    <DebuggerStepThrough()> Public Function Limits(value As PointF, min As PointF, max As PointF) As PointF
        value.X = Limits(value.X, min.X, max.X)
        value.Y = Limits(value.Y, min.Y, max.Y)
        Return value
    End Function

    <DebuggerStepThrough()> Public Function Limits(value As Size, min As Size, max As Size) As Size
        value.Width = Limits(value.Width, min.Width, max.Width)
        value.Height = Limits(value.Height, min.Height, max.Height)
        Return value
    End Function

    <DebuggerStepThrough()> Public Function Limits(value As SizeF, min As SizeF, max As SizeF) As SizeF
        value.Width = Limits(value.Width, min.Width, max.Width)
        value.Height = Limits(value.Height, min.Height, max.Height)
        Return value
    End Function

    <DebuggerStepThrough()> Public Function Limits(value As Double, min As Double, max As Double) As Double
        If value < min Then value = min
        If value > max Then value = max
        Return value
    End Function

    <DebuggerStepThrough()> Public Function Limits(value As Single, min As Single, max As Single) As Single
        If value < min Then value = min
        If value > max Then value = max
        Return value
    End Function

    <DebuggerStepThrough()> Public Function Limits(value As Integer, min As Integer, max As Integer) As Integer
        If value < min Then value = min
        If value > max Then value = max
        Return value
    End Function

#End Region

End Module