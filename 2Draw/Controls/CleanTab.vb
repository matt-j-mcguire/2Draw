Public Class CleanTab
    Inherits System.Windows.Forms.UserControl

#Region "Variables"
    ''' <summary>
    ''' list of current tabs
    ''' </summary>
    ''' <remarks></remarks>
    Private _tabs As List(Of Tab)
    ''' <summary>
    ''' location for the drop arrow (shows all the tabs, hidden or not)
    ''' </summary>
    ''' <remarks></remarks>
    Private _droparrowRec As Rectangle
    ''' <summary>
    ''' location of the menu (if it exists!)
    ''' </summary>
    ''' <remarks></remarks>
    Private _menuDropRec As Rectangle
#End Region

#Region "Public Events"
    ''' <summary>
    ''' raised when ever a tab is closeing, can be canceled
    ''' </summary>
    ''' <param name="t">tab that will close</param>
    ''' <param name="e">allows for a cancel</param>
    ''' <remarks></remarks>
    Public Event TabClosing(t As Tab, e As CancelEventArgs)
    ''' <summary>
    ''' raised when the selected tab changes
    ''' </summary>
    ''' <param name="t">the tab that's now selected</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event SelectedTabChanged(t As Tab, e As EventArgs)
    ''' <summary>
    ''' raised when there are no more tabs in the control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event LastTabClosed(sender As Object, e As EventArgs)
    ''' <summary>
    ''' allows the owner to modify the tabMenu before it is shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="SelectedTab">what was the tab clicked on</param>
    ''' <remarks></remarks>
    Public Event PreTabMenuOpen(sender As Object, SelectedTab As CleanTab.Tab)
    ''' <summary>
    ''' allows the owner to know that a file has been dropped
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="file"></param>
    ''' <remarks>this does not do any processing on the passed file, that's up to the owner</remarks>
    Public Event FileDropped(sender As Object, file As FileInfo)
    ''' <summary>
    ''' allows the owner form to cancel a drag drop of a file, before the filedropped event is fired
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="file"></param>
    ''' <param name="Allow"></param>
    ''' <remarks></remarks>
    Public Event AllowFileDrop(sender As Object, file As FileInfo, ByRef Allow As Boolean)
    ''' <summary>
    ''' if the tab order gets changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event TabOrderChanged(sender As Object, e As EventArgs)
#End Region

    Public Sub New()
        InitializeComponent()

        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ContainerControl Or ControlStyles.Opaque Or
                 ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw Or ControlStyles.StandardClick Or
                 ControlStyles.StandardDoubleClick, True)

        SelectedBackColor = Color.SteelBlue
        SelectedBackColor = Color.White
        EmptyBackColor = Color.Gray

        _tabs = New List(Of Tab)
    End Sub

#Region "Designer Stuff"

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.cms = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ttip = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'pnl
        '
        Me.pnl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnl.BackColor = System.Drawing.SystemColors.Control
        Me.pnl.Location = New System.Drawing.Point(0, 25)
        Me.pnl.Name = "pnl"
        Me.pnl.Size = New System.Drawing.Size(584, 314)
        Me.pnl.TabIndex = 0
        '
        'cms
        '
        Me.cms.Name = "cms"
        Me.cms.Size = New System.Drawing.Size(61, 4)
        '
        'CleanTab
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnl)
        Me.Name = "CleanTab"
        Me.Padding = New System.Windows.Forms.Padding(0, 25, 0, 0)
        Me.Size = New System.Drawing.Size(584, 339)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents cms As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ttip As System.Windows.Forms.ToolTip

#End Region

