
Public Class LineInfo
    Public item As Type_2D
    Public Pt1 As LinkedPoints.Lpt
    Public Pt2 As LinkedPoints.Lpt
    Public newPt As PointF
End Class


Public Class LineHelper
    Implements iMouseHandler

    Public Event ShowLineOpts(sender As Object, ap As LineInfo)
    Public Event LineMoved(sender As Object, e As EventArgs)
    Private _owner As Page

    Private _LI As LineInfo
    Private _offset As PointF

    Public Sub New(owner As Page)
        _owner = owner
    End Sub

    Public Sub ZoomChanged() Implements iMouseHandler.ZoomChanged
        'nothing needed here
    End Sub


    ''' <summary>
    ''' if the rotator has the mouse or not
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasMouse() As Boolean Implements iMouseHandler.HasMouse

    Private Function findline(pt As PointF) As Boolean
        Dim fnd As Boolean = False
        Dim iz As Single = _owner.InverseZoom
        Dim ll() As Type_2D = _owner.SelectedLayer.SelectedItems

        Dim t As New LineInfo

        For i As Integer = 0 To ll.Length - 1
            Dim r As RectangleF = ll(i).GetRecf
            r.Inflate(3, 3) 'a little growth to find
            If r.Contains(pt) Then
                If TypeOf ll(i) Is LinkedPoints Then
                    Dim v As Single = DirectCast(ll(i), LinkedPoints).findlineSCL(pt, t.Pt1, t.Pt2, iz)
                   ' Debug.WriteLine(v)
                    If v <> FINDNOTFOUND AndAlso v.IsBetween(1, -1) Then
                        t.item = ll(i)
                        t.newPt = pt
                        fnd = True
                        Exit For
                    End If
                End If
            End If
        Next
        If fnd Then _LI = t Else _LI = Nothing
        Return fnd
    End Function

    Public Sub MouseDown(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim pt As New PointF(e.X, e.Y)
            If findline(pt) Then
                _HasMouse = True
                _offset = pt
            End If
        End If
    End Sub

    Public Sub MouseMove(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseMove
        Dim pt As New PointF(e.X, e.Y)

        If e.Button = MouseButtons.None Then
            'this is to provide highlighting on the screen
            findline(pt)
        ElseIf e.Button = MouseButtons.Left AndAlso _HasMouse Then
            'this is for moving the items
            Dim remain As PointF = pt.Subtract(_offset)
            If _LI IsNot Nothing Then
                _LI.Pt1.Add(remain)
                _LI.Pt2.Add(remain)
            End If
            _offset = pt
        End If
    End Sub

    Public Sub MouseUp(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseUp
        If e.Button = MouseButtons.Left Then 'AndAlso Enabled Then
            _offset = New PointF(0, 0)
            RaiseEvent LineMoved(Me, New EventArgs)
            _LI = Nothing
        ElseIf e.Button = MouseButtons.Right AndAlso _LI IsNot Nothing Then
            Dim ep As New LineInfo
            _LI.newPt = New PointF(e.X, e.Y)
            RaiseEvent ShowLineOpts(Me, _LI)
            e.Handled = True
        End If
        _HasMouse = False
    End Sub

    Public Sub draw(g As Graphics) Implements iMouseHandler.Draw
        If _LI IsNot Nothing Then
            Try
                Using p As New Pen(Globals.Highlight, 1 * _owner.InverseZoom)
                    g.DrawLine(p, _LI.Pt1.Pointf, _LI.Pt2.Pointf)
                End Using
            Catch ex As Exception
            End Try
        End If

    End Sub

    Public Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean) Implements iMouseHandler.MouseDoubleClick
        'nothing to do here
        Handled = False
    End Sub

End Class

