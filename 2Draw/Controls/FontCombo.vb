Public Class FontCombo

    Public Event FontNameChanged As EventHandler
    Public Event FontSizeChanged As EventHandler

    Private _SelectedItem As String
    Private WithEvents _containerFN As DropdownFontNames
    Private WithEvents _ContainerSZ As DropDownFontsizes
    Private _dropFntbtn As Rectangle
    Private _dropSZbtn As Rectangle


    Public Sub New()
        InitializeComponent()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.UserMouse Or
                 ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw, True)

        _containerFN = New DropdownFontNames
        _ContainerSZ = New DropDownFontsizes
        _SelectedItem = "Buxton Sketch"
        _selectedSize = 16
    End Sub

    Public Property SelectedItem() As String
        Get
            Return _SelectedItem
        End Get
        Set(value As String)
            _SelectedItem = value
            Me.Invalidate()
        End Set
    End Property

    Public Property selectedSize() As Integer

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Height = 23
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        'readjust the drop down arrows
        _dropSZbtn = New Rectangle(Me.Width - 17, 0, 17, Me.Height)

        _dropFntbtn = New Rectangle(_dropSZbtn.X - 67, 0, 17, Me.Height)

        Using f As New Font(_SelectedItem, 10), strf As New StringFormat(StringFormatFlags.NoWrap)
            Dim rx As New Rectangle(0, 0, Me.Width - _dropFntbtn.X, Me.Height)
            strf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(_SelectedItem, f, Brushes.Black, rx, strf)

            strf.Alignment = StringAlignment.Center
            rx = New Rectangle(_dropFntbtn.Right, 0, 50, Me.Height)
            e.Graphics.DrawString(selectedSize, Me.Font, Brushes.Black, rx, strf)
        End Using


        Using b As New SolidBrush(Me.BackColor)
            Dim r As Rectangle = _dropFntbtn
            e.Graphics.FillRectangle(b, r)
            e.Graphics.DrawImage(My.Resources.dropdown, r.Location.X + 1, r.Location.Y + (r.Height \ 2) - 6)

            r = _dropSZbtn
            e.Graphics.FillRectangle(b, r)
            e.Graphics.DrawImage(My.Resources.dropdown, r.Location.X + 1, r.Location.Y + (r.Height \ 2) - 6)
        End Using

    End Sub

#Region "control base overrides"

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
            If _containerFN.Visible Or _ContainerSZ.Visible Then
                HideDropdown()
            Else
                If _dropFntbtn.Contains(e.X, e.Y) Then
                    ShowDropdown(_containerFN)
                ElseIf New Rectangle(0, 0, Me.Width - _dropFntbtn.X, Me.Height).Contains(e.X, e.Y) Then
                    RaiseEvent FontNameChanged(Me, New EventArgs)
                ElseIf _dropSZbtn.Contains(e.X, e.Y) Then
                    ShowDropdown(_ContainerSZ)
                ElseIf New Rectangle(_dropFntbtn.Right, 0, 50, Me.Height).Contains(e.X, e.Y) Then
                    RaiseEvent FontSizeChanged(Me, New EventArgs)
                End If

            End If
        End If
    End Sub


#End Region

#Region "Dropdown Handling"
    Private _indrop As Boolean

    Private Sub HideDropdown()
        _containerFN.Hide()
        _ContainerSZ.Hide()
    End Sub

    Private Sub ShowDropdown(frm As Form)
        _indrop = True
        If frm Is _containerFN Then
            _containerFN.SelectedItem = SelectedItem
            _containerFN.Width = Me.Width
            Dim loc As Point = Me.PointToScreen(New Point(0, Me.Height))
            _containerFN.Location = loc
            _containerFN.Show()
        Else
            _ContainerSZ.SelectedItem = selectedSize
            _ContainerSZ.Width = 70
            Dim loc As Point = Me.PointToScreen(New Point(_dropFntbtn.Right, Me.Height))
            _ContainerSZ.Location = loc
            _ContainerSZ.Show()
            _ContainerSZ.Width = 70
        End If

        _indrop = False
    End Sub

    Public Sub CloseDropdownfn(owner As Object, e As EventArgs) Handles _containerFN.OnClose
        HideDropdown()
        SelectedItem = _containerFN.SelectedItem
        RaiseEvent FontNameChanged(Me, New EventArgs)
    End Sub
    Public Sub CloseDropdownSZ(owner As Object, e As EventArgs) Handles _ContainerSZ.OnClose
        HideDropdown()
        selectedSize = _ContainerSZ.SelectedItem
        RaiseEvent FontSizeChanged(Me, New EventArgs)
    End Sub

