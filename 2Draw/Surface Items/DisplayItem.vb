
Imports _2Draw

Public Enum ItemType
    ''' <summary>
    ''' if this is a line
    ''' </summary>
    Line
    ''' <summary>
    ''' general shape
    ''' </summary>
    Shape
    ''' <summary>
    ''' text on display
    ''' </summary>
    Text
    ''' <summary>
    ''' this is a compound item with wires, shapes and text possible
    ''' </summary>
    Symbol
    ''' <summary>
    ''' just an image on the page
    ''' </summary>
    Image
End Enum

Public Class DisplayItem

    ''' <summary>
    ''' creates a new unique ID based off a time stamp
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CreateUID() As String
        Return System.Guid.NewGuid.ToString
    End Function

    ''' <summary>
    ''' creates a type_2d item based off of the "type" attribute in the xmlnode
    ''' </summary>
    ''' <param name="nd"></param>
    ''' <returns>nothing if not valid</returns>
    Public Shared Function CreateType(nd As XmlNode) As Type_2D
        Dim ret As Type_2D = Nothing

        Dim e As ItemType = [Enum].Parse(GetType(ItemType), XHelper.Get(nd, "type", ItemType.Line.ToString))
        Select Case e
            Case ItemType.Line
                ret = New Line_2D(nd)
            Case ItemType.Shape
                ret = New Shape_2D(nd)
            Case ItemType.Text
                ret = New Text_2D(nd)
            Case ItemType.Symbol
                ret = New Symbol_2D(nd)
            Case ItemType.Image
                ret = New Image_2D(nd)
        End Select
        Return ret
    End Function

    ''' <summary>
    ''' saves an item back to the xml
    ''' </summary>
    ''' <param name="ownernode"></param>
    ''' <param name="item"></param>
    Public Shared Function SaveAppendNode(ownernode As XmlNode, item As Type_2D) As XmlNode
        If item IsNot Nothing Then
            Dim n As XmlNode = XHelper.NodeAppendNew(ownernode, "Item")
            XHelper.Set(n, "type", item.ItemType.ToString)
            item.Save(n)
            Return n
        Else
            Return Nothing
        End If
    End Function




End Class

Public Structure GraphicsOpts
    Public InverseZoom As Single
    Public ZoomLevel As Single
    Public ScrollOffset As PointF
End Structure


Public Interface Type_2D

    ''' <summary>
    ''' what type of item this is
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ItemType As ItemType

    ''' <summary>
    ''' if this item is selected
    ''' </summary>
    ''' <returns></returns>
    Property Selected As Boolean

    ''' <summary>
    ''' how this item shoulc be drawn
    ''' </summary>
    ''' <returns></returns>
    Property DrawingOpts As drawingOpts

    ''' <summary>
    ''' forces a draw event
    ''' </summary>
    ''' <param name="G"></param>
    ''' <param name="go">graphics options</param>
    Sub Draw(G As Graphics, GO As GraphicsOpts)

    ''' <summary>
    ''' if this item exists on this point
    ''' </summary>
    ''' <param name="pt"></param>
    ''' <returns></returns>
    Function HitTest(pt As PointF, zoomScale As Single) As Boolean

    ''' <summary>
    ''' when saving back to file
    ''' </summary>
    ''' <param name="useNode"></param>
    Sub Save(useNode As XmlNode)

    ''' <summary>
    ''' unique identifier
    ''' </summary>
    ''' <returns></returns>
    Property UID() As String

    ''' <summary>
    ''' gets the bounding rectangle
    ''' </summary>
    ''' <returns></returns>
    Function GetRecf() As RectangleF

    ''' <summary>
    ''' for rotating the item
    ''' </summary>
    ''' <param name="center"></param>
    ''' <param name="angle"></param>
    Sub Rotate(center As PointF, angle As Double)

    ''' <summary>
    ''' for moving the item
    ''' </summary>
    ''' <param name="pointoffset"></param>
    Sub ApplyOffset(pointoffset As PointF)

    ''' <summary>
    ''' tells the object to resize to this  new size on the bounding rectangle
    ''' </summary>
    ''' <param name="newBoundingRecSize"></param>
    Sub Resize(newBoundingRecSize As RectangleF)

    Sub FlipVert(BoundingRec As RectangleF)

    Sub FlipHorz(BoundingRec As RectangleF)


    Function Clone() As Type_2D


End Interface












