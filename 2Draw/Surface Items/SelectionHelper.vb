

Imports _2Draw

Public Class SelectionHelper
    Implements iMouseHandler

    Public Event SelectionChanged(ByVal sender As Object, e As EventArgs)

    Private _owner As Page
    Private _possibleSelect As Type_2D
    Private _drawRectPt1 As Point
    Private _drawrectpt2 As Point


    Public Sub New(owner As Page)
        _owner = owner
    End Sub

    Public Sub ZoomChanged() Implements iMouseHandler.ZoomChanged
        'do nothing
    End Sub

    Private Function GetHittestItem(x As Integer, y As Integer) As Type_2D
        Dim ret As Type_2D = Nothing
        For i As Integer = 0 To _owner.SelectedLayer.items.Count - 1
            Dim itm As Type_2D = _owner.SelectedLayer.items(i)
            Dim r As RectangleF = itm.GetRecf()
            r.Inflate(0.5, 0.5)
            If r.Contains(x, y) AndAlso itm.HitTest(New PointF(x, y), _owner.InverseZoom) Then
                ret = itm
                Exit For
            End If
        Next
        Return ret
    End Function

    Public Sub MouseDown(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseDown
        If e.Button = MouseButtons.Left Then
            _possibleSelect = GetHittestItem(e.X, e.Y)
            _drawRectPt1 = New Point(e.X, e.Y)
            _drawrectpt2 = _drawRectPt1
            _HasMouse = True
        End If

    End Sub

    Public Sub MouseMove(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseMove
        If e.Button = MouseButtons.Left Then
            _drawrectpt2 = New Point(e.X, e.Y)
        End If
    End Sub

    Public Sub MouseUp(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseUp
        Dim r As Rectangle = GetRecFrmPts(_drawRectPt1, _drawrectpt2)
        If e.Button = MouseButtons.Left Then
            If Not r.Invalid Then

                For Each l As Type_2D In _owner.SelectedLayer.items
                    Dim tr As Rectangle = l.GetRecf.ToRec
                    If r.Contains(tr) Then
                        If Not CtrlKey Then
                            l.Selected = True
                        Else
                            l.Selected = Not l.Selected
                        End If
                        RaiseEvent SelectionChanged(Me, New EventArgs)
                    Else
                        If Not CtrlKey AndAlso l.Selected Then
                            l.Selected = False
                            RaiseEvent SelectionChanged(Me, New EventArgs)
                        End If
                    End If
                Next
            ElseIf _possibleSelect IsNot Nothing Then
                Dim l As Layer = _owner.SelectedLayer
                If CtrlKey Then
                    _possibleSelect.Selected = Not _possibleSelect.Selected
                Else
                    _possibleSelect.Selected = True
                    For i As Integer = 0 To l.items.Count - 1
                        If l.items(i) IsNot _possibleSelect Then l.items(i).Selected = False
                    Next
                End If
                RaiseEvent SelectionChanged(Me, New EventArgs)
            Else
                _owner.SelectedLayer.ClearSelectedItems()
                RaiseEvent SelectionChanged(Me, New EventArgs)
            End If
        End If


        _drawRectPt1 = New Point(0, 0)
        _drawrectpt2 = New Point(0, 0)
        _possibleSelect = Nothing
        _HasMouse = False
    End Sub


    ''' <summary>
    ''' if the rotator has the mouse or not
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasMouse() As Boolean Implements iMouseHandler.HasMouse

    Public Sub Draw(g As Graphics) Implements iMouseHandler.Draw
        Dim r As Rectangle = GetRecFrmPts(_drawRectPt1, _drawrectpt2)
        If r.IsEmpty = False Then
            Using p As New Pen(My.Settings.SurfaceHighLight, 1 * _owner.InverseZoom)
                p.DashStyle = DashStyle.Dash
                g.DrawRectangle(p, r)
            End Using
        End If
    End Sub

    Public Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean) Implements iMouseHandler.MouseDoubleClick
        'nothing to do here
        Handled = False
    End Sub


End Class
