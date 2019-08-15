

Public Class CleanContainerSolid

    Private _value As String
    Private titleBack As Color = Color.FromArgb(&HFF828282)
    Private titleText As Color = Color.White


    Public Event TextValueChanged As EventHandler '(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' creates a new clean container
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint, True)
    End Sub

    ''' <summary>
    ''' paints the item on the screen
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        If Me.Width > 0 AndAlso Me.Height > 0 Then
            Dim ebox As New Rectangle(0, 0, Me.Width, Me.Height)
            Dim tbox As New Rectangle(0, 0, Me.Width, 20)

            Using pb As New SolidBrush(Me.BackColor)
                e.Graphics.FillRectangle(pb, ebox)
            End Using

            Using xgb As New SolidBrush(titleBack)
                e.Graphics.FillRectangle(xgb, tbox)
            End Using

            Using pb As New SolidBrush(titleText)
                Using xstrf As New StringFormat
                    xstrf.LineAlignment = StringAlignment.Center
                    e.Graphics.DrawString(_value, Me.Font, pb, tbox, xstrf)
                End Using
            End Using

        End If
    End Sub


    ''' <summary>
    ''' the text of the button
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
            RaiseEvent TextValueChanged(Me, New EventArgs)
            Me.Invalidate()
        End Set
    End Property

End Class
