Imports _2Draw

Public Class ResizeHelper
    Implements iMouseHandler

    Public Event Resize(sender As Object, Resized As ResizeCord)
    Private _owner As Page
    Private _orgRec As Rectangle
    Private _newRec As Rectangle
    Private _mouseOver As Boolean
    Private _orgPt As Point
    Private _lastST As RecSide

    Public Sub New(owner As Page)
        _owner = owner
    End Sub

    Public Sub ZoomChanged() Implements iMouseHandler.ZoomChanged
        'do nothing here
    End Sub

    Public Sub Draw(g As Graphics) Implements iMouseHandler.Draw
        Dim iz As Single = _owner.InverseZoom
        Dim r As Rectangle = _owner.GetBoundingRecFromSelected
        If Not r.IsEmpty Then
            r.Inflate(5 * iz, 5 * iz) 'give it just a little more room to see the dashed lines
            _orgRec = r
            Dim c As Color = If(_mouseOver, Color.Gray, Color.DimGray)

            Dim rx As Rectangle = If(HasMouse, _newRec, _orgRec)
            Using p As New Pen(Color.FromArgb(155, c), 1 * iz)
                p.DashStyle = DashStyle.Dash
                g.DrawRectangle(p, rx)

                Dim gb() As RectangleF = {New RectangleF(rx.X - 3 * iz, rx.Y - 3 * iz, 6 * iz, 6 * iz),
                                        New RectangleF(rx.Right - 3 * iz, rx.Y - 3 * iz, 6 * iz, 6 * iz),
                                        New RectangleF(rx.Right - 3 * iz, rx.Bottom - 3 * iz, 6 * iz, 6 * iz),
                                        New RectangleF(rx.X - 3 * iz, rx.Bottom - 3 * iz, 6 * iz, 6 * iz)}
                p.DashStyle = DashStyle.Solid
                For i As Integer = 0 To 3
                    g.FillRectangle(p.Brush, gb(i))
                    g.DrawRectangle(p, gb(i).X, gb(i).Y, gb(i).Width, gb(i).Height)
                Next

            End Using
        End If
    End Sub

    Public ReadOnly Property HasMouse As Boolean Implements iMouseHandler.HasMouse


    Public Sub MouseDown(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseDown
        'if we capture the mouse make sure to copy the orgRec to the newRec to start
        If e.Button = MouseButtons.Left And _owner.SelectedLayer.SelectedItems.Length > 0 Then
            _orgPt = New Point(e.X, e.Y)
            Dim st As RecSide = _orgRec.PointTouches(_orgPt, 3)
            If st > RecSide.none Then
                _newRec = _orgRec
                _lastST = st
                _HasMouse = True
            Else
                _HasMouse = False
            End If
        Else
            _HasMouse = False
        End If
    End Sub


    Public Sub MouseMove(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseMove
        'check for mouseover one of the lines
        Dim st As RecSide = If(HasMouse, _newRec, _orgRec).PointTouches(New Point(e.X, e.Y), 3)
        _mouseOver = st > RecSide.none


        'for moving the lines
        If HasMouse AndAlso _lastST > RecSide.none Then

            Dim rc As New ResizeCord
            If _lastST And RecSide.left Then
                rc.left = e.X - _orgPt.X
                _newRec.X = e.X
                _newRec.Width -= rc.left
            End If
            If _lastST And RecSide.right Then
                rc.right = e.X - _orgPt.X
                _newRec.Width += rc.right
            End If
            If _lastST And RecSide.top Then
                rc.top = e.Y - _orgPt.Y
                _newRec.Y = e.Y
                _newRec.Height -= rc.top
            End If
            If _lastST And RecSide.bottom Then
                rc.bottom = e.Y - _orgPt.Y
                _newRec.Height += rc.bottom
            End If

            _orgPt = New Point(e.X, e.Y)
            'launch the event that something has changes with the new resizecord
            If rc.isvalid Then RaiseEvent Resize(Me, rc)
        End If
    End Sub

    Public Sub MouseUp(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseUp
        If _HasMouse Then
            _orgRec = _newRec
        End If
        _HasMouse = False
        _lastST = RecSide.none
    End Sub

    Public Class ResizeCord
        Public top As Integer
        Public bottom As Integer
        Public left As Integer
        Public right As Integer

        <DebuggerStepThrough()> Public Function isvalid() As Boolean
            Return top <> 0 Or bottom <> 0 Or left <> 0 Or right <> 0
        End Function

    End Class

    Public Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean) Implements iMouseHandler.MouseDoubleClick
        'nothing to do here
        Handled = False
    End Sub


End Class



