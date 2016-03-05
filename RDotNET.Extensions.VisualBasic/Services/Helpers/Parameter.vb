Imports System.Data.Linq.Mapping
Imports System.Linq
Imports Microsoft.VisualBasic.Serialization

<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)>
Public Class Parameter : Inherits Attribute

    Public ReadOnly Property [Optional] As Boolean
    Public ReadOnly Property Name As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="opt">Is this parameter optional?</param>
    Sub New(name As String, Optional opt As Boolean = False)
        Me.Name = name
        Me.[Optional] = opt
    End Sub

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function
End Class

<AttributeUsage(AttributeTargets.Class Or AttributeTargets.Struct, AllowMultiple:=False, Inherited:=True)>
Public Class RFunc : Inherits Attribute

    Public ReadOnly Property Name As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="name">R function name</param>
    Sub New(name As String)
        Me.Name = name
    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Public Shared Narrowing Operator CType(rfunc As RFunc) As String
        Return rfunc.Name
    End Operator
End Class