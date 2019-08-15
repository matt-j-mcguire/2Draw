
Public Class removePt
    Public item As Type_2D
    Public Pt As LinkedPoints.Lpt
End Class

Public Class pointHelper
    Implements iMouseHandler

    Public Event RemovePt(sender As Object, r As removePt)
    Public Event PointMoved(sender As Object, e As EventArgs)
    Private _owner As Page



    Public Sub New(owner As Page)
        _owner = owner
        _selItem = -1
        _selpoint = Nothing
    End Sub

    Public Sub ZoomChanged() Implements iMouseHandler.ZoomChanged
        'do nothing
    End Sub

    Public Sub Draw(g As Graphics) Implements iMouseHandler.Draw
        'do nothing
    End Sub


    ''' <summary>
    ''' if the rotator has the mouse or not
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasMouse() As Boolean Implements iMouseHandler.HasMouse

    Private _selItem As Integer
    Private _selpoint As LinkedPoints.Lpt

    Public Sub MouseDown(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseDown
        Dim ms As New Point(e.X, e.Y)

        If e.Button = MouseButtons.Left Then
            Dim t() As Type_2D = _owner.SelectedLayer.SelectedItems.ToArray

            For i As Integer = 0 To t.Length - 1
                If TypeOf t(i) Is LinkedPoints Then
                    Dim rad As Single = 5 * _owner.InverseZoom
                    If rad < 1 Then rad = 1
                    Dim r As LinkedPoints.Lpt = DirectCast(t(i), LinkedPoints).find(ms, rad)
                    If r IsNot Nothing Then
                        _selItem = i
                        _selpoint = r
                        _HasMouse = True
                        Exit For
                    End If
                End If

            Next
        End If
    End Sub

    Public Sub MouseMove(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseMove
        If e.Button = MouseButtons.Left Then
            If _selpoint IsNot Nothing AndAlso _selItem > -1 Then
                _selpoint.Pointf = New PointF(e.X, e.Y)
            End If
        End If
    End Sub

    Public Sub MouseUp(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseUp
        If e.Button = MouseButtons.Left Then
            _selItem = -1
            _selpoint = Nothing
            RaiseEvent PointMoved(Me, New EventArgs)

        ElseIf e.Button = MouseButtons.Right Then
            Dim ms As New Point(e.X, e.Y)
            Dim t() As Type_2D = _owner.SelectedLayer.SelectedItems.ToArray

            Dim r As New removePt

            For i As Integer = 0 To t.Length - 1
                If TypeOf t(i) Is LinkedPoints Then
                    Dim rl As LinkedPoints.Lpt = DirectCast(t(i), LinkedPoints).find(ms, 5)
                    If rl IsNot Nothing Then
                        r.item = t(i)
                        r.Pt = rl
                        RaiseEvent RemovePt(Me, r)
                        e.Handled = True
                        Exit For
                    End If
                End If

            Next
        End If
        _HasMouse = False
    End Sub

    Public Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean) Implements iMouseHandler.MouseDoubleClick
        'nothing to do here
        Handled = False
    End Sub

End Class

