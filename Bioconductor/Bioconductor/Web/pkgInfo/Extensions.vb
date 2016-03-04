Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text

Namespace Web.Packages

    Module Extensions

        Sub New()
            On Error Resume Next

            Call My.Resources.bioconductor.SaveTo(App.HOME & "/assets/bioconductor.css")
            Call My.Resources.bioconductor1.SaveTo(App.HOME & "/assets/js/bioconductor.js")
            Call My.Resources.bioc_style.SaveTo(App.HOME & "/assets/js/bioc-style.js")

            Using ico As FileStream = File.Open(App.HOME & "/assets/favicon.ico", FileMode.OpenOrCreate)
                Call My.Resources.favicon.Save(ico)
            End Using
        End Sub

        <Extension>
        Public Function GetURL(pack As Package) As String
            If pack.Category = BiocTypes.bioc Then
                Return $"http://master.bioconductor.org/packages/release/{pack.Category.ToString}/html/{pack.Package}.html"
            Else
                Return $"http://master.bioconductor.org/packages/release/data/{pack.Category.ToString}/html/{pack.Package}.html"
            End If
        End Function

        <Extension>
        Public Function GetDetails(pkg As Package) As Package
            Return pkg.GetDetails(pkg.GetURL.GET)
        End Function

        <Extension>
        Public Function GetDetails(ByRef pkg As Package, html As String) As Package
            Dim parts = Strings.Split(html, "<div class='do_not_rebase'>")
            html = parts.Last
            parts = Strings.Split(html, "<table>")

            pkg.Description = DescriptionParser(parts(Scan0), pkg.Package)
            pkg.Details = DetailsParser(parts(1))
            pkg.Archives = ArchivesParser(parts(2))

#If DEBUG Then
            Call pkg.Description.SaveTo(App.HOME & "/test.html")
#End If

            Return pkg
        End Function

        Public Function DetailsParser(pageHTML As String) As Details

        End Function

        Public Function ArchivesParser(pageHTML As String) As Archives

        End Function

        Public Function DescriptionParser(pageHTML As String, pkg As String) As String
            Dim html As New StringBuilder(My.Resources.Templates)
            Call html.Replace("{Package}", pkg)
            Call html.Replace("{Description}", pageHTML)
            Return html.ToString
        End Function
    End Module
End Namespace