Imports Microsoft.VisualBasic.DocumentFormat.Csv.StorageProvider.Reflection
Imports RDotNET.Extensions.VisualBasic
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

''' <summary>
''' 一个提供脚本语句的最基本的抽象对象
''' </summary>
''' <remarks>就只通过一个函数来提供脚本执行语句</remarks>
Public MustInherit Class IRProvider
    Implements IScriptProvider

    Dim __requires As String()

    ''' <summary>
    ''' The package names that required of this script file.
    ''' (需要加载的R的包的列表)
    ''' </summary>
    ''' <returns></returns>
    <Ignored> Public Overridable Property Requires As String()
        Get
            Return __requires
        End Get
        Protected Set(value As String())
            __requires = value
        End Set
    End Property

    ''' <summary>
    ''' Get R Script text from this R script object build model.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride Function RScript() As String Implements IScriptProvider.RScript

    Public Overrides Function ToString() As String
        Return RScript()
    End Function

    Public Shared Narrowing Operator CType(R As IRProvider) As String
        Return R.RScript
    End Operator
End Class

''' <summary>
''' R之中的单步函数调用
''' </summary>
Public Class IRToken : Inherits IRProvider
    Implements IScriptProvider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns>由于这个对象只是对一个表达式的抽象，最常用的是对一个函数调用的抽象，所以library在这里不可以自动添加，需要自己在后面手工添加</returns>
    Public Overrides Function RScript() As String
        Return Me.GetScript(Me.GetType)
    End Function

    Public Overloads Shared Narrowing Operator CType(token As IRToken) As String
        Return token.RScript
    End Operator

    Public Shared Operator &(token As IRToken, script As String) As String
        Return token.RScript & script
    End Operator

    Public Shared Operator &(script As String, token As IRToken) As String
        Return script & token.RScript
    End Operator
End Class

Public Interface IScriptProvider
    Function RScript() As String
End Interface