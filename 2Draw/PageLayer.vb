

Public Class Layer
    Private Const LAYERNAME As String = "name"
    Private Const LAYERVISI As String = "visible"

    Public items As List(Of Type_2D)
    Public ParentPage As Page



    ''' <summary>
    ''' creates a new layer
    ''' </summary>
    Public Sub New(thePage As Page)
        ParentPage = thePage
        Name = "Layer"
        Visible = True
        items = New List(Of Type_2D)
    End Sub

    ''' <summary>
    ''' creates a new layer based off an xmlnode
    ''' </summary>
    ''' <param name="node"></param>
    Public Sub New(node As XmlNode, thepage As Page)
        ParentPage = thepage
        Name = XHelper.Get(node, LAYERNAME, "Layer")
        Visible = XHelper.Get(node, LAYERVISI, True)

        'set up the items on the screen
        items = New List(Of Type_2D)
        Dim xls As XmlNodeList = node.SelectNodes("Item")
        For Each n As XmlNode In xls
            Dim t As Type_2D = DisplayItem.CreateType(n)
            If t IsNot Nothing Then items.Add(t)
        Next
    End Sub

    Public ReadOnly Property SelectedItems() As Type_2D()
        Get
            Dim l As New List(Of Type_2D)
            For Each d As Type_2D In items
                If d.Selected Then l.Add(d)
            Next
            Return l.ToArray
        End Get
    End Property

    Public Sub ClearSelectedItems()
        For Each d As Type_2D In items
            If d.Selected Then d.Selected = False
        Next
    End Sub

    ''' <summary>
    ''' this move and item back in the stack before any that may overlap
    ''' </summary>
    ''' <param name="item"></param>
    Public Sub MoveBeforeAny(item As Type_2D)
        Dim index As Integer = items.IndexOf(item)
        Dim r As RectangleF = item.GetRecf
        For i As Integer = 0 To index - 1
            Dim rx As RectangleF = items(i).GetRecf
            If rx.IntersectsWith(r) Then
                items.Remove(item)
                items.Insert(i, item)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' this moves and item forward in the stack before any that may overlap
    ''' </summary>
    ''' <param name="item"></param>
    Public Sub MoveAfterAny(item As Type_2D)
        Dim index As Integer = items.IndexOf(item)
        Dim r As RectangleF = item.GetRecf
        For i As Integer = items.Count - 1 To index Step -1
            Dim rx As RectangleF = items(i).GetRecf
            If rx.IntersectsWith(r) Then
                items.Remove(item)
                items.Insert(i, item)
                Exit For
            End If
        Next

    End Sub

    ''' <summary>
    ''' to save the items back to file
    ''' </summary>
    ''' <param name="nd"></param>
    Public Sub save(nd As XmlNode)
        XHelper.Set(nd, LAYERNAME, Name)
        XHelper.Set(nd, LAYERVISI, Visible)
        For Each t As Type_2D In items
            DisplayItem.SaveAppendNode(nd, t)
        Next
    End Sub

    ''' <summary>
    ''' copies the node to be saved.
    ''' </summary>
    ''' <param name="ownerDoc"></param>
    ''' <returns></returns>
    Public Function CloneNode(ownerDoc As XmlDocument) As XmlNode
        Dim nd As XmlNode = ownerDoc.CreateElement("Layer")
        Me.save(nd)
        Return nd
    End Function

    ''' <summary>
    ''' if this layer is visible or not
    ''' </summary>
    ''' <returns></returns>
    Public Property Visible() As Boolean

    ''' <summary>
    ''' name of this layer
    ''' </summary>
    ''' <returns></returns>
    Public Property Name() As String

    Private _selected As Boolean
    ''' <summary>
    ''' if this layer is selected or not
    ''' </summary>
    ''' <returns></returns>
    Public Property Selected() As Boolean
        Get
            Return _selected
        End Get
        Set(value As Boolean)
            _selected = value
            If Not _selected Then CancelSelectedItems()
        End Set
    End Property

    Public Sub CancelSelectedItems()
        For Each d As Type_2D In items
            d.Selected = False
        Next
    End Sub




