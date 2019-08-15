Public Enum DisplaySizing
    Width
    Height
    Both
End Enum

Public Class DispSizing

    Public Event SizingClicked(sender As Object, e As DisplaySizing)

    Private _recW As Rectangle
    Private _recH As Rectangle
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


        _recW = New Rectangle(0, 3, 16, 16)
        _recH = New Rectangle(_recW.Right + 5, 3, 16, 16)
        _recB = New Rectangle(_recH.Right + 5, 3, 16, 16)



    End Sub

    Private Sub LineWidth_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
        Me.Width = 60
    End Sub

    Private Sub DispAlignment_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If _recW.Contains(e.X, e.Y) Then
            _selREc = _recW
        ElseIf _rech.Contains(e.X, e.Y) Then
            _selREc = _recH
        ElseIf _recb.Contains(e.X, e.Y) Then
            _selREc = _recB
        End If
        Me.Invalidate()
    End Sub

    Private Sub me_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If _recW.Contains(e.X, e.Y) Then
            RaiseEvent SizingClicked(Me, DisplaySizing.Width)
        ElseIf _recH.Contains(e.X, e.Y) Then
            RaiseEvent SizingClicked(Me, DisplaySizing.Height)
        ElseIf _recb.Contains(e.X, e.Y) Then
            RaiseEvent SizingClicked(Me, DisplaySizing.Both)
        End If
        _selREc = Rectangle.Empty
        Me.Invalidate()
    End Sub

    Private Sub me_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Me.BackColor)

        e.Graphics.DrawImage(My.Resources.SizeSameWidth, _recW)
        e.Graphics.DrawImage(My.Resources.SizeSameHeight, _recH)
        e.Graphics.DrawImage(My.Resources.SizeSame, _recB)


        If Not _selREc.IsEmpty Then
            e.Graphics.DrawRectangle(Pens.DimGray, _selREc)
        End If
    End Sub
End Class
