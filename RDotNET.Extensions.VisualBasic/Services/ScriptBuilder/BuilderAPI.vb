Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Linq
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace Services.ScriptBuilder

    Public Module BuilderAPI

        Const IsNotAFunc = "Target object is not a R function abstract!"

        ''' <summary>
        ''' R.func(param="",...)
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="token"></param>
        ''' <returns></returns>
        ''' 
        <Extension>
        Public Function GetScript(Of T)(token As T) As String
            Dim type As Type = GetType(T)
            Dim name As RFunc = type.GetAttribute(Of RFunc)

            If name Is Nothing Then
                Dim ex As New Exception(IsNotAFunc)
                ex = New Exception(type.FullName, ex)
                Throw ex
            End If

            Dim props = (From prop As PropertyInfo In type.GetProperties
                         Where prop.CanRead
                         Let param As Parameter = prop.GetAttribute(Of Parameter)
                         Select prop,
                             func = prop.__getName(param),
                             param.__isOptional,
                             param
                         Order By __isOptional Ascending)
            Dim parameters As String() =
                props.ToArray(Function(x) __getExpr(token, x.prop, x.func, x.param))
            Dim script As String = $"{name}({String.Join(", " & vbCrLf, parameters)})"
            Return script
        End Function

        ''' <summary>
        ''' name=value
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="prop"></param>
        ''' <param name="name"></param>
        ''' <returns></returns>
        Private Function __getExpr(x As Object, prop As PropertyInfo, name As String, param As Parameter) As String
            Dim value As Object = prop.GetValue(x)
            Dim type = If(param Is Nothing, ValueTypes.String, param.Type)
            Return $"{name}={prop.PropertyType.__getValue(value, type)}"
        End Function

        <Extension>
        Private Function __getValue(type As Type, value As Object, valueType As ValueTypes) As String
            Select Case type
                Case GetType(String)
                    If valueType = ValueTypes.Path Then
                        Return Rstring(Scripting.ToString(value).UnixPath)
                    Else
                        Return Rstring(Scripting.ToString(value))
                    End If
                Case GetType(Boolean)
                    If True = DirectCast(value, Boolean) Then
                        Return RBoolean.TRUE.__value
                    Else
                        Return RBoolean.FALSE.__value
                    End If
                Case GetType(RExpression)
                    Return DirectCast(value, RExpression).__value
                Case Else
                    Return Scripting.ToString(value)
            End Select
        End Function

        <Extension>
        Private Function __isOptional(param As Parameter) As Boolean
            If param Is Nothing Then
                Return False
            Else
                Return param.Optional
            End If
        End Function

        <Extension>
        Private Function __getName(prop As PropertyInfo, param As Parameter) As String
            If param Is Nothing Then
                Return prop.Name
            Else
                Return param.Name
            End If
        End Function
    End Module
End Namespace