

Public Class FontStyles


    Public Event FontSyleChanged As EventHandler


    Private _recB As Rectangle
    Private _recU As Rectangle
    Private _recI As Rectangle
    Private _recS As Rectangle

    Private Sub LineWidth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.FixedWidth Or
                 ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.Selectable Or
                 ControlStyles.UserPaint, True)


        _recB = New Rectangle(0, 3, 16, 16)
        _recI = New Rectangle(_recB.Right + 5, 3, 16, 16)
        _recU = New Rectangle(_recI.Right + 5, 3, 16, 16)
        _recS = New Rectangle(_recU.Right + 5, 3, 16, 16)

        FontStyle = FontStyle.Regular
    End Sub

    Private Sub LineWidth_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
        Me.Width = 80
    End Sub

    Private Sub FontStylesCtrl_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If _recB.Contains(e.X, e.Y) Then
            FontStyle = FontStyle Xor FontStyle.Bold
        ElseIf _recI.Contains(e.X, e.Y) Then
            FontStyle = FontStyle Xor FontStyle.Italic
        ElseIf _recU.Contains(e.X, e.Y) Then
            FontStyle = FontStyle Xor FontStyle.Underline
        ElseIf _recS.Contains(e.X, e.Y) Then
            FontStyle = FontStyle Xor FontStyle.Strikeout
        End If
    End Sub

    Private Sub FontStylesCtrl_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Me.BackColor)

        e.Graphics.DrawImage(My.Resources.FontBold, _recB)
        If _fs And FontStyle.Bold Then e.Graphics.DrawRectangle(Pens.DimGray, _recB)
        e.Graphics.DrawImage(My.Resources.FontItalic, _recI)
        If _fs And FontStyle.Italic Then e.Graphics.DrawRectangle(Pens.DimGray, _recI)
        e.Graphics.DrawImage(My.Resources.FontUnderline, _recU)
        If _fs And FontStyle.Underline Then e.Graphics.DrawRectangle(Pens.DimGray, _recU)
        e.Graphics.DrawImage(My.Resources.FontStrike, _recS)
        If _fs And FontStyle.Strikeout Then e.Graphics.DrawRectangle(Pens.DimGray, _recS)
    End Sub

    Private _fs As FontStyle
    Public Property FontStyle() As FontStyle
        Get
            Return _fs
        End Get
        Set(value As FontStyle)
            _fs = value
            Me.Invalidate()
            RaiseEvent FontSyleChanged(Me, New EventArgs)
        End Set
    End Property



End Class
