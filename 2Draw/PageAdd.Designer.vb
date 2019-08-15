<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PageAdd
    Inherits System.Windows.Forms.Form

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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPageName = New System.Windows.Forms.TextBox()
        Me.cmbPageRatio = New System.Windows.Forms.ComboBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.chkLandScape = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Page Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Page Ratio"
        '
        'txtPageName
        '
        Me.txtPageName.Location = New System.Drawing.Point(14, 25)
        Me.txtPageName.Name = "txtPageName"
        Me.txtPageName.Size = New System.Drawing.Size(260, 20)
        Me.txtPageName.TabIndex = 2
        '
        'cmbPageRatio
        '
        Me.cmbPageRatio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPageRatio.FormattingEnabled = True
        Me.cmbPageRatio.Location = New System.Drawing.Point(16, 66)
        Me.cmbPageRatio.Name = "cmbPageRatio"
        Me.cmbPageRatio.Size = New System.Drawing.Size(257, 21)
        Me.cmbPageRatio.TabIndex = 3
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(104, 121)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(79, 24)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'chkLandScape
        '
        Me.chkLandScape.AutoSize = True
        Me.chkLandScape.Checked = True
        Me.chkLandScape.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLandScape.Location = New System.Drawing.Point(16, 94)
        Me.chkLandScape.Name = "chkLandScape"
        Me.chkLandScape.Size = New System.Drawing.Size(81, 17)
        Me.chkLandScape.TabIndex = 5
        Me.chkLandScape.Text = "LandScape"
        Me.chkLandScape.UseVisualStyleBackColor = True
        '
        'PageAdd
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(289, 148)
        Me.Controls.Add(Me.chkLandScape)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cmbPageRatio)
        Me.Controls.Add(Me.txtPageName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "PageAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add a Page"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPageName As TextBox
    Friend WithEvents cmbPageRatio As ComboBox
    Friend WithEvents btnOK As Button
    Friend WithEvents chkLandScape As CheckBox
End Class
