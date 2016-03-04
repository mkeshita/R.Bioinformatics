Public Module RBase

    ''' <summary>
    ''' Warnings and its print method print the variable last.warning in a pleasing form.
    ''' </summary>
    ''' <returns></returns>
    Public Function Warnings() As String()
        Dim out = RSystem.REngine.WriteLine("warnings()")
        Return out
    End Function
End Module
