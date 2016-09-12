Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic.Serialization.JSON

''' <summary>
''' The R runtime variable
''' </summary>
''' 
Public Class var

    Public ReadOnly Property Name As String
    Public ReadOnly Property type As String
        Get
            Return $"typeof({Name})".丶.AsCharacter.ToArray.FirstOrDefault
        End Get
    End Property

    <ScriptIgnore>
    Public ReadOnly Property RValue As SymbolicExpression
        Get
            Return Name.丶
        End Get
    End Property

    Dim _expr As String

    Public Property Expression As String
        Get
            Return _expr
        End Get
        Set(value As String)
            _expr = value
            Call __setValue()
        End Set
    End Property

    Private Sub __setValue()
        Call $"{Name} <- {_expr}".丶
    End Sub

    Sub New()
        Name = App.NextTempName
    End Sub

    Sub New(expr As String)
        Call Me.New
        Me._expr = expr
        Call __setValue()
    End Sub

    Sub New(name As String, expr As String)
        Me.Name = name
        Me._expr = expr
        Call __setValue()
    End Sub

    ''' <summary>
    ''' <see cref="out"/>
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Public Function [As](Of T)() As T
        Throw New NotImplementedException
    End Function

    Public Overrides Function ToString() As String
        Return Me.GetJson
    End Function

    Public Shared Narrowing Operator CType(var As var) As String
        Return var.Name
    End Operator

    Public Shared Widening Operator CType(expr As String) As var
        Return New var(expr)
    End Operator
End Class
