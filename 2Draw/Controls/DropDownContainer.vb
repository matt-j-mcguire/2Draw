
Public Class DropdownContainer

    Public Event colorChanged(newColor As Color)
    Public Event OnClose(e As EventArgs)


    Public Sub New(Optional showasDlg As Boolean = False)
        InitializeComponent()
        If showasDlg Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.StartPosition = FormStartPosition.Manual
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.BackColor = Color.White
        End If

    End Sub

    Public Property Selectedcolor As Color
        Get
            Return cpColor.SelectedItem
        End Get
        Set(value As Color)
            cpColor.SelectedItem = value
        End Set
    End Property

    Public Sub Accept()
        If Me.Visible Then
            Me.Hide()
            RaiseEvent OnClose(New EventArgs)
        End If
    End Sub

    Protected Overloads Overrides Sub OnDeactivate(e As EventArgs)
        MyBase.OnDeactivate(e)
        Accept()
    End Sub

    Public Sub ShowDropdown()
        Me.Show()
    End Sub

    Private Sub cpColor_ColorChanged(e As System.Drawing.Color) Handles cpColor.ColorChanged
        RaiseEvent colorChanged(e)
        Accept()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

End Class
