Public Module BuilderAPI

    Public Function GetScript(Of T)(token As T) As String
        Dim type As Type = GetType(T)
    End Function
End Module
