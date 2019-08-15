
Public Class xSettings

    Public Const BASE_NODE As String = "Settings"
    Public Const DEFAULTKEY As String = "Default"
    Public Const BACKUP_EXT As String = ".backup"

    Private _Doc As XmlDocument
    Private _Pth As String
    Private _Base As XmlNode
    Private _AllowSave As Boolean

    Public Sub New(ByVal FileName As String, Optional ByVal AllowSave As Boolean = True)
        _Doc = New XmlDocument
        _Pth = FileName
        _AllowSave = AllowSave
        Dim LoadDefaults As Boolean

        'try to open the file
        If IO.File.Exists(FileName) Then
            Try
                _Doc.Load(FileName)
            Catch ex As Exception
                LoadDefaults = True
            End Try
        Else
            LoadDefaults = True
        End If

        'file failed open start off with defaults
        If LoadDefaults Then
            _Doc.LoadXml($"<?xml version=""1.0"" encoding=""utf-8""?><{BASE_NODE}/>")
        End If

        If AllowSave AndAlso Not LoadDefaults Then
            Try
                _Doc.Save(FileName & BACKUP_EXT)
            Catch ex As Exception
            End Try
        End If


        'load up the base node
        _Base = _Doc.SelectSingleNode(BASE_NODE)
    End Sub


    Public Sub New(ByVal xmldoc As XmlDocument)
        _Doc = xmldoc
        _AllowSave = False
        'load up the base node
        _Base = _Doc.SelectSingleNode(BASE_NODE)
        If _Base Is Nothing Then
            _Base = _Doc.CreateElement(BASE_NODE)
            _Doc.AppendChild(_Base)
        End If
    End Sub

    ''' <summary>
    ''' Saves the file
    ''' </summary>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Sub Save()
        If Not (_Doc Is Nothing) AndAlso _AllowSave Then
            _Doc.Save(_Pth)
        End If
    End Sub

    ''' <summary>
    ''' gets a node by it's key
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ND(ByVal Key As String) As Setts_ND
        If Key = String.Empty Then Key = DEFAULTKEY
        Key = Xml.XmlConvert.EncodeName(Key)

        Dim tmpNode As XmlNode = _Base.SelectSingleNode(Key)
        If tmpNode Is Nothing Then
            tmpNode = _Doc.CreateElement(Key)
            _Base.AppendChild(tmpNode)
        End If

        Return New Setts_ND(tmpNode)
    End Function


    Public Sub Remove_ND(ByVal Key As String)
        If Key = String.Empty Then Key = DEFAULTKEY
        Key = Xml.XmlConvert.EncodeName(Key)

        Dim tmpNode As XmlNode = _Base.SelectSingleNode(Key)
        If tmpNode IsNot Nothing Then
            _Base.RemoveChild(tmpNode)
        End If
    End Sub

    ''' <summary>
    ''' clears all the attributes from the node
    ''' </summary>
    ''' <param name="Key">the item to clear</param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Sub Clear(Optional ByVal Key As String = DEFAULTKEY)
        Key = Xml.XmlConvert.EncodeName(Key)
        Dim tmpNode As XmlNode = _Base.SelectSingleNode(Key)
        If tmpNode Is Nothing Then
            tmpNode = _Doc.CreateElement(Key)
            _Base.AppendChild(tmpNode)
        End If
        tmpNode.RemoveAll()
    End Sub

    ''' <summary>
    ''' returns true if a key is found
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function CheckForKey(ByVal key As String) As Boolean
        Dim tmpNode As XmlNode = _Base.SelectSingleNode(Xml.XmlConvert.EncodeName(key))
        Return tmpNode IsNot Nothing
    End Function

End Class


Public Class Setts_ND

    Private _nd As XmlNode

    Friend Sub New(ByVal nx As XmlNode)
        _nd = nx
    End Sub

#Region "string"

    ''' <summary>
    ''' This get returns a string
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As String) As String
        Att = Xml.XmlConvert.EncodeName(Att)
        Dim Retval As String = DefaultValue
        Dim xatt As XmlAttribute = _nd.Attributes(Att)
        If xatt IsNot Nothing AndAlso Not String.IsNullOrEmpty(xatt.Value) Then Retval = xatt.Value
        Return Retval
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As String)
        Att = Xml.XmlConvert.EncodeName(Att)
        Dim tmpAtt As XmlAttribute = _nd.Attributes(Att)
        If tmpAtt Is Nothing Then
            tmpAtt = _nd.OwnerDocument.CreateAttribute(Att)
            _nd.Attributes.Append(tmpAtt)
        End If
        tmpAtt.Value = Value
    End Sub

    Public Function Instance() As Setts_ND
        Return Me
    End Function

#End Region

#Region "Boolean"

    ''' <summary>
    ''' This get returns a Boolean
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Boolean) As Boolean
        Return CBool([Get](Att, DefaultValue.ToString))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Boolean)
        [Set](Att, Value.ToString)
    End Sub

#End Region

#Region "integer"

    ''' <summary>
    ''' This get returns a Integer
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Integer) As Integer
        Return CInt([Get](Att, DefaultValue.ToString))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Integer)
        [Set](Att, Value.ToString)
    End Sub

#End Region

#Region "double"

    ''' <summary>
    ''' This get returns a double
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Double) As Double
        Return CDbl([Get](Att, DefaultValue.ToString))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Double)
        [Set](Att, Value.ToString)
    End Sub

#End Region

#Region "date"

    ''' <summary>
    ''' This get returns a date
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Date) As Date
        Return CDate([Get](Att, DefaultValue.ToString))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Date)
        [Set](Att, Value.ToString)
    End Sub

#End Region

#Region "Enum"

    ''' <summary>
    ''' returns a Enum as an object from the attribute/ gets value not value name
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnum(ByVal keyname As String, ByVal Defaultvalue As System.Enum) As System.Enum
        Dim int As Integer = DirectCast(Defaultvalue, IConvertible).ToInt32(CultureInfo.InvariantCulture)
        Dim a As String = [Get](keyname, int.ToString(CultureInfo.InvariantCulture))
        Dim typ As Type = Defaultvalue.GetType
        Return DirectCast(System.Enum.Parse(typ, a), System.Enum)
    End Function

    ''' <summary>
    ''' sets a enum value to an xmlnode's attribute/ saves the value not the value name
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>calls the [set] string method</remarks>
    Public Sub SetEnum(ByVal keyname As String, ByVal value As System.Enum)
        Dim int As Integer = DirectCast(value, IConvertible).ToInt32(Nothing)
        Me.Set(keyname, int.ToString(CultureInfo.InvariantCulture))
    End Sub

#End Region

#Region "Color"

    ''' <summary>
    ''' This get returns a color
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Color) As Color
        Return Color.FromArgb([Get](Att, DefaultValue.ToArgb))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Color)
        [Set](Att, Value.ToArgb.ToString)
    End Sub

#End Region

#Region "size"

    ''' <summary>
    ''' This get returns a size
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Size) As Size
        Dim xin() As String = [Get](Att, DefaultValue.Width & "," & DefaultValue.Height).Split(",")
        Return New Size(CInt(xin(0)), CInt(xin(1)))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Size)
        [Set](Att, Value.Width & "," & Value.Height)
    End Sub

#End Region

#Region "point"

    ''' <summary>
    ''' This get returns a point
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Point) As Point
        Dim xin() As String = [Get](Att, DefaultValue.X & "," & DefaultValue.Y).Split(",")
        Return New Point(CInt(xin(0)), CInt(xin(1)))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Point)
        [Set](Att, Value.X & "," & Value.Y)
    End Sub

#End Region

#Region "font"

    ''' <summary>
    ''' This get returns a font
    ''' </summary>
    ''' <param name="Att">attribute name</param>
    ''' <param name="DefaultValue">in case of missing</param>
    ''' <returns>this will not create the node only return the default value if missing</returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function [Get](ByVal Att As String, ByVal DefaultValue As Font) As Font
        Dim xin() As String = [Get](Att, DefaultValue.Name & "," & DefaultValue.Size & "," & CInt(DefaultValue.Style)).Split(",")
        Return New Font(xin(0), CSng(xin(1)), DirectCast(CInt(xin(2)), FontStyle))
    End Function

    ''' <summary>
    ''' This will save the value back to the xml file
    ''' </summary>
    ''' <param name="Att">the attribute to search for</param>
    ''' <param name="Value">the value to save</param>
    ''' <remarks>this will create a new node/attribute if missing and then save the data to it.</remarks>
    <DebuggerStepThrough()> Public Sub [Set](ByVal Att As String, ByVal Value As Font)
        [Set](Att, Value.Name & "," & Value.Size & "," & CInt(Value.Style))
    End Sub

#End Region

#Region "remove/check for att"

    ''' <summary>
    ''' removes and existing attribute
    ''' </summary>
    ''' <param name="att"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Sub RemoveAtt(ByVal att As String)
        Dim ax As XmlAttribute = _nd.Attributes(att)
        If ax IsNot Nothing Then _nd.Attributes.Remove(ax)
    End Sub

    ''' <summary>
    ''' removes and existing attributes
    ''' </summary>
    ''' <param name="att"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Sub RemoveAtt(ByVal att() As String)
        For xloop As Integer = 0 To att.Length - 1
            Dim ax As XmlAttribute = _nd.Attributes(att(xloop))
            If ax IsNot Nothing Then _nd.Attributes.Remove(ax)
        Next
    End Sub

    ''' <summary>
    ''' is a specified attribute exists or not
    ''' </summary>
    ''' <param name="att"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Function Exists(ByVal att As String) As Boolean
        Dim ax As XmlAttribute = _nd.Attributes(att)
        If ax IsNot Nothing Then Return True Else Return False
    End Function

    ''' <summary>
    ''' deletes the entire node
    ''' </summary>
    Public Sub Delete()
        _nd.ParentNode.RemoveChild(_nd)
    End Sub

#End Region

End Class