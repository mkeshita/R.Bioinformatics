Imports System.Text.RegularExpressions

Namespace Web

    Public Class WebService

        Const BIOCVIEWS_INSTALL As String = "http://master.bioconductor.org/install/"
        Const VERSION_NUMBER As String = "(\d+\.?)+"
        Const ABOUT_VERSION As String = "<p>The current release of Bioconductor is version[^<]+</p>"

        ''' <summary>
        ''' {bioconductor_version, R_required_version}
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Version() As String()
            Dim pageHTML As String = BIOCVIEWS_INSTALL.GET
            Dim About As String = Regex.Match(pageHTML, ABOUT_VERSION, RegexOptions.Singleline).Value
            Dim matches = (From m As Match In Regex.Matches(About, VERSION_NUMBER, RegexOptions.Singleline + RegexOptions.IgnoreCase) Select m.Value).ToArray

            Return matches
        End Function

        Dim BiocVersion As String, RRequired As String

        Public Property Softwares As Package()
        Public Property AnnotationData As Package()
        Public Property ExperimentData As Package()

        Sub New()
            Dim str As String() = Version()
            BiocVersion = str.First
            RRequired = str.Last
            RRequired = Mid(RRequired, 1, Len(RRequired) - 1)
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("The current release of Bioconductor is version {0}; it works with R version {1}.", BiocVersion, RRequired)
        End Function

        Public Function Initialize() As Integer
            Softwares = DownloadPackagesList(url:=String.Format(SOFTWARE_PACKAGES, Version))
            AnnotationData = DownloadPackagesList(url:=String.Format(ANNOTATION_DATA_PACKAGES, Version))
            ExperimentData = DownloadPackagesList(url:=String.Format(EXPERIMENT_DATA_PACKAGES, Version))

            Return 0
        End Function

        Const SOFTWARE_PACKAGES As String = "http://master.bioconductor.org/packages/{0}/bioc/"
        Const ANNOTATION_DATA_PACKAGES As String = "http://master.bioconductor.org/packages/{0}/data/annotation/"
        Const EXPERIMENT_DATA_PACKAGES As String = "http://master.bioconductor.org/packages/{0}/data/experiment/"

        Public Shared Function DownloadPackagesList(url As String) As Package()
            Const ROW_FORMAT_REGEX As String = "<tr class=""row_(odd|even)"">.+?</tr>"

            Dim pageHTML As String = url.GET
            Dim Items As String() = (From m As Match In Regex.Matches(pageHTML, ROW_FORMAT_REGEX, RegexOptions.Singleline + RegexOptions.IgnoreCase) Select m.Value).ToArray
            Dim Packages As Package() = New Package(Items.Count - 1) {}

            For i As Integer = 0 To Packages.Count - 1
                Packages(i) = Package.parse(Items(i))
            Next

            Return Packages
        End Function
    End Class
End Namespace