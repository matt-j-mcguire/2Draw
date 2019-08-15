

Public Class ColorTable
    Inherits UserControl

    Public Event ColorChanged(ByVal e As Color)

    Public Const COLS As Integer = 8
    Public Const ROWS As Integer = 8
    Public Const SPACING As Integer = 2
    Public Const CELLSZ As Integer = 12


    Private _c(COLS - 1, ROWS - 1) As Color '8x8 matrix
    Public Shared ReadOnly BaseColors() As Color
    Private _selected As Point
    Private _mouseOvr As Point
    Private _LastColors(COLS - 1) As Color

    Public Sub New()

        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.FixedHeight Or
                 ControlStyles.FixedWidth Or
                 ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.Selectable Or
                 ControlStyles.UserPaint, True)


        For i As Integer = 0 To COLS - 1
            instertColor(get8(BaseColors(i)), i)
            _LastColors(i) = _c(i, 0)
        Next

    End Sub

    Shared Sub New()
        BaseColors = {Color.Silver, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Cyan, Color.Blue, Color.Fuchsia}
    End Sub



    Private Sub instertColor(c() As Color, column As Integer)
        For i As Integer = 0 To ROWS - 1
            _c(column, i) = c(i)
        Next
    End Sub

    ''' <summary>
    ''' creates a range of 16 colors based on the origional color
    ''' </summary>
    ''' <param name="basecolor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function get8(basecolor As Color) As Color()
        Dim ret(7) As Color
        Dim hsl As New HSLColor(basecolor)

        For i As Integer = 0 To 7 '15
            hsl.Lightness = 0.1 + ((i + 1) * 0.1)
            ret(i) = hsl.Color
        Next

        If basecolor = Color.Silver Then
            ret(0) = Color.Black
            ret(7) = Color.White
        End If

        Return ret
    End Function

    Public Property SelectedItem() As Color
        Get
            Return _c(_selected.X, _selected.Y)
        End Get
        Set(value As Color)
            _selected = findClosestColor(value)
            Me.Invalidate()
        End Set
    End Property

    Public ReadOnly Property LastSelectedColorsPerColumn() As Color()
        Get
            Return _LastColors
        End Get
    End Property

    Private Function findClosestColor(c As Color) As Point
        Dim ret As New Point(0, 0)
        For i As Integer = 0 To COLS - 1
            For j As Integer = 0 To ROWS - 1
                Dim x As Color = _c(i, j)
                If Math.Abs(CInt(x.R) - CInt(c.R)) < 10 AndAlso Math.Abs(CInt(x.G) - CInt(c.G)) < 10 AndAlso Math.Abs(CInt(x.B) - CInt(c.B)) < 10 Then
                    ret.X = i
                    ret.Y = j
                    Exit For
                End If
            Next
        Next
        Return ret
    End Function



#Region "Drawing"

    Private Function CreateCellRec(c As Integer, r As Integer) As Rectangle
        Return New Rectangle(c * (SPACING + CELLSZ) + (SPACING * 2), r * (SPACING + CELLSZ) + (SPACING * 2), CELLSZ, CELLSZ)
    End Function
    Private Function CreateCellRec(p As Point) As Rectangle
        Return CreateCellRec(p.X, p.Y)
    End Function


    Protected Overloads Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.Clear(Color.White)

        'draw the cells
        For i As Integer = 0 To COLS - 1
            For j As Integer = 0 To ROWS - 1
                Dim r As Rectangle = CreateCellRec(i, j)
                Using b As New SolidBrush(_c(i, j))
                    e.Graphics.FillRectangle(b, r)
                End Using
            Next
        Next

        'draw the selected item
        Using b As New SolidBrush(_c(_selected.X, _selected.Y))
            Dim r As Rectangle = CreateCellRec(_selected)
            r.Inflate(SPACING * 2, SPACING * 2)
            e.Graphics.FillRectangle(b, r)
            e.Graphics.DrawRectangle(Pens.DimGray, r)
        End Using

        'draw the mouse over area
        If _mouseOvr.X > -1 AndAlso _mouseOvr.Y > -1 Then
            Using b As New SolidBrush(_c(_mouseOvr.X, _mouseOvr.Y))
                Dim r As Rectangle = CreateCellRec(_mouseOvr)
                r.Inflate(SPACING * 2, SPACING * 2)
                e.Graphics.FillRectangle(b, r)
                e.Graphics.DrawRectangle(Pens.DimGray, r)
            End Using
        End If



    End Sub

#End Region

#Region "overrides"

    Protected Overloads Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        _mouseOvr = New Point(-1, -1)
        For i As Integer = 0 To COLS - 1
            For j As Integer = 0 To ROWS - 1
                Dim r As New Rectangle(i * (SPACING + CELLSZ), j * (SPACING + CELLSZ), CELLSZ, CELLSZ)
                If r.Contains(e.X, e.Y) Then
                    _mouseOvr = New Point(i, j)

                    Exit For
                End If
            Next
        Next

        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _mouseOvr = New Point(-1, -1)
    End Sub

    Protected Overloads Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Focus()

        For i As Integer = 0 To COLS - 1
            For j As Integer = 0 To ROWS - 1
                Dim r As New Rectangle(i * (SPACING + CELLSZ), j * (SPACING + CELLSZ), CELLSZ, CELLSZ)
                If r.Contains(e.X, e.Y) Then
                    _selected = New Point(i, j)
                    _LastColors(i) = SelectedItem
                    RaiseEvent ColorChanged(_c(i, j))
                    Exit For
                End If
            Next
        Next
    End Sub

    Protected Overloads Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)
        Invalidate()
    End Sub

    Protected Overloads Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Size = New Size(COLS * (SPACING + CELLSZ) + (SPACING * 4), ROWS * (SPACING + CELLSZ) + (SPACING * 4))
    End Sub

#End Region

End Class
