

''' <summary>
''' short-cuts for xml generation
''' </summary>
''' <remarks>words the MS XMLNode's not DK lNodes</remarks>
Public NotInheritable Class XHelper

    ''' <summary>
    ''' just to get fxcop of my back
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
    End Sub

#Region "String Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String) As String
        Dim xatt As XmlAttribute = Node.Attributes(Name)
        If xatt IsNot Nothing Then
            If String.IsNullOrEmpty(xatt.Value) Then
                Return String.Empty
            Else
                Return xatt.Value
            End If
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As String) As String
        Dim xatt As XmlAttribute = Node.Attributes(Name)
        If xatt IsNot Nothing Then
            If String.IsNullOrEmpty(xatt.Value) Then
                xatt.Value = DefaultValue
                Return DefaultValue
            Else
                Return xatt.Value
            End If
        Else
            Return DefaultValue
        End If
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As String)
        Dim xatt As XmlAttribute = Node.Attributes(Name)
        If xatt Is Nothing Then
            xatt = Node.OwnerDocument.CreateAttribute(Name)
            Node.Attributes.Append(xatt)
        End If
        xatt.Value = Value
    End Sub

#End Region

#Region "Integer Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Integer) As Integer
        Dim s As String = [Get](Node, Name, DefaultValue.ToString(CultureInfo.InvariantCulture))
        If IsNumeric(s) Then
            Return CInt(s)
        Else
            Return DefaultValue
        End If
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks>this just passes the value to [set] for the string (integer.tostring)</remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Integer)
        [Set](Node, Name, Value.ToString(CultureInfo.InvariantCulture))
    End Sub

#End Region

#Region "long Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Long) As Long
        ' Return CLng([Get](Node, Name, DefaultValue.ToString(CultureInfo.InvariantCulture)))
        Dim s As String = [Get](Node, Name, DefaultValue.ToString(CultureInfo.InvariantCulture))
        If IsNumeric(s) Then
            Return CLng(s)
        Else
            Return DefaultValue
        End If
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks>this just passes the value to [set] for the string (integer.tostring)</remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Long)
        [Set](Node, Name, Value.ToString(CultureInfo.InvariantCulture))
    End Sub

#End Region



