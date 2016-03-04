Imports System.Text.RegularExpressions

Namespace Web

    Public Structure Package
        Dim Package, Maintainer, Title As String

        Const INSTALL_SCRIPT As String =
        "source(""http://bioconductor.org/biocLite.R"");" & vbCrLf &
        "biocLite(""{0}"")"

        Public ReadOnly Property InstallScript As String
            Get
                Return String.Format(INSTALL_SCRIPT, Package)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Package
        End Function

        Friend Shared Function parse(strText As String) As Package
            Const Item As String = "<td>.+?</td>"

            Dim Tokens As String() = (From m As Match In Regex.Matches(strText, Item, RegexOptions.Singleline + RegexOptions.IgnoreCase) Select m.Value).ToArray
            Dim Package As String = GetValue(Tokens(0))
            Dim Maintainer As String = GetValue(Tokens(1))
            Dim Title As String = GetValue(Tokens(2))

            Return New Package With {.Package = Package, .Maintainer = Maintainer, .Title = Title}
        End Function

        Private Shared Function GetValue(strText As String) As String
            Dim value As String = Regex.Match(strText, ">[^<]+?</").Value
            value = Mid(value, 2, Len(value) - 3)
            Return value
        End Function
    End Structure
End Namespace