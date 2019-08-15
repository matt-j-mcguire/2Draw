'todo: global options for:
'   Snap to grid / snap to line / none



Public Module Globals

    Friend ThisProject As Project
    Friend current As New drawingOpts(True)
    Friend SymbolLibs As New SymbolLib
    Friend CtrlKey As Boolean
    Public ShiftKey As Boolean
    Friend CurrDrawingTool As SelectedDrawingTool
    Friend myStartupLocation As String
    Friend ShowToolMoveHandle As Boolean
    Friend ShowToolRotation As Boolean
    Friend ShowToolGrid As Boolean
    Friend ShowCrossHairs As Boolean
    Friend ShowNormalLineWidth As Boolean
    Friend BlankCursor As Cursor
    Friend Warnings As AppWarnings
    Public ProgramUpdatePath As String
    Public Highlight As Color
    Public CircleDefPoints As Integer
    Public CrossHairColor As Color
    Public GridColor As Color
    Public GridSize As Integer
    Public titleBack As Color = Color.FromArgb(&HFF828282)
    Public titleFore As Color = Color.White
    Public innerBack As Color = Color.White
    Public innerFore As Color = Color.Black



    Public Sub Load()
        Highlight = My.Settings.SurfaceHighLight
        CircleDefPoints = My.Settings.CircleDefPoints
        Using ms As New MemoryStream(My.Resources.blank) : BlankCursor = New Cursor(ms) : End Using
        Warnings = New AppWarnings
        ProgramUpdatePath = My.Settings.ProgramUpdatePath
        CrossHairColor = My.Settings.CrossHairColor
        GridColor = My.Settings.GridColor
        GridSize = My.Settings.GridSpacing
    End Sub

    Public Sub Save()
        My.Settings.SurfaceHighLight = Highlight
        My.Settings.CircleDefPoints = CircleDefPoints
        My.Settings.ProgramUpdatePath = ProgramUpdatePath
        My.Settings.CrossHairColor = CrossHairColor
        My.Settings.GridSpacing = GridSize
        My.Settings.GridColor = GridColor
        Warnings.Save()
        My.Settings.Save()
    End Sub

End Module

Public Enum SelectedDrawingTool
    Mouse = 0
    Pan = 1
    Rectangle = 2
    Circle = 3
    Triangle = 4
    Line = 5
    Text = 6
    Image = 7
End Enum

Public Class drawingOpts

    Public LineWidth As Single
    Public LineColor As Color
    Public FillColor As Color
    Public FillHatch As Integer
    Public LineDash As Drawing2D.DashStyle
    Public LineStartCap As endCapType.capType
    Public LineEndCap As endCapType.capType
    Public FontName As String
    Public FontSize As Single
    Public FontStyle As Drawing.FontStyle
    Public FontAlignment As Drawing.StringAlignment


    Public Sub New(Defaults As Boolean)
        If Defaults Then
            LineWidth = 1.0
            LineColor = Color.Black
            FillColor = Color.Transparent
            LineDash = DashStyle.Solid
            LineStartCap = endCapType.capType.None
            LineEndCap = endCapType.capType.None
            FontName = "Buxton Sketch"
            FontSize = 16.0
            FontStyle = FontStyle.Regular
            FontAlignment = StringAlignment.Near
            FillHatch = -1
        End If

    End Sub

    Public Function clone() As drawingOpts
        Dim r As New drawingOpts(False)
        r.LineWidth = LineWidth
        r.LineColor = LineColor
        r.FillColor = FillColor
        r.LineDash = LineDash
        r.LineStartCap = LineStartCap
        r.LineEndCap = LineEndCap
        r.FontName = FontName
        r.FontSize = FontSize
        r.FontStyle = FontStyle
        r.FontAlignment = FontAlignment
        r.FillHatch = FillHatch
        Return r
    End Function



    ''' <summary>
    ''' get the options from xml
    ''' </summary>
    ''' <param name="n"></param>
    ''' <returns>loaded options</returns>
    Public Shared Function Load(n As XmlNode) As drawingOpts
        Dim r As New drawingOpts(False)
        r.LineWidth = XHelper.Get(n, "linewidth", 1.0)
        r.LineColor = XHelper.Get(n, "linecolor", Color.Black)
        r.FillColor = XHelper.Get(n, "fillcolor", Color.Transparent)
        r.LineDash = XHelper.Get(n, "linedash", DashStyle.Solid)
        r.LineStartCap = XHelper.Get(n, "linestartcap", endCapType.capType.None)
        r.LineEndCap = XHelper.Get(n, "linestopcap", endCapType.capType.None)
        r.FontName = XHelper.Get(n, "fontname", "Buxton Sketch")
        r.FontSize = XHelper.Get(n, "fontsize", 10.0)
        r.FontStyle = XHelper.Get(n, "fontstyle", FontStyle.Regular)
        r.FontAlignment = XHelper.Get(n, "fontalignment", StringAlignment.Near)
        r.FillHatch = XHelper.Get(n, "fillhatch", -1)
        Return r
    End Function

    ''' <summary>
    ''' saves the drawing Options back to the xml
    ''' </summary>
    ''' <param name="n"></param>
    Public Sub Save(n As XmlNode)
        XHelper.Set(n, "linewidth", LineWidth)
        XHelper.Set(n, "linecolor", LineColor)
        XHelper.Set(n, "fillcolor", FillColor)
        XHelper.Set(n, "linedash", LineDash)
        XHelper.Set(n, "linestartcap", LineStartCap)
        XHelper.Set(n, "linestopcap", LineEndCap)
        XHelper.Set(n, "fontname", FontName)
        XHelper.Set(n, "fontsize", FontSize)
        XHelper.Set(n, "fontstyle", FontStyle)
        XHelper.Set(n, "fontalignment", FontAlignment)
        XHelper.Set(n, "fillhatch", FillHatch)
    End Sub

End Class

Public Interface iMouseHandler

    ''' <summary>
    ''' handles the mouse down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Sub MouseDown(sender As Object, e As DoubleMouseEventArgs)


    ''' <summary>
    ''' handles the mouse move
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Sub MouseMove(sender As Object, e As DoubleMouseEventArgs)

    ''' <summary>
    ''' handles the mouse up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Sub MouseUp(sender As Object, e As DoubleMouseEventArgs)

    ''' <summary>
    ''' returns true if the mouse is currently captured
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property HasMouse() As Boolean

    Sub MouseDoubleClick(sender As Object, e As DoubleMouseEventArgs, ByRef Handled As Boolean)


    Sub Draw(g As Graphics)

    Sub ZoomChanged()


End Interface