#Region "Tab modification functions"

    ''' <summary>
    ''' adds a new tab programically
    ''' </summary>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub AddTab(t As Tab)
        _tabs.Add(t)
        pnl.Controls.Add(t)
        t.CleanTab = Me
        t.Dock = DockStyle.Fill
        If _tabs.Count = 1 Then SelectedTab = t
        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' closes all tabs
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseAll()
        CloseAllBut(Nothing)
        RaiseEvent SelectedTabChanged(Nothing, New EventArgs)
    End Sub

    ''' <summary>
    ''' closes all but one tab
    ''' </summary>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub CloseAllBut(t As CleanTab.Tab)
        Dim lst() As CleanTab.Tab = _tabs.ToArray
        For i As Integer = 0 To lst.Length - 1
            If t IsNot lst(i) Then Me.RemoveTab(lst(i))
        Next
    End Sub

    ''' <summary>
    ''' moves a specified tab to another cleantab container (without saving)
    ''' </summary>
    ''' <param name="t"></param>
    ''' <param name="Other"></param>
    ''' <remarks>if t or other does not exist, nothing happens</remarks>
    Public Sub MoveTabTo(t As CleanTab.Tab, Other As CleanTab)
        If _tabs.Contains(t) AndAlso Other IsNot Nothing Then
            _tabs.Remove(t)
            Other.AddTab(t)
            If _tabs.Count = 0 Then RaiseEvent LastTabClosed(Me, New EventArgs)
        End If
        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' saves one tab
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Save(t As CleanTab.Tab)
        If t IsNot Nothing Then t.Save()
        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' Saves all the tabs
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SaveAll()
        For i As Integer = 0 To _tabs.Count - 1
            _tabs(i).Save()
        Next
        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' removes a selected tab
    ''' </summary>
    ''' <param name="t"></param>
    ''' <remarks>will launch the tabClosing Event</remarks>
    Public Sub RemoveTab(t As Tab)
        Dim index As Integer = _tabs.IndexOf(t)
        If index > -1 Then
            Dim e As New CancelEventArgs
            RaiseEvent TabClosing(t, e)
            If e.Cancel = False Then
                t.Closing(e)
                If e.Cancel = False Then
                    _tabs.Remove(t)
                    t.CleanTab = Nothing
                    pnl.Controls.Remove(t)
                    If t.Selected AndAlso _tabs.Count > 0 Then
                        If index = _tabs.Count Then
                            'last one was removed in the row
                            SelectedTab = _tabs(index - 1)
                        Else
                            're-select the index
                            SelectedTab = _tabs(index)
                        End If
                    End If
                End If
            End If
            If _tabs.Count = 0 Then RaiseEvent LastTabClosed(Me, New EventArgs)
        End If
        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' moves the selected tab to the beginning of the list
    ''' </summary>
    ''' <param name="t"></param>
    ''' <remarks></remarks>
    Public Sub MoveTabBeggining(t As Tab)
        If _tabs.IndexOf(t) > -1 Then
            _tabs.Remove(t)
            _tabs.Insert(0, t)
            SelectedTab = t
            Me.Invalidate()
        End If
    End Sub

    ''' <summary>
    ''' tries to moves a tab to the desired index
    ''' </summary>
    ''' <param name="t"></param>
    ''' <param name="desiredIndex"></param>
    ''' <remarks></remarks>
    Public Sub MoveTabToIndex(t As Tab, desiredIndex As Integer)
        If _tabs.IndexOf(t) > -1 Then
            _tabs.Remove(t)
            If desiredIndex > _tabs.Count - 1 Then
                _tabs.Add(t)
            Else
                _tabs.Insert(desiredIndex, t)
            End If

            SelectedTab = t
            Me.Invalidate()
        End If
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' returns the full list of all tabs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property Items() As Tab()
        Get
            Return _tabs.ToArray
        End Get
    End Property

    ''' <summary>
    ''' the back color for the selected tab (and hightlighted tab)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), DefaultValue("Color.SteelBlue")> _
    Public Property SelectedBackColor() As Color

    ''' <summary>
    ''' the fore color for the selected tab (and hightlighted tab)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), DefaultValue("Color.White")> _
    Public Property SelectedForeColor() As Color

    ''' <summary>
    ''' the inner color to show no current tabs available
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), DefaultValue("Color.Gray")> _
    Public Property EmptyBackColor() As Color

    <Category("Behavior")>
    Public Property Menu() As ContextMenuStrip

    <Category("Behavior")>
    Public Property TabRclickMenu() As ContextMenuStrip

    <Category("Behavior")>
    Public Property MenuIcon() As Image

    ''' <summary>
    ''' gets or sets the selected tab
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public Property SelectedTab() As Tab
        Get
            Dim ret As Tab = Nothing
            For i As Integer = 0 To _tabs.Count - 1
                If _tabs(i).Selected Then
                    ret = _tabs(i)
                    Exit For
                End If
            Next
            Return ret
        End Get
        Set(value As Tab)
            Dim old As Tab = Me.SelectedTab
            If old IsNot Nothing Then old.TabLostFocus()

            For i As Integer = 0 To _tabs.Count - 1
                If _tabs(i) Is value Then
                    If Not _tabs(i).Selected Then RaiseEvent SelectedTabChanged(value, New EventArgs)
                    _tabs(i).Selected = True
                    _tabs(i).TabGotFocus()
                    _tabs(i).BringToFront()
                Else
                    _tabs(i).Selected = False
                End If
            Next
            Me.Invalidate()
        End Set
    End Property


    <Category("Behavior")> Public Property AllowCloseOfTabs() As Boolean


