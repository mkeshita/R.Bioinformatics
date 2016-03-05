Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq

Public Module Extensions

    Public Const NA = Nothing

    ''' <summary>
    ''' "NA" 字符串，而不是NA空值常量
    ''' </summary>
    Public Const NAstrings As String = """NA"""
    Public Const [TRUE] As String = "TRUE"
    Public Const [FALSE] As String = "FALSE"

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

    ''' <summary>
    ''' c(....)
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Public Function c(ParamArray x As String()) As String
        Dim cx As String = String.Join(", ", x.ToArray(Function(s) $"""{s}"""))
        Return $"c({cx})"
    End Function

    Public Function Rstring(s As String) As String
        Return $"""{s}"""
    End Function
End Module
