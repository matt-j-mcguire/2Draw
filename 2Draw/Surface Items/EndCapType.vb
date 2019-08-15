
Public Class endCapType

    Public Const MAXTYPES As Integer = 16

    Public Enum capType
        None = 0
        RectangleS
        RectangleM
        RectangleL
        RecFillS
        RecFillM
        RecFillL
        CircleS
        CircleM
        CircleL
        CircFillS
        CircFillM
        CircFillL
        ArrowS
        ArrowM
        ArrowL
        'ArrowFillS
        'ArrowFillM
        'ArrowFillL
    End Enum



    Public Shared Function GetCap(type As capType) As CustomLineCap
        Dim ret As CustomLineCap

        Using hpth As New GraphicsPath
            Dim fill As Boolean = False
            Dim offset As Single = 0
            Select Case type
                Case capType.RectangleS
                    hpth.AddRectangle(New RectangleF(-1.5, -1.5, 3, 3))
                    offset = 1.5
                Case capType.RectangleM
                    hpth.AddRectangle(New RectangleF(-2.5, -2.5, 5, 5))
                    offset = 2.5
                Case capType.RectangleL
                    hpth.AddRectangle(New RectangleF(-3.5, -3.5, 7, 7))
                    offset = 3.5
                Case capType.RecFillS
                    hpth.AddRectangle(New RectangleF(-0.75, -0.75, 1.5, 1.5))
                    offset = 0.75
                    fill = True
                Case capType.RecFillM
                    hpth.AddRectangle(New RectangleF(-1.25, -1.25, 2.5, 2.5))
                    offset = 1.25
                    fill = True
                Case capType.RecFillL
                    hpth.AddRectangle(New RectangleF(-1.75, -1.75, 3.5, 3.5))
                    offset = 1.75
                    fill = True

                Case capType.CircleS
                    hpth.AddEllipse(New RectangleF(-1.5, -1.5, 3, 3))
                    offset = 1.5
                Case capType.CircleM
                    hpth.AddEllipse(New RectangleF(-2.5, -2.5, 5, 5))
                    offset = 2.5
                Case capType.CircleL
                    hpth.AddEllipse(New RectangleF(-3.5, -3.5, 7, 7))
                    offset = 3.5
                Case capType.CircFillS
                    hpth.AddEllipse(New RectangleF(-0.75, -0.75, 1.5, 1.5))
                    offset = 0.75
                    fill = True
                Case capType.CircFillM
                    hpth.AddEllipse(New RectangleF(-1.25, -1.25, 2.5, 2.5))
                    offset = 1.25
                    fill = True
                Case capType.CircFillL
                    hpth.AddEllipse(New RectangleF(-1.75, -1.75, 3.5, 3.5))
                    offset = 1.75
                    fill = True
                Case capType.ArrowS
                    hpth.AddPolygon({New PointF(-1.5, -3), New PointF(0, 0), New PointF(1.5, -3)})
                    offset = 3
                Case capType.ArrowM
                    hpth.AddPolygon({New PointF(-2.5, -5), New PointF(0, 0), New PointF(2.5, -5)})
                    offset = 5
                Case capType.ArrowL
                    hpth.AddPolygon({New PointF(-3.5, -7), New PointF(0, 0), New PointF(3.5, -7)})
                    offset = 7
                    'Case capType.ArrowFillS
                    '    hpth.AddPolygon({New PointF(-1.5, -1.5), New PointF(0, 0), New PointF(1.5, -1.5)})
                    '    fill = True
                    '    offset = 1.5
                    'Case capType.ArrowFillM
                    '    hpth.AddPolygon({New PointF(-1.5, -2.5), New PointF(0, 0), New PointF(1.5, -2.5)})
                    '    fill = True
                    '    offset = 2.5
                    'Case capType.ArrowFillL
                    '    hpth.AddPolygon({New PointF(-2, -3.5), New PointF(0, 0), New PointF(2, -3.5)})
                    '    fill = True
                    '    offset = 3.5



                Case Else
                    'try nothing    
            End Select

            If fill Then
                ret = New CustomLineCap(hpth, Nothing)
            Else
                ret = New CustomLineCap(Nothing, hpth)
            End If
            ret.BaseInset = offset
            ret.SetStrokeCaps(LineCap.Flat, LineCap.Flat)
        End Using



        Return ret
    End Function

End Class