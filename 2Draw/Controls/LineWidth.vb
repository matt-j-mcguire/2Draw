



Public Class LineWidth
    Private _recs() As Rectangle

    Public Event ValueChanged As EventHandler

    Private Sub LineWidth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.FixedWidth Or
                 ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.Selectable Or
                 ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw, True)

        LineWidth = 1.0
    End Sub

    Private Sub LineWidth_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
    End Sub

    Private Sub LineWidth_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Me.BackColor)

        Dim left As Integer = 1
        ReDim _recs(6)
        For i As Integer = 0 To 6
            _recs(i) = New Rectangle(left, 1, 20, 21)
            left = _recs(i).Right + 4

            Using p As New Pen(Color.Black, i + 1)
                e.Graphics.DrawLine(p, _recs(i).X + 2, 11, _recs(i).Right - 2, 11)
            End Using
            If (i * 0.5) + 1 = LineWidth Then
                e.Graphics.DrawRectangle(Pens.DimGray, _recs(i))
            End If

        Next
    End Sub

    Private Sub LineWidth_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If e.Button = MouseButtons.Left Then
            For i As Integer = 0 To _recs.Length - 1
                If _recs(i).Contains(e.X, e.Y) Then
                    LineWidth = (i * 0.5) + 1
                End If
            Next
        End If
    End Sub


    Private _lw As Single
    Public Property LineWidth() As Single
        Get
            Return _lw
        End Get
        Set(value As Single)
            _lw = value
            Me.Invalidate()
            RaiseEvent ValueChanged(Me, New EventArgs)
        End Set
    End Property




End Class
