Imports System.Runtime.InteropServices
Imports System.Text

Namespace RDotNet.Internals.Windows
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    <MarshalAs(UnmanagedType.Bool)>
    Friend Delegate Function blah1(
    <[In]>
    <MarshalAs(UnmanagedType.LPStr)> ByVal prompt As String,
    <MarshalAs(UnmanagedType.LPStr)> ByVal buffer As StringBuilder, ByVal length As Integer,
    <MarshalAs(UnmanagedType.Bool)> ByVal history As Boolean) As Boolean
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    Friend Delegate Sub blah2(
    <[In]>
    <MarshalAs(UnmanagedType.LPStr)> ByVal buffer As String, ByVal length As Integer)
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    Friend Delegate Sub blah3()
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    Friend Delegate Sub blah4(
    <[In]>
    <MarshalAs(UnmanagedType.LPStr)> ByVal message As String)
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    Friend Delegate Function blah5(
    <[In]>
    <MarshalAs(UnmanagedType.LPStr)> ByVal question As String) As YesNoCancel
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    Friend Delegate Sub blah6(ByVal which As BusyType)
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
    Friend Delegate Sub blah7(
    <[In]>
    <MarshalAs(UnmanagedType.LPStr)> ByVal buffer As String, ByVal length As Integer, ByVal outputType As ConsoleOutputType)
End Namespace
