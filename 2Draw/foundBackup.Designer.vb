<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class foundBackup
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
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.btnBoth = New System.Windows.Forms.Button()
        Me.btngood = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnRestore
        '
        Me.btnRestore.Location = New System.Drawing.Point(12, 39)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(83, 44)
        Me.btnRestore.TabIndex = 0
        Me.btnRestore.Text = "Restore File" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(overwrite)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'btnBoth
        '
        Me.btnBoth.Location = New System.Drawing.Point(174, 39)
        Me.btnBoth.Name = "btnBoth"
        Me.btnBoth.Size = New System.Drawing.Size(83, 44)
        Me.btnBoth.TabIndex = 1
        Me.btnBoth.Text = "Open Both" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to compare"
        Me.btnBoth.UseVisualStyleBackColor = True
        '
        'btngood
        '
        Me.btngood.Location = New System.Drawing.Point(354, 39)
        Me.btngood.Name = "btngood"
        Me.btngood.Size = New System.Drawing.Size(83, 44)
        Me.btngood.TabIndex = 2
        Me.btngood.Text = "I'm good with " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "this file"
        Me.btngood.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(412, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "A newer version of this file was found in the backup directory (\AppData\Local\Te" &
    "mp)"
        '
        'foundBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 97)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btngood)
        Me.Controls.Add(Me.btnBoth)
        Me.Controls.Add(Me.btnRestore)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "foundBackup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Found Backup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnRestore As Button
    Friend WithEvents btnBoth As Button
    Friend WithEvents btngood As Button
    Friend WithEvents Label1 As Label
End Class
