Public Class PageAdd
    Private Sub PageAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbPageRatio.Items.AddRange({PageSizes.PS_EXEC, PageSizes.PS_LETT, PageSizes.PS_LEGA, PageSizes.PS_TABL})
        cmbPageRatio.SelectedIndex = 3

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If txtPageName.Text <> "" Then
            Me.DialogResult = DialogResult.OK

            Me.Close()
        End If
    End Sub

    Public ReadOnly Property PageName() As String
        Get
            Return txtPageName.Text

        End Get
    End Property

    Public ReadOnly Property PageSize() As String
        Get
            Return cmbPageRatio.SelectedItem
        End Get
    End Property

    Public ReadOnly Property PageLandscape() As Boolean
        Get
            Return chkLandScape.Checked
        End Get
    End Property



End Class