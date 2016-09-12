﻿#Region "Microsoft.VisualBasic::e5f0f7342bf6b32a89aad60c4c6e7deb, ..\R.Bioconductor\RDotNET\NativeLibrary\IDynamicLibraryLoader.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    '       xie (genetics@smrucc.org)
    ' 
    ' Copyright (c) 2016 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.

#End Region


Namespace NativeLibrary

    ''' <summary>
    ''' An interface definition to hide the platform specific aspects of library loading
    ''' </summary>
    ''' <remarks>There are probably projects 'out there' doing this already, but nothing 
    ''' is obvious from a quick search. Re-consider again if you need more features.</remarks>
    Public Interface IDynamicLibraryLoader
        ''' <summary>
        ''' Load a native library (DLL on windows, shared libraries on Linux and MacOS)
        ''' </summary>
        ''' <param name="filename">The file name (short file name) of the library to load, e.g. R.dll on Windows</param>
        ''' <returns></returns>
        Function LoadLibrary(filename As String) As IntPtr

        ''' <summary>
        ''' Gets the last error message from the native API used to load the library.
        ''' </summary>
        ''' <returns></returns>
        Function GetLastError() As String

        ''' <summary>
        ''' Unloads a library
        ''' </summary>
        ''' <param name="handle">The pointer to the entry point of the library</param>
        ''' <returns></returns>
        Function FreeLibrary(handle As IntPtr) As Boolean

        ''' <summary>
        ''' Gets a pointer to the address of a native function in the specified loaded library
        ''' </summary>
        ''' <param name="hModule">Handle of the module(library)</param>
        ''' <param name="lpProcName">The name of the function sought</param>
        ''' <returns>Handle to the native function</returns>
        Function GetFunctionAddress(hModule As IntPtr, lpProcName As String) As IntPtr
    End Interface

End Namespace
