Namespace API.grDevices

    Public Module dev

        ''' <summary>
        ''' ``dev.off`` returns the number and name of the new active device (after the specified device has been shut down).
        ''' </summary>
        ''' <param name="which"></param>
        ''' <returns></returns>
        Public Function off(Optional which As String = "dev.cur()") As Integer
            Call $"dev.off(which={which})".ζ
        End Function
    End Module
End Namespace