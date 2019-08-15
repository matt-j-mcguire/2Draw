<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WarningMSG
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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.chkDoNotShow = New System.Windows.Forms.CheckBox()
        Me.lbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(345, 115)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'chkDoNotShow
        '
        Me.chkDoNotShow.AutoSize = True
        Me.chkDoNotShow.Location = New System.Drawing.Point(13, 115)
        Me.chkDoNotShow.Name = "chkDoNotShow"
        Me.chkDoNotShow.Size = New System.Drawing.Size(120, 17)
        Me.chkDoNotShow.TabIndex = 1
        Me.chkDoNotShow.Text = "Do Not Show Again"
        Me.chkDoNotShow.UseVisualStyleBackColor = True
        '
        'lbl
        '
        Me.lbl.Location = New System.Drawing.Point(10, 9)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(410, 93)
        Me.lbl.TabIndex = 2
        '
        'WarningMSG
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 144)
        Me.Controls.Add(Me.lbl)
        Me.Controls.Add(Me.chkDoNotShow)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WarningMSG"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Warning"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOK As Button
    Friend WithEvents chkDoNotShow As CheckBox
    Friend WithEvents lbl As Label
End Class