#End Region

#Region "tab Drawing"

    ''' <summary>
    ''' paints the tab header
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim pt As Point = Me.PointToClient(MousePosition)


        Using b As New SolidBrush(Me.BackColor)
            e.Graphics.FillRectangle(b, New Rectangle(0, 0, Me.Width, Me.Height))
        End Using
        _droparrowRec = New Rectangle(Me.Width - 24, 4, 16, 16)
        If _droparrowRec.Contains(pt) Then
            Using b As New SolidBrush(ScaleColor(BackColor, 1.25))
                e.Graphics.FillRectangle(b, _droparrowRec)
            End Using
        End If
        e.Graphics.DrawImage(My.Resources.downarrow, _droparrowRec, 0, 0, _droparrowRec.Width, _droparrowRec.Height, GraphicsUnit.Pixel, ModImageColor(Me.ForeColor))


        Dim l As Integer = 0
        '----------draws the drop down menu if available-----------
        If Menu IsNot Nothing Then
            _menuDropRec = New Rectangle(1, 1, 24, 24)
            If _menuDropRec.Contains(pt) Then
                Using bc As New SolidBrush(ScaleColor(BackColor, 1.25))
                    e.Graphics.FillRectangle(bc, _menuDropRec)
                End Using
            End If

            If MenuIcon IsNot Nothing Then
                e.Graphics.DrawImage(MenuIcon, 4, 4, 16, 16)
            End If
            l = 26
        End If




        Using xstrf As New StringFormat()
            xstrf.Alignment = StringAlignment.Near
            xstrf.LineAlignment = StringAlignment.Center



            For i As Integer = 0 To _tabs.Count - 1
                Dim sz As SizeF = e.Graphics.MeasureString(_tabs(i).Text, Me.Font)
                Dim r As New Rectangle(l, 0, sz.Width + If(AllowCloseOfTabs, 32, 5), 24)
                'don't paint any more tabs if over the drop down arrow
                If r.Right > _droparrowRec.Left Then Exit For
                Dim showx As Boolean = False

                Using fc As New SolidBrush(Me.ForeColor)
                    Using bc As New SolidBrush(Me.BackColor)
                        If _tabs(i).Selected Then
                            ' r.Y = 0
                            ' r.Height = 24
                            bc.Color = Me.SelectedBackColor
                            fc.Color = Me.SelectedForeColor
                            If AllowCloseOfTabs Then showx = True
                        ElseIf r.Contains(pt) Then
                            bc.Color = ScaleColor(BackColor, 1.25)
                            fc.Color = Me.ForeColor
                            If AllowCloseOfTabs Then showx = True
                        End If
                            e.Graphics.FillRectangle(bc, r)
                    End Using


                    If showx Then
                        Dim xrec As New Rectangle(r.Right - 20, r.Top + (r.Height - 16) \ 2, 16, 16)
                        If xrec.Contains(pt) Then
                            Using b As New SolidBrush(If(_tabs(i).Selected, ScaleColor(SelectedBackColor, 1.25), BackColor))
                                Dim t As Rectangle = xrec
                                t.Inflate(-2, -2)
                                e.Graphics.FillRectangle(b, t)
                            End Using
                        End If

                        e.Graphics.DrawImage(My.Resources.Close1, xrec, 0, 0, xrec.Width, xrec.Height, GraphicsUnit.Pixel, ModImageColor(fc.Color))
                        _tabs(i).CloseRec = xrec
                    End If

                    Dim tx As String = _tabs(i).Text & If(_tabs(i).DirtyBit, "*", "")
                    e.Graphics.DrawString(tx, Me.Font, fc, r, xstrf)
                End Using

                _tabs(i).TitleRec = r
                l = r.Right + 1
            Next


            If _tabs.Count > 0 Then
                Using bc As New SolidBrush(Me.SelectedBackColor)
                    e.Graphics.FillRectangle(bc, New Rectangle(0, 21, Me.Width, 10))
                End Using
            End If


        End Using
    End Sub

    ''' <summary>
    ''' modifies a ImageAttributes to display a image as partially transparent, with a new color
    ''' </summary>
    ''' <param name="col">color to ckange the image to</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ModImageColor(ByVal col As Color) As System.Drawing.Imaging.ImageAttributes
        Dim r As Double = col.R / 255
        Dim g As Double = col.G / 255
        Dim b As Double = col.B / 255
        Dim a As Double = col.A / 255

        Dim cm As New System.Drawing.Imaging.ColorMatrix(New Single()() _
       {New Single() {r, 0, 0, 0, 0}, _
        New Single() {0, g, 0, 0, 0}, _
        New Single() {0, 0, b, 0, 0}, _
        New Single() {0, 0, 0, a, 0}, _
        New Single() {0, 0, 0, 0, 1.0}})

        ' Create an ImageAttributes object and set its color matrix.
        Dim imgattr As New System.Drawing.Imaging.ImageAttributes
        imgattr.SetColorMatrix(cm)

        Return imgattr
    End Function

