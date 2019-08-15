<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImageItemEdit
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
        Me.pnlpreview = New _2Draw.PanelBFRED()
        Me.optBrowse = New System.Windows.Forms.RadioButton()
        Me.optSelect = New System.Windows.Forms.RadioButton()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtbrowse = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.lbItems = New System.Windows.Forms.ListBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.colorFade = New _2Draw.ColorPickerCombobox()
        Me.ColorFadePer = New System.Windows.Forms.NumericUpDown()
        CType(Me.ColorFadePer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlpreview
        '
        Me.pnlpreview.BackColor = System.Drawing.Color.White
        Me.pnlpreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlpreview.Location = New System.Drawing.Point(263, 40)
        Me.pnlpreview.Name = "pnlpreview"
        Me.pnlpreview.Size = New System.Drawing.Size(323, 295)
        Me.pnlpreview.TabIndex = 8
        '
        'optBrowse
        '
        Me.optBrowse.AutoSize = True
        Me.optBrowse.Checked = True
        Me.optBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.optBrowse.Location = New System.Drawing.Point(10, 12)
        Me.optBrowse.Name = "optBrowse"
        Me.optBrowse.Size = New System.Drawing.Size(128, 18)
        Me.optBrowse.TabIndex = 0
        Me.optBrowse.TabStop = True
        Me.optBrowse.Text = "Browse for image file"
        Me.optBrowse.UseVisualStyleBackColor = True
        '
        'optSelect
        '
        Me.optSelect.AutoSize = True
        Me.optSelect.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.optSelect.Location = New System.Drawing.Point(10, 67)
        Me.optSelect.Name = "optSelect"
        Me.optSelect.Size = New System.Drawing.Size(212, 18)
        Me.optSelect.TabIndex = 3
        Me.optSelect.Text = "Select From images already in poroject"
        Me.optSelect.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(511, 344)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Location = New System.Drawing.Point(430, 344)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtbrowse
        '
        Me.txtbrowse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtbrowse.Location = New System.Drawing.Point(16, 37)
        Me.txtbrowse.Name = "txtbrowse"
        Me.txtbrowse.Size = New System.Drawing.Size(206, 20)
        Me.txtbrowse.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.FlatAppearance.BorderSize = 0
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Image = Global._2Draw.My.Resources.Resources.Openfile
        Me.btnBrowse.Location = New System.Drawing.Point(228, 37)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(29, 20)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'lbItems
        '
        Me.lbItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbItems.FormattingEnabled = True
        Me.lbItems.Location = New System.Drawing.Point(17, 99)
        Me.lbItems.Name = "lbItems"
        Me.lbItems.Size = New System.Drawing.Size(204, 236)
        Me.lbItems.TabIndex = 4
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.SystemColors.Control
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Image = Global._2Draw.My.Resources.Resources.layerRemove
        Me.btnDelete.Location = New System.Drawing.Point(227, 99)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(29, 24)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(260, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Color Fade"
        '
        'colorFade
        '
        Me.colorFade.AllowTransparent = True
        Me.colorFade.HasBoarder = False
        Me.colorFade.Location = New System.Drawing.Point(316, 16)
        Me.colorFade.Name = "colorFade"
        Me.colorFade.SelectedItem = System.Drawing.Color.Transparent
        Me.colorFade.Size = New System.Drawing.Size(167, 23)
        Me.colorFade.TabIndex = 10
        Me.colorFade.Text = "ColorPickerCombobox1"
        '
        'ColorFadePer
        '
        Me.ColorFadePer.Location = New System.Drawing.Point(506, 17)
        Me.ColorFadePer.Maximum = New Decimal(New Integer() {95, 0, 0, 0})
        Me.ColorFadePer.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ColorFadePer.Name = "ColorFadePer"
        Me.ColorFadePer.Size = New System.Drawing.Size(78, 20)
        Me.ColorFadePer.TabIndex = 11
        Me.ColorFadePer.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'ImageItemEdit
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(596, 379)
        Me.ControlBox = False
        Me.Controls.Add(Me.ColorFadePer)
        Me.Controls.Add(Me.colorFade)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.lbItems)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtbrowse)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.optSelect)
        Me.Controls.Add(Me.optBrowse)
        Me.Controls.Add(Me.pnlpreview)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ImageItemEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Image Item Edit"
        CType(Me.ColorFadePer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlpreview As PanelBFRED
    Friend WithEvents optBrowse As RadioButton
    Friend WithEvents optSelect As RadioButton
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents txtbrowse As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents lbItems As ListBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents colorFade As ColorPickerCombobox
    Friend WithEvents ColorFadePer As NumericUpDown
End Class
