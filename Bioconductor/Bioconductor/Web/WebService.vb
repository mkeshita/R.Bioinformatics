Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Linq

Namespace Web

    Public Class WebService

        Public Const BIOCVIEWS_INSTALL As String = "http://master.bioconductor.org/install/"

        Public ReadOnly Property Version As Version

        Public ReadOnly Property Softwares As Package()
        Public ReadOnly Property AnnotationData As Package()
        Public ReadOnly Property ExperimentData As Package()

        Sub New()
            Version = Web.Version.GetVersion
            Call __init()
        End Sub

        Public Overrides Function ToString() As String
            Return Version.ToString
        End Function

        Private Function __init() As Integer
            _Softwares = DownloadPackagesList(url:=String.Format(SOFTWARE_PACKAGES, Version))
            _AnnotationData = DownloadPackagesList(url:=String.Format(ANNOTATION_DATA_PACKAGES, Version))
            _ExperimentData = DownloadPackagesList(url:=String.Format(EXPERIMENT_DATA_PACKAGES, Version))

            Return 0
        End Function

        Const SOFTWARE_PACKAGES As String = "http://master.bioconductor.org/packages/{0}/bioc/"
        Const ANNOTATION_DATA_PACKAGES As String = "http://master.bioconductor.org/packages/{0}/data/annotation/"
        Const EXPERIMENT_DATA_PACKAGES As String = "http://master.bioconductor.org/packages/{0}/data/experiment/"

        Const ROW_FORMAT_REGEX As String = "<tr class=""row_(odd|even)"">.+?</tr>"

        Public Shared Function DownloadPackagesList(url As String) As Package()
            Dim pageHTML As String = url.GET
            Dim ms As String() = Regex.Matches(pageHTML, ROW_FORMAT_REGEX, RegexOptions.Singleline + RegexOptions.IgnoreCase).ToArray
            Dim packages As Package() = ms.ToArray(Function(token) Package.Parser(token))
            Return packages
        End Function
    End Class
End Namespace