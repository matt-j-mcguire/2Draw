

Public Class FontJustified

    Public Event FontAlignmentChanged As EventHandler


    Private _recLJ As Rectangle
    Private _recMJ As Rectangle
    Private _recRJ As Rectangle

    Private Sub LineWidth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.FixedWidth Or
                 ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.Selectable Or
                 ControlStyles.UserPaint, True)

        _recLJ = New Rectangle(0, 3, 16, 16)
        _recMJ = New Rectangle(_recLJ.Right + 5, 3, 16, 16)
        _recRJ = New Rectangle(_recMJ.Right + 5, 3, 16, 16)

        fontAlignment = StringAlignment.Near
    End Sub

    Private Sub LineWidth_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
        Me.Width = 60
    End Sub

    Private Sub FontStylesCtrl_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If _recLJ.Contains(e.X, e.Y) Then
            fontAlignment = StringAlignment.Near
        ElseIf _recMJ.Contains(e.X, e.Y) Then
            fontAlignment = StringAlignment.Center
        ElseIf _recRJ.Contains(e.X, e.Y) Then
            fontAlignment = StringAlignment.Far
        End If
        Me.Invalidate()
    End Sub

    Private Sub FontStylesCtrl_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Me.BackColor)

        e.Graphics.DrawImage(My.Resources.FontLeft, _recLJ)
        If _fa = StringAlignment.Near Then e.Graphics.DrawRectangle(Pens.DimGray, _recLJ)
        e.Graphics.DrawImage(My.Resources.FontCenter, _recMJ)
        If _fa = StringAlignment.Center Then e.Graphics.DrawRectangle(Pens.DimGray, _recMJ)
        e.Graphics.DrawImage(My.Resources.FontRight, _recRJ)
        If _fa = StringAlignment.Far Then e.Graphics.DrawRectangle(Pens.DimGray, _recRJ)
    End Sub


    Private _fa As StringAlignment
    Public Property fontAlignment() As StringAlignment
        Get
            Return _fa
        End Get
        Set(value As StringAlignment)
            _fa = value
            Me.Invalidate()
            RaiseEvent FontAlignmentChanged(Me, New EventArgs)
        End Set
    End Property


End Class
