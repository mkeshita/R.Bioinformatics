Imports RDotNet.Internals
Imports System

Namespace Devices
    ''' <summary>
    ''' A sink with (almost) no effect, similar in purpose to /dev/null
    ''' </summary>
    Public Class NullCharacterDevice
        Implements ICharacterDevice
#Region "ICharacterDevice Members"

        ''' <summary>
        ''' Read input from console.
        ''' </summary>
        ''' <paramname="prompt">The prompt message.</param>
        ''' <paramname="capacity">The buffer's capacity in byte.</param>
        ''' <paramname="history">Whether the input should be added to any command history.</param>
        ''' <returns>A null reference</returns>
        Public Function ReadConsole(ByVal prompt As String, ByVal capacity As Integer, ByVal history As Boolean) As String Implements ICharacterDevice.ReadConsole
            Return Nothing
        End Function

        ''' <summary>
        ''' This implementation has no effect
        ''' </summary>
        ''' <paramname="output">The output message</param>
        ''' <paramname="length">The output's length in byte.</param>
        ''' <paramname="outputType">The output type.</param>
        Public Sub WriteConsole(ByVal output As String, ByVal length As Integer, ByVal outputType As ConsoleOutputType) Implements ICharacterDevice.WriteConsole
        End Sub

        ''' <summary>
        ''' This implementation has no effect
        ''' </summary>
        ''' <paramname="message">The message.</param>
        Public Sub ShowMessage(ByVal message As String) Implements ICharacterDevice.ShowMessage
        End Sub

        ''' <summary>
        ''' This implementation has no effect
        ''' </summary>
        ''' <paramname="which"></param>
        Public Sub Busy(ByVal which As BusyType) Implements ICharacterDevice.Busy
        End Sub

        ''' <summary>
        ''' This implementation has no effect
        ''' </summary>
        Public Sub Callback() Implements ICharacterDevice.Callback
        End Sub

        ''' <summary>
        ''' Always return the default value of the YesNoCancel enum (yes?)
        ''' </summary>
        ''' <paramname="question"></param>
        ''' <returns></returns>
        Public Function Ask(ByVal question As String) As YesNoCancel Implements ICharacterDevice.Ask
            Return Nothing
        End Function

        ''' <summary>
        ''' Ignores the message, but triggers a CleanUp, a termination with no action.
        ''' </summary>
        ''' <paramname="message"></param>
        Public Sub Suicide(ByVal message As String) Implements ICharacterDevice.Suicide
            CleanUp(StartupSaveAction.Suicide, 2, False)
        End Sub

        ''' <summary>
        ''' This implementation has no effect
        ''' </summary>
        Public Sub ResetConsole() Implements ICharacterDevice.ResetConsole
        End Sub

        ''' <summary>
        ''' This implementation has no effect
        ''' </summary>
        Public Sub FlushConsole() Implements ICharacterDevice.FlushConsole
        End Sub

        ''' <summary>
        ''' This implementation has no effect
        ''' </summary>
        Public Sub ClearErrorConsole() Implements ICharacterDevice.ClearErrorConsole
        End Sub

        ''' <summary>
        ''' Clean up action; exit the process with a specified status
        ''' </summary>
        ''' <paramname="saveAction">Ignored</param>
        ''' <paramname="status"></param>
        ''' <paramname="runLast">Ignored</param>
        Public Sub CleanUp(ByVal saveAction As StartupSaveAction, ByVal status As Integer, ByVal runLast As Boolean) Implements ICharacterDevice.CleanUp
            Environment.Exit(status)
        End Sub

        ''' <summary>
        ''' Always returns false, no other side effect
        ''' </summary>
        ''' <paramname="files"></param>
        ''' <paramname="headers"></param>
        ''' <paramname="title"></param>
        ''' <paramname="delete"></param>
        ''' <paramname="pager"></param>
        ''' <returns>Returns false</returns>
        Public Function ShowFiles(ByVal files As String(), ByVal headers As String(), ByVal title As String, ByVal delete As Boolean, ByVal pager As String) As Boolean Implements ICharacterDevice.ShowFiles
            Return False
        End Function

        ''' <summary>
        ''' Always returns null; no other side effect
        ''' </summary>
        ''' <paramname="create">ignored</param>
        ''' <returns>null</returns>
        Public Function ChooseFile(ByVal create As Boolean) As String Implements ICharacterDevice.ChooseFile
            Return Nothing
        End Function

        ''' <summary>
        ''' No effect
        ''' </summary>
        ''' <paramname="file"></param>
        Public Sub EditFile(ByVal file As String) Implements ICharacterDevice.EditFile
        End Sub

        ''' <summary>
        ''' Return the NULL SEXP; no other effect
        ''' </summary>
        ''' <paramname="call"></param>
        ''' <paramname="operation"></param>
        ''' <paramname="args"></param>
        ''' <paramname="environment"></param>
        ''' <returns></returns>
        Public Function LoadHistory(ByVal [call] As Language, ByVal operation As SymbolicExpression, ByVal args As Pairlist, ByVal environment As REnvironment) As SymbolicExpression Implements ICharacterDevice.LoadHistory
            Return environment.Engine.NilValue
        End Function

        ''' <summary>
        ''' Return the NULL SEXP; no other effect
        ''' </summary>
        ''' <paramname="call"></param>
        ''' <paramname="operation"></param>
        ''' <paramname="args"></param>
        ''' <paramname="environment"></param>
        ''' <returns></returns>
        Public Function SaveHistory(ByVal [call] As Language, ByVal operation As SymbolicExpression, ByVal args As Pairlist, ByVal environment As REnvironment) As SymbolicExpression Implements ICharacterDevice.SaveHistory
            Return environment.Engine.NilValue
        End Function

        ''' <summary>
        ''' Return the NULL SEXP; no other effect
        ''' </summary>
        ''' <paramname="call"></param>
        ''' <paramname="operation"></param>
        ''' <paramname="args"></param>
        ''' <paramname="environment"></param>
        ''' <returns></returns>
        Public Function AddHistory(ByVal [call] As Language, ByVal operation As SymbolicExpression, ByVal args As Pairlist, ByVal environment As REnvironment) As SymbolicExpression Implements ICharacterDevice.AddHistory
            Return environment.Engine.NilValue
        End Function

#End Region
    End Class
End Namespace
