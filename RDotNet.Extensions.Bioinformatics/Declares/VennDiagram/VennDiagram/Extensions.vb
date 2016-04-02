Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic

Namespace VennDiagram.ModelAPI

    Public Module Extensions

        ''' <summary>
        ''' 尝试着从一个字符串集合中猜测出可能的名称
        ''' </summary>
        ''' <param name="Collection"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Extension> Public Function ParseName(Collection As Generic.IEnumerable(Of String), Serial As Integer) As String
            Dim LCollection = (From s As String In Collection.AsParallel Where Not String.IsNullOrEmpty(s) Select s Distinct).ToArray
            Dim Name As List(Of Char) = New List(Of Char)
            For i As Integer = 0 To (From s As String In LCollection.AsParallel Select Len(s) Distinct).Max - 1
                Dim p As Integer = i
                Dim Query = From s As String In LCollection.AsParallel Select s(p) Distinct  '
                Dim Result = Query.ToArray
                If Result.Count = 1 Then
                    Call Name.Add(Result.First)
                Else
                    Exit For
                End If
            Next

            If Name.Count > 0 Then
                Return Name.ToArray
            Else
                Return "Serial_" & Serial
            End If
        End Function
    End Module
End Namespace