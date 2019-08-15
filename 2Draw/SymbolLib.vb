


Imports _2Draw

Public Class SymbolLib


    Public Class Library
        Public Name As String
        Public Items As List(Of Symbol)


        Public Sub New(theName As String)
            Name = theName
            Items = New List(Of Symbol)
        End Sub

        Public Class Symbol
            Public Const THUMBSIZE As Integer = 64

            Public Sub New(theItem As Symbol_2D, theName As String)
                item = theItem
                Dim r As RectangleF = item.GetOrigionalRec
                If r.X > 0 OrElse r.Y > 0 Then
                    item.ApplyOffset(New PointF(-r.X, -r.Y))
                End If
                Thumbnail = theItem.CreateThumbNail(THUMBSIZE)
                Name = theName
            End Sub

            Public item As Symbol_2D
            Public Thumbnail As Image
            Public Name As String
            ''' <summary>
            ''' this is only for the display lists
            ''' </summary>
            Public DisplayRec As Rectangle
        End Class

    End Class


    ''' <summary>
    ''' the file to open or save from
    ''' </summary>
    Private _FileName As String
    ''' <summary>
    ''' the collection of xml files contained in the project
    ''' </summary>
    Public Librarys As List(Of Library)
    ''' <summary>
    ''' temporary name used for backup saving
    ''' </summary>
    Private _Tempname As String

    Private Const XROOT As String = "Libraries"
    Private Const XLIBR As String = "Library"
    Private Const XITEM As String = "SymbolItem"
    Private Const XNAME As String = "name"
    Private Const XSYM2 As String = "Item"
    Private Const DEFLIB As String = "General"
    Private Const DEFFIL As String = "\2Draw Symbols.Library"


    ''' <summary>
    ''' loads the libraries of symbols
    ''' </summary>
    Public Sub New()
        Librarys = New List(Of Library)

        Dim fld As New DirectoryInfo(Application.StartupPath)
        fld = DirectoryIOAccess.GetUsableDirectory(fld, DirectoryIOAccess.Other.Docs)
        Dim file As String = fld.FullName & DEFFIL
        _FileName = file


        If IO.File.Exists(file) Then
            Dim xdoc As New XmlDocument
            xdoc.Load(file)

            Dim r As XmlNode = xdoc.SelectSingleNode(XROOT)
            Dim libs As XmlNodeList = r.SelectNodes(XLIBR)
            For Each xn As XmlNode In libs
                Dim ll As New Library(XHelper.Get(xn, XNAME))
                Librarys.Add(ll)
                Dim items As XmlNodeList = xn.SelectNodes(XITEM)
                For Each xa As XmlNode In items
                    Dim sb As New Symbol_2D(xa.SelectSingleNode(XSYM2))
                    Dim sy As New Library.Symbol(sb, XHelper.Get(xa, XNAME))
                    ll.Items.Add(sy)
                Next
            Next
        Else
            Librarys.Add(New Library(DEFLIB))
        End If
    End Sub

    Public ReadOnly Property SymbolFileName() As String
        Get
            Return _FileName
        End Get
    End Property


    ''' <summary>
    ''' if not found will return the default library
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function LibraryByName(name) As Library
        Dim ret As Library = Nothing
        For i As Integer = 0 To Librarys.Count - 1
            If Librarys(i).Name = name Then
                ret = Librarys(i)
                Exit For
            End If
        Next
        Return ret
    End Function

    ''' <summary>
    ''' saves a project to a file
    ''' </summary>
    Public Sub Save()
        If Not String.IsNullOrEmpty(_FileName) Then
            SaveFile(_FileName)
        End If
    End Sub

    ''' <summary>
    ''' saves a temporary copy to a file
    ''' </summary>
    Public Sub SaveBackupTemp()
        If String.IsNullOrEmpty(_FileName) Then
            If String.IsNullOrEmpty(_Tempname) Then _Tempname = $"2Draw {Now.ToString("yyyyMMddHHmm")}.Library"
            SaveFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & _Tempname)
        Else
            Dim fl As New FileInfo(_FileName)
            SaveFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & fl.Name)
        End If
    End Sub

    ''' <summary>
    ''' does the actual saveing of the file
    ''' </summary>
    ''' <param name="Fullname"></param>
    Private Sub SaveFile(Fullname As String)
        If File.Exists(Fullname) Then
            File.Delete(Fullname)
        End If

        Dim r As XmlNode = Nothing
        Dim xdoc As XmlDocument = XHelper.CreateNewDocument(XROOT, r)

        For i As Integer = 0 To Librarys.Count - 1
            Dim ll As Library = Librarys(i)
            Dim ln As XmlNode = XHelper.NodeAppendNew(r, XLIBR)
            XHelper.Set(ln, XNAME, ll.Name)

            For j As Integer = 0 To ll.Items.Count - 1
                Dim lx As Library.Symbol = ll.Items(j)
                Dim sn As XmlNode = XHelper.NodeAppendNew(ln, XITEM)
                XHelper.Set(sn, XNAME, lx.Name)
                Dim symn As XmlNode = XHelper.NodeAppendNew(sn, XSYM2)
                lx.item.Save(symn)
            Next
        Next
        xdoc.Save(Fullname)
    End Sub


    Public Function GetLibraryNames() As String()
        Dim ret(Librarys.Count - 1) As String
        For i As Integer = 0 To Librarys.Count - 1
            ret(i) = Librarys(i).Name
        Next
        Return ret
    End Function

    Friend Function GetItemByUID(UID As String) As Symbol_2D
        Dim ret As Symbol_2D = Nothing
        For i As Integer = 0 To Librarys.Count - 1
            For j As Integer = 0 To Librarys(i).Items.Count - 1
                If Librarys(i).Items(j).item.UID = UID Then
                    ret = Librarys(i).Items(j).item
                    Exit For
                End If
            Next
        Next
        Return ret
    End Function


End Class
