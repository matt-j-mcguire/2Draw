

Public Class LineStyled


    Public Event StartChanged As EventHandler
    Public Event EndChanged As EventHandler
    Public Event LineChanged As EventHandler

    Private WithEvents frmStart As lineOptsForm
    Private WithEvents frmLine As lineOptsForm
    Private WithEvents frmEnd As lineOptsForm

    Private Sub LineWidth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.FixedWidth Or
                 ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.Selectable Or
                 ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw, True)

        StartCap = LineCap.Flat
        EndCap = LineCap.Flat
        LineStyle = DashStyle.Solid

        frmStart = New lineOptsForm(lineOptsForm.opts.LineStart)
        frmLine = New lineOptsForm(lineOptsForm.opts.LineMiddle)
        frmEnd = New lineOptsForm(lineOptsForm.opts.LineEnd)

        RegenerateSizes()
    End Sub

#Region "Drawing"

    Private _recStart As Rectangle
    Private _recStartBtn As Rectangle
    Private _recLine As Rectangle
    Private _recLineBtn As Rectangle
    Private _recEnd As Rectangle
    Private _recEndBtn As Rectangle

    Private Sub LineWidth_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.Clear(Me.BackColor)
        e.Graphics.SmoothingMode = SmoothingMode.HighQuality

        Using p As New Pen(Color.Black,2), b As New SolidBrush(Me.BackColor),
            sc As CustomLineCap = endCapType.GetCap(StartCap),
            ec As CustomLineCap = endCapType.GetCap(EndCap)


            p.StartCap = LineCap.Custom
            p.CustomStartCap = sc
            p.DashStyle = LineStyle
            p.EndCap = LineCap.Flat
            e.Graphics.DrawLine(p, _recStart.X + 8, 11, _recStart.Right - 8, 11)

            e.Graphics.FillRectangle(b, _recStartBtn)
            e.Graphics.DrawImage(My.Resources.dropdown, _recStartBtn.Location.X + 1, _recStartBtn.Location.Y + 6)

            p.StartCap = LineCap.Flat
           ' p.CustomStartCap = Nothing
            p.DashStyle = LineStyle
            p.EndCap = LineCap.Flat
            e.Graphics.DrawLine(p, _recLine.X + 2, 11, _recLine.Right - 2, 11)

            e.Graphics.FillRectangle(b, _recLineBtn)
            e.Graphics.DrawImage(My.Resources.dropdown, _recLineBtn.Location.X + 1, _recLineBtn.Location.Y + 6)

            p.StartCap = LineCap.Flat
            p.DashStyle = LineStyle
            p.EndCap = LineCap.Custom
            p.CustomEndCap = ec
            e.Graphics.DrawLine(p, _recEnd.X + 8, 11, _recEnd.Right - 8, 11)

            e.Graphics.FillRectangle(b, _recEndBtn)
            e.Graphics.DrawImage(My.Resources.dropdown, _recEndBtn.Location.X + 1, _recEndBtn.Location.Y + 6)

        End Using

    End Sub

    Private Sub RegenerateSizes()
        Dim btnmin As Integer = 17

        Dim tot As Integer = Me.Width
        tot -= (btnmin * 3)

        Dim middle As Integer = tot / 2
        Dim ends As Integer = middle / 2

        _recStart = New Rectangle(0, 0, ends, Me.Height)
        _recStartBtn = New Rectangle(_recStart.Right, 0, btnmin, Me.Height)
        _recLine = New Rectangle(_recStartBtn.Right, 0, middle, Me.Height)
        _recLineBtn = New Rectangle(_recLine.Right, 0, btnmin, Me.Height)
        _recEnd = New Rectangle(_recLineBtn.Right, 0, ends, Me.Height)
        _recEndBtn = New Rectangle(_recEnd.Right, 0, btnmin, Me.Height)

    End Sub

#End Region


#Region "control base overrides"

    Private Sub LineWidth_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.Height = 23
        RegenerateSizes()
    End Sub

    Protected Overloads Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)
        Invalidate()
    End Sub

    Protected Overloads Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        Invalidate()
    End Sub

    Protected Overloads Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = MouseButtons.Left Then
            Focus()
            If frmStart.Visible Then
                frmStart.Hide()
            ElseIf frmLine.Visible Then
                frmLine.Hide()
            ElseIf frmend.Visible Then
                frmEnd.Hide()
            Else
                If _recStartBtn.Contains(e.X, e.Y) Then
                    ShowDropdown(frmStart)
                ElseIf _recStart.Contains(e.X, e.Y) Then
                    RaiseEvent StartChanged(Me, New EventArgs)
                ElseIf _recLinebtn.Contains(e.X, e.Y) Then
                    ShowDropdown(frmLine)
                ElseIf _recLine.Contains(e.X, e.Y) Then
                    RaiseEvent LineChanged(Me, New EventArgs)
                ElseIf _recEndbtn.Contains(e.X, e.Y) Then
                    ShowDropdown(frmEnd)
                ElseIf _recEnd.Contains(e.X, e.Y) Then
                    RaiseEvent EndChanged(Me, New EventArgs)
                End If
            End If
        End If
    End Sub

    Private _indrop As Boolean
    Private Sub ShowDropdown(item As lineOptsForm)
        _indrop = True
        Dim loc As Point
        Dim w As Integer

        If item Is frmStart Then
            frmStart.SelectedIndex = Me.StartCap
            w = _recStart.Width
            loc = New Point(_recStart.X, Me.Height)
        ElseIf item Is frmLine Then
            frmLine.SelectedIndex = Me.LineStyle
            w = _recLine.Width
            loc = New Point(_recLine.X, Me.Height)
        ElseIf item Is frmEnd Then
            frmEnd.SelectedIndex = Me.EndCap
            w = _recEnd.Width
            loc = New Point(_recEnd.X, Me.Height)
        End If

        item.Location = Me.PointToScreen(loc)
        item.Show()
        item.Width = w
        _indrop = False
    End Sub

    Public Sub CloseDropdown(owner As Object, e As EventArgs) Handles frmStart.OnClose, frmLine.OnClose, frmEnd.OnClose
        DirectCast(owner, Form).Hide()
        If owner Is frmStart Then
            Me.StartCap = frmStart.SelectedIndex
            RaiseEvent StartChanged(Me, New EventArgs)
        ElseIf owner Is frmLine Then
            Me.LineStyle = frmLine.SelectedIndex
            RaiseEvent LineChanged(Me, New EventArgs)
        ElseIf owner Is frmEnd Then
            Me.EndCap = frmEnd.SelectedIndex
            RaiseEvent EndChanged(Me, New EventArgs)
        End If
    End Sub

