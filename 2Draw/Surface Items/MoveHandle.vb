Public Class MoveHandle
    Implements iMouseHandler

    Private _rec As Rectangle
    Private _surfacerec As Rectangle
    Private _owner As Page


    Public Event Move(sender As Object, pointOffset As Point)

    Public Sub New(owner As Page)
        _owner = owner
        UpdatePoints(Rectangle.Empty)
    End Sub

    Public Sub UpdatePoints(boundingrec As Rectangle)
        _surfacerec = _owner.BoundingRecAdjustedToZoomAndOffset
        If Not HasMouse AndAlso Not boundingrec.IsEmpty Then
            Dim pt As Point
            Dim iz As Single = _owner.InverseZoom

            If boundingrec.X > 20 AndAlso boundingrec.Y > 20 Then
                'upper left hand corner
                pt = New Point(boundingrec.X - 20 * iz, boundingrec.Y - 20 * iz)
            ElseIf boundingrec.X > 20 AndAlso boundingrec.Bottom < _surfacerec.Height - 20 Then
                'lower left hand corner
                pt = New Point(boundingrec.X - 20 * iz, boundingrec.Bottom + 4 * iz)
            ElseIf boundingrec.Right < _surfacerec.Width - 20 AndAlso boundingrec.Y > 20 Then
                'top right corner
                pt = New Point(boundingrec.Right + 4 * iz, boundingrec.Y - 20 * iz)
            ElseIf boundingrec.Right < _surfacerec.Width - 20 AndAlso boundingrec.Bottom < _surfacerec.Height - 20 Then
                'bottom right cornet
                pt = New Point(boundingrec.Right + 4 * iz, boundingrec.Bottom + 4 * iz)
            Else
                'somewhere in the center
                pt = New Point(boundingrec.X + 20 * iz, boundingrec.Y + 20 * iz)
            End If

            _rec.Location = pt
            _rec.Size = New Size(16 * iz, 16 * iz)
        End If

    End Sub

    Public Sub ZoomChanged() Implements iMouseHandler.ZoomChanged
        UpdatePoints(_owner.GetBoundingRecFromSelected)
    End Sub

    Public Property Enabled As Boolean


    ''' <summary>
    ''' if the rotator has the mouse or not
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasMouse() As Boolean Implements iMouseHandler.HasMouse

    Private _mouseover As Boolean
    Private _org As Point
    Private _orgbox As Point

    Public Sub MouseDown(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseDown
        If Enabled AndAlso ShowToolMoveHandle Then
            Dim pt As New Point(e.X, e.Y)
            If e.Button = MouseButtons.Left Or e.Button = MouseButtons.Right Then
                If _rec.Contains(pt) Then
                    _HasMouse = True
                    _orgbox = pt.Subtract(_rec.Location)
                    _org = pt
                End If
            End If
        End If


    End Sub

    Public Sub MouseMove(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseMove
        If Enabled AndAlso ShowToolMoveHandle Then
            Dim npt As New Point(e.X, e.Y)

            If e.Button = MouseButtons.Left AndAlso HasMouse Then
                _rec.Location = npt.Subtract(_orgbox)
                RaiseEvent Move(Me, npt.Subtract(_org))
                _org = npt 'reset the orgion point
            ElseIf e.Button = MouseButtons.Right Then
                _rec.Location = npt.Subtract(_orgbox)
            End If
            _mouseover = _rec.Contains(e.X, e.Y)
        End If

    End Sub

    Public Sub MouseUp(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseUp
        _HasMouse = False
    End Sub

    Public Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean) Implements iMouseHandler.MouseDoubleClick
        'nothing to do here
        Handled = False
    End Sub

    Public Sub draw(g As Graphics) Implements iMouseHandler.Draw
        If Enabled AndAlso ShowToolMoveHandle Then
            If HasMouse Or _mouseover Then
                g.FillRectangle(Brushes.LightBlue, _rec)
            End If
            g.DrawImage(My.Resources.move16, _rec)
        End If
    End Sub

End Class

