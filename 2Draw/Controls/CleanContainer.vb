

Public Class CleanContainer
    Private _value As String
    Private titleBack As Color = Color.FromArgb(&HFF828282)
    Private titleText As Color = Color.White
    Private _isopen As Boolean
    Private _OpenHeight As Integer
    Private ClosedHeight As Integer = 20
    Private opnRec As New Rectangle(2, 2, 16, 16)

    Public Event TextValueChanged As EventHandler '(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' creates a new clean container
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint, True)
        _isopen = True
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

            If Open Then
                e.Graphics.DrawImage(My.Resources.ArrowDn, opnRec)
            Else
                e.Graphics.DrawImage(My.Resources.Arrowside, opnRec)
            End If

            Using pb As New SolidBrush(titleText)
                Using xstrf As New StringFormat
                    xstrf.LineAlignment = StringAlignment.Center
                    tbox.X += 20
                    e.Graphics.DrawString(_value, Me.Font, pb, tbox, xstrf)
                End Using
            End Using

        End If
    End Sub

    Private Sub CleanContainer_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        If e.Button = MouseButtons.Left Then
            If opnRec.Contains(e.X, e.Y) Then
                Open = Not Open
            End If
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



    <Category("Appearance"), Browsable(True)>
    Public Property Open() As Boolean
        Get
            Return _isopen
        End Get
        Set(value As Boolean)

            _isopen = value
            ReAdjustSize()
            Me.Invalidate()
        End Set
    End Property

    Public Sub ReAdjustSize()
        If _isopen Then
            If AutoFill Then
                Me.Dock = DockStyle.Fill
            Else
                Me.Dock = DockStyle.Top
                Me.Height = Me.OpenHeight
            End If

            SetallVisible(True)
        Else
            Me.Dock = DockStyle.Top
            Me.Height = Me.ClosedHeight
            SetallVisible(False)
        End If
    End Sub

    Private Sub SetallVisible(isVis As Boolean)
        For i As Integer = 0 To Me.Controls.Count - 1
            Controls(i).Visible = isVis
        Next
    End Sub

    Private Sub CleanContainer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ReAdjustSize()
    End Sub

    Private Sub CleanContainer_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        ReAdjustSize()
    End Sub

    <Category("Appearance"), Browsable(True)>
    Public Property OpenHeight() As Integer
        Get
            Return _OpenHeight
        End Get
        Set(value As Integer)
            If value < ClosedHeight Then value = ClosedHeight
            _OpenHeight = value
            If _isopen Then
                Me.Height = _OpenHeight
            End If
        End Set
    End Property

    <Category("Appearance"), Browsable(True)>
    Public Property AutoFill() As Boolean



End Class