#End Region

#Region "Panel drawing"

    ''' <summary>
    ''' paints the inner empty panel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pnl_Paint(sender As Object, e As PaintEventArgs) Handles pnl.Paint
        Using bc As New SolidBrush(EmptyBackColor)
            e.Graphics.FillRectangle(bc, pnl.DisplayRectangle)
            Using p As New Pen(ScaleColor(bc.Color, 0.75))
                e.Graphics.DrawLine(p, 0, 0, pnl.Width, 0)
            End Using

        End Using


    End Sub

    ''' <summary>
    ''' scales a color lighter or darker
    ''' </summary>
    ''' <param name="sourceColor">origional color</param>
    ''' <param name="scale">0.0 to 2.0</param>
    ''' <returns></returns>
    ''' <remarks>0.0-0.99 goes darker, 1.01-1.99 goes lighter</remarks>
    Public Shared Function ScaleColor(ByVal sourceColor As Color, ByVal scale As Single) As Color
        Dim newR As Integer = CInt((sourceColor.R * scale))
        Dim newG As Integer = CInt((sourceColor.G * scale))
        Dim newB As Integer = CInt((sourceColor.B * scale))
        If newR > 255 Then
            newR = 255
        End If
        If newG > 255 Then
            newG = 255
        End If
        If newB > 255 Then
            newB = 255
        End If
        Return Color.FromArgb(newR, newG, newB)
    End Function

#End Region

    ''' <summary>
    ''' captures the mouse click for changing tabs or removing them
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)

        For i As Integer = 0 To _tabs.Count - 1
            Dim r As Rectangle = _tabs(i).TitleRec
            If r.Contains(e.X, e.Y) Then
                If _tabs(i).CloseRec.Contains(e.X, e.Y) AndAlso AllowCloseOfTabs Then
                    Me.RemoveTab(_tabs(i))
                    Exit For
                Else
                    SelectedTab = _tabs(i)
                End If
            End If
        Next

        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' for capturing the dropdown for all the tabs
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If _droparrowRec.Contains(e.X, e.Y) AndAlso e.Button = Windows.Forms.MouseButtons.Left Then
            For i As Integer = 0 To cms.Items.Count - 1
                RemoveHandler cms.Items(i).Click, AddressOf Tb_click
            Next
            cms.Items.Clear()
            For i As Integer = 0 To _tabs.Count - 1
                Dim x As New ToolStripMenuItem(_tabs(i).Text, Nothing, AddressOf Tb_click)
                x.Tag = _tabs(i)
                cms.Items.Add(x)
            Next
            cms.Show(Me, _droparrowRec.X, _droparrowRec.Bottom)
        ElseIf Menu IsNot Nothing AndAlso _menuDropRec.Contains(e.X, e.Y) AndAlso e.Button = Windows.Forms.MouseButtons.Left Then
            Menu.Show(Me, _menuDropRec.X, _menuDropRec.Bottom)
        ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso TabRclickMenu IsNot Nothing Then
            For i As Integer = 0 To _tabs.Count - 1
                If _tabs(i).TitleRec.Contains(e.X, e.Y) Then
                    SelectedTab = _tabs(i)
                    RaiseEvent PreTabMenuOpen(Me, SelectedTab)
                    TabRclickMenu.Show(Me, e.X, e.Y)
                    Exit For
                End If
            Next
        ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
            For i As Integer = 0 To _tabs.Count - 1
                If _tabs(i).TitleRec.Contains(e.X, e.Y) AndAlso Not _tabs(i).CloseRec.Contains(e.X, e.Y) Then
                    SelectedTab = _tabs(i)
                    Me.DoDragDrop(_tabs(i), DragDropEffects.Move)
                    Exit For
                End If
            Next

        End If
    End Sub

 

    ''' <summary>
    ''' when one of the menuitems from the drop down gets selected
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Tb_click(sender As Object, e As EventArgs)
        Dim t As Tab = DirectCast(sender, ToolStripMenuItem).Tag
        Me.MoveTabBeggining(t)
    End Sub

    ''' <summary>
    ''' forces a repaint of the header
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TC_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, Me.LostFocus, Me.MouseCaptureChanged
        Me.Invalidate()
    End Sub

    ''' <summary>
    ''' for showing the helpfull hints during the mouse over
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TC_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.None Then
            Dim str As String = ""
            Dim btm As Integer
            For i As Integer = 0 To _tabs.Count - 1
                If _tabs(i).TitleRec.Contains(e.X, e.Y) Then
                    str = _tabs(i).FullText
                    If _tabs(i).CloseRec.Contains(e.X, e.Y) AndAlso AllowCloseOfTabs Then
                        str = "Close"
                    End If
                    btm = _tabs(i).TitleRec.Bottom
                End If
            Next

            If str = "" Then
                ttip.Hide(Me)
            ElseIf str <> ttip.GetToolTip(Me) Then
                ttip.SetToolTip(Me, str)
                ttip.Show(str, Me, e.X, btm + 5, 2000)
            End If
        End If
        Me.Invalidate()
    End Sub

#Region "Drag Drop"

    Private Sub CleanTab_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        ' Determine whether string data exists in the drop data. If not, then
        ' the drop effect reflects that the drop cannot occur.
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim draggedfiles As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            For Each x As String In draggedfiles
                Dim xfile As New FileInfo(x)
                Dim allow As Boolean = False
                RaiseEvent AllowFileDrop(Me, xfile, allow)
                If allow Then
                    e.Effect = DragDropEffects.Copy
                End If
            Next
        Else
            Dim o As Object = e.Data.GetData((e.Data.GetFormats(False)(0)))
            If TypeOf o Is CleanTab.Tab Then
                'all good here, just moving a tab around
                e.Effect = DragDropEffects.Move
            Else
                e.Effect = DragDropEffects.None
                Return
            End If
        End If
    End Sub

    Private Sub CleanTab_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim draggedfiles As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            For Each x As String In draggedfiles
                Dim xfile As New FileInfo(x)
                RaiseEvent FileDropped(Me, xfile)
            Next
        Else
            Dim fmt() As String = e.Data.GetFormats(False)
            If fmt.Length > 0 AndAlso (e.Data.GetDataPresent(fmt(0))) Then
                Dim item As CleanTab.Tab = e.Data.GetData((e.Data.GetFormats(False)(0)))

                ' Perform drag and drop, depending upon the effect.
                Dim index As Integer = GetTabIndex(Me.PointToClient(New Point(e.X, e.Y)))
                If e.Effect = DragDropEffects.Move Then
                    item.CleanTab.MoveTabTo(item, Me)
                    Me.MoveTabToIndex(item, index)
                    RaiseEvent TabOrderChanged(Me, New EventArgs)
                End If
            End If
        End If
        Me.Invalidate()
    End Sub

    Private Function GetTabIndex(cord As Point) As Integer
        Dim ret As Integer = -1
        For i As Integer = 0 To _tabs.Count - 1
            If _tabs(i).TitleRec.Contains(cord) Then
                ret = i
                Exit For
            End If
        Next
        If ret = -1 Then ret = _tabs.Count
        Return ret
    End Function

    Private Sub CleanTab_QueryContinueDrag(sender As Object, e As QueryContinueDragEventArgs) Handles Me.QueryContinueDrag
        Dim p As Form = Me.FindForm
        Dim pt As Point = Control.MousePosition
        ' Cancel the drag if the mouse moves off the listbox. The screenOffset
        ' takes into account any desktop bands that may be at the top or left
        ' side of the screen.

        If ((pt.X < p.Left) Or _
            (pt.X > p.Right) Or _
            (pt.Y < p.Top) Or _
            (pt.Y > p.Bottom)) Then

            e.Action = DragAction.Cancel
        End If
    End Sub

#End Region

    ''' <summary>
    ''' this is a blank tab with no controls, this should be inherited to do something interesting
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Tab
        Inherits UserControl

        Public Sub New()
            MyBase.New()
            Me.SuspendLayout()
            Me.Name = "BaseTab"
            Me.Text = "Base Tab"
            Me.Size = New System.Drawing.Size(421, 244)
            Me.ResumeLayout(False)
            Me.SetStyle(ControlStyles.ContainerControl Or ControlStyles.StandardClick Or ControlStyles.StandardDoubleClick, True)
        End Sub

#Region "properites"

        ''' <summary>
        ''' text to show on the tab. this should never be blank
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Category("Appearance"), Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Shadows Property Text() As String

        ''' <summary>
        ''' text to show on the mouse over of the tab. this can be blank
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Category("Appearance"), Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Shadows Property FullText() As String

        ''' <summary>
        ''' this is only used for the CleanTab control
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Browsable(False)>
        Friend Property TitleRec() As Rectangle

        ''' <summary>
        ''' this is only used for the CleanTab control
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Browsable(False)>
        Friend Property CloseRec() As Rectangle

        ''' <summary>
        ''' this is only used for the CleanTab control
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Browsable(False)>
        Friend Property Selected As Boolean

        <Browsable(False)>
        Friend Property CleanTab() As CleanTab

        Private _dt As Boolean
        <Browsable(False)>
        Public Property DirtyBit() As Boolean
            Get
                Return _dt
            End Get
            Set(value As Boolean)
                If value <> _dt Then
                    _dt = value
                    If CleanTab IsNot Nothing Then CleanTab.Invalidate()
                End If
            End Set
        End Property

        ''' <summary>
        ''' if currently selected or not
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property isSelected() As Boolean
            Get
                Return Selected
            End Get
        End Property

#End Region

#Region "empty constructs"

        ''' <summary>
        ''' Does nothing at base class, override to enable saving
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub Save(Optional ByVal SaveAs As Boolean = False)
        End Sub

        ''' <summary>
        ''' Does nothing at base class, override to recieve the closing call
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub Closing(e As CancelEventArgs)
        End Sub

        ''' <summary>
        ''' Does nothing at base class, override to get when the tab has got focuse
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub TabGotFocus()
        End Sub

        ''' <summary>
        ''' Does nothing at base class, override to get when the tab has lost focuse
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub TabLostFocus()
        End Sub

#End Region

    End Class

End Class


