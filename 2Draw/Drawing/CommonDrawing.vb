
''' <summary>
''' Helper functions for drawing graphics
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class Drawlib

    ''' <summary>
    ''' draws a line through all the points
    ''' </summary>
    ''' <param name="G"></param>
    ''' <param name="p"></param>
    ''' <param name="c"></param>
    ''' <param name="size"></param>
    Public Shared Sub DrawAllPoints(G As Graphics, p() As PointF, c As Color, size As Double)
        Dim s As Double = size / 2
        Using br As New SolidBrush(c)
            For i As Integer = 0 To p.Length - 1
                G.FillEllipse(br, New RectangleF(p(i).X - s, p(i).Y - s, size, size))
            Next
        End Using
    End Sub

    ''' <summary>
    ''' draws a line through all the points
    ''' </summary>
    ''' <param name="G"></param>
    ''' <param name="p"></param>
    ''' <param name="pn"></param>
    ''' <param name="size"></param>
    Public Shared Sub DrawAllPoints(G As Graphics, p() As PointF, Pn As Pen, size As Double)
        Dim s As Double = size / 2

        For i As Integer = 0 To p.Length - 1
            G.FillEllipse(Pn.Brush, New RectangleF(p(i).X - s, p(i).Y - s, size, size))
        Next
    End Sub

    ''' <summary>
    ''' modifies a ImageAttributes to display a image as partially transparent
    ''' </summary>
    ''' <param name="op">.0 to 1.0 for the % of transparent</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetImgAtt(ByVal op As Single) As System.Drawing.Imaging.ImageAttributes
        ' Note the value op in row 4, column 4.  We are making the image op% transparent.

        Dim cm As New System.Drawing.Imaging.ColorMatrix(New Single()() _
                       {New Single() {1, 0, 0, 0, 0},
                        New Single() {0, 1, 0, 0, 0},
                        New Single() {0, 0, 1, 0, 0},
                        New Single() {0, 0, 0, op, 0},
                        New Single() {0, 0, 0, 0, 1}})

        ' Create an ImageAttributes object and set its color matrix.
        Dim imgattr As New System.Drawing.Imaging.ImageAttributes
        imgattr.SetColorMatrix(cm)

        Return imgattr
    End Function

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
       {New Single() {r, 0, 0, 0, 0},
        New Single() {0, g, 0, 0, 0},
        New Single() {0, 0, b, 0, 0},
        New Single() {0, 0, 0, a, 0},
        New Single() {0, 0, 0, 0, 1.0}})

        ' Create an ImageAttributes object and set its color matrix.
        Dim imgattr As New System.Drawing.Imaging.ImageAttributes
        imgattr.SetColorMatrix(cm)

        Return imgattr
    End Function

    ''' <summary>
    ''' Rounded Rectangle Corner
    ''' </summary>
    ''' <remarks></remarks>
    <FlagsAttribute()> Public Enum rrCor
        ''' <summary>
        ''' Top Left
        ''' </summary>
        ''' <remarks></remarks>
        tl = 1
        ''' <summary>
        ''' Top Right
        ''' </summary>
        ''' <remarks></remarks>
        tr = 2
        ''' <summary>
        ''' Bottom Left
        ''' </summary>
        ''' <remarks></remarks>
        bl = 4
        ''' <summary>
        ''' Bottom Right
        ''' </summary>
        ''' <remarks></remarks>
        br = 8
        ''' <summary>
        ''' All Corners
        ''' </summary>
        ''' <remarks></remarks>
        All = 15
        ''' <summary>
        ''' no rounded corners
        ''' </summary>
        ''' <remarks></remarks>
        None = 0
    End Enum

    ''' <summary>
    ''' returns a rounded rectangle using Rectangle
    ''' </summary>
    ''' <param name="Rect">how big the square</param>
    ''' <param name="CornerRadius">how big the curve</param>
    ''' <param name="Margin">interior offset</param>
    ''' <param name="RCor">which quardrent to make round</param>
    ''' <returns>Graphics path</returns>
    ''' <remarks></remarks>
    Public Shared Function RoundedRectangle(ByVal Rect As Rectangle, ByVal CornerRadius As Integer, ByVal Margin As Integer, Optional ByVal RCor As rrCor = rrCor.All) As GraphicsPath
        Dim roundedRect As New GraphicsPath
        Dim m As Integer = Margin
        Dim cr As Integer = CornerRadius

        If RCor = rrCor.None Then
            roundedRect.AddRectangle(Rect)
        Else
            If RCor And rrCor.tl Then
                roundedRect.AddArc(Rect.X + m, Rect.Y + m, cr * 2, cr * 2, 180, 90)
            Else
                Dim pts() As Point = {New Point(Rect.X + m, Rect.Y + m)}
                roundedRect.AddLines(pts)
            End If

            If RCor And rrCor.tr Then
                roundedRect.AddArc(Rect.X + Rect.Width - m - cr * 2, Rect.Y + m, cr * 2, cr * 2, 270, 90)
            Else
                Dim pts() As Point = {New Point(Rect.Right - m, Rect.Y + m)}
                roundedRect.AddLines(pts)
            End If

            If RCor And rrCor.br Then
                roundedRect.AddArc(Rect.X + Rect.Width - m - cr * 2, Rect.Y + Rect.Height - m - cr * 2, cr * 2, cr * 2, 0, 90)
            Else
                Dim pts() As Point = {New Point(Rect.Right - m, Rect.Bottom - m)}
                roundedRect.AddLines(pts)
            End If

            If RCor And rrCor.bl Then
                roundedRect.AddArc(Rect.X + m, Rect.Y + Rect.Height - m - cr * 2, cr * 2, cr * 2, 90, 90)
            Else
                Dim pts() As Point = {New Point(Rect.X + m, Rect.Bottom - m)}
                roundedRect.AddLines(pts)
            End If
        End If

        roundedRect.CloseFigure()
        Return roundedRect
    End Function

    ''' <summary>
    ''' returns a rounded rectangle using RectangleF
    ''' </summary>
    ''' <param name="Rect">how big the square</param>
    ''' <param name="CornerRadius">how big the curve</param>
    ''' <param name="Margin">interior offset</param>
    ''' <param name="RCor">which quardrent to make round</param>
    ''' <returns>Graphics path</returns>
    ''' <remarks></remarks>
    Public Shared Function RoundedRectangle(ByVal Rect As RectangleF, ByVal CornerRadius As Integer, ByVal Margin As Integer, Optional ByVal RCor As rrCor = rrCor.All) As GraphicsPath
        Dim rr As New Rectangle(CInt(Rect.X), CInt(Rect.Y), CInt(Rect.Width), CInt(Rect.Height))
        Return RoundedRectangle(rr, CornerRadius, Margin, RCor)
    End Function

    ''' <summary>
    ''' Rotate around the indicated point.
    ''' </summary>
    ''' <param name="gr">graphics object</param>
    ''' <param name="X">desired x location</param>
    ''' <param name="Y">desired y location</param>
    ''' <param name="degrees">rotational degrees (0-360)</param>
    ''' <remarks>call graphics.reset to put the tranlation back to normal</remarks>
    Public Shared Sub RotateAround(ByVal gr As Graphics, ByVal X As Single, ByVal Y As Single, ByVal degrees As Single)
        ' Translate to center the rectangle at the origin.
        Dim m As Matrix = gr.Transform
        ' m.Translate(-X, -Y, MatrixOrder.Append)

        '  gr.TranslateTransform(-X, -Y, Drawing2D.MatrixOrder.Append)

        ' Rotate.
        '  gr.RotateTransform(degrees, Drawing2D.MatrixOrder.Append)
        m.RotateAt(degrees, New Point(X, Y))
        gr.Transform = m

        'm= gr.Transform
        'm.Translate(X, Y, MatrixOrder.Append)
        'gr.Transform = m
        ' Translate the result back to its original position.
        ' gr.TranslateTransform(X, Y, Drawing2D.MatrixOrder.Append)
    End Sub

    ''' <summary>
    ''' blends to colors together
    ''' </summary>
    ''' <param name="C1">first color</param>
    ''' <param name="C2">second color</param>
    ''' <param name="P1">0.0 to 1.0 range for who has the most influnce</param>
    ''' <returns>a blended new color</returns>
    ''' <remarks></remarks>
    Public Shared Function Blend(ByVal C1 As Color, ByVal C2 As Color, ByVal P1 As Single) As Color
        Dim R1 As Single = C1.R * P1
        Dim G1 As Single = C1.G * P1
        Dim B1 As Single = C1.B * P1
        Dim R2 As Single = C2.R * (1 - P1)
        Dim G2 As Single = C2.G * (1 - P1)
        Dim B2 As Single = C2.B * (1 - P1)
        Blend = Color.FromArgb(CInt(R1 + R2), CInt(G1 + G2), CInt(B1 + B2))
    End Function

    'Private Function TranslatePointToPageCoord(pt As PointF) As PointF
    '    Dim pSize As SizeF = PageSize.getInitalSize
    '    Dim pto As PointF = ScrollOffset
    '    Dim ret As New Point(pt.X, pt.Y)

    '    ret.X -= pto.X
    '    ret.Y -= pto.Y


    '    ret.X /= ZoomPerecentage
    '    ret.Y /= ZoomPerecentage


    '    Return ret
    'End Function

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

    ''' <summary>
    ''' makes any color transparent by a 0 to 100%
    ''' </summary>
    ''' <param name="sourceColor">origional color</param>
    ''' <param name="alpha">0.0-1.0 for the %</param>
    ''' <returns>a new transparent color</returns>
    ''' <remarks>quick hand for Color.FromArgb(CInt((alpha * 255)), sourceColor.R, sourceColor.G, sourceColor.B)</remarks>
    Public Shared Function TransparentColor(ByVal sourceColor As Color, ByVal alpha As Single) As Color
        Return Color.FromArgb(CInt((alpha * 255)), sourceColor.R, sourceColor.G, sourceColor.B)
    End Function

    ''' <summary>
    ''' returns a color based on two colors and the range between them
    ''' </summary>
    ''' <param name="startColor">top color</param>
    ''' <param name="endColor">bottom color</param>
    ''' <param name="position">0.0-1.0 range for 0 to 100% one way or the other</param>
    ''' <returns>a blend of the two colors somwhere in the range</returns>
    ''' <remarks></remarks>
    Public Shared Function GradientColor(ByVal startColor As Color, ByVal endColor As Color, ByVal position As Single) As Color
        Dim newR As Integer = CInt((startColor.R * (position - 1) + endColor.R * position))
        Dim newG As Integer = CInt((startColor.G * (position - 1) + endColor.G * position))
        Dim newB As Integer = CInt((startColor.B * (position - 1) + endColor.B * position))
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



End Class