#End Region


    Private Class DropdownFontNames
        Inherits Form

        Public Event OnClose As EventHandler
        Private WithEvents lb As ListBox

        Public Sub New()
            Me.FormBorderStyle = FormBorderStyle.None
            Me.StartPosition = FormStartPosition.Manual
            Me.ControlBox = False
            Me.ShowInTaskbar = False
            Me.BackColor = Color.White

            lb = New ListBox
            lb.IntegralHeight = False
            lb.Dock = DockStyle.Fill
            lb.DrawMode = DrawMode.OwnerDrawFixed

            'get all the fonts
            Dim l As New List(Of String)
            Using f As New InstalledFontCollection
                Dim ff() As FontFamily = f.Families
                For i As Integer = 0 To ff.Length - 1
                    l.Add(ff(i).Name)
                    ff(i).Dispose()
                Next
            End Using
            lb.Items.AddRange(l.ToArray)

            Me.Controls.Add(lb)
        End Sub

        Private _selected As String
        Private _loading As Boolean
        Public Property SelectedItem() As String
            Get
                Return _selected
            End Get
            Set(value As String)
                _loading = True
                _selected = value
                lb.SelectedItem = _selected
                _loading = False
            End Set
        End Property

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

        Private Sub lb_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lb.DrawItem
            If e.Index > -1 Then

                Dim b As Brush = Brushes.Black
                If e.State = DrawItemState.Selected Then
                    b = Brushes.White
                    e.Graphics.FillRectangle(Brushes.DimGray, e.Bounds)
                End If

                Dim fn As String = lb.Items(e.Index)
                Using f As New Font(fn, 9), strf As New StringFormat()
                    strf.LineAlignment = StringAlignment.Center
                    e.Graphics.DrawString(fn, f, b, e.Bounds, strf)
                End Using
            End If
        End Sub

        Private Sub lb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lb.SelectedIndexChanged
            If Not _loading Then
                SelectedItem = CStr(lb.SelectedItem)
                Accept()
            End If
        End Sub

    End Class


    Public Class DropDownFontsizes
        Inherits Form

        Public Event OnClose As EventHandler
        Private WithEvents lb As ListBox

        Public Sub New()
            Me.FormBorderStyle = FormBorderStyle.None
            Me.StartPosition = FormStartPosition.Manual
            Me.ControlBox = False
            Me.ShowInTaskbar = False
            Me.BackColor = Color.White

            lb = New ListBox
            lb.IntegralHeight = False
            lb.Dock = DockStyle.Fill

            'add in all the sizes
            Dim l As New List(Of String)
            For i As Integer = 3 To 108
                l.Add(i)
            Next
            lb.Items.AddRange(l.ToArray)

            Me.Controls.Add(lb)
        End Sub

        Private _selected As Integer
        Private _loading As Boolean
        Public Property SelectedItem() As Integer
            Get
                Return _selected
            End Get
            Set(value As Integer)
                _loading = True
                _selected = value
                lb.SelectedItem = _selected
                _loading = False
            End Set
        End Property

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

        Private Sub lb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lb.SelectedIndexChanged
            If Not _loading Then
                SelectedItem = CInt(lb.SelectedItem)
                Accept()
            End If
        End Sub

    End Class
End Class
