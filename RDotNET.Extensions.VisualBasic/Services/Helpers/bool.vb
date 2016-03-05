Imports RDotNET.Extensions.VisualBasic

Public Structure bool
    Implements IScriptProvider

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
