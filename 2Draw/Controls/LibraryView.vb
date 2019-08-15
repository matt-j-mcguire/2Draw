Public Class LibraryView

    Private _list As List(Of SymbolLib.Library.Symbol)
    Private _maxItemsWidth As Integer = 1
    Public Event MouseOverItem(sender As Object, item As SymbolLib.Library.Symbol)

    Public Sub New()
        _list = New List(Of SymbolLib.Library.Symbol)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
         ControlStyles.Opaque Or
         ControlStyles.OptimizedDoubleBuffer Or
         ControlStyles.Selectable Or
         ControlStyles.UserPaint Or
         ControlStyles.ResizeRedraw, True)




    End Sub

#Region "Add, remove, clear"

    Public Sub add(item As SymbolLib.Library.Symbol)
        _list.Add(item)
        Regen()
    End Sub

    Public Sub addRange(item() As SymbolLib.Library.Symbol)
        _list.AddRange(item)
        Regen()
    End Sub

    Public Sub Remove(item As SymbolLib.Library.Symbol)
        _list.Remove(item)
        Regen()
    End Sub

    Public Sub clear()
        _list.Clear()
        Regen()
    End Sub

#End Region



    Public Sub Regen()
        'set the scrollbar for vertical only
        _maxItemsWidth = Me.Width \ SymbolLib.Library.Symbol.THUMBSIZE
        If _maxItemsWidth < 1 Then _maxItemsWidth = 1
        sel_index = -1
        'figure out how long the list will be
        Dim top As Integer = 0
        Dim left As Integer = 0
        For i As Integer = 0 To _list.Count - 1
            If left >= _maxItemsWidth * SymbolLib.Library.Symbol.THUMBSIZE Then
                top = _list(i - 1).DisplayRec.Bottom
                left = 0
            End If
            _list(i).DisplayRec = New Rectangle(left, top, SymbolLib.Library.Symbol.THUMBSIZE, SymbolLib.Library.Symbol.THUMBSIZE)
            left = _list(i).DisplayRec.Right
        Next
        Me.AutoScrollMinSize = New Size(0, top + SymbolLib.Library.Symbol.THUMBSIZE)

        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.Clear(Me.BackColor)

        e.Graphics.TranslateTransform(Me.AutoScrollPosition.X, Me.AutoScrollPosition.Y)
        For i As Integer = 0 To _list.Count - 1
            If sel_index = i Or mouseoverindex = i Then
                e.Graphics.DrawRectangle(Pens.CornflowerBlue, _list(i).DisplayRec)
                Using bx As New SolidBrush(Color.FromArgb(50, Color.CornflowerBlue))
                    e.Graphics.FillRectangle(bx, _list(i).DisplayRec)
                End Using
            End If
            e.Graphics.DrawImage(_list(i).Thumbnail, _list(i).DisplayRec)
        Next
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Regen()
    End Sub

    Private sel_index As Integer
    Private mouseoverindex As Integer
    ' Private DragRec As Rectangle
    Private screenOffset As Point

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Dim fndindex As Integer = -1
        'apply the scrolling offset
        Dim pt As New Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y)
        For i As Integer = 0 To _list.Count - 1
            If _list(i).DisplayRec.Contains(pt.X, pt.Y) Then
                RaiseEvent MouseOverItem(Me, _list(i))
                fndindex = i
                Exit For
            End If
        Next

        sel_index = fndindex
        Me.Invalidate()
    End Sub



    Public ReadOnly Property SelectedItem() As SymbolLib.Library.Symbol
        Get
            If sel_index = -1 Then
                Return Nothing
            Else
                Return _list(sel_index)
            End If
        End Get
    End Property

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim pt As New Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y)


        If e.Button = MouseButtons.Left Then
            If sel_index > -1 AndAlso Not _list(sel_index).DisplayRec.Contains(pt.X, pt.Y) Then
                screenOffset = SystemInformation.WorkingArea.Location
                ' Proceed with the drag-and-drop, passing in the list item.                     
                Dim dropEffect As DragDropEffects = Me.DoDragDrop(_list(sel_index).item.UID, DragDropEffects.Copy Or DragDropEffects.Move)
            End If
        Else
            Dim fnd As Boolean
            For i As Integer = 0 To _list.Count - 1
                If _list(i).DisplayRec.Contains(pt.X, pt.Y) Then
                    RaiseEvent MouseOverItem(Me, _list(i))
                    mouseoverindex = i
                    fnd = True
                    Exit For
                End If
            Next
            If Not fnd Then mouseoverindex = -1
        End If
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        mouseoverindex = -1
        Me.Invalidate
    End Sub

    Private Sub me_QueryContinueDrag(ByVal sender As Object, ByVal e As QueryContinueDragEventArgs) Handles Me.QueryContinueDrag
        ' Cancel the drag if the mouse moves off the form. 
        Dim f As Form = Me.FindForm()

        ' Cancel the drag if the mouse moves off the form. The screenOffset 
        ' takes into account any desktop bands that may be at the top or left 
        ' side of the screen. 
        If (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) Or
                ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) Or
                ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) Or
                ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom)) Then

            e.Action = DragAction.Cancel
        End If
    End Sub


