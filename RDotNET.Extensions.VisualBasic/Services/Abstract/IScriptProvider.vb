Imports RDotNET.Extensions.VisualBasic
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

''' <summary>
''' 一个提供脚本语句的最基本的抽象对象
''' </summary>
''' <remarks>就只通过一个函数来提供脚本执行语句</remarks>
Public MustInherit Class IRProvider
    Implements IScriptProvider

    ''' <summary>
    ''' Get R Script text from this R script object build model.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride Function RScript() As String Implements IScriptProvider.RScript

    Public Overrides Function ToString() As String
        Return RScript()
    End Function
End Class

''' <summary>
''' R之中的单步函数调用
''' </summary>
Public Class IRToken : Inherits IRProvider
    Implements IScriptProvider

    Public Overrides Function RScript() As String
        Return Me.GetScript([GetType])
    End Function

    Public Shared Narrowing Operator CType(token As IRToken) As String
        Return token.RScript
    End Operator

    Public Shared Operator &(token As IRToken, script As String) As String
        Return token.RScript & script
    End Operator

    Public Shared Operator &(script As String, token As IRToken) As String
        Return token.RScript & script
    End Operator
End Class

Public Interface IScriptProvider
    Function RScript() As String
End Interface