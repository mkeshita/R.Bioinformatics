Public Module BuilderAPI

    Const IsNotAFunc = "Target object is not a R function abstract!"

    Public Function GetScript(Of T)(token As T) As String
        Dim type As Type = GetType(T)
        Dim name As RFunc = type.GetAttribute(Of RFunc)

        If name Is Nothing Then
            Dim ex As New Exception(IsNotAFunc)
            ex = New Exception(type.FullName, ex)
            Throw ex
        End If

        Dim props = 
    End Function
End Module
