Imports RDotNet.Internals
Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Security.Permissions

Namespace RDotNet
    ''' <summary>
    ''' A collection of real numbers in double precision.
    ''' </summary>
    <SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
    Public Class NumericVector
        Inherits Vector(Of Double)
        ''' <summary>
        ''' Creates a new empty NumericVector with the specified length.
        ''' </summary>
        ''' <paramname="engine">The <seecref="REngine"/> handling this instance.</param>
        ''' <paramname="length">The length.</param>
        ''' <seealsocref="REngineExtension.CreateNumericVector(REngine,Integer)"/>
        Public Sub New(ByVal engine As REngine, ByVal length As Integer)
            MyBase.New(engine, SymbolicExpressionType.NumericVector, length)
        End Sub

        ''' <summary>
        ''' Creates a new NumericVector with the specified values.
        ''' </summary>
        ''' <paramname="engine">The <seecref="REngine"/> handling this instance.</param>
        ''' <paramname="vector">The values.</param>
        ''' <seealsocref="REngineExtension.CreateNumericVector(REngine,IEnumerable(OfDouble))"/>
        Public Sub New(ByVal engine As REngine, ByVal vector As IEnumerable(Of Double))
            MyBase.New(engine, SymbolicExpressionType.NumericVector, vector)
        End Sub

        ''' <summary>
        ''' Creates a new NumericVector with the specified values.
        ''' </summary>
        ''' <paramname="engine">The <seecref="REngine"/> handling this instance.</param>
        ''' <paramname="vector">The values.</param>
        ''' <seealsocref="REngineExtension.CreateNumericVector(REngine,IEnumerable(OfDouble))"/>
        Public Sub New(ByVal engine As REngine, ByVal vector As Double())
            MyBase.New(engine, SymbolicExpressionType.NumericVector, vector.Length)
            Marshal.Copy(vector, 0, DataPointer, vector.Length)
        End Sub

        ''' <summary>
        ''' Creates a new instance for a numeric vector.
        ''' </summary>
        ''' <paramname="engine">The <seecref="REngine"/> handling this instance.</param>
        ''' <paramname="coerced">The pointer to a numeric vector.</param>
        Protected Friend Sub New(ByVal engine As REngine, ByVal coerced As IntPtr)
            MyBase.New(engine, coerced)
        End Sub

        ''' <summary>
        ''' Gets the element at the specified index.
        ''' </summary>
        ''' <remarks>Used for pre-R 3.5 </remarks>
        ''' <paramname="index">The zero-based index of the element to get.</param>
        ''' <returns>The element at the specified index.</returns>
        Protected Overrides Function GetValue(ByVal index As Integer) As Double
            Dim data = New Double(0) {}
            Dim offset = GetOffset(index)
            Dim pointer = IntPtr.Add(DataPointer, offset)
            Marshal.Copy(pointer, data, 0, data.Length)
            Return data(0)
        End Function

        ''' <summary>
        ''' Gets the element at the specified index.
        ''' </summary>
        ''' <remarks>Used for R 3.5 and higher, to account for ALTREP objects</remarks>
        ''' <paramname="index">The zero-based index of the element to get.</param>
        ''' <returns>The element at the specified index.</returns>
        Protected Overrides Function GetValueAltRep(ByVal index As Integer) As Double
            Return GetFunction(Of REAL_ELT)()(DangerousGetHandle(), CType(index, IntPtr))
        End Function

        ''' <summary>
        ''' Sets the element at the specified index.
        ''' </summary>
        ''' <remarks>Used for pre-R 3.5 </remarks>
        ''' <paramname="index">The zero-based index of the element to set.</param>
        ''' <paramname="value">The value to set</param>
        Protected Overrides Sub SetValue(ByVal index As Integer, ByVal value As Double)
            Dim data = {value}
            Dim offset = GetOffset(index)
            Dim pointer = IntPtr.Add(DataPointer, offset)
            Marshal.Copy(data, 0, pointer, data.Length)
        End Sub

        ''' <summary>
        ''' Sets the element at the specified index.
        ''' </summary>
        ''' <remarks>Used for R 3.5 and higher, to account for ALTREP objects</remarks>
        ''' <paramname="index">The zero-based index of the element to set.</param>
        ''' <paramname="value">The value to set</param>
        Protected Overrides Sub SetValueAltRep(ByVal index As Integer, ByVal value As Double)
            GetFunction(Of SET_REAL_ELT)()(DangerousGetHandle(), CType(index, IntPtr), value)
        End Sub

        ''' <summary>
        ''' Efficient conversion from R vector representation to the array equivalent in the CLR
        ''' </summary>
        ''' <returns>Array equivalent</returns>
        Protected Overrides Function GetArrayFast() As Double()
            Dim res = New Double(Length - 1) {}
            Marshal.Copy(DataPointer, res, 0, res.Length)
            Return res
        End Function

        ''' <summary> Gets alternate rep array.</summary>
        '''
        ''' <exceptioncref="NotSupportedException"> Thrown when the requested operation is not supported.</exception>
        '''
        ''' <returns> An array of t.</returns>
        Public Overrides Function GetAltRepArray() As Double()
            ' by inference from `static SEXP compact_intseq_Duplicate(SEXP x, Rboolean deep)`  in altrep.c
            Dim res = New Double(Length - 1) {}
            GetFunction(Of REAL_GET_REGION)()(DangerousGetHandle(), CType(0, IntPtr), CType(Length, IntPtr), res)
            Return res
        End Function


        ''' <summary>
        ''' Efficient initialisation of R vector values from an array representation in the CLR
        ''' </summary>
        Protected Overrides Sub SetVectorDirect(ByVal values As Double())
            Dim pointer = IntPtr.Add(DataPointer, 0)
            Marshal.Copy(values, 0, pointer, values.Length)
        End Sub

        ''' <summary>
        ''' Gets the size of a real number in byte.
        ''' </summary>
        Protected Overrides ReadOnly Property DataSize As Integer
            Get
                Return
            End Get
        End Property

        ''' <summary>
        ''' Copies the elements to the specified array.
        ''' </summary>
        ''' <paramname="destination">The destination array.</param>
        ''' <paramname="length">The length to copy.</param>
        ''' <paramname="sourceIndex">The first index of the vector.</param>
        ''' <paramname="destinationIndex">The first index of the destination array.</param>
        Public Overloads Sub CopyTo(ByVal destination As Double(), ByVal length As Integer, ByVal Optional sourceIndex As Integer = 0, ByVal Optional destinationIndex As Integer = 0)
            If destination Is Nothing Then
                Throw New ArgumentNullException("destination")
            End If

            If length < 0 Then
                Throw New IndexOutOfRangeException("length")
            End If

            If sourceIndex < 0 OrElse MyBase.Length < sourceIndex + length Then
                Throw New IndexOutOfRangeException("sourceIndex")
            End If

            If destinationIndex < 0 OrElse destination.Length < destinationIndex + length Then
                Throw New IndexOutOfRangeException("destinationIndex")
            End If

            Dim offset = GetOffset(sourceIndex)
            Dim pointer = IntPtr.Add(DataPointer, offset)
            Marshal.Copy(pointer, destination, destinationIndex, length)
        End Sub
    End Class
End Namespace
