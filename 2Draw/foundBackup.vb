Public Class foundBackup
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Me.DialogResult = DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnBoth.Click
        Me.DialogResult = DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btngood.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class