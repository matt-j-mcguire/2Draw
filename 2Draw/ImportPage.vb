Public Class ImportPage
    Private _pro As Project

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Dim ov As New OpenFileDialog
        ov.Filter = $"*{Project.FILE_EXTENTION}|*{Project.FILE_EXTENTION}"
        If ov.ShowDialog = DialogResult.OK Then
            txtFilePath.Text = ov.FileName
            _pro = New Project(ov.FileName)
            lbPages.Items.AddRange(_pro.Pages.ToArray)
        End If
    End Sub

    ''' <summary>
    ''' returns a reference for the project selected and the pages from the project selected
    ''' </summary>
    ''' <param name="pro"></param>
    ''' <param name="p"></param>
    Public Sub GetSelectedProjectPages(ByRef pro As Project, ByRef p() As Page)
        pro = _pro
        Dim ret As New List(Of Page)
        For Each i As Integer In lbPages.SelectedIndices
            ret.Add(lbPages.Items(i))
        Next
        p = ret.ToArray
    End Sub


    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lbPages_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbPages.SelectedIndexChanged
        If lbPages.SelectedIndex > -1 Then
            Dim p As Page = lbPages.Items(lbPages.SelectedIndex)
            PicPreview.Image = p.DrawAsBitmap
        End If
    End Sub
    Private Sub lbPages_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lbPages.DrawItem
        If e.Index > -1 Then
            Dim f, b As Color
            If e.State And DrawItemState.Selected Then
                f = titleFore
                b = titleBack
            Else
                f = innerFore
                b = innerBack
            End If

            Dim p As Page = lbPages.Items(e.Index)
            Using fb As New SolidBrush(f), bb As New SolidBrush(b), strf As New StringFormat
                strf.LineAlignment = StringAlignment.Center
                e.Graphics.FillRectangle(bb, e.Bounds)
                e.Graphics.DrawString(p.Text, Me.Font, fb, e.Bounds, strf)
            End Using

        End If
    End Sub


End Class