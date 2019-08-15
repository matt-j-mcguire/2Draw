Public Class TextItemEdit

    Private _item As Text_2D

    Public Sub New(item As Text_2D)
        InitializeComponent()
        _item = item
        txt.Text = _item.Text
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        _item.Text = txt.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Dim _ctrl As Boolean
    Private Sub TextItemEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        _ctrl = e.Control
    End Sub

    Private Sub TextItemEdit_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            If _ctrl Then
                'txt.AppendText(ControlChars.CrLf)

            Else
                BtnOk_Click(BtnOk, New EventArgs)
            End If
        End If

        _ctrl = e.Control
    End Sub


End Class