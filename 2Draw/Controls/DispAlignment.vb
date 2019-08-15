
Public Enum DisplayAlignment
    Left
    HCenter
    Right
    Top
    VCenter
    Bottom
End Enum

Public Class DispAlignment

    Public Event AlignmentClicked(sender As Object, e As DisplayAlignment)

    Private _recL As Rectangle
    Private _recHC As Rectangle
    Private _recR As Rectangle
    Private _recT As Rectangle
    Private _recVC As Rectangle
    Private _recB As Rectangle
    Private _selREc As Rectangle

    Private Sub LineWidth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.FixedWidth Or
                 ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.Selectable Or
                 ControlStyles.UserPaint, True)


        _recL = New Rectangle(0, 3, 16, 16)
        _recVC = New Rectangle(_recL.Right + 5, 3, 16, 16)
        _recR = New Rectangle(_recVC.Right + 5, 3, 16, 16)
        _recT = New Rectangle(_recR.Right + 5, 3, 16, 16)
        _recHC = New Rectangle(_recT.Right + 5, 3, 16, 16)
        _recB = New Rectangle(_recHC.Right + 5, 3, 16, 16)


    End Sub

    Private Sub LineWidth_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
        Me.Width = 122
    End Sub

    Private Sub DispAlignment_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If _recL.Contains(e.X, e.Y) Then
            _selREc = _recL
        ElseIf _recHC.Contains(e.X, e.Y) Then
            _selREc = _recHC
        ElseIf _recR.Contains(e.X, e.Y) Then
            _selREc = _recR
        ElseIf _recT.Contains(e.X, e.Y) Then
            _selREc = _recT
        ElseIf _recVC.Contains(e.X, e.Y) Then
            _selREc = _recVC
        ElseIf _recB.Contains(e.X, e.Y) Then
            _selREc = _recB
        End If
        Me.Invalidate()
    End Sub

    Private Sub me_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If _recL.Contains(e.X, e.Y) Then
            RaiseEvent AlignmentClicked(Me, DisplayAlignment.Left)
        ElseIf _recHC.Contains(e.X, e.Y) Then
            RaiseEvent AlignmentClicked(Me, DisplayAlignment.HCenter)
        ElseIf _recR.Contains(e.X, e.Y) Then
            RaiseEvent AlignmentClicked(Me, DisplayAlignment.Right)
        ElseIf _recT.Contains(e.X, e.Y) Then
            RaiseEvent AlignmentClicked(Me, DisplayAlignment.Top)
        ElseIf _recVC.Contains(e.X, e.Y) Then
            RaiseEvent AlignmentClicked(Me, DisplayAlignment.VCenter)
        ElseIf _recB.Contains(e.X, e.Y) Then
            RaiseEvent AlignmentClicked(Me, DisplayAlignment.Bottom)
        End If
        _selREc = Rectangle.Empty
        Me.Invalidate()
    End Sub

    Private Sub me_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Me.BackColor)

        e.Graphics.DrawImage(My.Resources.AlignLeft, _recL)
        e.Graphics.DrawImage(My.Resources.AlignVertCenters, _recVC)
        e.Graphics.DrawImage(My.Resources.AlignRight, _recR)
        e.Graphics.DrawImage(My.Resources.AlignTop, _recT)
        e.Graphics.DrawImage(My.Resources.AlignHorzCenters, _recHC)
        e.Graphics.DrawImage(My.Resources.AlignBottom, _recB)

        If Not _selREc.IsEmpty Then

            e.Graphics.DrawRectangle(Pens.DimGray, _selREc)
        End If
    End Sub


End Class
