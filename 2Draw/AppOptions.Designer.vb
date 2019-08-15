<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppOptions
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
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.btnPath = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.udCircle = New System.Windows.Forms.DomainUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cpHighlight = New _2Draw.ColorPickerCombobox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cpCrossHair = New _2Draw.ColorPickerCombobox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cpGrid = New _2Draw.ColorPickerCombobox()
        Me.udGrid = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbPDF = New System.Windows.Forms.ComboBox()
        CType(Me.udGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Program Update Path"
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(127, 5)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(181, 20)
        Me.txtPath.TabIndex = 1
        '
        'btnPath
        '
        Me.btnPath.Location = New System.Drawing.Point(314, 5)
        Me.btnPath.Name = "btnPath"
        Me.btnPath.Size = New System.Drawing.Size(22, 20)
        Me.btnPath.TabIndex = 2
        Me.btnPath.Text = "..."
        Me.btnPath.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Default Circle Points"
        '
        'udCircle
        '
        Me.udCircle.Items.Add("8")
        Me.udCircle.Items.Add("16")
        Me.udCircle.Items.Add("24")
        Me.udCircle.Items.Add("32")
        Me.udCircle.Items.Add("48")
        Me.udCircle.Items.Add("64")
        Me.udCircle.Location = New System.Drawing.Point(127, 32)
        Me.udCircle.Name = "udCircle"
        Me.udCircle.ReadOnly = True
        Me.udCircle.Size = New System.Drawing.Size(209, 20)
        Me.udCircle.TabIndex = 4
        Me.udCircle.Text = "32"
        Me.udCircle.Wrap = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Highlight Color"
        '
        'cpHighlight
        '
        Me.cpHighlight.AllowTransparent = False
        Me.cpHighlight.HasBoarder = False
        Me.cpHighlight.Location = New System.Drawing.Point(127, 57)
        Me.cpHighlight.Name = "cpHighlight"
        Me.cpHighlight.SelectedItem = System.Drawing.Color.Black
        Me.cpHighlight.Size = New System.Drawing.Size(209, 23)
        Me.cpHighlight.TabIndex = 6
        Me.cpHighlight.Text = "ColorPickerCombobox1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Cross Hair Color"
        '
        'cpCrossHair
        '
        Me.cpCrossHair.AllowTransparent = False
        Me.cpCrossHair.HasBoarder = False
        Me.cpCrossHair.Location = New System.Drawing.Point(127, 92)
        Me.cpCrossHair.Name = "cpCrossHair"
        Me.cpCrossHair.SelectedItem = System.Drawing.Color.Black
        Me.cpCrossHair.Size = New System.Drawing.Size(209, 23)
        Me.cpCrossHair.TabIndex = 8
        Me.cpCrossHair.Text = "ColorPickerCombobox2"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 122)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Grid Color"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 151)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Grid Size (pixels)"
        '
        'cpGrid
        '
        Me.cpGrid.AllowTransparent = False
        Me.cpGrid.HasBoarder = False
        Me.cpGrid.Location = New System.Drawing.Point(127, 122)
        Me.cpGrid.Name = "cpGrid"
        Me.cpGrid.SelectedItem = System.Drawing.Color.Black
        Me.cpGrid.Size = New System.Drawing.Size(209, 23)
        Me.cpGrid.TabIndex = 11
        Me.cpGrid.Text = "ColorPickerCombobox3"
        '
        'udGrid
        '
        Me.udGrid.Location = New System.Drawing.Point(127, 151)
        Me.udGrid.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.udGrid.Name = "udGrid"
        Me.udGrid.Size = New System.Drawing.Size(209, 20)
        Me.udGrid.TabIndex = 12
        Me.udGrid.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 185)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Default PDF Printer"
        '
        'cmbPDF
        '
        Me.cmbPDF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPDF.FormattingEnabled = True
        Me.cmbPDF.Items.AddRange(New Object() {"Microsoft Print to PDF", "Foxit Reader PDF Printer", "Adobe PDF"})
        Me.cmbPDF.Location = New System.Drawing.Point(127, 185)
        Me.cmbPDF.Name = "cmbPDF"
        Me.cmbPDF.Size = New System.Drawing.Size(209, 21)
        Me.cmbPDF.TabIndex = 14
        '
        'AppOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 226)
        Me.Controls.Add(Me.cmbPDF)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.udGrid)
        Me.Controls.Add(Me.cpGrid)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cpCrossHair)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cpHighlight)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.udCircle)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnPath)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "AppOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        CType(Me.udGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtPath As TextBox
    Friend WithEvents btnPath As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents udCircle As DomainUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents cpHighlight As ColorPickerCombobox
    Friend WithEvents Label4 As Label
    Friend WithEvents cpCrossHair As ColorPickerCombobox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cpGrid As ColorPickerCombobox
    Friend WithEvents udGrid As NumericUpDown
    Friend WithEvents Label7 As Label
    Friend WithEvents cmbPDF As ComboBox
End Class
