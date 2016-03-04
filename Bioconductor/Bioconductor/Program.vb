Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Module Program

    Public Sub Main()
        Call New InstallPackage().ShowDialog()

        Dim bc = New WebService
        Console.WriteLine(bc.Version)
        Call bc.Initialize()
    End Sub

    Const PAGE_CONTENT_TITLE As String = "<title>.+</title>"

    ''' <summary>
    ''' Get the html page content from a website request or a html file on the local filesystem.
    ''' </summary>
    ''' <param name="url">web http request url or a file path handle</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension> Public Function Get_PageContent(url As String) As String
        Dim Timer As Stopwatch = New Stopwatch
        Call Timer.Start()

        If FileIO.FileSystem.FileExists(url) Then
            Return FileIO.FileSystem.ReadAllText(url)
        End If

        Dim WebRequest As Global.System.Net.HttpWebRequest = Global.System.Net.HttpWebRequest.Create(url)
        Dim WebResponse As WebResponse = WebRequest.GetResponse

        Using respStream As Stream = WebResponse.GetResponseStream, ioStream As StreamReader = New StreamReader(respStream)
            Dim pageContent As String = ioStream.ReadToEnd
            Dim Title = Regex.Match(pageContent, PAGE_CONTENT_TITLE, RegexOptions.IgnoreCase).Value
            Title = Mid(Title, 8, Len(Title) - 15)
            Call Console.WriteLine("[WebRequst Handler Get Response Data] --> {0}" & vbCrLf &
                                   "   Package Size  :=  {1} bytes" & vbCrLf &
                                   "   Response time :=  {2} ms", Title, Len(pageContent), Timer.ElapsedMilliseconds)
#If DEBUG Then
            Call FileIO.FileSystem.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "/WebService_DEBUG.html", pageContent, False)
#End If
            Return pageContent
        End Using
    End Function

    <Extension> Public Function DownloadFile(strUrl As String, SavedPath As String) As Boolean
        Try
            Dim dwl As New Global.System.Net.WebClient()
            Call dwl.DownloadFile(strUrl, SavedPath)
            Call dwl.Dispose()
            Return True
        Catch ex As Exception
            Call FileIO.FileSystem.WriteAllText(My.Computer.FileSystem.SpecialDirectories.Temp & "\WebClientHandler_error.log", ex.ToString & vbCrLf & vbCrLf, append:=True)
            Return False
        End Try
    End Function
End Module