#Region "Double Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks>calls the string method of [get]</remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Double) As Double
        ' Return CDbl([Get](Node, Name, DefaultValue.ToString(CultureInfo.InvariantCulture)))
        Dim s As String = [Get](Node, Name, DefaultValue.ToString(CultureInfo.InvariantCulture))
        If IsNumeric(s) Then
            Return CDbl(s)
        Else
            Return DefaultValue
        End If
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <param name="decimalplaces">how many decimal places to store, default 2 places .00</param>
    ''' <remarks>this just passes the value to [set] for the string (double.tostring)</remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Double, ByVal decimalplaces As Integer)
        [Set](Node, Name, Value.ToString("F" & decimalplaces, CultureInfo.InvariantCulture))
    End Sub

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created, default 2 decimal places
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks>this just passes the value to [set] for the string (double.tostring)</remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Double)
        [Set](Node, Name, Value.ToString("F2", CultureInfo.InvariantCulture))
    End Sub

#End Region

#Region "Boolean Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks>calls the string method of [get]</remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Boolean) As Boolean
        'Return CBool([Get](Node, Name, DefaultValue.ToString))
        Dim s As String = [Get](Node, Name, DefaultValue.ToString(CultureInfo.InvariantCulture))
        Dim ret As Boolean
        If Boolean.TryParse(s, ret) Then
            Return ret
        Else
            Return DefaultValue
        End If
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks>this just passes the value to [set] for the string (boolean.tostring)</remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Boolean)
        [Set](Node, Name, Value.ToString)
    End Sub

#End Region

#Region "Date Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks>calls the string method of [get]</remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Date, ByVal format As String) As Date
        Try
            Dim dt As Date = Date.Parse([Get](Node, Name, DefaultValue.ToString(format)))
            'If Not format.Contains("y") Then dt = dt.AddYears(DefaultValue.Year)
            'If Not format.Contains("M") Then dt = dt.AddMonths(DefaultValue.Month)
            'If Not format.Contains("d") Then dt = dt.AddDays(DefaultValue.Day)
            Return dt
        Catch ex As Exception
            Return DefaultValue
        End Try
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks>this just passes the value to [set] for the string (boolean.tostring)</remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Date, ByVal format As String)
        [Set](Node, Name, Value.ToString(format))
    End Sub

#End Region

#Region "Color Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks>call the string methed of get for the attribute work</remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Color) As Color
        Dim c As Color = Color.Transparent
        Dim sc As String = [Get](Node, Name, DefaultValue.ToArgb.ToString(CultureInfo.InvariantCulture))
        If IsNumeric(sc) Then
            c = Color.FromArgb([Get](Node, Name, DefaultValue.ToArgb))
            If c.A = 0 Then c = Color.Transparent
        Else
            'this is only necessary if upgrading 
            'from an older system that stored it's
            'color values as names instead of RGB 
            'values
            Try
                c = Color.FromName(sc)
            Catch ex As Exception
                c = DefaultValue
            End Try
        End If
        Return c
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks>call the string of [set]</remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Color)
        [Set](Node, Name, Value.ToArgb.ToString(CultureInfo.InvariantCulture))
    End Sub

#End Region

#Region "Point Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value [xml example: x34|y100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value</param>
    ''' <param name="Name">name of the xml node attribute</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks>call the base string method of [get]</remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Point) As Point
        Dim c() As String = [Get](Node, Name, "x" & DefaultValue.X & "|y" & DefaultValue.Y).Split("|")

        Dim pt As Point
        Try
            If c.Length = 2 Then
                pt.X = CInt(c(0).Substring(1))
                pt.Y = CInt(c(1).Substring(1))
            Else
                pt = DefaultValue
            End If
        Catch ex As Exception
            pt = DefaultValue
        End Try

        Return pt
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created. [xml example: x34|y100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the xml attribute</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Point)
        [Set](Node, Name, "x" & Value.X & "|y" & Value.Y)
    End Sub

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value [xml example: x34|y100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value</param>
    ''' <param name="Name">name of the xml node attribute</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks>call the base string method of [get]</remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As PointF) As PointF
        Dim c() As String = [Get](Node, Name, "x" & DefaultValue.X & "|y" & DefaultValue.Y).Split("|")

        Dim pt As PointF
        Try
            If c.Length = 2 Then
                pt.X = CDbl(c(0).Substring(1))
                pt.Y = CDbl(c(1).Substring(1))
            Else
                pt = DefaultValue
            End If
        Catch ex As Exception
            pt = DefaultValue
        End Try

        Return pt
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created. [xml example: x34|y100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the xml attribute</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As PointF)
        [Set](Node, Name, "x" & Value.X & "|y" & Value.Y)
    End Sub

#End Region

#Region "Size Attributes"

    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value [xml example: w34|h100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the xml attribute node</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Size) As Size
        Dim c() As String = [Get](Node, Name, "w" & DefaultValue.Width & "|h" & DefaultValue.Height).Split("|")

        Dim sz As Size
        Try
            If c.Length = 2 Then
                sz.Width = CInt(c(0).Substring(1))
                sz.Height = CInt(c(1).Substring(1))
            Else
                sz = DefaultValue
            End If
        Catch ex As Exception
            sz = DefaultValue
        End Try

        Return sz
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created  [xml example: w34|h100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Size)
        [Set](Node, Name, "w" & Value.Width & "|h" & Value.Height)
    End Sub


    ''' <summary>
    ''' will return a value from the xml node if present otherwise if the 
    ''' attribute is missing return just the Default value [xml example: w34|h100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Name">the name of the xml attribute node</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As SizeF) As SizeF
        Dim c() As String = [Get](Node, Name, "w" & DefaultValue.Width & "|h" & DefaultValue.Height).Split("|")

        Dim sz As SizeF
        Try
            If c.Length = 2 Then
                sz.Width = CDbl(c(0).Substring(1))
                sz.Height = CDbl(c(1).Substring(1))
            Else
                sz = DefaultValue
            End If
        Catch ex As Exception
            sz = DefaultValue
        End Try

        Return sz
    End Function

    ''' <summary>
    ''' Saves a value to a given XML node's attribute, if the attribute is missing, then it is created  [xml example: w34|h100 ]
    ''' </summary>
    ''' <param name="Node">the XML node, that a given attribute will be stored</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As SizeF)
        [Set](Node, Name, "w" & Value.Width & "|h" & Value.Height)
    End Sub

#End Region

#Region "Rectangle Attributes"

    ''' <summary>
    ''' Returns a Rectangle (x,y,w,h) from the attribute  [xml example: x23|y78|w34|h100 ]
    ''' </summary>
    ''' <param name="Node">the xmlnode to pull the attribute from</param>
    ''' <param name="name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, ByVal DefaultValue As Rectangle) As Rectangle
        Dim c() As String = [Get](Node, Name, "x" & DefaultValue.X & "|y" & DefaultValue.Y & "|w" & DefaultValue.Width & "|h" & DefaultValue.Height).Split("|"c)

        Dim rc As Rectangle
        Try
            If c.Length = 4 Then
                rc.X = CInt(c(0).Substring(1))
                rc.Y = CInt(c(1).Substring(1))
                rc.Width = CInt(c(2).Substring(1))
                rc.Height = CInt(c(3).Substring(1))
            Else
                rc = DefaultValue
            End If
        Catch ex As Exception
            rc = DefaultValue
        End Try

        Return rc
    End Function

    ''' <summary>
    ''' sets a Rectangle (x,y,w,h) to the attribute [xml example: x23|y78|w34|h100 ]
    ''' </summary>
    ''' <param name="Node">the xml node to add the attribute</param>
    ''' <param name="name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Sub [Set](ByVal Node As XmlNode, ByVal Name As String, ByVal Value As Rectangle)
        [Set](Node, Name, "x" & Value.X & "|y" & Value.Y & "|w" & Value.Width & "|h" & Value.Height)
    End Sub

#End Region

#Region "Font Attributes"

    ''' <summary>
    ''' Returns a Font from the attribute [xml example: "Sans Serif|18|0" ]
    ''' </summary>
    ''' <param name="Node">the xmlnode to parse for this attribute</param>
    ''' <param name="name">the name of the attribute needed</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Shared Function [Get](ByVal Node As XmlNode, ByVal Name As String, defaultname As String, defaultsize As Single, defaultstyle As FontStyle) As Font
        Dim c() As String = [Get](Node, Name, defaultname & "|" & defaultsize.ToString("#0.00") & "|" & defaultstyle).Split("|"c)

        Dim fnt As Font = Nothing
        If c.Length = 3 Then
            Try
                fnt = New Font(c(0), CSng(c(1)), DirectCast(CInt(c(2)), FontStyle))
            Catch ex As Exception
                'possible parse error, ignore
            End Try
        End If
        If fnt Is Nothing Then
            fnt = New Font(defaultname, defaultsize, defaultstyle)
        End If
        Return fnt
    End Function

    ''' <summary>
    ''' sets a Font to the attribute [xml example: "Sans Serif|18|0" ]
    ''' </summary>
    ''' <param name="Node"> the xmlnode to parse for the attribute</param>
    ''' <param name="name">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Shared Sub [Set](ByVal Node As XmlNode, ByVal name As String, ByVal Value As Font)
        [Set](Node, name, Value.Name & "|" & Value.Size & "|" & Value.Style)
    End Sub

#End Region

#Region "Enum Attributes"

    ''' <summary>
    ''' returns a Enum as an object from the attribute/ gets value not value name
    ''' </summary>
    ''' <param name="Node">the xmlnode to parse for the attribute</param>
    ''' <param name="name">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value</param>
    ''' <returns>an enum type value</returns>
    ''' <remarks>if enum is not passed, will return the defaultvalue with out processing</remarks>
    Public Shared Function GetEnum(ByVal Node As XmlNode, ByVal name As String, ByVal Defaultvalue As System.Enum) As System.Enum
        Dim int As Integer = DirectCast(Defaultvalue, IConvertible).ToInt32(CultureInfo.InvariantCulture)
        Dim a As String = [Get](Node, name, int.ToString(CultureInfo.InvariantCulture))
        Dim typ As Type = Defaultvalue.GetType
        Return DirectCast(System.Enum.Parse(typ, a), System.Enum)
    End Function

    ''' <summary>
    ''' sets a enum value to an xmlnode's attribute/ saves value not value name
    ''' </summary>
    ''' <param name="Node">the xmlnode to parse for the attribute</param>
    ''' <param name="name">the name of the attribute needed</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>calls the [set] string method</remarks>
    Public Shared Sub SetEnum(ByVal Node As XmlNode, ByVal name As String, ByVal Value As System.Enum)
        Dim int As Integer = DirectCast(Value, IConvertible).ToInt32(Nothing)
        [Set](Node, name, int.ToString(CultureInfo.InvariantCulture))
    End Sub

#End Region

#Region "Node, Attribute and Document creation"

    ''' <summary>
    ''' this just creates an attribute, but does not append it to anything
    ''' </summary>
    ''' <param name="OwnerDoc">where the attribute will be</param>
    ''' <param name="name">name of att</param>
    ''' <param name="value">value of att</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function AttCreate(ByVal OwnerDoc As XmlDocument, ByVal name As String, ByVal value As String) As XmlAttribute
        Dim xatt As XmlAttribute = OwnerDoc.CreateAttribute(name)
        xatt.Value = value
        Return xatt
    End Function

    ''' <summary>
    ''' this creates and appends a new attribute with value
    ''' </summary>
    ''' <param name="Node">the xmlnode to append</param>
    ''' <param name="name">name of att</param>
    ''' <param name="value">value of att</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function AttAppendNew(ByVal Node As XmlNode, ByVal name As String, ByVal value As String) As XmlAttribute
        Dim xatt As XmlAttribute = Node.OwnerDocument.CreateAttribute(name)
        xatt.Value = value
        Node.Attributes.Append(xatt)
        Return xatt
    End Function

    ''' <summary>
    ''' removes an attribute from an existing xmlnode
    ''' </summary>
    ''' <param name="Node"></param>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function AttRemove(ByVal Node As XmlNode, ByVal name As String) As XmlAttribute
        Dim xatt As XmlAttribute = Node.Attributes(name)
        If xatt IsNot Nothing Then Node.Attributes.Remove(xatt)
        Return xatt
    End Function

    ''' <summary>
    ''' creates a xmlnode using the owner doc as reference, does not add
    ''' </summary>
    ''' <param name="OwnerDoc">the owner xml document</param>
    ''' <param name="name">the name of the node</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function NodeCreate(ByVal OwnerDoc As XmlDocument, ByVal name As String) As XmlNode
        Dim xNode As XmlNode = OwnerDoc.CreateElement(name)
        Return xNode
    End Function

    ''' <summary>
    ''' creates and adds a new xmlnode to an existing node
    ''' </summary>
    ''' <param name="Node">the node to append to</param>
    ''' <param name="name">the name of the xmlnode</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function NodeAppendNew(ByVal Node As XmlNode, ByVal name As String) As XmlNode
        Dim xNode As XmlNode = Node.OwnerDocument.CreateElement(name)
        Node.AppendChild(xNode)
        Return xNode
    End Function

    ''' <summary>
    ''' returns a sub node. if it does not exist it will return a new created one
    ''' </summary>
    ''' <param name="Owner">where to pull the node from</param>
    ''' <param name="childName">what the sub node is</param>
    ''' <param name="CreateifMissing">if to create it if missing</param>
    ''' <returns>the xmlnode if found or created</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function GetSubNode(ByVal Owner As XmlNode, ByVal childName As String, ByVal CreateifMissing As Boolean) As XmlNode
        Dim retval As XmlNode = Nothing
        If Owner IsNot Nothing Then
            retval = Owner.SelectSingleNode(childName)
            If retval Is Nothing AndAlso CreateifMissing Then retval = NodeAppendNew(Owner, childName)
        End If
        Return retval
    End Function

    ''' <summary>
    ''' creates a new xml document with a default base name
    ''' </summary>
    ''' <param name="BaseNodeName">if empty then "Base" is substuted</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function CreateNewDocument(ByVal BaseNodeName As String) As XmlDocument
        If String.IsNullOrEmpty(BaseNodeName) Then BaseNodeName = "Base"
        Dim xDoc As New XmlDocument
        xDoc.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?><" & BaseNodeName & "/>")
        Return xDoc
    End Function

    ''' <summary>
    ''' creates a new xml document with a default base name
    ''' </summary>
    ''' <param name="BaseNodeName">if empty then "Base" is substuted</param>
    ''' <param name="RootReference">returns a reference to the root node</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function CreateNewDocument(ByVal BaseNodeName As String, ByRef RootReference As XmlNode) As XmlDocument
        If String.IsNullOrEmpty(BaseNodeName) Then BaseNodeName = "Base"
        Dim xDoc As New XmlDocument
        xDoc.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?><" & BaseNodeName & "/>")
        RootReference = xDoc.SelectSingleNode(BaseNodeName)
        Return xDoc
    End Function


#End Region

#Region "path get and set"

    ''' <summary>
    ''' returns a path of the current node  back to base
    ''' </summary>
    ''' <param name="CurrentNode">the path to this point</param>
    ''' <param name="StartNode">back to root node, can be nothing</param>
    ''' <returns>if startnode string is "" then assumed ="Controller"</returns>
    ''' <remarks></remarks>
    Public Shared Function PathGet(ByVal CurrentNode As XmlNode, ByVal StartNode As String) As String
        Dim xstr As New StringBuilder
        If String.IsNullOrEmpty(StartNode) Then StartNode = "Controller"

        xstr.Append(CurrentNode.Name & "\")
        Dim tempND As XmlNode = CurrentNode
        Do
            If tempND.Name = StartNode Then Exit Do
            Dim pt As XmlNode = tempND.ParentNode
            If pt Is Nothing OrElse pt.Name = StartNode Then
                Dim str As String = pt.Name & "\"
                xstr.Insert(0, str)
                Exit Do
            Else
                Dim str As String = pt.Name & "\"
                xstr.Insert(0, str)
                tempND = pt
            End If
        Loop
        Return xstr.ToString
    End Function

    ''' <summary>
    ''' looks though all the path sent to try and find the location of the last node in the list
    ''' </summary>
    ''' <param name="StartNode">the xmlnode to start with</param>
    ''' <param name="thePath">the path to walk</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PathFind(ByVal StartNode As XmlNode, ByVal thePath As String) As XmlNode
        Dim retval As XmlNode = StartNode
        Dim Xstr() As String = thePath.Split("\".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
        Dim index As Integer = Array.IndexOf(Xstr, StartNode.Name)
        If index > -1 Then
            For xloop As Integer = index + 1 To Xstr.Length - 1
                Dim xnode As XmlNode = retval.SelectSingleNode(Xstr(xloop))
                If xnode Is Nothing Then
                    Exit For
                Else
                    retval = xnode
                End If
            Next

        Else
            retval = Nothing
        End If
        Return retval
    End Function

#End Region

#Region "SafeNaming functions"

    ''' <summary>
    ''' returns a safe name for storage
    ''' </summary>
    ''' <param name="theName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function GetSafeName(ByVal theName As String) As String
        Return XmlConvert.EncodeName(theName)
    End Function

    ''' <summary>
    ''' takes a make safe name and converts it back to the original
    ''' </summary>
    ''' <param name="theName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shared Function GetOrginalName(ByVal theName As String) As String
        Return XmlConvert.DecodeName(theName)
    End Function

#End Region

End Class


