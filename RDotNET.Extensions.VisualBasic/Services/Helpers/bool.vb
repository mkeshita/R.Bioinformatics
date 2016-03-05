Imports RDotNET.Extensions.VisualBasic

Public Structure bool : Implements IScriptProvider

    Public Shared ReadOnly Property [TRUE] As New bool(Extensions.TRUE)
    Public Shared ReadOnly Property [FALSE] As New bool(Extensions.FALSE)

    ReadOnly __value As String

    Sub New(value As String)
        __value = value
    End Sub

    Public Function RScript() As String Implements IScriptProvider.RScript
        Return __value
    End Function
End Structure


Public Structure RExpression

    ReadOnly __value As String

    Sub New(R As String)
        __value = R
    End Sub

    Public Overrides Function ToString() As String
        Return __value
    End Function

    Public Shared Widening Operator CType(R As String) As RExpression
        Return New RExpression(R)
    End Operator
End Structure