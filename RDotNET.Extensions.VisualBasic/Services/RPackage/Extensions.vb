Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Public Module Extensions

    Public Const NULL As String = "NULL"
    ''' <summary>
    ''' "NA" 字符串，而不是NA空值常量
    ''' </summary>
    Public ReadOnly Property NA As RExpression = New RExpression("NA")
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
        If String.IsNullOrEmpty(file) Then
            Return ""
        End If
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

    ''' <summary>
    ''' c(....)
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Public Function c(ParamArray x As Object()) As String
        Dim cx As String = String.Join(",", x.ToArray(Function(o) Scripting.ToString(o)))
        Return $"c({cx})"
    End Function

    Public Function getOption(verbose As String) As String
        Return $"getOption(""{verbose}"")"
    End Function

    Public Function Rstring(s As String) As String
        Return $"""{s}"""
    End Function

    Public Function par(x As String) As String
        Return $"par(""{x}"")"
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="x">是一个对象，不是字符串</param>
    ''' <returns></returns>
    Public Function median(x As String) As String
        Return $"media({x})"
    End Function
End Module
