Imports System.Runtime.CompilerServices

Namespace Web.Packages

    Module Extensions

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
            Dim page As String = pkg.GetURL.GET

        End Function

        Public Function DetailsParser(pageHTML As String) As Details

        End Function

        Public Function ArchivesParser(pageHTML As String) As Archives

        End Function

        Public Function DescriptionParser(pageHTML As String) As String

        End Function
    End Module
End Namespace