#End Region


#Region "Line settings props"

    Private _sc As endCapType.capType
    Public Property StartCap() As endCapType.capType
        Get
            Return _sc
        End Get
        Set(value As endCapType.capType)
            _sc = value
            RaiseEvent StartChanged(Me, New EventArgs)
            Me.Invalidate()
        End Set
    End Property

    Private _ec As endCapType.capType
    Public Property EndCap() As endCapType.capType
        Get
            Return _ec
        End Get
        Set(value As endCapType.capType)
            _ec = value
            RaiseEvent EndChanged(Me, New EventArgs)
            Me.Invalidate()
        End Set
    End Property

    Private _ls As DashStyle
    Public Property LineStyle() As Drawing2D.DashStyle
        Get
            Return _ls
        End Get
        Set(value As Drawing2D.DashStyle)
            _ls = value
            RaiseEvent LineChanged(Me, New EventArgs)
            Me.Invalidate()
        End Set
    End Property

#End Region


    Private Class lineOptsForm
        Inherits Form

        Public Event OnClose As EventHandler
        Private Const ITEMHEIGHT As Integer = 16
        Private Const CAPSCNT As Integer = endCapType.MAXTYPES
        Private Const LINECNT As Integer = 5

        Public Enum opts
            LineStart
            LineMiddle
            LineEnd
        End Enum
        Private _type As opts

        Public Sub New(thetype As opts)
            Me.FormBorderStyle = FormBorderStyle.None
            Me.StartPosition = FormStartPosition.Manual
            Me.ControlBox = False
            Me.ShowInTaskbar = False
            Me.BackColor = Color.White


            Select Case thetype
                Case opts.LineStart
                    Me.Height = CAPSCNT * ITEMHEIGHT '9 end caps
                Case opts.LineMiddle
                    Me.Height = LINECNT * ITEMHEIGHT '5 dash styles
                Case opts.LineEnd
                    Me.Height = CAPSCNT * ITEMHEIGHT '9 end caps
            End Select

            _type = thetype
        End Sub

        Public Property SelectedIndex As Integer

        Public Sub Accept()
            If Me.Visible Then
                Me.Hide()
                RaiseEvent OnClose(Me, New EventArgs)
            End If
        End Sub

        Protected Overloads Overrides Sub OnDeactivate(e As EventArgs)
            MyBase.OnDeactivate(e)
            Accept()
        End Sub



        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            SelectedIndex = e.Y \ ITEMHEIGHT
            Accept()
        End Sub


        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.Clear(Color.White)
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality

            Select Case _type
                Case opts.LineStart
                    paintStart(e.Graphics)
                Case opts.LineMiddle
                    paintMid(e.Graphics)
                Case opts.LineEnd
                    paintend(e.Graphics)
            End Select
        End Sub

        Private Sub paintStart(g As Graphics)
            For i As Integer = 0 To CAPSCNT - 1
                Using p As New Pen(Color.Black, 2), sc As CustomLineCap = endCapType.GetCap(i)
                    p.StartCap = LineCap.Custom
                    p.CustomStartCap = sc
                    g.DrawLine(p, 8, i * 16 + 5, Me.Width - 8, i * 16 + 5)
                End Using
            Next
        End Sub

        Private Sub paintMid(g As Graphics)
            For i As Integer = 0 To LINECNT - 1
                Using p As New Pen(Color.Black, 2)
                    p.DashStyle = i
                    g.DrawLine(p, 2, i * 16 + 5, Me.Width - 2, i * 16 + 5)
                End Using
            Next
        End Sub

        Private Sub paintend(g As Graphics)
            For i As Integer = 0 To CAPSCNT - 1
                Using p As New Pen(Color.Black, 2), sc As CustomLineCap = endCapType.GetCap(i)
                    p.EndCap = LineCap.Custom
                    p.CustomEndCap = sc
                    g.DrawLine(p, 8, i * 16 + 5, Me.Width - 8, i * 16 + 5)
                End Using
            Next
        End Sub



    End Class

End Class



