


Public Class LinkedPoints

    Public Sub New()
    End Sub


    Public Sub New(existing As LinkedPoints)
        Me._First = existing.First
        Me._Last = existing.Last
        Me.Count = existing.Count
    End Sub


    ''' <summary>
    ''' the first item in the double linked list
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property First As Lpt

    ''' <summary>
    ''' the last item in the double linked list
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Last As Lpt

    ''' <summary>
    ''' returns a count of all the items
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer


#Region "Add/Remove points"


    ''' <summary>
    ''' adds an item to the linked list
    ''' </summary>
    ''' <param name="pt"></param>
    Public Sub Add(pt As Lpt)
        If First IsNot Nothing Then
            If pt.x = _Last.x AndAlso pt.y = _Last.y Then
                'do nothing
            Else
                'add to the end of the list
                Dim oldnext As Lpt = _Last.Next

                _Last.Next = pt
                pt.Prevous = _Last
                _Last = pt

                If oldnext IsNot Nothing Then
                    pt.Next = oldnext 'just incase the line comes back to the begnining
                    If oldnext.Prevous IsNot Nothing Then oldnext.Prevous = pt
                End If
            End If
        Else
            'the first one added to the list
            _First = pt
            _Last = pt
        End If
        _Count += 1
    End Sub

    ''' <summary>
    ''' adds an item to the linked list
    ''' </summary>
    ''' <param name="pt"></param>
    Public Sub Add(pt As PointF)
        Dim p As New Lpt(pt)
        Add(p)
    End Sub

    Public Sub Add(x As Single, y As Single)
        Dim p As New Lpt(x, y)
        Add(p)
    End Sub

    ''' <summary>
    ''' adds a range of items to the list
    ''' </summary>
    ''' <param name="pt"></param>
    Public Sub AddRange(pt() As Lpt)
        For i As Integer = 0 To pt.Length - 1
            Me.Add(pt(i))
        Next
    End Sub

    ''' <summary>
    ''' adds a range of items to the list
    ''' </summary>
    ''' <param name="pt"></param>
    Public Sub AddRange(pt() As PointF)
        For i As Integer = 0 To pt.Length - 1
            Me.Add(New Lpt(pt(i)))
        Next
    End Sub

    ''' <summary>
    ''' inserts an item after an existing point
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <param name="existingPt"></param>
    Public Sub InsertAfter(pt As Lpt, existingPt As Lpt)
        If Belongs(existingPt) Then
            If existingPt Is Last OrElse First Is Nothing Then
                'check for the special case of last
                Add(pt)
            Else
                'added somewhere in the middle
                Dim nxt As Lpt = existingPt.Next
                pt.Next = nxt
                pt.Prevous = existingPt
                nxt.Prevous = pt
                existingPt.Next = pt
            End If
            _Count += 1
        End If
    End Sub

    ''' <summary>
    ''' inserts an item before and existing point
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <param name="existingPt"></param>
    Public Sub InsertBefore(pt As Lpt, existingPt As Lpt)
        If Belongs(existingPt) Then
            If First Is Nothing Then
                'check for special case of no start
                Add(pt)
            ElseIf existingPt Is First Then
                'check for the special case of first position
                pt.Next = _First
                pt.Prevous = _First.Prevous
                _First = pt
                If pt.Prevous IsNot Nothing Then pt.Prevous.Next = pt
            Else
                'added somewhere in the middle
                Dim prv As Lpt = existingPt.Prevous
                pt.Next = existingPt
                pt.Prevous = prv
                prv.Next = pt
                existingPt.Prevous = pt
            End If
            _Count += 1
        End If
    End Sub

    ''' <summary>
    ''' clear the list 
    ''' </summary>
    Public Sub Clear()
        _First = Nothing
        _Last = Nothing
        _Count = 0
    End Sub

    ''' <summary>
    ''' removes a point from the list
    ''' </summary>
    ''' <param name="pt"></param>
    Public Sub Remove(pt As Lpt)
        If Belongs(pt) Then
            'reassign the links
            If pt.Prevous IsNot Nothing Then pt.Prevous.Next = pt.Next
            If pt.Next IsNot Nothing Then pt.Next.Prevous = pt.Prevous

            If First Is pt AndAlso Last Is pt Then
                Me.Clear()
            ElseIf pt Is First Then
                _First = pt.Next
            ElseIf pt Is Last Then
                _Last = pt.Prevous
            End If

            _Count -= 1
        End If
    End Sub



#End Region

#Region "Links"

    ''' <summary>
    ''' if a link removed "breaks" a list in two, a new collection 
    ''' will be returned with the broken off part
    ''' </summary>
    ''' <param name="Lpt1">the first point to remove between</param>
    ''' <param name="Lpt2">the second point to remove between</param>
    ''' <returns>if the two points are not linked, this is ignored</returns>
    Public Function RemoveLink(Lpt1 As Lpt, Lpt2 As Lpt) As LinkedPoints
        Dim ret As LinkedPoints = Nothing
        Dim wasclosed As Boolean = ClosedFigure
        InOrder(Lpt1, Lpt2)

        If HasLink(Lpt1, Lpt2) Then
            Lpt1.Next = Nothing
            Lpt2.Prevous = Nothing

            If wasclosed Then
                'this was a closed shape just reset the location of the 
                'first and last items
                _First = Lpt2
                _Last = Lpt1
            Else
                'the shape was already broken, create a new list with the remainer
                Dim oldend As Lpt = _Last
                _Last = Lpt1
                ret = New LinkedPoints()
                ret._First = Lpt2
                ret._Last = oldend
                ret.RegenCount()
            End If
            Me.RegenCount()
        End If

        Return ret
    End Function

    ''' <summary>
    ''' will return true if they link to each other. make sure in order before calling
    ''' </summary>
    ''' <param name="lpt1"></param>
    ''' <param name="lpt2"></param>
    ''' <returns></returns>
    Public Function HasLink(lpt1 As Lpt, lpt2 As Lpt) As Boolean
        Return lpt1.Next Is lpt2 AndAlso lpt2.Prevous Is lpt1
        'OrElse lpt1.Prevous Is lpt2 AndAlso lpt2.Next Is lpt1
    End Function

    ''' <summary>
    ''' gets/sets the status of the closed figure
    ''' </summary>
    ''' <returns></returns>
    Public Property ClosedFigure() As Boolean
        Get
            If Count > 1 Then
                If First.Prevous Is Last AndAlso Last.Next Is First Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value Then
                If Count > 1 Then
                    First.Prevous = Last
                    Last.Next = First
                End If
            Else
                If Count >= 1 Then
                    First.Prevous = Nothing
                    Last.Next = Nothing
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' splits the list into a seperate list, finalizing the markers
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <returns></returns>
    Public Function SplitStartingAt(pt As Lpt) As LinkedPoints
        'if we are splitting a list,it's not a closed figure
        ClosedFigure = False

        'create a new list, setting the first
        'as the pt and the last as my last
        Dim ret As New LinkedPoints()
        If Belongs(pt) Then
            ret._First = pt
            ret._Last = Me.Last
            ret.RegenCount()

            'set my last to pt's prevous item
            'and clear any next items from the list
            'this is the my end
            Me._Last = pt.Prevous
            Me._Last.Next = Nothing

            'clear the new points prevouse point from
            'my list
            pt.Prevous = Nothing
            Me.RegenCount()
        End If
        Return ret
    End Function


