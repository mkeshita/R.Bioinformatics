Imports RDotNET.Extensions.VisualBasic

Namespace Services.ScriptBuilder.RTypes

    Public Structure RBoolean : Implements IScriptProvider

        Public Shared ReadOnly Property [TRUE] As New RBoolean(Extensions.TRUE)
        Public Shared ReadOnly Property [FALSE] As New RBoolean(Extensions.FALSE)

        ReadOnly __value As String

        Sub New(value As String)
            __value = value
        End Sub

        Public Function RScript() As String Implements IScriptProvider.RScript
            Return __value
        End Function
    End Structure

    Public Structure RExpression : Implements IScriptProvider

        ReadOnly __value As String

        Sub New(R As String)
            __value = R
        End Sub

        Public Function RScript() As String Implements IScriptProvider.RScript
            Return __value
        End Function

        Public Overrides Function ToString() As String
            Return __value
        End Function

        Public Shared Widening Operator CType(R As String) As RExpression
            Return New RExpression(R)
        End Operator
    End Structure
End Namespace