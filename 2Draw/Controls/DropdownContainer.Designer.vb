
Partial Class DropdownContainer
    Inherits Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer


    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Me.cpColor = New _2Draw.ColorTable()
        Me.SuspendLayout()
        '
        'cpColor
        '
        Me.cpColor.BackColor = System.Drawing.Color.Transparent
        Me.cpColor.Location = New System.Drawing.Point(0, -2)
        Me.cpColor.Name = "cpColor"
        Me.cpColor.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.cpColor.SelectedItem = System.Drawing.Color.White
        Me.cpColor.Size = New System.Drawing.Size(120, 120)
        Me.cpColor.TabIndex = 0
        '
        'DropdownContainer
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(120, 120)
        Me.Controls.Add(Me.cpColor)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DropdownContainer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cpColor As ColorTable




End Class