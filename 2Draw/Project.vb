


Public Class Project

    Public Const FILE_EXTENTION As String = ".2Draw"
    Public Const SETS_MAIN As String = "Main"
    Public Const SETS_SELECTEDPAGE As String = "selectedpage"


    ''' <summary>
    ''' the file to open or save from
    ''' </summary>
    Public FileName As String
    ''' <summary>
    ''' the collection of xml files contained in the project
    ''' </summary>
    Public Pages As List(Of Page)
    ''' <summary>
    ''' the collection of any image files contained in the project
    ''' </summary>
    Public Images As List(Of XPic)
    ''' <summary>
    ''' contains information about the project
    ''' </summary>
    Private _ProjectInfo As XmlDocument
    ''' <summary>
    ''' to hold more formatted settings
    ''' </summary>
    Public Settings As xSettings
    ''' <summary>
    ''' if the progject needs saved
    ''' </summary>
    Public DirtyFlag As Boolean
    ''' <summary>
    ''' temporary name used for backup saving
    ''' </summary>
    Private _Tempname As String


    ''' <summary>
    ''' no file passed to this, create a default setup
    ''' <param name="name">the name of the page</param>
    ''' <param name="Landscape">if it will be in landscape or portrate</param>
    ''' <param name="PS_size">must be one of the pageSizes.PS_ constants</param>
    ''' </summary>
    Public Sub New(name As String, PS_size As String, Landscape As Boolean)
        Pages = New List(Of Page)
        Images = New List(Of XPic)

        '====create the defrault page====
        AddPage(name, PS_size, Landscape)

        '====create the default project info====
        _ProjectInfo = XHelper.CreateNewDocument(xSettings.BASE_NODE)
        Settings = New xSettings(_ProjectInfo)
    End Sub

    Public Sub New(SaveName As String, existingPage As Page)
        Pages = New List(Of Page)
        Images = New List(Of XPic)
        Pages.Add(existingPage)
        FileName = SaveName
        _ProjectInfo = XHelper.CreateNewDocument(xSettings.BASE_NODE)
        Settings = New xSettings(_ProjectInfo)
    End Sub

    ''' <summary>
    ''' loads the project from a known file
    ''' </summary>
    ''' <param name="file"></param>
    Public Sub New(file As String)
        Pages = New List(Of Page)
        Images = New List(Of XPic)


        If IO.File.Exists(file) Then
            FileName = file
            Try
                Dim headerURI As New Uri("/Header", UriKind.Relative)
                Dim z As ZipPackage = ZipPackage.Open(file, IO.FileMode.Open)
                Dim prt As PackagePart = z.GetPart(headerURI)
                Dim lst() As URIS = readinURIS(prt.GetStream)

                For i As Integer = 0 To lst.Length - 1
                    If lst(i).isValid Then
                        prt = z.GetPart(lst(i).URI)
                        Select Case lst(i).filetype
                            Case URIS.typ.pro
                                _ProjectInfo = New XmlDocument
                                _ProjectInfo.Load(prt.GetStream)
                                Settings = New xSettings(_ProjectInfo)
                            Case URIS.typ.xml
                                Dim d As New XmlDocument
                                d.Load(prt.GetStream)
                                Dim x As New Page(d, lst(i).name)
                                Pages.Add(x)
                                AddHandler x.FileChanged, AddressOf SomethingChanged
                            Case URIS.typ.pic
                                Dim p As Bitmap = Bitmap.FromStream(prt.GetStream)
                                Dim x As New XPic(p, lst(i).name)
                                Me.Images.Add(x)
                        End Select
                    End If
                Next
                z.Close()
            Catch ex As Exception
                Throw New Exception("unable to open file", ex)
            End Try
        End If
    End Sub

    Private Function readinURIS(strx As Stream) As URIS()
        Dim read As New StreamReader(strx)
        Dim ret As New List(Of URIS)
        While Not read.EndOfStream
            Dim str As String = read.ReadLine
            If Not String.IsNullOrEmpty(str) Then
                ret.Add(URIS.fromString(str))
            End If
        End While

        Return ret.ToArray
    End Function

    ''' <summary>
    ''' saves a project to a file
    ''' </summary>
    ''' <param name="SaveAs"></param>
    Public Sub Save(Optional SaveAs As String = "")
        If Not String.IsNullOrEmpty(SaveAs) Then FileName = SaveAs
        If Not String.IsNullOrEmpty(FileName) Then
            SaveFile(FileName)
            DirtyFlag = False

            For Each x As Page In Pages
                x.TheFileWasSaved()
            Next
        End If
    End Sub

    ''' <summary>
    ''' saves a temporary copy to a file
    ''' </summary>
    Public Sub SaveBackupTemp()
        If String.IsNullOrEmpty(FileName) Then
            If String.IsNullOrEmpty(_Tempname) Then _Tempname = "2Draw " & Now.ToString("yyyyMMddHHmm") & FILE_EXTENTION
            SaveFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & _Tempname)
        Else
            Dim fl As New FileInfo(FileName)
            SaveFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & fl.Name)
        End If
    End Sub

    ''' <summary>
    ''' checks for the backup file, if it is newer than the project file
    ''' will return the path. (possible crash or forgot to save when closing)
    ''' </summary>
    ''' <returns></returns>
    Public Function DoesBackupExistAndisNewer() As String
        Dim ret As String = ""
        If Not String.IsNullOrEmpty(FileName) Then
            Dim fl As New FileInfo(FileName)
            Dim xfl As New FileInfo(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & fl.Name)
            If fl.Exists AndAlso xfl.Exists AndAlso fl.FullName <> xfl.FullName Then
                If fl.LastWriteTime < xfl.LastWriteTime Then
                    ret = xfl.FullName
                End If
            End If
        End If

        Return ret
    End Function


    Public Function GetFileName() As String
        If String.IsNullOrEmpty(FileName) Then
            Return My.Computer.FileSystem.SpecialDirectories.Temp & $"\2Draw-{Now.ToString("yyyyMMddHHmm")}{FILE_EXTENTION}"
        Else
            Return FileName
        End If
    End Function


    ''' <summary>
    ''' does the actual saveing of the file
    ''' </summary>
    ''' <param name="Fullname"></param>
    Private Sub SaveFile(Fullname As String)
        If File.Exists(Fullname) Then
            File.Delete(Fullname)
        End If

        Dim z As ZipPackage = ZipPackage.Open(Fullname, FileMode.Create)
        Dim headerURI As New Uri("/Header", UriKind.Relative)
        Dim prt As PackagePart = z.CreatePart(headerURI, Net.Mime.MediaTypeNames.Text.Plain, CompressionOption.Normal)


        Using wrh As New StreamWriter(prt.GetStream)
            '===wrh is the stream for the header ===
            Dim u As New URIS("Projectinfo", "Projectinfo", URIS.typ.pro)
            wrh.WriteLine(u.ToString)

            'add the project file
            Dim nprt As PackagePart = z.CreatePart(u.URI, System.Net.Mime.MediaTypeNames.Text.Xml, CompressionOption.Normal)
            Using binwri As New StreamWriter(nprt.GetStream)
                _ProjectInfo.Save(binwri)
            End Using


            '-------------------------------------
            'add the page files
            For i As Integer = 0 To Pages.Count - 1
                u = New URIS(Pages(i).Name, Pages(i).Name, URIS.typ.xml)
                wrh.WriteLine(u.ToString)

                'save the xml file to the zip
                nprt = z.CreatePart(u.URI, System.Net.Mime.MediaTypeNames.Text.Xml, CompressionOption.Normal)
                Using binwri As New StreamWriter(nprt.GetStream)
                    Pages(i).Save()
                    Pages(i).Doc.Save(binwri)
                End Using
            Next

            '-------------------------------------
            'add the image files
            For i As Integer = 0 To Me.Images.Count - 1
                u = New URIS(Images(i).name, Images(i).name, URIS.typ.pic)
                wrh.WriteLine(u.ToString)

                'save the image file to the zip
                nprt = z.CreatePart(u.URI, System.Net.Mime.MediaTypeNames.Image.Jpeg)
                Using binwri As Stream = nprt.GetStream
                    Images(i).Img.Save(binwri, ImageFormat.Png)
                End Using
            Next
        End Using

        z.Close()

    End Sub

    Private Sub SomethingChanged(sender As Object, e As EventArgs)
        Me.DirtyFlag = True
    End Sub

#Region "Adding or removing of resouces"

    ''' <summary>
    ''' the correct way to add a page
    ''' </summary>
    ''' <param name="name">the name of the page</param>
    ''' <param name="Landscape">if it will be in landscape or portrate</param>
    ''' <param name="PS_size">must be one of the pageSizes.PS_ constants</param>
    ''' <returns></returns>
    Public Function AddPage(name As String, PS_size As String, Landscape As Boolean) As Page
        Dim p As Page = Page.CreatePage(name, PS_size, Landscape)
        Pages.Add(p)
        AddHandler p.FileChanged, AddressOf SomethingChanged
        SomethingChanged(Me, New EventArgs)
        Return p
    End Function

    ''' <summary>
    ''' the correct was to remove a page
    ''' </summary>
    ''' <param name="p"></param>
    Public Sub RemovePage(p As Page)
        If p.CleanTab IsNot Nothing Then p.CleanTab.RemoveTab(p)
        Pages.Remove(p)
        RemoveHandler p.FileChanged, AddressOf SomethingChanged
        SomethingChanged(Me, New EventArgs)
    End Sub

    ''' <summary>
    ''' the correct way to add an image
    ''' </summary>
    ''' <param name="FullFileName"></param>
    ''' <returns>returns nothing if file is not found</returns>
    Public Function AddImage(FullFileName As String) As XPic
        Dim f As New FileInfo(FullFileName)
        If f.Exists Then
            Dim p As Image = Image.FromFile(FullFileName)
            Dim x As New XPic(p, f.Name.Replace(f.Extension, ""))
            Images.Add(x)
            SomethingChanged(Me, New EventArgs)
            Return x
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' the correct way to add an image
    ''' </summary>
    Public Sub AddImage(pic As XPic)
        Dim fnd As Boolean
        For i As Integer = 0 To Images.Count - 1
            If Images(i).name = pic.name Then
                fnd = True
                Exit For
            End If
        Next
        If Not fnd Then
            Images.Add(pic)
            SomethingChanged(Me, New EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' correct was to remove an image
    ''' </summary>
    ''' <param name="pic"></param>
    Public Sub RemoveImage(pic As XPic)
        Images.Remove(pic)
        SomethingChanged(Me, New EventArgs)
    End Sub

    ''' <summary>
    ''' returns a picture by name (usually used when drawing)
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Public Function GetPicByName(name As String) As XPic
        Dim ret As XPic = Nothing
        For Each p As XPic In Images
            If p.name = name Then
                ret = p
                Exit For
            End If
        Next
        Return ret
    End Function

#End Region

    Private Structure URIS
        Public name As String
        Public URI As Uri
        Public filetype As typ

        Public Sub New(n As String, u As String, t As typ)
            name = n
            u = u.Replace(" ", "_")
            URI = New Uri("/" & u, UriKind.Relative)
            filetype = t
        End Sub

        Public Enum typ
            xml
            pic
            pro
        End Enum

        Public Function isValid() As Boolean
            Return URI IsNot Nothing
        End Function

        Public Shared Function fromString(str As String) As URIS
            Dim p() As String = str.Split("|")
            Dim u As New URIS

            Try
                u.filetype = [Enum].Parse(GetType(typ), p(0))
                u.name = p(1)
                u.URI = New Uri(p(2), UriKind.Relative)
            Catch ex As Exception
            End Try

            Return u
        End Function

        Public Overrides Function ToString() As String
            Return $"{filetype.ToString}|{name}|{URI.ToString}"
        End Function

    End Structure


    Public Class XPic
        Public Sub New()
        End Sub

        Public Sub New(d As Bitmap, n As String)
            Img = d
            name = n
        End Sub

        Friend Function clone() As XPic
            Dim x As New XPic
            x.name = name
            x.Img = Img.Clone
            Return x
        End Function

        Public Img As Bitmap
        ''' <summary>
        ''' this should be the name of the image without path and extention
        ''' </summary>
        Public name As String
    End Class




End Class
