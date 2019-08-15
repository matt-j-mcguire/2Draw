Public Class PageSizes

    Public ReadOnly PS_Size As String
    Public ReadOnly Landscape As Boolean


    Public Sub New(PS_const As String, isLandscape As Boolean)
        'this is just to fix the incorrect cased flag in older docs
        Select Case PS_const.ToLower
            Case PS_EXEC.ToLower
                PS_Size = PS_EXEC
            Case PS_LETT.ToLower
                PS_Size = PS_LETT
            Case PS_LEGA.ToLower
                PS_Size = PS_LEGA
            Case PS_TABL.ToLower
                PS_Size = PS_TABL
            Case Else
                'something other got passed, leave it alone
        End Select
        ' PS_Size = PS_const


        Landscape = isLandscape
    End Sub

    Public Const PS_EXEC As String = "Executive 7.25 x 10.5"
    Public Const PS_LETT As String = "Letter 8.5 x 11"
    Public Const PS_LEGA As String = "Legal 8.5 x 14"
    Public Const PS_TABL As String = "Tabloid 11 x 17"
    Private Const DPI As Integer = 300 'minimum dots per inch supported by printers
    '   96 '96 dots per inch

    Public Function GetPageBoundry() As RectangleF
        Dim sz As SizeF = getInitalSize()
        Dim bord As Double = My.Settings.PageBoarder * DPI
        Return New RectangleF(bord, bord, sz.Width - (bord * 2), sz.Height - (bord * 2))
    End Function


    ''' <summary>
    ''' this returns a width / length ratio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRatio() As Double
        Return PageSizes.GetRatio(PS_Size)
    End Function

    ''' <summary>
    ''' this returns a width / length ratio
    ''' </summary>
    ''' <param name="PS_Const">must be one of the PS_ constants else return 1.0</param>
    ''' <returns></returns>
    ''' 
    Public Shared Function GetRatio(PS_Const As String) As Double
        Select Case PS_Const
            Case PS_EXEC
                Return 7.25 / 10.5 '0.69047~
            Case PS_LETT
                Return 8.5 / 11 '0.7727~
            Case PS_LEGA
                Return 8.5 / 14 '0.589928~
            Case PS_TABL
                Return 11 / 17 '.6470588~
            Case Else
                Return 1.0 'something other got passed, return a square
        End Select
    End Function


    Public Function getPaperSizeName() As String
        Dim ret As String
        Select Case PS_Size
            Case PS_EXEC
                ret = "Executive"
            Case PS_LETT
                ret = "Letter"
            Case PS_LEGA
                ret = "Legal"
            Case PS_TABL
                ret = "Tabloid"
            Case Else
                ret = "Custom" 'something other got passed, return a square
        End Select
        Return ret
    End Function

    ''' <summary>
    ''' returns the print options in hundrets of a inch, not adjusted for landscape
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPrintSize() As Size
        Dim ret As Size
        Select Case PS_Size
            Case PS_EXEC
                ret = New Size(7.25 * 100, 10.5 * 100)
            Case PS_LETT
                ret = New Size(8.5 * 100, 11.0 * 100)
            Case PS_LEGA
                ret = New Size(8.5 * 100, 14.0 * 100)
            Case PS_TABL
                ret = New Size(11.0 * 100, 17.0 * 100)
            Case Else
                ret = New Size(1.0 * 100, 1.0 * 100) 'something other got passed, return a square
        End Select
        Return ret
    End Function

    ''' <summary>
    ''' returns inital size in pixels using standard DPI
    ''' </summary>
    ''' <returns></returns>
    Public Function getInitalSize() As SizeF
        Return GetInitalSize(PS_Size, Landscape)
    End Function



    ''' <summary>
    ''' returns inital size in pixels using standard DPI
    ''' </summary>
    ''' <param name="PS_Const">must be one of the PS_ constants else return 96x96</param>
    ''' <returns></returns>
    Public Shared Function GetInitalSize(PS_Const As String, useLandscape As Boolean) As SizeF
        Dim ret As SizeF

        Select Case PS_Const
            Case PS_EXEC
                ret = New SizeF(7.25 * DPI, 10.5 * DPI)
            Case PS_LETT
                ret = New SizeF(8.5 * DPI, 11.0 * DPI)
            Case PS_LEGA
                ret = New SizeF(8.5 * DPI, 14.0 * DPI)
            Case PS_TABL
                ret = New SizeF(11.0 * DPI, 17.0 * DPI)
            Case Else
                ret = New SizeF(1.0 * DPI, 1.0 * DPI) 'something other got passed, return a square
        End Select

        If useLandscape Then
            Dim s As New SizeF(ret.Height, ret.Width)
            ret = s
        End If

        Return ret
    End Function

    Public Shared Function GetList() As String()
        Return {PS_EXEC, PS_LETT, PS_LEGA, PS_TABL}
    End Function


End Class
