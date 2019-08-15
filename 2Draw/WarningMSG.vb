Public Class WarningMSG


    Public Sub New(msg As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lbl.Text = msg
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If chkDoNotShow.Checked Then
            Me.DialogResult = DialogResult.Yes
        Else
            Me.DialogResult = DialogResult.OK
        End If
        Me.Close()
    End Sub

    Private Sub WarningMSG_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult = DialogResult.None Then DialogResult = DialogResult.Cancel
    End Sub

End Class