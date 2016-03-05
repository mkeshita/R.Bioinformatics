Imports System.Runtime.CompilerServices

Public Module Extensions

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="file"></param>
    ''' <param name="extendsFull">是否转换为全路径？默认不转换</param>
    ''' <returns></returns>
    <Extension>
    Public Function UnixPath(file As String, Optional extendsFull As Boolean = False) As String
        If extendsFull Then
            file = FileIO.FileSystem.GetFileInfo(file).FullName
        End If
        Return file.Replace("\"c, "/"c)
    End Function
End Module
