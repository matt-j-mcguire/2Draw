
''' <summary>
''' helper class to ease Form common tasks
''' </summary>
''' <remarks>all function are shared</remarks>
Public NotInheritable Class windowHelp

    Public Const KEY_LOCATION As String = "location"
    Public Const KEY_SIZE As String = "size"

    ''' <summary>
    ''' gets the settings for the windows location and size from xSettings
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="xset">the xSettings reference</param>
    ''' <param name="windowName">the xmlnode name to lookup</param>
    ''' <remarks>will readjust the location if off the screen</remarks>
    Public Shared Sub SetWinRec(window As Form, xset As xSettings, windowName As String)
        SetWinRec(window, xset.ND(windowName))
    End Sub

    ''' <summary>
    ''' gets the settings for the windows location and size from xSettings, uses the form name as the node key
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="xset">the xSettings reference</param>
    ''' <remarks>will readjust the location if off the screen</remarks>
    Public Shared Sub SetWinRec(window As Form, xset As xSettings)
        SetWinRec(window, xset.ND(window.Name))
    End Sub

    ''' <summary>
    '''  gets the settings for the windows location and size from xSettings
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="nd">a pre-found node to use</param>
    ''' <remarks>will readjust the location if off the screen</remarks>
    Public Shared Sub SetWinRec(window As Form, nd As Setts_ND)
        If nd IsNot Nothing Then
            Dim msz As Size = window.MinimumSize
            Dim sloc As Point = nd.Get(KEY_LOCATION, window.Location)
            Dim ssiz As Size = nd.Get(KEY_SIZE, window.Size)

            If Not msz.IsEmpty AndAlso (msz.Width > ssiz.Width OrElse msz.Height > ssiz.Height) Then ssiz = msz

            Dim rec As New Rectangle(sloc, ssiz)
            Dim s() As Screen = Screen.AllScreens
            Dim fnd As Boolean = False
            For xloop As Integer = 0 To s.Length - 1
                Dim r As Rectangle = s(xloop).Bounds
                If r.IntersectsWith(rec) Then
                    If ssiz.Width > r.Width Then ssiz.Width = r.Width
                    If ssiz.Height > r.Height Then ssiz.Height = r.Height
                    rec.Size = ssiz
                    If r.Contains(rec) = False Then
                        If rec.X < r.X Then sloc.X = r.X
                        If rec.Y < r.Y Then sloc.Y = r.Y
                        If rec.Right > r.Right Then sloc.X = (r.Right - rec.Width)
                        If rec.Bottom > r.Bottom Then sloc.Y = (r.Bottom - rec.Height)
                    End If
                    fnd = True
                    Exit For
                End If
            Next

            If fnd Then
                window.Location = sloc
            Else
                Dim r As Rectangle = Screen.PrimaryScreen.Bounds
                window.Location = r.Location
                If ssiz.Width > r.Width Then ssiz.Width = r.Width
                If ssiz.Height > r.Height Then ssiz.Height = r.Height
            End If
            window.Size = ssiz
        End If
    End Sub

    ''' <summary>
    '''  gets the settings for the windows location and size from xSettings
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="nd">a pre-found node to use</param>
    ''' <remarks>will readjust the location if off the screen</remarks>
    Public Shared Sub SetWinLoc(window As Form, nd As Setts_ND)
        If nd IsNot Nothing Then

            Dim sloc As Point = nd.Get(KEY_LOCATION, window.Location)
            Dim ssiz As Size = window.Size

            Dim rec As New Rectangle(sloc, ssiz)
            Dim s() As Screen = Screen.AllScreens
            Dim fnd As Boolean = False
            For xloop As Integer = 0 To s.Length - 1
                Dim r As Rectangle = s(xloop).Bounds
                If r.IntersectsWith(rec) Then
                    If ssiz.Width > r.Width Then ssiz.Width = r.Width
                    If ssiz.Height > r.Height Then ssiz.Height = r.Height
                    rec.Size = ssiz
                    If r.Contains(rec) = False Then
                        If rec.X < r.X Then sloc.X = r.X
                        If rec.Y < r.Y Then sloc.Y = r.Y
                        If rec.Right > r.Right Then sloc.X = (r.Right - rec.Width)
                        If rec.Bottom > r.Bottom Then sloc.Y = (r.Bottom - rec.Height)
                    End If
                    fnd = True
                    Exit For
                End If
            Next

            If fnd Then
                window.Location = sloc
            Else
                Dim r As Rectangle = Screen.PrimaryScreen.Bounds
                window.Location = r.Location
            End If
        End If
    End Sub

    ''' <summary>
    ''' saves the screen location and size only if the window state is 'normal'
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="xset">the xSettings reference</param>
    ''' <param name="windowName">the xmlnode name to lookup</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveWinRec(window As Form, xset As xSettings, windowname As String)
        SaveWinRec(window, xset.ND(windowname))
    End Sub

    ''' <summary>
    ''' saves the screen location and size only if the window state is 'normal', uses the form name as the node key
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="xset">the xSettings reference</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveWinRec(window As Form, xset As xSettings)
        SaveWinRec(window, xset.ND(window.Name))
    End Sub

    ''' <summary>
    ''' saves the screen location and size only if the window state is 'normal'
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="nd">a pre-found node to use</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveWinRec(window As Form, nd As Setts_ND)
        If window.WindowState = FormWindowState.Normal Then
            nd.Set(KEY_LOCATION, window.Location)
            nd.Set(KEY_SIZE, window.Size)
        End If
    End Sub

    ''' <summary>
    ''' saves the screen location and size only if the window state is 'normal'
    ''' </summary>
    ''' <param name="window">the form to get</param>
    ''' <param name="nd">a pre-found node to use</param>
    ''' <remarks></remarks>
    Public Shared Sub SaveWinLoc(window As Form, nd As Setts_ND)
        If window.WindowState = FormWindowState.Normal Then
            nd.Set(KEY_LOCATION, window.Location)
        End If
    End Sub

End Class
