Imports RDotNET.Extensions.VisualBasic
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder

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

Public Class IRToken : Inherits IRProvider
    Implements IScriptProvider

    Public Overrides Function RScript() As String
        Return Me.GetScript
    End Function
End Class

Public Interface IScriptProvider
    Function RScript() As String
End Interface