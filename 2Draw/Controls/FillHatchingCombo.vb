Public Class FillHatchingCombo

    Public Event ValueChanged As EventHandler
    Private WithEvents frmHatch As FillHatchForm
    Private _isHatch As Boolean
    Private _HatchValue As HatchStyle
    Private _lastHatches() As Integer
    Private _quickLastHatches(6) As Rectangle

    Private Sub FillHatchingCombo_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
              ControlStyles.FixedHeight Or
              ControlStyles.FixedWidth Or
              ControlStyles.Opaque Or
              ControlStyles.OptimizedDoubleBuffer Or
              ControlStyles.Selectable Or
              ControlStyles.UserPaint Or
              ControlStyles.ResizeRedraw, True)

        _lastHatches = {0, 1, 2, 3, 4, 5, 6} 'throw some defaults at it.
        _isHatch = False
        _HatchValue = 0

        frmHatch = New FillHatchForm
    End Sub



    Public Property isHatch() As Boolean
        Get
            Return _isHatch
        End Get
        Set(value As Boolean)

            _isHatch = value
            Me.Invalidate()
            RaiseEvent ValueChanged(Me, New EventArgs)

        End Set
    End Property

    Public Property HatchValue() As HatchStyle
        Get
            Return _HatchValue
        End Get
        Set(value As HatchStyle)
            If value < 0 Then value = 0

            _HatchValue = value
            Me.Invalidate()
            RaiseEvent ValueChanged(Me, New EventArgs)

        End Set
    End Property

#Region "overrides"

    ''' <summary>
    ''' forces redraw
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overloads Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)
        Invalidate()
    End Sub

    ''' <summary>
    ''' forces redraw
    ''' </summary>
    ''' <param name="e"></param>
    Protected Overloads Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        Invalidate()
    End Sub

    ''' <summary>
    ''' forces the same height
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub me_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
    End Sub


    Protected Overloads Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = MouseButtons.Left Then
            Focus()

            Dim fnd As Boolean = False
            For i As Integer = 0 To _quickLastHatches.Length - 1
                If _quickLastHatches(i).Contains(e.X, e.Y) Then
                    If i = 0 Then
                        isHatch = False
                    Else
                        HatchValue = _lastHatches(i - 1)
                        isHatch = True
                    End If
                    fnd = True
                End If
            Next
            If Not fnd AndAlso ButtonRectangle.Contains(e.X, e.Y) Then
                If frmHatch.Visible Then
                    frmHatch.Hide()
                Else
                    frmHatch.Location = Me.PointToScreen(New Point(0, Me.Height))
                    frmHatch.Show()
                    frmHatch.Width = Me.Width
                End If
            End If
        End If
        Me.Invalidate()
    End Sub

    Public Sub CloseDropdown(owner As Object, e As EventArgs) Handles frmHatch.onClose
        frmHatch.Hide()
        Me.Invalidate()
    End Sub

    Public Sub Hatch_ValueChanged(owner As Object, value As HatchStyle) Handles frmHatch.NewValueChanged
        HatchValue = value
        isHatch = True
        Dim t As Integer = _lastHatches(0)
        _lastHatches(0) = value
        For i As Integer = 1 To _lastHatches.Length - 1
            Dim v As Integer = _lastHatches(i)
            _lastHatches(i) = t
            t = v
        Next
        Me.Invalidate()
    End Sub



#End Region


#Region "drawing"

    Private ReadOnly Property ItemRectangle() As Rectangle
        Get
            Dim r As Rectangle = ClientRectangle
            r.Y += 2
            r.Height -= 4
            r.X += 2
            r.Width = r.Height
            Return r
        End Get
    End Property
    Private ReadOnly Property ButtonRectangle() As Rectangle
        Get
            Dim r As Rectangle = ClientRectangle
            r.Y += 2
            r.Height -= 4
            r.X = r.Right - 20
            r.Width = 17
            Return r
        End Get
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)


        Dim r As Rectangle = ClientRectangle
        e.Graphics.Clear(Me.BackColor)

        'draw the boarer and the drop down arrow
        Using p As New Pen(Color.DimGray)
            p.Alignment = PenAlignment.Inset
            r.Inflate(-1, -1)
            ' If HasBoarder Then e.Graphics.DrawRectangle(p, r)
        End Using

        'draw the selected current color
        Using bx As Brush = If(isHatch, New HatchBrush(HatchValue, Color.DimGray, Color.Transparent), New SolidBrush(Color.DimGray))
            r = ItemRectangle
            r.Inflate(-1, -1)
            e.Graphics.FillRectangle(bx, r)
            e.Graphics.DrawRectangle(Pens.DimGray, r)
        End Using

        'draw the quick squares
        Dim left As Integer = r.Right + 10
        For i As Integer = 0 To _quickLastHatches.Length - 1
            _quickLastHatches(i) = New Rectangle(left, Me.Height \ 2 - 12 \ 2, 12, 12)
            If i = 0 Then
                Using b As New SolidBrush(Color.DimGray)
                    e.Graphics.FillRectangle(b, _quickLastHatches(i))
                End Using
            Else
                Using b As New HatchBrush(_lastHatches(i - 1), Color.DimGray, Color.Transparent)
                    e.Graphics.FillRectangle(b, _quickLastHatches(i))
                End Using
            End If
            e.Graphics.DrawRectangle(Pens.DimGray, _quickLastHatches(i))
            left = _quickLastHatches(i).Right + 4
        Next

        Using b As New SolidBrush(Me.BackColor)
            r = ButtonRectangle
            e.Graphics.FillRectangle(b, r)
            e.Graphics.DrawImage(My.Resources.dropdown, r.Location.X + 1, r.Location.Y + (r.Height \ 2) - 6)
        End Using
    End Sub

#End Region


End Class


Public Class FillHatchForm
    Inherits Form

    Public Event onClose(sender As Object, e As EventArgs)
    Public Event NewValueChanged(sender As Object, value As HatchStyle)

    Private _recs(53) As Rectangle

    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
      ControlStyles.FixedHeight Or
      ControlStyles.FixedWidth Or
      ControlStyles.Opaque Or
      ControlStyles.OptimizedDoubleBuffer Or
      ControlStyles.Selectable Or
      ControlStyles.UserPaint Or
      ControlStyles.ResizeRedraw, True)

        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.Manual
        Me.ControlBox = False
        Me.ShowInTaskbar = False
        Me.BackColor = Color.White
    End Sub

    Private Sub me_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Color.White)
        Dim hs As HatchStyle = 0
        Dim arr() As Integer = [Enum].GetValues(GetType(HatchStyle))
        Dim l As Integer = 2
        Dim t As Integer = 2
        Dim bot As Integer
        For i As Integer = 0 To 52
            Dim r As New Rectangle(l, t, 16, 16)
            _recs(i) = r
            Using b As New HatchBrush(i, Color.DimGray, Color.Transparent)
                e.Graphics.FillRectangle(b, r)
            End Using

            If r.Right + 20 > Me.Width Then
                l = 2
                t += 20
            Else
                l += 20
            End If
            e.Graphics.DrawRectangle(Pens.DimGray, r)
            bot = r.Bottom + 2
        Next

        Me.Height = bot
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        RaiseEvent onClose(Me, e)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        For i As Integer = 0 To _recs.Length - 1
            If _recs(i).Contains(e.X, e.Y) Then
                RaiseEvent NewValueChanged(Me, i)
                RaiseEvent onClose(Me, e)
                Exit For
            End If
        Next
    End Sub


End Class