#End Region

#Region "other"

    ''' <summary>
    ''' swapps around points so they are in order
    ''' </summary>
    ''' <param name="lpt1"></param>
    ''' <param name="lpt2"></param>
    ''' <returns>return true if values were not swapped</returns>
    Public Function InOrder(ByRef lpt1 As Lpt, ByRef lpt2 As Lpt) As Boolean
        Dim p1 As Integer = IndexOf(lpt1)
        Dim p2 As Integer = IndexOf(lpt2)
        If p1 > p2 Then
            Dim t As Lpt = lpt1
            lpt1 = lpt2
            lpt2 = t
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' regenerates the item count
    ''' </summary>
    Private Sub RegenCount()
        Dim cnt As Integer = 0
        If First IsNot Nothing Then
            Dim l As Lpt = First
            cnt = 1 'marks the first one
            Do
                l = l.Next

                If l IsNot Nothing Then cnt += 1 Else Exit Do
                If l Is Last Then Exit Do
            Loop
        End If

        _Count = cnt
    End Sub

#End Region

#Region "points"

    ''' <summary>
    ''' returns if a point belong to this list
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <returns></returns>
    Public Function Belongs(pt As Lpt) As Boolean
        Dim ret As Boolean = False

        Dim l As Lpt = First
        Do
            If pt Is l Then
                ret = True
                Exit Do
            End If

            If l Is Last Then
                Exit Do
            Else
                l = l.Next
            End If
        Loop

        Return ret
    End Function

    ''' <summary>
    ''' returns the index of the items in hte list
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <returns>returns -1 if not found</returns>
    Public Function IndexOf(pt As Lpt) As Integer
        Dim ret As Integer = -1

        Dim l As Lpt = First
        Do
            ret += 1
            If pt Is l Then
                Exit Do
            End If

            If l Is Last Then
                ret = -1
                Exit Do
            Else
                l = l.Next
            End If
        Loop

        Return ret
    End Function

    ''' <summary>
    ''' returns an item based off of a index
    ''' </summary>
    ''' <param name="index"></param>
    ''' <returns></returns>
    Public Function Item(index As Integer) As Lpt
        Dim ret As Lpt = Nothing
        Dim cntr As Integer = -1

        Dim l As Lpt = First
        Do
            cntr += 1
            If cntr = index Then
                ret = l
                Exit Do
            End If

            If l Is Last Then
                Exit Do
            Else
                l = l.Next
            End If
        Loop

        Return ret
    End Function

    ''' <summary>
    ''' tries to find an item based off of a point
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function find(value As PointF, buff As Integer) As Lpt
        Return find(value, New Size(buff, buff))
    End Function

    ''' <summary>
    ''' tries to find an item based off of a point
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function find(value As PointF, buff As Size) As Lpt
        Dim ret As Lpt = Nothing

        Dim l As Lpt = First
        Do
            If l.Pointf.IsWithin(buff, value) Then
                ret = l
                Exit Do
            End If

            If l Is Last Then
                Exit Do
            Else
                l = l.Next
            End If
        Loop

        Return ret
    End Function

    ''' <summary>
    ''' returns a pointf array
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Points() As PointF()
        Get
            Dim ret As New List(Of PointF)
            Dim l As Lpt = First
            Do
                ret.Add(l.Pointf)
                l = l.Next
                If l Is Nothing OrElse l.Prevous Is Last Then Exit Do
            Loop
            Return ret.ToArray
        End Get
    End Property

    ''' <summary>
    ''' returns an array of all the points
    ''' </summary>
    ''' <returns></returns>
    Public Function toArray() As Lpt()
        Dim ret As New List(Of Lpt)
        Dim l As Lpt = First
        Do
            ret.Add(l)
            l = l.Next
            If l Is Nothing OrElse l.Prevous Is Last Then Exit Do
        Loop
        Return ret.ToArray
    End Function

    Public Function findlineSCL(pt As PointF, ByRef selpt1 As Lpt, ByRef selpt2 As Lpt, Optional deadscale As Single = 1.0) As Single
        Dim fnd As Boolean = False
        Dim ret As Single = FINDNOTFOUND


        Dim l As Lpt = First
        Do
            Dim n As Lpt = l.Next
            If n IsNot Nothing Then

                ret = CheckWithinSCL(l, n, pt)
                If ret <> FINDNOTFOUND AndAlso IsBetween(ret, 1 * deadscale, -1 * deadscale) Then
                    selpt1 = l
                    selpt2 = n
                    fnd = True
                    Exit Do
                End If
                If n Is First Then Exit Do
            Else
                Exit Do
            End If
            l = n
        Loop

        If Not fnd Then
            selpt1 = Nothing
            selpt2 = Nothing
        End If

        Return ret
    End Function

    ''' <summary>
    ''' this just calls findline, use if you are just finding out if you captured an item
    ''' </summary>
    ''' <param name="pt">point or pointf is fine for test</param>
    ''' <returns>close to 0 is good, single.maxvalue is invalid</returns>
    Public Function HittestSCL(pt As PointF, zoomScale As Single) As Single
        Dim s1 As Lpt = Nothing
        Dim s2 As Lpt = Nothing
        Return findlineSCL(pt, s1, s2, zoomScale)
    End Function

#End Region

#Region "Math for point data"

    ''' <summary>
    ''' converts a group of points to the new location
    ''' </summary>
    ''' <param name="cent"></param>
    ''' <param name="angle"></param>
    Public Sub Rotate(cent As PointF, angle As Double)
        Dim l As Lpt = First
        l.Rotate(cent, angle)
        Do
            l = l.Next
            If l Is Nothing Then Exit Do
            l.Rotate(cent, angle)
            If l Is Last Then Exit Do
        Loop

    End Sub

    Public Function GetBoundingRecf() As RectangleF
        Dim l As Lpt = First
        Dim mx As PointF = l.Pointf
        Dim mn As PointF = mx

        Do
            l = l.Next
            If l Is Nothing Then Exit Do
            'check for the minimum point
            If l.x < mn.X Then mn.X = l.x
            If l.y < mn.Y Then mn.Y = l.y
            'check for the maximum point
            If l.x > mx.X Then mx.X = l.x
            If l.y > mx.Y Then mx.Y = l.y
            If l Is Last Then Exit Do
        Loop

        'return a new rectangle
        Dim r As New RectangleF(mn.X, mn.Y, mx.X - mn.X, mx.Y - mn.Y)

        Return r

    End Function

    ''' <summary>
    ''' applys a point offset to all the points
    ''' </summary>
    ''' <param name="offset"></param>
    Public Sub ApplyOffset(offset As Point)
        Dim l As Lpt = First
        l.Add(offset)
        Do
            l = l.Next
            If l Is Nothing Then Exit Do
            l.Add(offset)
            If l Is Last Then Exit Do
        Loop
    End Sub

    Public Sub ScalePoints(basePt As PointF, ScaleX As Double, ScaleY As Double)

        Dim l As Lpt = First
        l.ScalePoint(basePt, ScaleX, ScaleY)
        Do
            l = l.Next
            If l Is Nothing Then Exit Do
            l.ScalePoint(basePt, ScaleX, ScaleY)
            If l Is Last Then Exit Do
        Loop
    End Sub