#Region "sizeing,alignment,spacing"

    Private Class origion
        Public rec As RectangleF
        Public item As Type_2D
    End Class

    Public Sub SetSelectedSpacing(e As DisplaySpacing)
        Dim li() As Type_2D = SelectedItems
        If li.Length > 2 Then


            Dim m As Integer = li.Length
            Dim r(m - 1) As origion

            For i As Integer = 0 To m - 1
                r(i) = New origion
                r(i).rec = li(i).GetRecf
                r(i).item = li(i)
            Next
            Select Case e
                Case DisplaySpacing.DecreaseH, DisplaySpacing.EqualeH, DisplaySpacing.NoneH, DisplaySpacing.IncreaseH
                    Array.Sort(r, AddressOf SortbyX)
                    Dim minwidth As Single = 0
                    If e = DisplaySpacing.EqualeH Then minwidth = (r(1).rec.X - r(0).rec.Right)
                    If e = DisplaySpacing.DecreaseH Then minwidth = (r(1).rec.X - r(0).rec.Right) - 5
                    If e = DisplaySpacing.IncreaseH Then minwidth = (r(1).rec.X - r(0).rec.Right) + 5

                    For i As Integer = 1 To m - 1
                        Dim lf As Single = r(i - 1).rec.Right + minwidth
                        Dim diff As Single = lf - r(i).rec.X
                        r(i).rec.X += diff
                        r(i).item.ApplyOffset(New PointF(diff, 0))
                    Next

                Case DisplaySpacing.DecreaseV, DisplaySpacing.EqualeV, DisplaySpacing.NoneV, DisplaySpacing.IncreaseV
                    Array.Sort(r, AddressOf SortbyY)
                    Dim minheight As Single = 0
                    If e = DisplaySpacing.EqualeV Then minheight = (r(1).rec.Y - r(0).rec.Bottom)
                    If e = DisplaySpacing.DecreaseV Then minheight = (r(1).rec.Y - r(0).rec.Bottom) - 5
                    If e = DisplaySpacing.IncreaseV Then minheight = (r(1).rec.Y - r(0).rec.Bottom) + 5

                    For i As Integer = 1 To m - 1
                        Dim lf As Single = r(i - 1).rec.Bottom + minheight
                        Dim diff As Single = lf - r(i).rec.Y
                        r(i).rec.Y += diff
                        r(i).item.ApplyOffset(New PointF(0, diff))
                    Next

            End Select

            ParentPage.Invalidate
        End If
    End Sub

    Private Function SortbyX(ByVal x1 As origion, ByVal x2 As origion) As Integer
        If x1.rec.X > x2.rec.X Then
            Return 1
        ElseIf x1.rec.X < x2.rec.X Then
            Return -1
        Else
            Return 0
        End If
    End Function

    Private Function SortbyY(ByVal y1 As origion, ByVal y2 As origion) As Integer
        If y1.rec.X > y2.rec.X Then
            Return 1
        ElseIf y1.rec.X < y2.rec.X Then
            Return -1
        Else
            Return 0
        End If
    End Function

    Public Sub setSelectedSizing(e As DisplaySizing)
        Dim li() As Type_2D = SelectedItems
        Dim m As Integer = li.Length
        Dim r(m - 1) As RectangleF

        Dim biggest As SizeF
        For i As Integer = 0 To m - 1
            r(i) = li(i).GetRecf
            If r(i).Width > biggest.Width Then biggest.Width = r(i).Width
            If r(i).Height > biggest.Height Then biggest.Height = r(i).Height
        Next

        For i As Integer = 0 To m - 1
            Select Case e
                Case DisplaySizing.Width
                    r(i).Width = biggest.Width
                Case DisplaySizing.Height
                    r(i).Height = biggest.Height
                Case DisplaySizing.Both
                    r(i).Width = biggest.Width
                    r(i).Height = biggest.Height
            End Select
            For j As Integer = 0 To 3
                'not sure why i had to do this, to make sizing 
                'actually get to the same size. but it actually
                'takes 3 itteration to get to the same size
                li(i).Resize(r(i))
            Next

        Next
        ParentPage.Invalidate
    End Sub

    Public Sub setSelectedAlignment(e As DisplayAlignment)
        Dim li() As Type_2D = SelectedItems
        Dim m As Integer = li.Length
        If m > 0 Then

            Dim pf As New PointF 'top left point
            Dim pcf As New PointF 'center point
            Dim prf As New PointF 'bommon right point
            Dim r As RectangleF
            For i As Integer = 0 To m - 1
                r = li(i).GetRecf
                pf = pf.Add(r.Location)
                pcf = pcf.Add(New PointF(r.Width / 2 + r.X, r.Height / 2 + r.Y))
                prf = prf.Add(New PointF(r.Right, r.Bottom))
            Next
            pf = New PointF(pf.X / m, pf.Y / m)
            pcf = New PointF(pcf.X / m, pcf.Y / m)
            prf = New PointF(prf.X / m, prf.Y / m)

            'set all the rectangles to whatever the average worked out to be
            For i As Integer = 0 To m - 1
                r = li(i).GetRecf
                Dim offset As PointF
                Select Case e
                    Case DisplayAlignment.Left
                        offset.X = pf.X - r.X' r.X = pf.X
                    Case DisplayAlignment.VCenter
                        offset.X = (pcf.X - r.Width / 2) - r.X ' r.X = pcf.X - r.Width / 2
                    Case DisplayAlignment.Right
                        offset.X = (prf.X - r.Width) - r.X' r.X = prf.X - r.Width
                    Case DisplayAlignment.Top
                        offset.Y = pf.Y - r.Y' r.Y = pf.Y
                    Case DisplayAlignment.HCenter
                        offset.Y = (pcf.Y - r.Height / 2) - r.Y'  r.Y = pcf.Y - r.Height / 2
                    Case DisplayAlignment.Bottom
                        offset.Y = (prf.Y - r.Height) - r.Y ' r.Y = prf.Y - r.Height
                End Select

                li(i).ApplyOffset(offset)
            Next

            ParentPage.Invalidate()
        End If

    End Sub

#End Region

End Class
