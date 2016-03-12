Imports System.IO
Imports RDotNet
Imports System.Text.RegularExpressions
Imports System.Text

Imports RDotNET.REngineExtension
Imports RDotNET.SymbolicExpressionExtension

Friend Class REngine : Inherits RDotNET.REngine
    Implements Global.System.IDisposable

    Public ReadOnly Property StandardOutput As String()

    Sub New(id As String, dll As String)
        Call MyBase.New(id, dll)
    End Sub

    Public Sub PrintSTDOUT()
        If Not StandardOutput.IsNullOrEmpty Then
            Dim s As String = String.Join(vbCrLf, _StandardOutput)
            Call Console.WriteLine(s)
        End If
    End Sub

    ''' <summary>
    ''' Automatically search for the path of the R system and then construct a R session for you.
    ''' (如果在注册表之中已经存在了R的路径的值或者你已经设置好了环境变量，则可以直接使用本函数进行初始化操作)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function StartEngineServices() As REngine
        Dim Directories As String() = ProgramPathSearchTool.SearchDirectory("R", "")
        If Directories.IsNullOrEmpty Then
            Throw New Exception(REngine.INIT_FAILURE)
        End If

        Dim R_HOME As String = ""

        For Each Direactory As String In Directories
            Dim Files As String() = ProgramPathSearchTool.SearchProgram(Direactory, "R")
            If Not Files.IsNullOrEmpty Then
                R_HOME = Files.First
                Exit For
            End If
        Next

        If String.IsNullOrEmpty(R_HOME) Then
            Throw New Exception(REngine.INIT_FAILURE)
        Else
            R_HOME = R_HOME.ParentPath
        End If

        Return REngine.StartEngineServices(R_HOME:=R_HOME)
    End Function

    Const INIT_FAILURE As String = "Could not initialize the R session automatically!"
    Const R_HOME_NOT_FOUND As String = "Could not found the specified path to the directory containing R.dll: "

    ''' <summary>
    ''' Manual setup the R system path.(这个函数是在没有自动设置好环境变量的时候进行手动的环境变量设置所使用的初始化方法)
    ''' </summary>
    ''' <param name="R_HOME"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function StartEngineServices(R_HOME As String) As REngine
        Dim oldPath As String = Global.System.Environment.GetEnvironmentVariable("PATH")
        Dim rPath As String = IIf(Global.System.Environment.Is64BitProcess,
                                  $"{R_HOME}/x64",
                                  $"{R_HOME}/i386")

        If Directory.Exists(rPath) = False Then
            Throw New DirectoryNotFoundException(R_HOME_NOT_FOUND & " ---> """ & rPath & """")
        End If

        Dim newPath = String.Format("{0}{1}{2}", rPath, Path.PathSeparator, oldPath)
        Dim rHome As String = ""

        Select Case (Environment.OSVersion.Platform)
            Case PlatformID.Win32NT
            Case PlatformID.MacOSX : rHome = "/Library/Frameworks/R.framework/Resources"
            Case PlatformID.Unix : rHome = "/usr/lib/R"
            Case Else : Throw New NotSupportedException($"No support such platform: {Environment.OSVersion.Platform.ToString}")
        End Select

        Call Global.System.Environment.SetEnvironmentVariable("PATH", newPath)
        If Not String.IsNullOrEmpty(rHome) Then Call Global.System.Environment.SetEnvironmentVariable("R_HOME", rHome)

        Dim REngine As REngine = CreateInstance(Of REngine)("RDotNet")
        Call REngine._R_HOME.InvokeSet(R_HOME)
        Call REngine.Initialize()

        Return REngine
    End Function

    ''' <summary>
    ''' Creates a new instance that handles R.DLL.
    ''' </summary>
    ''' <param name="id">ID.</param>
    ''' <param name="dll">The file name of the library to load, e.g. "R.dll" for Windows. You should usually not provide this optional parameter</param>
    ''' <returns>The engine.</returns>
    Protected Overloads Shared Function CreateInstance(Of TEngine As RDotNET.REngine)(id As String, Optional dll As String = Nothing) As TEngine
        If id Is Nothing Then
            Throw New ArgumentNullException("id", "Empty ID is not allowed.")
        End If
        If id = String.Empty Then
            Throw New ArgumentException("Empty ID is not allowed.", "id")
        End If

        dll = ProcessRDllFileName(dll)

        Dim engine As TEngine = DirectCast(Activator.CreateInstance(GetType(TEngine), id, dll), TEngine)
        Return engine
    End Function

    Public ReadOnly Property R_HOME As String

    Public Overrides Function ToString() As String
        If Len(StandardOutput) = 0 Then
            Return _R_HOME
        Else
            Return StandardOutput.JoinBy(vbCrLf)
        End If
    End Function

    ''' <summary>
    ''' This function equals to the function &lt;library> in R system.
    ''' </summary>
    ''' <param name="packageName"></param>
    ''' <returns></returns>
    Public Function Library(packageName As String) As Boolean
        Dim Command As String = $"library(""{packageName}"");"
        Try
            Dim Result As String() = Evaluate(Command).AsCharacter().ToArray()
            Return True
        Catch ex As Exception
            Call App.LogException(ex)
            Return False
        End Try
    End Function

    Public Overloads Function Evaluate(RScript As IRScript) As RDotNET.SymbolicExpression
        Return Evaluate(RScript.RScript)
    End Function

    ''' <summary>
    ''' 获取来自于R服务器的输出，而不将结果打印于终端之上
    ''' </summary>
    ''' <param name="script"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WriteLine(script As String) As String()
        Dim Result As SymbolicExpression = Evaluate(script)

        If Result Is Nothing Then
            Return {}
        Else
            If Result.Type = Internals.SymbolicExpressionType.Closure OrElse
                Result.Type = Internals.SymbolicExpressionType.Null Then
                Return New String() {}
            End If

            Dim array As String() = Result.AsCharacter().ToArray
            Me._StandardOutput = array
            Return Me.StandardOutput
        End If
    End Function

    ''' <summary>
    ''' Quite the R system.
    ''' </summary>
    Public Sub q()
        Call Evaluate("q()")
    End Sub

    Public Shared Widening Operator CType(R_HOME As String) As REngine
        Dim inst = REngine.StartEngineServices(R_HOME)
        Return inst
    End Operator

    Public Shared Operator <=(REngine As REngine, RScript As String) As String()
        Dim StandardOutput = REngine.WriteLine(RScript)
        Return StandardOutput
    End Operator

    Public Shared Operator <=(REngine As REngine, RScript As StringBuilder) As String()
        Return REngine <= RScript.ToString
    End Operator

    Public Shared Operator >=(REngine As REngine, RScript As StringBuilder) As String()
        Throw New NotImplementedException
    End Operator

    Public Shared Operator >=(REngine As REngine, RScript As String) As String()
        Throw New NotImplementedException
    End Operator

    Public Shared Operator <<(REngine As REngine, i As Integer)
        Return REngine
    End Operator

    Public Shared Operator <=(REngine As REngine, RScript As IRScript) As String()
        Dim StandardOutput = REngine.WriteLine(RScript.RScript)
        Return StandardOutput
    End Operator

    Public Shared Operator >=(REngine As REngine, RScript As IRScript) As String()
        Throw New NotImplementedException
    End Operator
End Class