#End Region

#Region "Load/Save"


    ''' <summary>
    ''' tries to load the linked list from a string
    ''' </summary>
    ''' <param name="str">(15,25)>(25,65)>(,)
    ''' where (,) mean a link to origional position, and none is an open item</param>
    Public Sub LoadFromString(str As String)
        Dim r As New Regex("\((\d*\.*\d*),(\d*\.*\d*)\)")
        Dim m As MatchCollection = r.Matches(str)
        Me.Clear()

        For Each ma As Match In m
            If ma.Groups(1).Value <> "" AndAlso ma.Groups(2).Value <> "" Then
                Dim l As New Lpt(CSng(ma.Groups(1).Value), CSng(ma.Groups(2).Value))
                Add(l)
            Else
                ClosedFigure = True
                Exit For
            End If
        Next

    End Sub

    ''' <summary>
    ''' saves all the data to a string like: (15,25)>(25,65)>(,)
    ''' where (,) mean a link to origional position, and none is an open item
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveToString() As String
        Dim l() As Lpt = toArray()
        Dim str As New StringBuilder

        For i As Integer = 0 To l.Length - 1
            str.Append($"({l(i).x:f1},{l(i).y:f1})")
        Next
        If l.Last.Next IsNot Nothing Then str.Append("(,)")

        Return str.ToString
    End Function

