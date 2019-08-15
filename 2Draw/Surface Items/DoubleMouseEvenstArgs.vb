Public Class DoubleMouseEventArgs
    Inherits EventArgs



    Public Sub New(button As MouseButtons, clicks As Integer, x As Single, y As Single, delta As Integer)
        _Button = button
        _Clicks = clicks
        _Delta = delta
        _X = x
        _Y = y
    End Sub

    Public Sub New(org As MouseEventArgs)
        _Button = org.Button
        _Clicks = org.Clicks
        _Delta = org.Delta
        _X = org.X
        _Y = org.Y
    End Sub

    Public Property Handled As Boolean

    Public ReadOnly Property Button As MouseButtons

    Public ReadOnly Property Clicks As Integer

    Public ReadOnly Property Delta As Integer

    Public Property Location As Drawing.PointF
        Get
            Return New PointF(X, Y)
        End Get
        Set(value As Drawing.PointF)
            X = value.X
            Y = value.Y
        End Set
    End Property

    Public Property X As Single

    Public Property Y As Single

End Class

