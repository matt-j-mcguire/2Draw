Public Class ImageItemEdit

    Private _img As Image_2D
    Private _xpic As Project.XPic

    Public Sub New(img As Image_2D)
        InitializeComponent()
        _img = img

        For i As Integer = 0 To ThisProject.Images.Count - 1
            lbItems.Items.Add(ThisProject.Images(i).name)
        Next


        If Globals.ThisProject IsNot Nothing Then
            If Not String.IsNullOrEmpty(_img.Image) Then
                optSelect.Checked = True
                lbItems.SelectedIndex = lbItems.Items.IndexOf(_img.Image)
                Dim pic As Project.XPic = ThisProject.GetPicByName(_img.Image)
                _xpic = pic
                colorFade.SelectedItem = _img.Colorout
                ColorFadePer.Value = _img.ColoroutPer
                pnlpreview.Invalidate()
            End If

        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If optBrowse.Checked Then
            'this might be needed to be added into the project
            Globals.ThisProject.AddImage(_xpic)
        End If

        _img.Image = _xpic.name
        _img.Colorout = colorFade.SelectedItem
        _img.ColoroutPer = ColorFadePer.Value

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub


#Region "enable /disable controls"

    Private Sub optBrowse_CheckedChanged(sender As Object, e As EventArgs) Handles optBrowse.CheckedChanged
        EnableBrowse(True)
    End Sub

    Private Sub optSelect_CheckedChanged(sender As Object, e As EventArgs) Handles optSelect.CheckedChanged
        EnableBrowse(False)
    End Sub

    Private Sub EnableBrowse(isBrowse As Boolean)
        txtbrowse.Enabled = isBrowse
        btnBrowse.Enabled = isBrowse
        lbItems.Enabled = Not isBrowse
        btnDelete.Enabled = Not isBrowse
    End Sub

#End Region

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim opfd As New OpenFileDialog()
        opfd.RestoreDirectory = True
        opfd.Filter = "Image Files|*.bmp;*.png;*.jpg;*.gif"
        Try
            If opfd.ShowDialog(Me) = DialogResult.OK Then
                txtbrowse.Text = opfd.FileName
                Dim imgx As Image = Image.FromFile(txtbrowse.Text)
                Dim fl As New FileInfo(opfd.FileName)
                _xpic = New Project.XPic(imgx, fl.Name.Replace(fl.Extension, ""))
                pnlpreview.Invalidate()
            End If
        Catch ex As Exception
            txtbrowse.Text = ""
        End Try
    End Sub

    Private Sub lbItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbItems.SelectedIndexChanged
        _xpic = ThisProject.GetPicByName(CStr(lbItems.SelectedItem))
        pnlpreview.Invalidate()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lbItems.SelectedIndex > -1 Then
            If MessageBox.Show("Removing this Image will remove if from the whole project, Continue?", "Delete", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Dim p As Project.XPic = ThisProject.GetPicByName(CStr(lbItems.SelectedItem))
                ThisProject.RemoveImage(p)
                _xpic = Nothing
                pnlpreview.Invalidate()
            End If

        End If
    End Sub

    ''' <summary>
    ''' draws the image on the screen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub pnlpreview_Paint(sender As Object, e As PaintEventArgs) Handles pnlpreview.Paint
        If _xpic IsNot Nothing Then
            Dim filr As Rectangle
            If _xpic.Img.Size.IsLessThan(pnlpreview.Size) Then
                'center the image
                Dim pt As Point = pnlpreview.Size.ToCenterifRecs(_xpic.Img.Size)
                e.Graphics.DrawImage(_xpic.Img, pt)
                filr = New Rectangle(pt, _xpic.Img.Size)
            Else
                Dim r As New Rectangle(0, 0, pnlpreview.Width, pnlpreview.Height)
                Dim rx As Rectangle = r.ScaleRecToFitCentered(New Rectangle(0, 0, _xpic.Img.Width, _xpic.Img.Height))
                e.Graphics.DrawImage(_xpic.Img, rx)
                filr = rx
            End If

            If colorFade.SelectedItem <> Color.Transparent Then
                Using b As New SolidBrush(Color.FromArgb((ColorFadePer.Value / 100) * 255, colorFade.SelectedItem))
                    e.Graphics.FillRectangle(b, filr)
                End Using
            End If
        End If
    End Sub


    Private Sub colorFade_ValueChanged(sender As Object, e As EventArgs) Handles colorFade.ValueChanged
        pnlpreview.Invalidate()
    End Sub

    Private Sub ColorFadePer_ValueChanged(sender As Object, e As EventArgs) Handles ColorFadePer.ValueChanged
        pnlpreview.Invalidate()
    End Sub

End Class