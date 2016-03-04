Imports Microsoft.VisualBasic.Serialization

Namespace Web.Packages

    Public Class Archives

        Public Property Source As String
        Public Property WindowsBinary As String
        Public Property MacSnowLeopard As String
        Public Property MacMavericks As String
        Public Property Subversion As String
        Public Property Git As String
        Public Property ShortUrl As String
        Public Property DownloadsReport As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Class
End Namespace