#End Region


#Region "mirroring"


    Public Sub FlipVert(BoundingRec As RectangleF)
        Dim l As Lpt = First
        l.Pointf = invert(True, BoundingRec, l.Pointf)
        Do
            l = l.Next
            If l Is Nothing Then Exit Do
            l.Pointf = invert(True, BoundingRec, l.Pointf)
            If l Is Last Then Exit Do
        Loop

    End Sub


    Public Sub FlipHorz(BoundingRec As RectangleF)
        Dim l As Lpt = First
        l.Pointf = invert(False, BoundingRec, l.Pointf)
        Do
            l = l.Next
            If l Is Nothing Then Exit Do
            l.Pointf = invert(False, BoundingRec, l.Pointf)
            If l Is Last Then Exit Do
        Loop
    End Sub

    Public Function invert(isvert As Boolean, r As RectangleF, pt As PointF) As PointF
        Dim ret As PointF
        If isvert Then
            Dim b As Double = r.Bottom - pt.Y
            ret.X = pt.X
            ret.Y = r.Y + b
        Else
            Dim l As Double = r.Right - pt.X
            ret.Y = pt.Y
            ret.X = r.X + l
        End If
        Return ret
    End Function


#End Region


    Public Class Lpt

#Region "Creation"

        Public Sub New()
        End Sub

        Public Sub New(x As Single, y As Single)
            Me.x = x
            Me.y = y
        End Sub

        Public Sub New(p As PointF)
            x = p.X
            y = p.Y
        End Sub

