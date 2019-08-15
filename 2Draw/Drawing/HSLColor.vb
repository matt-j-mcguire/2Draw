

Public Structure HSLColor
    Private m_hue As Double
    Private m_saturation As Double
    Private m_lightness As Double
    ' http://en.wikipedia.org/wiki/HSL_color_space

    Public Property Hue() As Double
        Get
            Return m_hue
        End Get
        Set(value As Double)
            m_hue = value
        End Set
    End Property

    Public Property Saturation() As Double
        Get
            Return m_saturation
        End Get
        Set(value As Double)
            m_saturation = value
        End Set
    End Property

    Public Property Lightness() As Double
        Get
            Return m_lightness
        End Get
        Set(value As Double)
            m_lightness = value
            If m_lightness < 0 Then
                m_lightness = 0
            End If
            If m_lightness > 1 Then
                m_lightness = 1
            End If
        End Set
    End Property

    Public Sub New(hue As Double, saturation As Double, lightness As Double)
        m_hue = Math.Min(360, hue)
        m_saturation = Math.Min(1, saturation)
        m_lightness = Math.Min(1, lightness)
    End Sub

    Public Sub New(color As Color)
        m_hue = 0
        m_saturation = 1
        m_lightness = 1
        FromRGB(color)
    End Sub

    Public Property Color() As Color
        Get
            Return ToRGB()
        End Get
        Set(value As Color)
            FromRGB(value)
        End Set
    End Property

    Private Sub FromRGB(cc As Color)
        Dim r As Double = CDbl(cc.R) / 255.0
        Dim g As Double = CDbl(cc.G) / 255.0
        Dim b As Double = CDbl(cc.B) / 255.0

        Dim min As Double = Math.Min(Math.Min(r, g), b)
        Dim max As Double = Math.Max(Math.Max(r, g), b)
        ' calulate hue according formula given in
        ' "Conversion from RGB to HSL or HSV"
        m_hue = 0
        If min <> max Then
            If r = max AndAlso g >= b Then
                m_hue = 60 * ((g - b) / (max - min)) + 0
            ElseIf r = max AndAlso g < b Then
                m_hue = 60 * ((g - b) / (max - min)) + 360
            ElseIf g = max Then
                m_hue = 60 * ((b - r) / (max - min)) + 120
            ElseIf b = max Then
                m_hue = 60 * ((r - g) / (max - min)) + 240
            End If
        End If
        ' find lightness
        m_lightness = (min + max) / 2

        ' find saturation
        If m_lightness = 0 OrElse min = max Then
            m_saturation = 0
        ElseIf m_lightness > 0 AndAlso m_lightness <= 0.5 Then
            m_saturation = (max - min) / (2 * m_lightness)
        ElseIf m_lightness > 0.5 Then
            m_saturation = (max - min) / (2 - 2 * m_lightness)
        End If
    End Sub
    Private Function ToRGB() As Color
        ' convert to RGB according to
        ' "Conversion from HSL to RGB"

        Dim r As Double = m_lightness
        Dim g As Double = m_lightness
        Dim b As Double = m_lightness
        If m_saturation = 0 Then
            Return Color.FromArgb(255, CInt(Math.Truncate(r * 255)), CInt(Math.Truncate(g * 255)), CInt(Math.Truncate(b * 255)))
        End If

        Dim q As Double = 0
        If m_lightness < 0.5 Then
            q = m_lightness * (1 + m_saturation)
        Else
            q = m_lightness + m_saturation - (m_lightness * m_saturation)
        End If
        Dim p As Double = 2 * m_lightness - q
        Dim hk As Double = m_hue / 360

        ' r,g,b colors
        Dim tc As Double() = New Double(2) {hk + (1.0 / 3.0), hk, hk - (1.0 / 3.0)}
        Dim colors As Double() = New Double(2) {0, 0, 0}

        For color__1 As Integer = 0 To colors.Length - 1
            If tc(color__1) < 0 Then
                tc(color__1) += 1
            End If
            If tc(color__1) > 1 Then
                tc(color__1) -= 1
            End If

            If tc(color__1) < (1.0 / 6.0) Then
                colors(color__1) = p + ((q - p) * 6 * tc(color__1))
            ElseIf tc(color__1) >= (1.0 / 6.0) AndAlso tc(color__1) < (1.0 / 2.0) Then
                colors(color__1) = q
            ElseIf tc(color__1) >= (1.0 / 2.0) AndAlso tc(color__1) < (2.0 / 3.0) Then
                colors(color__1) = p + ((q - p) * 6 * (2.0 / 3.0 - tc(color__1)))
            Else
                colors(color__1) = p
            End If

            ' convert to value expected by Color
            colors(color__1) *= 255
        Next
        Return Color.FromArgb(255, CInt(Math.Truncate(colors(0))), CInt(Math.Truncate(colors(1))), CInt(Math.Truncate(colors(2))))
    End Function

    Public Shared Operator <>(left As HSLColor, right As HSLColor) As Boolean
        Return Not (left = right)
    End Operator

    Public Shared Operator =(left As HSLColor, right As HSLColor) As Boolean
        Return (left.Hue = right.Hue AndAlso left.Lightness = right.Lightness AndAlso left.Saturation = right.Saturation)
    End Operator

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim right As HSLColor = obj
        Return (Me.Hue = right.Hue AndAlso Me.Lightness = right.Lightness AndAlso Me.Saturation = right.Saturation)
    End Function

    Public Overloads Overrides Function ToString() As String
        Dim s As String = String.Format("HSL({0:f2}, {1:f2}, {2:f2})", Hue, Saturation, Lightness)
        Return s
    End Function

End Structure
