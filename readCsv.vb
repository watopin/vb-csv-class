''' <summary>
''' a class handling csv file
''' </summary>
''' <remarks></remarks>
Public Class Csv
    Dim filepath As String
    Dim encoding As Encoding
    Dim headerRow As Boolean

    ''' <summary>
    ''' ctor
    ''' </summary>
    ''' <param name="filepath">path of a target csv file</param>
    ''' <remarks>fixed encoding. shift-jis</remarks>
    Public Sub New(ByVal filepath As String)
        Me.filepath = filepath
        headerRow = True
        encoding = encoding.GetEncoding(932)
    End Sub

    ''' <summary>
    ''' making a list of string arrays
    ''' each rows in the csv -> string array
    ''' </summary>
    ''' <returns>list of string arrays</returns>
    ''' <remarks>if headerRow=true, delete the first row(remove the first element of list)</remarks>
    Function toList() As List(Of String())
        Dim reader As New TextFieldParser(filepath, encoding)
        reader.TextFieldType = FieldType.Delimited
        reader.SetDelimiters(",")
        Dim datalist As New List(Of String())
        While Not reader.EndOfData
            Dim currentrow As String()
            currentrow = reader.ReadFields
            datalist.Add(currentrow)
        End While
        If headerRow Then
            datalist.RemoveAt(0)
            Return datalist
        Else
            Return datalist
        End If
    End Function

    ''' <summary>
    ''' you can specify needed columns
    ''' </summary>
    ''' <param name="filter">integer array. index starts from 0.</param>
    ''' <returns>list of string arrays</returns>
    ''' <remarks>overload</remarks>
    Function toList(ByVal filter As Integer()) As List(Of String())
        Dim datalist As List(Of String()) = toList()
        Dim filteredList As New List(Of String())
        For Each Item As String() In datalist
            Dim newlist As New List(Of String)
            For Each index As Integer In filter
                newlist.Add(Item(index))
            Next
            filteredList.Add(newlist.ToArray)
        Next
        Return filteredList
    End Function
End Class
