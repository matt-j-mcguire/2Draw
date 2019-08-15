
Public Class RotationHandle
    Implements iMouseHandler

    Private _CenterPt As PointF 'center grab
    Private _RotatPt As PointF 'rotation grab
    Private _Angle As Double 'current angle
    Private _conln() As PointF
    Private _crsln() As PointF
    Private _owner As Page

    Public Event Rotation(Origin As PointF, Angle As Double)
    'Public Event FinalizeMove(sender As Object, e As EventArgs)

    Public Sub New(owner As Page)
        _owner = owner
        _Angle = 0
        UpdateAll()

    End Sub

    Public Sub ZoomChanged() Implements iMouseHandler.ZoomChanged
        UpdateAll()
    End Sub

    Public Property Center As PointF
        Get
            Return _CenterPt
        End Get
        Set(value As PointF)
            _CenterPt = value
            UpdateAll()
        End Set
    End Property

    Public Property Angle() As Double
        Get
            Return _Angle
        End Get
        Set(value As Double)
            _Angle = value
            UpdateAll()
        End Set
    End Property

    Private Sub UpdateAll()
        Dim iz As Single = _owner.InverseZoom
        If Not _hasRotHan Then _RotatPt = New PointF(_CenterPt.X + 40 * iz, _CenterPt.Y)
        _conln = {New PointF(_CenterPt.X - 10 * iz, _CenterPt.Y), New PointF(_RotatPt.X, _CenterPt.Y)}
        _crsln = {New PointF(_CenterPt.X, _CenterPt.Y - 10 * iz), New PointF(_CenterPt.X, _CenterPt.Y + 10 * iz)}

        _RotatPt.Rotate(_CenterPt, _Angle)
        _conln.Rotate(_CenterPt, _Angle)
        _crsln.Rotate(_CenterPt, _Angle)
    End Sub

    Private _enabled As Boolean
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(value As Boolean)
            _enabled = value
            If Not value Then Angle = 0
        End Set
    End Property

    Public Sub Draw(g As Graphics) Implements iMouseHandler.Draw
        Dim iz As Single = _owner.InverseZoom

        If Enabled AndAlso ShowToolRotation Then
            Using fnt As New Font(_owner.Font.FontFamily, _owner.Font.Size * iz, FontStyle.Regular), p As New Pen(Color.DimGray, 1 * iz)
                g.DrawEllipse(p, Center.toRecf(10 * iz))
                If Not _movCent Then
                    g.DrawLines(p, _conln)
                    g.DrawLines(p, _crsln)
                    Dim r As RectangleF = _RotatPt.toRecf(10 * iz)
                    If _HasMouse Or _mouseover Then
                        g.FillEllipse(Brushes.LightBlue, r)
                    Else
                        g.FillEllipse(Brushes.LightGray, r)
                    End If

                    g.DrawEllipse(p, r)
                    g.DrawString($"{Angle:f1}°", fnt, p.Brush, _CenterPt.X + 5 * iz, _CenterPt.Y - 15 * iz)
                End If
            End Using


        End If

    End Sub

#Region "rotation caculations"

    Private _start As PointF
    Private _startAng As Double
    Private Sub SetStartRotate()
        _start = _RotatPt
        _startAng = _Angle
    End Sub

    ''' <summary>
    ''' returns the difference between this and the last angle
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <returns></returns>
    Private Function SetEndRotate(pt As PointF) As Single
        Dim l As Double = pt.DistanceBetween(_CenterPt)
        _RotatPt.X = _CenterPt.X + l
        _RotatPt.Y = _CenterPt.Y
        Dim t As Double = _startAng + (Math.Atan2(_start.X - _CenterPt.X, _start.Y - _CenterPt.Y) - Math.Atan2(pt.X - _CenterPt.X, pt.Y - _CenterPt.Y)) * (180 / Math.PI)
        Dim ret As Single = t - Angle
        Me.Angle = t
        Return ret
    End Function


#End Region

#Region "Mouse related"

    ''' <summary>
    ''' if the rotator has the mouse or not
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasMouse() As Boolean Implements iMouseHandler.HasMouse


    Private _mouseover As Boolean
    Private _hasRotHan As Boolean
    Private _movCent As Boolean
    Private _rotate As Boolean

    Public Sub MouseDown(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseDown
        If Enabled AndAlso ShowToolRotation Then
            Dim pt As New PointF(e.X, e.Y)


            If e.Button = MouseButtons.Right AndAlso Center.IsWithin(10 * _owner.InverseZoom, pt) Then
                _movCent = True
                Angle = 0
                _HasMouse = True
            End If
            If e.Button = MouseButtons.Left AndAlso _RotatPt.IsWithin(10 * _owner.InverseZoom, pt) Then
                _rotate = True
                SetStartRotate()
                _hasRotHan = True
                _HasMouse = True
                UpdateAll()
            End If
        End If


    End Sub

    Public Sub MouseMove(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseMove
        If Enabled AndAlso ShowToolRotation Then

            If e.Button = MouseButtons.Right AndAlso _movCent Then
                Center = New PointF(e.X, e.Y)
            End If
            If e.Button = MouseButtons.Left AndAlso _rotate Then
                Dim ret As Single = SetEndRotate(New PointF(e.X, e.Y))
                RaiseEvent Rotation(Center, ret)
            End If

            _mouseover = _RotatPt.IsWithin(10, New Point(e.X, e.Y))
        End If

    End Sub

    Public Sub MouseUp(sender As Object, e As DoubleMouseEventArgs) Implements iMouseHandler.MouseUp
        If Enabled AndAlso ShowToolRotation Then

            If e.Button = MouseButtons.Right AndAlso _movCent Then
                ' RaiseEvent FinalizeMove(Me, New EventArgs)
                _movCent = False
                e.Handled = True
            End If
            If e.Button = MouseButtons.Left AndAlso _rotate Then

                _hasRotHan = False
                _rotate = False
                UpdateAll()
            End If

        End If

        _HasMouse = False
    End Sub

    Public Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean) Implements iMouseHandler.MouseDoubleClick
        'nothing to do here
        Handled = False
    End Sub

#End Region

End Class
