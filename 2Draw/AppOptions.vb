Public Class AppOptions
    Private Sub AppOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPath.Text = Globals.ProgramUpdatePath
        udCircle.Text = Globals.CircleDefPoints
        cpHighlight.SelectedItem = Globals.Highlight
        cpCrossHair.SelectedItem = Globals.CrossHairColor
        cpGrid.SelectedItem = Globals.GridColor
        udGrid.Value = Globals.GridSize
        cmbPDF.SelectedItem = My.Settings.CurrentPDF
    End Sub

    Private Sub AppOptions_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Globals.ProgramUpdatePath = txtPath.Text
        Globals.CircleDefPoints = udCircle.Text
        Globals.Highlight = cpHighlight.SelectedItem
        Globals.CrossHairColor = cpCrossHair.SelectedItem
        Globals.GridColor = cpGrid.SelectedItem
        Globals.GridSize = udGrid.Value
        My.Settings.CurrentPDF = cmbPDF.SelectedItem
    End Sub


End Class