End Class



''' <summary>
''' example
''' </summary>

Public NotInheritable Class Form1
    Inherits System.Windows.Forms.Form

#Region "fldjf"
    Friend WithEvents ListDragSource As System.Windows.Forms.ListBox
    Friend WithEvents ListDragTarget As System.Windows.Forms.ListBox
    Friend WithEvents UseCustomCursorsCheck As System.Windows.Forms.CheckBox
    Friend WithEvents DropLocationLabel As System.Windows.Forms.Label

    Private indexOfItemUnderMouseToDrag As Integer
    Private indexOfItemUnderMouseToDrop As Integer

    Private dragBoxFromMouseDown As Rectangle
    Private screenOffset As Point

    Private MyNoDropCursor As Cursor
    Private MyNormalCursor As Cursor

    <System.STAThread()>
    Public Shared Sub Main()
        System.Windows.Forms.Application.Run(New Form1())
    End Sub 'Main
#End Region


#Region "source"

    Public Sub New()
        MyBase.New()

        Me.ListDragSource = New System.Windows.Forms.ListBox()
        Me.ListDragTarget = New System.Windows.Forms.ListBox()
        Me.UseCustomCursorsCheck = New System.Windows.Forms.CheckBox()
        Me.DropLocationLabel = New System.Windows.Forms.Label()

        Me.SuspendLayout()

        ' ListDragSource 
        Me.ListDragSource.Items.AddRange(New Object() {"one", "two", "three", "four",
                                                            "five", "six", "seven", "eight",
                                                            "nine", "ten"})
        Me.ListDragSource.Location = New System.Drawing.Point(10, 17)
        Me.ListDragSource.Size = New System.Drawing.Size(120, 225)

        ' ListDragTarget 
        Me.ListDragTarget.AllowDrop = True
        Me.ListDragTarget.Location = New System.Drawing.Point(154, 17)
        Me.ListDragTarget.Size = New System.Drawing.Size(120, 225)

        ' UseCustomCursorsCheck 
        Me.UseCustomCursorsCheck.Location = New System.Drawing.Point(10, 243)
        Me.UseCustomCursorsCheck.Size = New System.Drawing.Size(137, 24)
        Me.UseCustomCursorsCheck.Text = "Use Custom Cursors"

        ' DropLocationLabel 
        Me.DropLocationLabel.Location = New System.Drawing.Point(154, 245)
        Me.DropLocationLabel.Size = New System.Drawing.Size(137, 24)
        Me.DropLocationLabel.Text = "None"

        ' Form1 
        Me.ClientSize = New System.Drawing.Size(292, 270)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.ListDragSource,
                                            Me.ListDragTarget, Me.UseCustomCursorsCheck,
                                            Me.DropLocationLabel})

        Me.Text = "drag-and-drop Example"
        Me.ResumeLayout(False)
    End Sub

    Private Sub ListDragSource_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListDragSource.MouseDown

        ' Get the index of the item the mouse is below.
        indexOfItemUnderMouseToDrag = ListDragSource.IndexFromPoint(e.X, e.Y)

        If (indexOfItemUnderMouseToDrag <> ListBox.NoMatches) Then

            ' Remember the point where the mouse down occurred. The DragSize indicates 
            ' the size that the mouse can move before a drag event should be started.                 
            Dim dragSize As Size = SystemInformation.DragSize

            ' Create a rectangle using the DragSize, with the mouse position being 
            ' at the center of the rectangle.
            dragBoxFromMouseDown = New Rectangle(New Point(e.X - (dragSize.Width / 2),
                                                            e.Y - (dragSize.Height / 2)), dragSize)
        Else
            ' Reset the rectangle if the mouse is not over an item in the ListBox.
            dragBoxFromMouseDown = Rectangle.Empty
        End If

    End Sub

    Private Sub ListDragSource_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListDragSource.MouseUp

        ' Reset the drag rectangle when the mouse button is raised.
        dragBoxFromMouseDown = Rectangle.Empty
    End Sub

    Private Sub ListDragSource_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListDragSource.MouseMove

        If ((e.Button And MouseButtons.Left) = MouseButtons.Left) Then

            ' If the mouse moves outside the rectangle, start the drag. 
            If (Rectangle.op_Inequality(dragBoxFromMouseDown, Rectangle.Empty) And
                Not dragBoxFromMouseDown.Contains(e.X, e.Y)) Then

                ' Creates custom cursors for the drag-and-drop operation. 
                Try
                    MyNormalCursor = New Cursor("3dwarro.cur")
                    MyNoDropCursor = New Cursor("3dwno.cur")

                Catch
                    ' An error occurred while attempting to load the cursors so use 
                    ' standard cursors.
                    UseCustomCursorsCheck.Checked = False
                Finally
                    ' The screenOffset is used to account for any desktop bands  
                    ' that may be at the top or left side of the screen when  
                    ' determining when to cancel the drag drop operation.
                    screenOffset = SystemInformation.WorkingArea.Location

                    ' Proceed with the drag-and-drop, passing in the list item.                     
                    Dim dropEffect As DragDropEffects = ListDragSource.DoDragDrop(ListDragSource.Items(indexOfItemUnderMouseToDrag),
                                                                                  DragDropEffects.All Or DragDropEffects.Link)

                    ' If the drag operation was a move then remove the item. 
                    If (dropEffect = DragDropEffects.Move) Then
                        ListDragSource.Items.RemoveAt(indexOfItemUnderMouseToDrag)

                        ' Select the previous item in the list as long as the list has an item. 
                        If (indexOfItemUnderMouseToDrag > 0) Then
                            ListDragSource.SelectedIndex = indexOfItemUnderMouseToDrag - 1

                        ElseIf (ListDragSource.Items.Count > 0) Then
                            ' Selects the first item.
                            ListDragSource.SelectedIndex = 0
                        End If
                    End If

                    ' Dispose the cursors since they are no longer needed. 
                    If (Not MyNormalCursor Is Nothing) Then _
                        MyNormalCursor.Dispose()

                    If (Not MyNoDropCursor Is Nothing) Then _
                        MyNoDropCursor.Dispose()
                End Try

            End If
        End If
    End Sub


    Private Sub ListDragSource_GiveFeedback(ByVal sender As Object, ByVal e As GiveFeedbackEventArgs) Handles ListDragSource.GiveFeedback
        ' Use custom cursors if the check box is checked. 
        If (UseCustomCursorsCheck.Checked) Then

            ' Set the custom cursor based upon the effect.
            e.UseDefaultCursors = False
            If ((e.Effect And DragDropEffects.Move) = DragDropEffects.Move) Then
                Cursor.Current = MyNormalCursor
            Else
                Cursor.Current = MyNoDropCursor
            End If
        End If

    End Sub

    Private Sub ListDragSource_QueryContinueDrag(ByVal sender As Object, ByVal e As QueryContinueDragEventArgs) Handles ListDragSource.QueryContinueDrag
        ' Cancel the drag if the mouse moves off the form. 
        Dim lb As ListBox = CType(sender, System.Windows.Forms.ListBox)

        If (lb IsNot Nothing) Then

            Dim f As Form = lb.FindForm()

            ' Cancel the drag if the mouse moves off the form. The screenOffset 
            ' takes into account any desktop bands that may be at the top or left 
            ' side of the screen. 
            If (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) Or
                ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) Or
                ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) Or
                ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom)) Then

                e.Action = DragAction.Cancel
            End If
        End If
    End Sub

#End Region

#Region "Target"



    Private Sub ListDragTarget_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles ListDragTarget.DragOver
        ' Determine whether string data exists in the drop data. If not, then 
        ' the drop effect reflects that the drop cannot occur. 
        If Not (e.Data.GetDataPresent(GetType(System.String))) Then

            e.Effect = DragDropEffects.None
            DropLocationLabel.Text = "None - no string data."
            Return
        End If

        ' Set the effect based upon the KeyState. 
        If ((e.KeyState And (8 + 32)) = (8 + 32) And
            (e.AllowedEffect And DragDropEffects.Link) = DragDropEffects.Link) Then
            ' KeyState 8 + 32 = CTL + ALT 

            ' Link drag-and-drop effect.
            e.Effect = DragDropEffects.Link

        ElseIf ((e.KeyState And 32) = 32 And
            (e.AllowedEffect And DragDropEffects.Link) = DragDropEffects.Link) Then

            ' ALT KeyState for link.
            e.Effect = DragDropEffects.Link

        ElseIf ((e.KeyState And 4) = 4 And
            (e.AllowedEffect And DragDropEffects.Move) = DragDropEffects.Move) Then

            ' SHIFT KeyState for move.
            e.Effect = DragDropEffects.Move

        ElseIf ((e.KeyState And 8) = 8 And
            (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy) Then

            ' CTL KeyState for copy.
            e.Effect = DragDropEffects.Copy

        ElseIf ((e.AllowedEffect And DragDropEffects.Move) = DragDropEffects.Move) Then

            ' By default, the drop action should be move, if allowed.
            e.Effect = DragDropEffects.Move

        Else
            e.Effect = DragDropEffects.None
        End If

        ' Gets the index of the item the mouse is below.  

        ' The mouse locations are relative to the screen, so they must be  
        ' converted to client coordinates.

        indexOfItemUnderMouseToDrop =
            ListDragTarget.IndexFromPoint(ListDragTarget.PointToClient(New Point(e.X, e.Y)))

        ' Updates the label text. 
        If (indexOfItemUnderMouseToDrop <> ListBox.NoMatches) Then

            DropLocationLabel.Text = "Drops before item #" & (indexOfItemUnderMouseToDrop + 1)
        Else
            DropLocationLabel.Text = "Drops at the end."
        End If

    End Sub


    Private Sub ListDragTarget_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles ListDragTarget.DragDrop
        ' Ensures that the list item index is contained in the data. 

        If (e.Data.GetDataPresent(GetType(System.String))) Then

            Dim item As Object = CType(e.Data.GetData(GetType(System.String)), System.Object)

            ' Perform drag-and-drop, depending upon the effect. 
            If (e.Effect = DragDropEffects.Copy Or
                e.Effect = DragDropEffects.Move) Then

                ' Insert the item. 
                If (indexOfItemUnderMouseToDrop <> ListBox.NoMatches) Then
                    ListDragTarget.Items.Insert(indexOfItemUnderMouseToDrop, item)
                Else
                    ListDragTarget.Items.Add(item)

                End If
            End If
            ' Reset the label text.
            DropLocationLabel.Text = "None"
        End If
    End Sub

    Private Sub ListDragTarget_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles ListDragTarget.DragEnter

        ' Reset the label text.
        DropLocationLabel.Text = "None"
    End Sub

    Private Sub ListDragTarget_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListDragTarget.DragLeave

        ' Reset the label text.
        DropLocationLabel.Text = "None"
    End Sub

#End Region
End Class