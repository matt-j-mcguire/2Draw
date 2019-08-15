Public Class SymbolManage

    Private SelBack As Color = Color.FromArgb(&HFF828282)
    Private SelText As Color = Color.White

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        txtSymbolLocation.Text = SymbolLibs.SymbolFileName

        lbLibraries.Items.AddRange(SymbolLibs.Librarys.ToArray)
        lbLibraries.SelectedIndex = 0
    End Sub



    Private Sub blLibraries_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lbLibraries.DrawItem
        If e.Index > -1 Then
            Dim bc, fc As Color
            If e.State And DrawItemState.Selected Then
                bc = SelBack
                fc = SelText
            Else
                bc = Color.White
                fc = Color.Black
            End If

            Dim l As SymbolLib.Library = lbLibraries.Items.Item(e.Index)
            Using b As New SolidBrush(bc), f As New SolidBrush(fc), strf As New StringFormat
                e.Graphics.FillRectangle(b, e.Bounds)
                strf.LineAlignment = StringAlignment.Center
                e.Graphics.DrawString(l.Name, Me.Font, f, e.Bounds, strf)
            End Using

        End If
    End Sub

    Private Sub tsAddLibrary_Click(sender As Object, e As EventArgs) Handles tsAddLibrary.Click
        Dim lname As String = InputBox("Name for new library", "New Library", "")
        If Not String.IsNullOrEmpty(lname) Then
            Dim ex As New SymbolLib.Library(lname)
            SymbolLibs.Librarys.Add(ex)
            lbLibraries.Items.Add(ex)
        End If
    End Sub

    Private Sub tsRemoveLibrary_Click(sender As Object, e As EventArgs) Handles tsRemoveLibrary.Click
        If lbLibraries.Items.Count > 1 Then
            If lbLibraries.SelectedIndex > -1 AndAlso Warnings.Show(AppWarnings.REMOVELIB) Then
                Dim l As SymbolLib.Library = lbLibraries.SelectedItem
                SymbolLibs.Librarys.Remove(l)
                lbLibraries.Items.Remove(l)
            End If
        Else
            Globals.Warnings.Show(AppWarnings.LASTLIB)
        End If
    End Sub


    Private Sub blLibraries_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbLibraries.SelectedIndexChanged
        lvItems.clear()
        If lbLibraries.SelectedIndex > -1 Then
            Dim l As SymbolLib.Library = lbLibraries.SelectedItem
            lvItems.addRange(l.Items.ToArray)
        End If
    End Sub

    Private Sub DeleteSymbolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteSymbolToolStripMenuItem.Click
        Dim sl As SymbolLib.Library.Symbol = lvItems.SelectedItem
        If sl IsNot Nothing Then
            Dim l As SymbolLib.Library = lbLibraries.SelectedItem
            l.Items.Remove(sl)
            lvItems.Remove(sl)
        End If
    End Sub


    Private Sub lvItems_MouseUp(sender As Object, e As MouseEventArgs) Handles lvItems.MouseUp
        If e.Button = MouseButtons.Right Then
            Dim sl As SymbolLib.Library.Symbol = lvItems.SelectedItem
            If sl IsNot Nothing Then cmsItems.Show(lvItems, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub lbLibraries_DragOver(sender As Object, e As DragEventArgs) Handles lbLibraries.DragOver
        Dim index As Integer = lbLibraries.IndexFromPoint(lbLibraries.PointToClient(New Point(e.X, e.Y)))
        If index > -1 AndAlso index <> lbLibraries.SelectedIndex Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub


    Private Sub lbLibraries_DragDrop(sender As Object, e As DragEventArgs) Handles lbLibraries.DragDrop
        If e.Data.GetDataPresent(DataFormats.StringFormat) Then

            Dim item As String = CType(e.Data.GetData(GetType(System.String)), System.String)
            Dim l As SymbolLib.Library = lbLibraries.SelectedItem
            Dim sym As SymbolLib.Library.Symbol = Nothing
            For i As Integer = 0 To l.Items.Count - 1
                If l.Items(i).item.UID = item Then
                    sym = l.Items.Item(i)
                End If
            Next
            Dim lx As SymbolLib.Library = Nothing
            Dim index As Integer = lbLibraries.IndexFromPoint(lbLibraries.PointToClient(New Point(e.X, e.Y)))
            If index > -1 AndAlso sym IsNot Nothing Then
                lx = lbLibraries.Items(index)
                If lx IsNot l Then
                    l.Items.Remove(sym)
                    lvItems.Remove(sym)
                    lx.Items.Add(sym)
                End If
            End If
        End If
    End Sub

    Private Sub tsImportsymbols_Click(sender As Object, e As EventArgs) Handles tsImportsymbols.Click
        MessageBox.Show("not implemented yet")
    End Sub


End Class