#End Region

        Public x As Single
        Public y As Single

        Public [Next] As Lpt
        Public Prevous As Lpt

#Region "Conversion"

        ''' <summary>
        ''' gets or sets the value as a pointF
        ''' </summary>
        ''' <returns></returns>
        Public Property Pointf() As PointF
            Get
                Return New PointF(x, y)
            End Get
            Set(value As PointF)
                x = value.X
                y = value.Y
            End Set
        End Property

        ''' <summary>
        ''' gets or sets the value as a point
        ''' </summary>
        ''' <returns></returns>
        Public Property Point() As PointF
            Get
                Return New PointF(x, y)
            End Get
            Set(value As PointF)
                x = value.X
                y = value.Y
            End Set
        End Property

#End Region

#Region "Operators"

        Public Shared Narrowing Operator CType(l As Lpt) As PointF
            Return New PointF(l.x, l.y)
        End Operator

        Public Shared Widening Operator CType(l As PointF) As Lpt
            Return New Lpt(l.X, l.Y)
        End Operator

        Public Shared Operator +(pt1 As Lpt, pt2 As Point) As Lpt
            pt1.x += pt2.X
            pt1.y += pt2.Y
            Return pt1
        End Operator

        Public Shared Operator -(pt1 As Lpt, pt2 As Point) As Lpt
            pt1.x -= pt2.X
            pt1.y -= pt2.Y
            Return pt1
        End Operator

        Public Shared Operator +(pt1 As Lpt, pt2 As Lpt) As Lpt
            pt1.x += pt2.x
            pt1.y += pt2.y
            Return pt1
        End Operator

        Public Shared Operator -(pt1 As Lpt, pt2 As Lpt) As Lpt
            pt1.x -= pt2.x
            pt1.y -= pt2.y
            Return pt1
        End Operator

        Public Sub Add(addr As PointF)
            x += addr.X
            y += addr.Y
        End Sub

        Public Sub Subtract(remove As PointF)
            x -= remove.X
            y -= remove.Y
        End Sub

#End Region

#Region "Math"

        Public Function toRecf(size As Double) As RectangleF
            Return toRecf(New Size(size, size))
        End Function

        Public Function toRecf(size As SizeF) As RectangleF
            Return New RectangleF(x - size.Width / 2, y - size.Height / 2, size.Width, size.Height)
        End Function

        Public Function IsWithin(size As Double, MatchPt As PointF) As Boolean
            Return IsWithin(New SizeF(size, size), MatchPt)
        End Function

        Public Function IsWithin(size As SizeF, MatchPt As PointF) As Boolean
            Dim s As New SizeF(size.Width / 2, size.Height / 2)
            Dim ret As Boolean = False
            If x - s.Width <= MatchPt.X AndAlso x + s.Width >= MatchPt.X AndAlso y - s.Height <= MatchPt.Y AndAlso y + s.Height >= MatchPt.Y Then ret = True
            Return ret
        End Function

        ''' <summary>
        ''' caculates the distance between two points
        ''' </summary>
        ''' <param name="p2"></param>
        ''' <returns></returns>
        Public Function DistanceBetween(p2 As PointF) As Double
            Return Math.Sqrt((x - p2.X) ^ 2 + (y - p2.Y) ^ 2)
        End Function

        ''' <summary>
        ''' caculates a point in it's new location because of a rotation
        ''' </summary>
        ''' <param name="cent">location of the center of the rotation</param>
        ''' <param name="angle">angle of the rotation</param>
        ''' <returns></returns>
        Public Function AtAngle(cent As PointF, angle As Double) As PointF
            Dim theta As Double = angle * Math.PI / 180


            Dim r As PointF
            x -= cent.X
            y -= cent.Y

            r.X = x * Math.Cos(theta) - y * Math.Sin(theta)
            r.Y = x * Math.Sin(theta) + y * Math.Cos(theta)

            r.X += cent.X
            r.Y += cent.Y

            Return r
        End Function

        Public Sub Rotate(cent As PointF, angle As Double)
            Me.Pointf = AtAngle(cent, angle)
        End Sub

        Public Sub ScalePoint(basept As PointF, ScaleX As Double, ScaleY As Double)
            Dim newpt As New PointF(x - basept.X, y - basept.Y)
            newpt = newpt.Scale(ScaleX, ScaleY)
            x = newpt.X + basept.X
            y = newpt.Y + basept.Y
        End Sub



#End Region

    End Class

End Class




