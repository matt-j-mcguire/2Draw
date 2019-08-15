

Public Class AppWarnings

    Public Const UNGROUP As String = "Ungrouping this item will cause all pieces to expand to normal size"
    Public Const LASTLIB As String = "Cannot Delete; you must have at least one Library."
    Public Const REMOVELIB As String = "Removeing library deletes it forever!"


    Private _usedlist As List(Of String)

    Public Sub New()
        Dim lst() As String = My.Settings.Warnings.Split(",".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
        _usedlist = New List(Of String)
        For i As Integer = 0 To lst.Length - 1
            _usedlist.Add(lst(i))
        Next
    End Sub

    Public Sub Save()
        My.Settings.Warnings = String.Join(",", _usedlist.ToArray)
    End Sub

    ''' <summary>
    ''' returns true if user has ok'ed
    ''' </summary>
    ''' <param name="Warningconstant"></param>
    ''' <returns></returns>
    Public Function Show(Warningconstant As String) As Boolean
        Dim hs As String = Warningconstant.GetHashCode.ToString
        If Not _usedlist.Contains(hs) Then
            Dim ex As New WarningMSG(Warningconstant)
            Dim rslt As DialogResult = ex.ShowDialog
            If rslt = DialogResult.Yes Then
                _usedlist.Add(hs)
                Return True
            ElseIf rslt = DialogResult.OK Then
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If
    End Function


End Class
