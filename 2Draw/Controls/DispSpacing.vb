
Public Enum DisplaySpacing
    IncreaseH
    EqualeH
    DecreaseH
    NoneH
    IncreaseV
    EqualeV
    DecreaseV
    NoneV
End Enum

Public Class DispSpacing
    Public Event SpacingClicked(sender As Object, e As DisplaySpacing)

    Private _recHI As Rectangle
    Private _recHE As Rectangle
    Private _recHD As Rectangle
    Private _recHN As Rectangle
    Private _recVI As Rectangle
    Private _recVE As Rectangle
    Private _recVD As Rectangle
    Private _recVN As Rectangle
    Private _selREc As Rectangle

    Private Sub LineWidth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.FixedWidth Or
                 ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.Selectable Or
                 ControlStyles.UserPaint, True)


        _recHI = New Rectangle(0, 3, 16, 16)
        _recHE = New Rectangle(_recHI.Right + 5, 3, 16, 16)
        _recHD = New Rectangle(_recHE.Right + 5, 3, 16, 16)
        _recHN = New Rectangle(_recHD.Right + 5, 3, 16, 16)
        _recVI = New Rectangle(_recHN.Right + 5, 3, 16, 16)
        _recVE = New Rectangle(_recVI.Right + 5, 3, 16, 16)
        _recVD = New Rectangle(_recVE.Right + 5, 3, 16, 16)
        _recVN = New Rectangle(_recVD.Right + 5, 3, 16, 16)

    End Sub

    Private Sub LineWidth_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
        Me.Width = 165
    End Sub

    Private Sub DispAlignment_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If _recHI.Contains(e.X, e.Y) Then
            _selREc = _recHI
        ElseIf _reche.Contains(e.X, e.Y) Then
            _selREc = _recHE
        ElseIf _rechd.Contains(e.X, e.Y) Then
            _selREc = _recHD
        ElseIf _rechn.Contains(e.X, e.Y) Then
            _selREc = _recHN
        ElseIf _recVi.Contains(e.X, e.Y) Then
            _selREc = _recVI
        ElseIf _recve.Contains(e.X, e.Y) Then
            _selREc = _recVE
        ElseIf _recvd.Contains(e.X, e.Y) Then
            _selREc = _recVD
        ElseIf _recvn.Contains(e.X, e.Y) Then
            _selREc = _recVN
        End If
        Me.Invalidate()
    End Sub

    Private Sub me_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If _recHI.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.IncreaseH)
        ElseIf _recHE.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.EqualeH)
        ElseIf _recHD.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.DecreaseH)
        ElseIf _recHN.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.NoneH)
        ElseIf _recVI.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.IncreaseV)
        ElseIf _recVE.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.EqualeV)
        ElseIf _recVD.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.DecreaseV)
        ElseIf _recVN.Contains(e.X, e.Y) Then
            RaiseEvent SpacingClicked(Me, DisplaySpacing.NoneV)
        End If
        _selREc = Rectangle.Empty
        Me.Invalidate()
    End Sub

    Private Sub me_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Me.BackColor)

        e.Graphics.DrawImage(My.Resources.SpaceHorzIncrease, _recHI)
        e.Graphics.DrawImage(My.Resources.SpaceHorzEqual, _recHE)
        e.Graphics.DrawImage(My.Resources.SpaceHorzdecrease, _recHD)
        e.Graphics.DrawImage(My.Resources.SpaceHorzNone, _recHN)
        e.Graphics.DrawImage(My.Resources.SpaceVertIncrease, _recVI)
        e.Graphics.DrawImage(My.Resources.SpaceVertEqual, _recVE)
        e.Graphics.DrawImage(My.Resources.SpaceVertdecrease, _recVD)
        e.Graphics.DrawImage(My.Resources.SpaceVertNone, _recVN)

        If Not _selREc.IsEmpty Then
            e.Graphics.DrawRectangle(Pens.DimGray, _selREc)
        End If
    End Sub
End Class
