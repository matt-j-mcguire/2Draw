

Public Class ColorPickerCombobox
    Inherits Control

    Public Event SelectedColorChanged(sender As Object, e As Color)
    Public Event ValueChanged As EventHandler '(sender As Object, e As EventArgs)
    Private WithEvents _container As DropdownContainer
    Private _selectedItem As Color
    ' Private _mouseIn As Boolean = False
    Private _QuickColorRecs() As Rectangle

    Public Sub New()

        DoubleBuffered = True
        _container = New DropdownContainer()
        _container.BackColor = Color.White
        SelectedItem = Color.Black
    End Sub

    Public Property SelectedItem() As Color
        Get
            Return _selectedItem
        End Get
        Set(value As Color)
            _selectedItem = value
            Me.Invalidate()
        End Set
    End Property

    Private ReadOnly Property ItemRectangle() As Rectangle
        Get
            Dim r As Rectangle = ClientRectangle
            r.Y += 2
            r.Height -= 4
            r.X += 2
            r.Width = r.Height
            Return r
        End Get
    End Property

    Private ReadOnly Property ButtonRectangle() As Rectangle
        Get
            Dim r As Rectangle = ClientRectangle
            r.Y += 2
            r.Height -= 4
            r.X = r.Right - 20
            r.Width = 17
            Return r
        End Get
    End Property

    Private Sub ColorChanged(e As Color) Handles _container.colorChanged
        If Not _indrop Then
            SelectedItem = e
            RaiseEvent SelectedColorChanged(Me, e)
            RaiseEvent ValueChanged(Me, New EventArgs)
        End If
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
            If _container.Visible Then
                HideDropdown()
            Else
                If ButtonRectangle.Contains(e.X, e.Y) Then
                    ShowDropdown()
                Else
                    For i As Integer = 0 To _QuickColorRecs.Length - 1
                        If _QuickColorRecs(i).Contains(e.X, e.Y) Then
                            Me.SelectedItem = _container.cpColor.LastSelectedColorsPerColumn(i)
                            ColorChanged(Me.SelectedItem)
                        End If
                    Next
                End If

            End If
        End If
    End Sub

    Public Property AllowTransparent() As Boolean

    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        If ItemRectangle.Contains(e.X, e.Y) AndAlso AllowTransparent Then
            SelectedItem = Color.Transparent
            ColorChanged(SelectedItem)
        End If
    End Sub






#End Region

#Region "Dropdown Handling"
    Private _indrop As Boolean

    Private Sub HideDropdown()
        _container.Hide()
    End Sub

    Private Sub ShowDropdown()
        _indrop = True
        _container.Selectedcolor = SelectedItem
        _container.Width = Me.Width
        _container.cpColor.Left = _QuickColorRecs(0).Left - ColorTable.SPACING * 2
        ' only need to register / unregister because the dropdown is static and shared
        AddHandler _container.KeyDown, New KeyEventHandler(AddressOf OnDropdownKeyDown)

        Dim loc As Point = Parent.PointToScreen(Location)
        loc.Y += Height
        ' adjust dropdown location in case it goes off the screen;
        Dim r As Rectangle = Parent.RectangleToScreen(Bounds)
        Dim scr As Rectangle = Screen.GetWorkingArea(Me)
        If loc.X + _container.Width > scr.Right Then
            loc.X = r.Right - _container.Width
        End If
        If loc.X < 0 Then
            loc.X = 0
        End If

        If loc.Y + _container.Height > scr.Bottom Then
            loc.Y = r.Top - _container.Height
        End If
        If loc.Y < 0 Then
            loc.Y = 0
        End If

        _container.Location = loc
        _container.ShowDropdown() '(Me)
        _indrop = False
    End Sub

    Public Sub CloseDropdown(e As EventArgs) Handles _container.OnClose
        RemoveHandler _container.KeyDown, New KeyEventHandler(AddressOf OnDropdownKeyDown)
        HideDropdown()
        SelectedItem = _container.Selectedcolor
        RaiseEvent ValueChanged(Me, New EventArgs)
    End Sub

    Private Sub OnDropdownKeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            CloseDropdown(New EventArgs)
        End If
    End Sub

#End Region

    Public Property HasBoarder() As Boolean


#Region "Drawing"

    Protected Overloads Overrides Sub OnPaint(e As PaintEventArgs)
        Dim r As Rectangle = ClientRectangle
        e.Graphics.Clear(Me.BackColor)

        'draw the boarer and the drop down arrow
        Using p As New Pen(Color.DimGray)
            p.Alignment = PenAlignment.Inset
            r.Inflate(-1, -1)
            If HasBoarder Then e.Graphics.DrawRectangle(p, r)
        End Using

        'draw the selected current color
        Using bx As New SolidBrush(SelectedItem)
            r = ItemRectangle
            r.Inflate(-1, -1)
            e.Graphics.FillRectangle(bx, r)
            e.Graphics.DrawRectangle(Pens.DimGray, r)
            If SelectedItem = Color.Transparent Then
                e.Graphics.DrawLine(Pens.Red, New Point(r.X, r.Y), New Point(r.Right, r.Bottom))
            End If
        End Using

        'draw the quick squares
        Dim c() As Color
        If _container IsNot Nothing Then
            c = _container.cpColor.LastSelectedColorsPerColumn
        Else
            c = ColorTable.BaseColors
        End If
        Dim left As Integer = r.Right + 10
        ReDim _QuickColorRecs(c.Length - 1)
        For i As Integer = 0 To c.Length - 1
            _QuickColorRecs(i) = New Rectangle(left, Me.Height \ 2 - ColorTable.CELLSZ \ 2, ColorTable.CELLSZ, ColorTable.CELLSZ)
            Using b As New SolidBrush(c(i))
                e.Graphics.FillRectangle(b, _QuickColorRecs(i))
            End Using
            left = _QuickColorRecs(i).Right + ColorTable.SPACING
        Next

        Using b As New SolidBrush(Me.BackColor)
            r = ButtonRectangle
            e.Graphics.FillRectangle(b, r)
            e.Graphics.DrawImage(My.Resources.dropdown, r.Location.X + 1, r.Location.Y + (r.Height \ 2) - 6)
        End Using





        RaisePaintEvent(Me, e)
    End Sub



#End Region

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Height = 23
        Me.Invalidate()
    End Sub

End Class




