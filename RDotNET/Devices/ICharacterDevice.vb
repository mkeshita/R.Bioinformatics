Imports RDotNet.Internals

Namespace RDotNet.Devices
    ''' <summary>
    ''' A console class handles user's inputs and outputs.
    ''' </summary>
    Public Interface ICharacterDevice
        ''' <summary>
        ''' Read input from console.
        ''' </summary>
        ''' <paramname="prompt">The prompt message.</param>
        ''' <paramname="capacity">The buffer's capacity in byte.</param>
        ''' <paramname="history">Whether the input should be added to any command history.</param>
        ''' <returns>The input.</returns>
        Function ReadConsole(ByVal prompt As String, ByVal capacity As Integer, ByVal history As Boolean) As String

        ''' <summary>
        ''' Write output on console.
        ''' </summary>
        ''' <paramname="output">The output message</param>
        ''' <paramname="length">The output's length in byte.</param>
        ''' <paramname="outputType">The output type.</param>
        Sub WriteConsole(ByVal output As String, ByVal length As Integer, ByVal outputType As ConsoleOutputType)

        ''' <summary>
        ''' Displays the message.
        ''' </summary>
        ''' <remarks>
        ''' It should be brought to the user's attention immediately.
        ''' </remarks>
        ''' <paramname="message">The message.</param>
        Sub ShowMessage(ByVal message As String)

        ''' <summary>
        ''' Invokes actions.
        ''' </summary>
        ''' <paramname="which">The state.</param>
        Sub Busy(ByVal which As BusyType)

        ''' <summary>
        ''' Callback function.
        ''' </summary>
        Sub Callback()

        ''' <summary>
        ''' Asks user's decision.
        ''' </summary>
        ''' <paramname="question">The question.</param>
        ''' <returns>User's decision.</returns>
        Function Ask(ByVal question As String) As YesNoCancel

        ''' <summary>
        ''' Abort R environment itself as soon as possible.
        ''' </summary>
        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        ''' <paramname="message">The message.</param>
        Sub Suicide(ByVal message As String)

        ''' <summary>
        ''' Clear the console.
        ''' </summary>
        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        Sub ResetConsole()

        ''' <summary>
        ''' Flush the console.
        ''' </summary>
        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        Sub FlushConsole()

        ''' <summary>
        ''' Clear the error console.
        ''' </summary>
        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        Sub ClearErrorConsole()

        ''' <summary>
        ''' Invokes any actions which occur at system termination.
        ''' </summary>
        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        ''' <paramname="saveAction">The save type.</param>
        ''' <paramname="status">Exit code.</param>
        ''' <paramname="runLast">Whether R should execute <code>.Last</code>.</param>
        Sub CleanUp(ByVal saveAction As StartupSaveAction, ByVal status As Integer, ByVal runLast As Boolean)

        ''' <summary>
        ''' Displays the contents of files.
        ''' </summary>
        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        ''' <paramname="files">The file paths.</param>
        ''' <paramname="headers">The header before the contents is printed.</param>
        ''' <paramname="title">The window title.</param>
        ''' <paramname="delete">Whether the file will be deleted.</param>
        ''' <paramname="pager">The pager used.</param>
        ''' <returns></returns>
        Function ShowFiles(ByVal files As String(), ByVal headers As String(), ByVal title As String, ByVal delete As Boolean, ByVal pager As String) As Boolean

        ''' <summary>
        ''' Chooses a file.
        ''' </summary>
        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        ''' <paramname="create">To be created.</param>
        ''' <returns>The length of input.</returns>
        Function ChooseFile(ByVal create As Boolean) As String

        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        Sub EditFile(ByVal file As String)

        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        Function LoadHistory(ByVal [call] As Language, ByVal operation As SymbolicExpression, ByVal args As Pairlist, ByVal environment As REnvironment) As SymbolicExpression

        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        Function SaveHistory(ByVal [call] As Language, ByVal operation As SymbolicExpression, ByVal args As Pairlist, ByVal environment As REnvironment) As SymbolicExpression

        ''' <remarks>
        ''' Only Unix.
        ''' </remarks>
        Function AddHistory(ByVal [call] As Language, ByVal operation As SymbolicExpression, ByVal args As Pairlist, ByVal environment As REnvironment) As SymbolicExpression
    End Interface
End Namespace
