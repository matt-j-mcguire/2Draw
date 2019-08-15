Imports System.Text.RegularExpressions

''' <summary>
''' represents a light version of the standard xml Document
''' </summary>
''' <remarks></remarks>
<CLSCompliant(True)> Public Class LDom

    ''' <summary>
    ''' the base xml node of the document
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Base As LNode
    ''' <summary>
    ''' the file name of the xml document
    ''' </summary>
    ''' <remarks></remarks>
    Public Property FileName As String
    ''' <summary>
    ''' if there are any comments for the document
    ''' </summary>
    ''' <remarks></remarks>
    Private _attComments As List(Of String)
    ''' <summary>
    ''' turn off comments in the code for faster processing
    ''' </summary>
    ''' <remarks></remarks>
    Public Property SupportsComments As Boolean = True

#Region "Open"

    ''' <summary>
    ''' opens the xml via stream
    ''' </summary>
    ''' <param name="Stream"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Open(ByVal Stream As IO.Stream) As Boolean
        FileName = ""
        If Stream IsNot Nothing Then
            Try
                Dim reader As New XmlTextReader(Stream)
                Return Openx(reader)
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' opens the xml via text fragment
    ''' </summary>
    ''' <param name="PartialText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Open(ByVal PartialText As String) As Boolean
        FileName = ""
        If PartialText <> "" Then
            Try
                Dim con As New XmlParserContext(Nothing, Nothing, Nothing, XmlSpace.None)
                Dim reader As New XmlTextReader(PartialText, XmlNodeType.Document, con)
                Return Openx(reader)
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' opens a xml file
    ''' </summary>
    ''' <param name="theFile"></param>
    ''' <remarks></remarks>
    Public Function Open(ByVal theFile As IO.FileInfo) As Boolean
        FileName = theFile.FullName
        If theFile.Exists Then
            Try
                Dim reader As New XmlTextReader(theFile.FullName)
                Return Openx(reader)
            Catch ex As Exception
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' begins the parse
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Openx(ByVal reader As XmlTextReader) As Boolean
        Base = Nothing
        _attComments = New List(Of String)
        Dim retval As Boolean

        Try
            reader.WhitespaceHandling = WhitespaceHandling.None
            Dim xerr As Boolean = False
            Do Until reader.NodeType = XmlNodeType.Element
                If reader.Read() = False Then
                    xerr = True
                End If
                If SupportsComments AndAlso reader.NodeType = XmlNodeType.Comment Then
                    _attComments.Add(reader.Value)
                End If
            Loop
            If xerr = False Then
                Base = New LNode(reader.Name, "")
                For xloop As Integer = 0 To reader.AttributeCount - 1
                    reader.MoveToNextAttribute()
                    Dim at As New LAtt(reader.Name, reader.Value)
                    Base.Attributes.Add(at)
                Next
                reader.MoveToElement()
                If _attComments.Count > 0 Then
                    Base.Comments.AddRange(_attComments)
                    _attComments.Clear()
                End If
                reader.Read()
                checkunder(reader, Base, reader.Depth)

            End If
            retval = True
        Catch ex As Exception
            retval = False
        Finally
            reader.Close()
        End Try

        Return retval
    End Function

    ''' <summary>
    ''' called by open to parse the file
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="thenode"></param>
    ''' <param name="depth"></param>
    ''' <remarks></remarks>
    Private Sub checkunder(ByVal reader As XmlTextReader, ByVal thenode As LNode, ByVal depth As Integer)
        Dim nt As LNode = Nothing
        Select Case reader.NodeType
            Case XmlNodeType.Element
                'add the new node and the children
                nt = New LNode(reader.Name, "")
                For xloop As Integer = 0 To reader.AttributeCount - 1
                    reader.MoveToNextAttribute()
                    Dim at As New LAtt(reader.Name, reader.Value)
                    nt.Attributes.Add(at)
                Next
                reader.MoveToElement()
                thenode.children.Add(nt)
                If _attComments.Count > 0 Then
                    nt.Comments.AddRange(_attComments)
                    _attComments.Clear()
                End If
            Case XmlNodeType.Text
                thenode.Value = reader.Value.Trim
            Case XmlNodeType.Comment
                If SupportsComments Then _attComments.Add(reader.Value)
        End Select

        If reader.Read() = True Then
            If depth = reader.Depth Then
                checkunder(reader, thenode, depth)
            ElseIf depth < reader.Depth Then
                checkunder(reader, nt, reader.Depth)
            Else
                checkunder(reader, thenode.Parent, reader.Depth)
            End If
        End If

    End Sub

#End Region

#Region "Saving"

#Region "Private Constants for writing xml"
    Private Const HED As String = "<?xml version=""1.0"" encoding=""utf-8""?>"
    Private Const BEG As String = "<"
    Private Const BEGCLOSE As String = "</"
    Private Const ENDN As String = ">"
    Private Const ENDCLOSE As String = "/>"
    Private Const ATTBEG As String = "="""
    Private Const ATTEND As String = """"
    Private Const INDCHAR As Char = " "c
    Private Const INDNUM As Integer = 2
    Private Const COMBEG As String = "<!--"
    Private Const COMEND As String = "-->"
    Private Const CRLF As String = ControlChars.CrLf
#End Region

    ''' <summary>
    ''' overload for no save to somewhere else
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save() As Boolean
        Return Me.Save("")
    End Function

    ''' <summary>
    ''' saves a xml file with save somewhere else
    ''' </summary>
    ''' <param name="file">somewhere else to save the file</param>
    ''' <remarks></remarks>
    Public Function Save(ByVal file As String) As Boolean
        Dim retval As Boolean
        If file <> "" Then FileName = file
        If Base IsNot Nothing Or FileName <> "" Then
            Dim writer As New StringBuilder
            Dim stk As New Stack(Of String)

            Try
                writer.Append(HED & CRLF)
                WriteNode(writer, Base, stk, SupportsComments)
                My.Computer.FileSystem.WriteAllText(FileName, writer.ToString, False)
                retval = True
            Catch ex As Exception
                retval = False
            End Try
        Else
            retval = False
        End If
        Return retval
    End Function

    ''' <summary>
    ''' like save but returns a xml string
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAsString() As String
        If Base IsNot Nothing Then
            Dim writer As New StringBuilder
            Dim stk As New Stack(Of String)

            Try
                writer.Append(HED & CRLF)
                WriteNode(writer, Base, stk, SupportsComments)
                Return writer.ToString
            Catch ex As Exception
                Return ""
            End Try
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' returns a string that represents the node
    ''' </summary>
    ''' <param name="nd"></param>
    ''' <param name="appendComments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NodeToString(ByVal nd As LNode, ByVal appendComments As Boolean) As String
        Dim writer As New StringBuilder
        Dim stk As New Stack(Of String)
        WriteNode(writer, nd, stk, appendComments)
        Return writer.ToString
    End Function

    ''' <summary>
    ''' called by save to build up the info in a builder first
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="nd"></param>
    ''' <param name="stk"></param>
    ''' <remarks></remarks>
    Private Shared Sub WriteNode(ByVal writer As StringBuilder, ByVal nd As LNode, ByVal stk As Stack(Of String), ByVal SupportsComments As Boolean)
        With writer

            'first write any comments
            If SupportsComments Then
                For xloop As Integer = 0 To nd.Comments.Count - 1
                    .Append(INDCHAR, (stk.Count) * INDNUM)
                    writer.Append(COMBEG & nd.Comments(xloop) & COMEND & CRLF)
                Next
            End If

            'now start the node
            Dim safnam As String = nd.SafeName
            stk.Push(safnam)
            .Append(INDCHAR, (stk.Count - 1) * INDNUM)
            .Append(BEG & safnam)

            'add the attributes
            For xloop As Integer = 0 To nd.Attributes.Count - 1
                .Append(INDCHAR & nd.Attributes(xloop).SafeName & ATTBEG & nd.Attributes(xloop).SafeValue & ATTEND)
            Next

            'check for children and innervalues
            If nd.children.Count > 0 OrElse nd.Value <> "" Then
                .Append(ENDN & CRLF)
                If nd.Value <> "" Then
                    .Append(INDCHAR, (stk.Count) * INDNUM)
                    .Append(nd.SafeValue.Trim & CRLF)
                    'no children like: <x>fdfd</x>
                    If nd.children.Count = 0 Then
                        .Append(INDCHAR, (stk.Count - 1) * INDNUM)
                        .Append(BEGCLOSE & stk.Pop & ENDN & CRLF)
                    End If
                End If
            Else
                .Append(ENDCLOSE & CRLF)
                stk.Pop()
            End If

            'add any inner children
            If nd.children.Count > 0 Then
                For xloop As Integer = 0 To nd.children.Count - 1
                    WriteNode(writer, nd.children(xloop), stk, SupportsComments)
                Next
                .Append(INDCHAR, (stk.Count - 1) * INDNUM)
                .Append(BEGCLOSE & stk.Pop & ENDN & CRLF)
            End If

        End With
    End Sub

#End Region

#Region "Find functions"

    ''' <summary>
    ''' finds and returns the first node with a matching attribute and value
    ''' </summary>
    ''' <param name="attname">the name of the att to find</param>
    ''' <param name="valuematch">the value of the att to find</param>
    ''' <returns>the found lnode else returns nothing</returns>
    ''' <remarks>call the shared function using this base</remarks>
    Public Function FindNodeWithAttribute(ByVal attname As String, ByVal valuematch As String) As LNode
        If String.IsNullOrEmpty(attname) = False Then
            Return FindNodeWithAttribute(Base, attname, valuematch)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' finds and returns the first node with a matching attribute and value
    ''' </summary>
    ''' <param name="attname">the name of the att to find</param>
    ''' <param name="valuematch">the value of the att to find</param>
    ''' <returns>the found lnode else returns nothing</returns>
    ''' <remarks></remarks>
    Public Shared Function FindNodeWithAttribute(ByVal theNd As LNode, ByVal attname As String, ByVal valuematch As String) As LNode
        If theNd IsNot Nothing AndAlso String.IsNullOrEmpty(attname) = False Then
            Dim at As LAtt = theNd.Attributes.ItemKey(attname)
            Dim retval As LNode = Nothing
            If at IsNot Nothing AndAlso at.Value = valuematch Then
                'item found and matched
                retval = theNd
            Else
                'no value match or no att found 
                'look under the other nodes
                For xloop As Integer = 0 To theNd.children.Count - 1
                    retval = FindNodeWithAttribute(theNd.children(xloop), attname, valuematch)
                    If retval IsNot Nothing Then Exit For
                Next
            End If
            Return retval
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' works with the createpath function to get a stored path, Like: base\node\node(marker)\node(marker)
    ''' </summary>
    ''' <param name="thePath">the path to search in this ldom</param>
    ''' <param name="markeratt">if any (markers) exist in the string, will find a match in the nodes</param>
    ''' <returns>nothing if unsucessfull, else the LNode it was trying to find </returns>
    ''' <remarks></remarks>
    Public Function FindPath(ByVal thePath As String, ByVal markeratt As String) As LNode
        Dim xstr() As String = thePath.Split("\"c)
        Dim retval As LNode = Base

        Dim isatt As Boolean = Not String.IsNullOrEmpty(markeratt)
        For xloop As Integer = 1 To xstr.Length - 1
            Dim pth As New pathpair(xstr(xloop))

            If isatt AndAlso pth.hasName Then
                '=========================================
                'has a attribute name and a value to search
                'for in the sub nodes
                '=========================================
                Dim lst() As LNode = retval.children.ItemList(pth.nodeName)
                retval = Nothing
                For yloop As Integer = 0 To lst.Length - 1
                    Dim x As LAtt = lst(yloop).Attributes.ItemKey(markeratt)
                    If x.Value = pth.name Then
                        retval = lst(yloop)
                        Exit For
                    End If
                Next
            ElseIf isatt = False AndAlso pth.hasName Then
                '=========================================
                'have a value for this level but no att
                'name to search for, this causes an error
                '=========================================
                retval = Nothing
            Else
                '=========================================
                'no value item to find where or not there
                'is a markeratt or not, just get the next
                'node below that matches the name first one
                'found in the sub nodes that match
                '=========================================
                retval = retval.children.ItemKey(pth.nodeName)
            End If
            '=========================================
            'at some point an error generated no node
            'and the search has failed, return nothing
            '=========================================
            If retval Is Nothing Then Exit For
        Next

        Return retval
    End Function

    ''' <summary>
    ''' breaks down this section of the path
    ''' </summary>
    ''' <remarks></remarks>
    Private Structure pathpair

        ''' <summary>
        ''' stores the string and any optional () names
        ''' </summary>
        ''' <param name="inName">string to parse</param>
        ''' <remarks>stores the path node and optional name like: zone(CA 8)</remarks>
        Public Sub New(ByVal inName As String)
            Dim index As Integer = inName.IndexOf("("c)
            If index = -1 Then
                nodeName = inName
                hasName = False
            Else
                nodeName = inName.Substring(index, index)
                name = inName.Substring(index + 1, (inName.Length - index) - 2)
                hasName = Not String.IsNullOrEmpty(name)
            End If
        End Sub

        ''' <summary>
        ''' the name of the node
        ''' </summary>
        ''' <remarks></remarks>
        Public nodeName As String
        ''' <summary>
        ''' the value of a name attribute
        ''' </summary>
        ''' <remarks></remarks>
        Public name As String
        ''' <summary>
        ''' if this item has an attribute
        ''' </summary>
        ''' <remarks></remarks>
        Public hasName As Boolean

    End Structure

    ''' <summary>
    ''' returns a path string to usein the formatof: base\node\node(marker)\node(marker)
    ''' </summary>
    ''' <param name="nd">the lnode to work backwords with</param>
    ''' <param name="markeratt">optional:if any node had a attribute matching, the value will be but in:(value)</param>
    ''' <returns>a string of the full path with "\" as seperators</returns>
    ''' <remarks></remarks>
    Public Shared Function CreatePath(ByVal nd As LNode, ByVal markeratt As String) As String
        Dim xstr As New StringBuilder
        Dim isatt As Boolean = Not String.IsNullOrEmpty(markeratt)
        Do
            If isatt Then
                Dim at As LAtt = nd.Attributes(markeratt)
                If at IsNot Nothing Then
                    xstr.Insert(0, nd.Name & "(" & at.Value & ")\")
                Else
                    xstr.Insert(0, nd.Name & "\")
                End If
            Else
                xstr.Insert(0, nd.Name & "\")
            End If

            nd = nd.Parent
            If nd Is Nothing Then Exit Do
        Loop

        Return xstr.ToString
    End Function

#End Region

End Class

''' <summary>
''' represents a xml node
''' </summary>
''' <remarks></remarks>
<CLSCompliant(True)> Public Class LNode
    ''' <summary>
    ''' items name of the node
    ''' </summary>
    ''' <remarks></remarks>
    Private _Name As String
    ''' <summary>
    ''' items Value of the node
    ''' </summary>
    ''' <remarks></remarks>
    Private _Value As String
    ''' <summary>
    ''' a list of all the children
    ''' </summary>
    ''' <remarks></remarks>
    Public Property children As LNodeCollection
    ''' <summary>
    ''' a list of all the attributes
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Attributes As LAttCollection
    ''' <summary>
    ''' who the parent (node) is
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Parent As LNode
    ''' <summary>
    ''' any comments for this xmlnode 
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Comments As List(Of String)

    ''' <summary>
    ''' crates a new xml light node
    ''' </summary>
    ''' <param name="thename"></param>
    ''' <param name="thevalue"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Sub New(ByVal thename As String, ByVal thevalue As String)
        Me.New()
        _Name = XmlConvert.DecodeName(thename)
        Value = thevalue
    End Sub

    ''' <summary>
    ''' private instance of creating a new Node
    ''' </summary>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Private Sub New()
        _children = New LNodeCollection(Me)
        _Attributes = New LAttCollection
        _Comments = New List(Of String)
    End Sub

    ''' <summary>
    ''' returns a shallow copy of this node
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyShallow() As LNode
        Dim ex As New LNode(_Name, _Value)
        ex.children = Me.children
        ex.Attributes = Me.Attributes
        ex.Parent = Me.Parent
        Return ex
    End Function

    ''' <summary>
    ''' returns a completely new copy of this
    ''' node and all it's children and attributes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyDeep() As LNode
        Dim ex As LNode = Nothing
        startcopy(Me, ex, Nothing)
        Return ex
    End Function

    ''' <summary>
    ''' works with CopyDeep
    ''' </summary>
    ''' <param name="orgnode">the original node</param>
    ''' <param name="cpynode">if empty node to copy to</param>
    ''' <param name="parent">where to put the new copied node</param>
    ''' <remarks></remarks>
    Private Shared Sub startcopy(ByVal orgnode As LNode, ByRef cpynode As LNode, ByVal parent As LNode)
        cpynode = New LNode(orgnode.Name, orgnode.Value)
        If parent IsNot Nothing Then parent.children.Add(cpynode)

        '---add the attributes---
        For xloop As Integer = 0 To orgnode.Attributes.Count - 1
            Dim a As New LAtt(orgnode.Attributes(xloop))
            cpynode.Attributes.Add(a)
        Next

        '---add the children---
        For xloop As Integer = 0 To orgnode.children.Count - 1
            Dim ex As LNode = Nothing
            startcopy(orgnode.children(xloop), ex, cpynode)
        Next
    End Sub

    ''' <summary>
    ''' returns the name-value of this node (value if any)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        If Value <> "" Then
            Return _Name & "-" & _Value
        Else
            Return _Name
        End If
    End Function

    ''' <summary>
    ''' name is readonly once created
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    ''' <summary>
    ''' when set will parse for safestring values
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal valuex As String)
            If valuex.Contains("&") Then
                Dim sb As New System.Text.StringBuilder(valuex)
                sb.Replace("&lf;", "<")
                sb.Replace("&apos;", "'")
                sb.Replace("&quot;", """")
                sb.Replace("&amp;", "&")
                _Value = sb.ToString
            Else
                _Value = valuex
            End If
        End Set
    End Property

    ''' <summary>
    ''' returns a safe formatted name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SafeName() As String
        Get
            Return XmlConvert.EncodeName(_Name)
        End Get
    End Property

    ''' <summary>
    ''' returns a safe parsed value for this attribue
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SafeValue() As String
        Get
            If String.IsNullOrEmpty(_Value) = False AndAlso Regex.IsMatch(_Value, "[<&'""]") Then
                Dim sb As New System.Text.StringBuilder(_Value)
                sb.Replace("&", "&amp;")
                sb.Replace("<", "&lf;")
                sb.Replace("'", "&apos;")
                sb.Replace("""", "&quot;")
                Return sb.ToString
            Else
                Return _Value
            End If
        End Get
    End Property

End Class

''' <summary>
''' is a collection of child nodes
''' </summary>
''' <remarks></remarks>
<CLSCompliant(True)> Public Class LNodeCollection
    Inherits List(Of LNode)

    ''' <summary>
    ''' who owns this list, backwards ref
    ''' </summary>
    ''' <remarks></remarks>
    Private OwnerRef As LNode

    ''' <summary>
    ''' creates a new lnode list
    ''' </summary>
    ''' <param name="Owner"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Friend Sub New(ByVal Owner As LNode)
        OwnerRef = Owner
    End Sub

    ''' <summary>
    ''' adds a single lnode to this list
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shadows Sub Add(ByVal item As LNode)
        If item.Parent IsNot Nothing Then item.Parent.children.Remove(item)
        item.Parent = OwnerRef
        MyBase.Add(item)
    End Sub

    ''' <summary>
    ''' adds a range of lNodes to this node list
    ''' </summary>
    ''' <param name="Items"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Shadows Sub AddRange(ByVal Items() As LNode)
        For xloop As Integer = 0 To Items.Length - 1
            If Items(xloop).Parent IsNot Nothing Then Items(xloop).Parent.children.Remove(Item(xloop))
            Items(xloop).Parent = OwnerRef
        Next
        MyBase.AddRange(Items)
    End Sub

    ''' <summary>
    ''' returns a single item that matches by name
    ''' </summary>
    ''' <param name="KeyName">key name to match</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>may return nothing if item not found</remarks>
    Public ReadOnly Property ItemKey(ByVal KeyName As String) As LNode
        Get
            Dim retval As LNode = Nothing

            For xloop As Integer = 0 To Me.Count - 1
                If KeyName = Item(xloop).Name Then
                    retval = Item(xloop)
                    Exit For
                End If
            Next
            Return retval
        End Get
    End Property

    ''' <summary>
    ''' returns a single item that matches by name
    ''' </summary>
    ''' <param name="KeyName">key name to match</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>may return nothing if item not found</remarks>
    Public ReadOnly Property ItemKey(ByVal KeyName As String, ByVal returnItemIfMissing As Boolean) As LNode
        Get
            Dim retval As LNode = Nothing

            For xloop As Integer = 0 To Me.Count - 1
                If KeyName = Item(xloop).Name Then
                    retval = Item(xloop)
                    Exit For
                End If
            Next
            If returnItemIfMissing AndAlso retval Is Nothing Then
                retval = New LNode(KeyName, "")
                Me.Add(retval)
            End If
            Return retval
        End Get
    End Property

    ''' <summary>
    ''' returns an array of nodes that match  the key name
    ''' </summary>
    ''' <param name="KeyName">the name to search for</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>may return nothing if not found</remarks>
    Public ReadOnly Property ItemList(ByVal KeyName As String) As LNode()
        Get
            Dim retval As New List(Of LNode)
            For xloop As Integer = 0 To Me.Count - 1
                If KeyName = Item(xloop).Name Then
                    retval.Add(Item(xloop))
                End If
            Next
            Return retval.ToArray
        End Get
    End Property

End Class

''' <summary>
''' a single attribute
''' </summary>
''' <remarks></remarks>
<CLSCompliant(True)> Public Class LAtt
    Private _Name As String
    Private _Value As String

    ''' <summary>
    ''' creates a new light attribute from a name and value
    ''' </summary>
    ''' <param name="theName"></param>
    ''' <param name="theValue"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Sub New(ByVal theName As String, ByVal theValue As String)
        _Name = XmlConvert.DecodeName(theName)
        Value = theValue
    End Sub

    ''' <summary>
    ''' creates a new light attribute from an existing one
    ''' </summary>
    ''' <param name="att"></param>
    ''' <remarks></remarks>
    <DebuggerStepThrough()> Public Sub New(ByVal att As LAtt)
        _Name = att._Name
        _Value = att._Value
    End Sub

    ''' <summary>
    ''' returns a name-value string if a value does exist
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        If _Value <> "" Then
            Return _Name & "-" & _Value
        Else
            Return _Name
        End If
    End Function

    ''' <summary>
    ''' name is readonly once created
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    ''' <summary>
    ''' when set will parse for safestring values
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal valuex As String)
            If valuex.Contains("&") Then
                Dim sb As New System.Text.StringBuilder(valuex)
                sb.Replace("&lf;", "<")
                sb.Replace("&apos;", "'")
                sb.Replace("&quot;", """")
                sb.Replace("&amp;", "&")
                _Value = sb.ToString
            Else
                _Value = valuex
            End If
        End Set
    End Property

    ''' <summary>
    ''' returns a safe formatted name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SafeName() As String
        Get
            Return XmlConvert.EncodeName(_Name)
        End Get
    End Property

    ''' <summary>
    ''' returns a safe parsed value for this attribue
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SafeValue() As String
        Get
            If String.IsNullOrEmpty(_Value) = False AndAlso Regex.IsMatch(_Value, "[<&'""]") Then
                Dim sb As New System.Text.StringBuilder(_Value)
                sb.Replace("&", "&amp;")
                sb.Replace("<", "&lf;")
                sb.Replace("'", "&apos;")
                sb.Replace("""", "&quot;")
                Return sb.ToString
            Else
                Return _Value
            End If
        End Get
    End Property

End Class

''' <summary>
''' represents a collection of Attributes
''' </summary>
''' <remarks></remarks>
<CLSCompliant(True)> Public Class LAttCollection
    Inherits List(Of LAtt)

    ''' <summary>
    ''' returns a single item that matches by name
    ''' </summary>
    ''' <param name="KeyName">key name to match</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>may return nothing if item not found</remarks>
    Public ReadOnly Property ItemKey(ByVal KeyName As String) As LAtt
        Get
            Dim retval As LAtt = Nothing
            For xloop As Integer = 0 To Me.Count - 1
                If KeyName = Item(xloop).Name Then
                    retval = Item(xloop)
                    Exit For
                End If
            Next
            Return retval
        End Get
    End Property

    ''' <summary>
    ''' returns a single item that matches by name
    ''' </summary>
    ''' <param name="KeyName">key name to match</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>if not found creates a new LAtt and adds it to the list</remarks>
    Public ReadOnly Property ItemKey(ByVal KeyName As String, ByVal defaultvalue As String) As LAtt
        Get
            Dim retval As LAtt = Nothing
            For xloop As Integer = 0 To Me.Count - 1
                If KeyName = Item(xloop).Name Then
                    retval = Item(xloop)
                    Exit For
                End If
            Next

            If retval Is Nothing Then
                retval = New LAtt(KeyName, defaultvalue)
                Me.Add(retval)
            End If

            Return retval
        End Get
    End Property

#Region "String"

    ''' <summary>
    ''' returns a string from the attribute
    ''' </summary>
    ''' <param name="keyname"></param>
    ''' <param name="defaultvalue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal keyname As String, ByVal defaultvalue As String) As String
        Dim a As LAtt = ItemKey(keyname)
        If a Is Nothing Then
            a = New LAtt(keyname, defaultvalue)
            Me.Add(a)
        End If
        If String.IsNullOrEmpty(a.Value) Then a.Value = defaultvalue
        Return a.Value
    End Function


    ''' <summary>
    ''' returns a string from the attribute
    ''' </summary>
    ''' <param name="keyname"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal keyname As String) As String
        Return Me.Get(keyname, "")
    End Function

    ''' <summary>
    ''' sets string to the attribute
    ''' </summary>
    ''' <param name="keyname"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal keyname As String, ByVal value As String)
        Dim a As LAtt = ItemKey(keyname)
        If a Is Nothing Then
            a = New LAtt(keyname, value)
            Me.Add(a)
        Else
            a.Value = value
        End If
    End Sub

#End Region

#Region "Integer"

    ''' <summary>
    ''' Returns a Integer from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Integer) As Integer
        Return CInt(Me.Get(Keyname, DefaultValue.ToString(CultureInfo.InvariantCulture)))
    End Function

    ''' <summary>
    ''' sets a integer to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Integer)
        Me.Set(Keyname, Value.ToString(CultureInfo.InvariantCulture))
    End Sub

#End Region

#Region "Double"

    ''' <summary>
    ''' Returns a double from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Double) As Double
        Return CDbl(Me.Get(Keyname, DefaultValue.ToString(CultureInfo.InvariantCulture)))
    End Function

    ''' <summary>
    ''' sets a double to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Double, ByVal decimalplaces As Integer)
        Me.Set(Keyname, Value.ToString("F" & decimalplaces, CultureInfo.InvariantCulture))
    End Sub

#End Region

#Region "Boolean"

    ''' <summary>
    ''' Returns a Boolean from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Boolean) As Boolean
        Return CBool(Me.Get(Keyname, DefaultValue.ToString))
    End Function

    ''' <summary>
    ''' sets a Boolean to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Boolean)
        Me.Set(Keyname, Value.ToString)
    End Sub

#End Region

#Region "Color"

    ''' <summary>
    ''' Returns a color from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Color) As Color
        Dim c As Color = Color.FromArgb(Me.Get(Keyname, DefaultValue.ToArgb))
        If c.A = 0 Then c = Color.Transparent
        Return c
    End Function

    ''' <summary>
    ''' sets a color to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Color)
        Me.Set(Keyname, Value.ToArgb)
    End Sub

#End Region

#Region "Point"

    ''' <summary>
    ''' Returns a point (x,y) from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Point) As Point
        Dim c As String = Me.Get(Keyname, "x" & DefaultValue.X & ":y" & DefaultValue.Y)

        Dim ix, id, iy As Integer
        ix = c.IndexOf("x", StringComparison.OrdinalIgnoreCase)
        id = c.IndexOf(":", StringComparison.OrdinalIgnoreCase)
        iy = c.IndexOf("y", StringComparison.OrdinalIgnoreCase)

        Dim pt As Point
        Try
            pt.X = CInt(c.Substring(ix + 1, id - (ix + 1)))
            pt.Y = CInt(c.Substring(iy + 1, c.Length - (iy + 1)))
        Catch ex As Exception
            pt = DefaultValue
        End Try

        Return pt
    End Function

    ''' <summary>
    ''' sets a point (x,y) to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Point)
        Me.Set(Keyname, "x" & Value.X & ":y" & Value.Y)
    End Sub

#End Region

#Region "Size"

    ''' <summary>
    ''' Returns a size (w,h) from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Size) As Size
        Dim c As String = Me.Get(Keyname, "w" & DefaultValue.Width & ":h" & DefaultValue.Height)

        Dim ix, id, iy As Integer
        ix = c.IndexOf("w", StringComparison.OrdinalIgnoreCase)
        id = c.IndexOf(":", StringComparison.OrdinalIgnoreCase)
        iy = c.IndexOf("h", StringComparison.OrdinalIgnoreCase)

        Dim sz As Size
        Try
            sz.Width = CInt(c.Substring(ix + 1, id - (ix + 1)))
            sz.Height = CInt(c.Substring(iy + 1, c.Length - (iy + 1)))
        Catch ex As Exception
            sz = DefaultValue
        End Try

        Return sz
    End Function

    ''' <summary>
    ''' sets a size (w,h) to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Size)
        Me.Set(Keyname, "w" & Value.Width & ":h" & Value.Height)
    End Sub

#End Region

#Region "Rectangle"

    ''' <summary>
    ''' Returns a Rectangle (x,y,w,h) from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Rectangle) As Rectangle
        Dim c() As String = Me.Get(Keyname, "x" & DefaultValue.X & ":y" & DefaultValue.Y & ":w" & DefaultValue.Width & ":h" & DefaultValue.Height).Split(":"c)

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
    ''' sets a Rectangle (x,y,w,h) to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Rectangle)
        Me.Set(Keyname, "x" & Value.X & ":y" & Value.Y & ":w" & Value.Width & ":h" & Value.Height)
    End Sub

#End Region

#Region "Font"

    ''' <summary>
    ''' Returns a Font from the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute needed</param>
    ''' <param name="DefaultValue">if the value or attribute is missing then return this value (optional)</param>
    ''' <returns>a string for the attributes value</returns>
    ''' <remarks></remarks>
    Public Function [Get](ByVal Keyname As String, ByVal DefaultValue As Font) As Font
        Dim c() As String = Me.Get(Keyname, DefaultValue.Name & ":" & DefaultValue.Size & ":" & DefaultValue.Style).Split(":"c)

        Dim fnt As Font = DefaultValue
        If c.Length = 3 Then
            Try
                fnt = New Font(c(0), CInt(c(1)), DirectCast(CInt(c(2)), FontStyle))
            Catch ex As Exception
                'possible parse error, ignore
            End Try
        End If
        Return fnt
    End Function

    ''' <summary>
    ''' sets a Font to the attribute
    ''' </summary>
    ''' <param name="keyname">the name of the attribute to be saved</param>
    ''' <param name="Value">the value of the attribute to be saved</param>
    ''' <remarks></remarks>
    Public Sub [Set](ByVal Keyname As String, ByVal Value As Font)
        Me.Set(Keyname, Value.Name & ":" & Value.Size & ":" & Value.Style)
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

End Class