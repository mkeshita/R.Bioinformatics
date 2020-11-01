Imports System
Imports System.Collections.Generic


    ''' <summary>
    ''' A built-in function.
    ''' </summary>
    Public Class BuiltinFunction
        Inherits [Function]
        ''' <summary>
        ''' Creates a built-in function proxy.
        ''' </summary>
        ''' <paramname="engine">The engine.</param>
        ''' <paramname="pointer">The pointer.</param>
        Protected Friend Sub New(ByVal engine As REngine, ByVal pointer As IntPtr)
            MyBase.New(engine, pointer)
        End Sub

        ''' <summary>
        ''' Invoke this builtin function, using an ordered list of unnamed arguments.
        ''' </summary>
        ''' <paramname="args">The arguments of the function</param>
        ''' <returns>The result of the evaluation</returns>
        Public Overrides Overloads Function Invoke(ParamArray args As SymbolicExpression()) As SymbolicExpression
            Return InvokeOrderedArguments(args)
        End Function

        ''' <summary>
        ''' NotSupportedException
        ''' </summary>
        ''' <paramname="args">key-value pairs</param>
        ''' <returns>Always throws an exception</returns>
        Public Overrides Overloads Function Invoke(ByVal args As IDictionary(Of String, SymbolicExpression)) As SymbolicExpression
            Throw New NotSupportedException()
        End Function
    End Class

