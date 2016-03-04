Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Serialization

Namespace Web

    Public Class Package

        Public Property Package As String
        Public Property Maintainer As String
        Public Property Title As String

        Const INSTALL_SCRIPT As String =
        "source(""http://bioconductor.org/biocLite.R"");" & vbCrLf &
        "biocLite(""{0}"")"

        Public ReadOnly Property InstallScript As String
            Get
                Return String.Format(INSTALL_SCRIPT, Package)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

        Const RegexItem As String = "<td>.+?</td>"

        Public Shared Function Parser(html As String) As Package
            Dim Tokens As String() = Regex.Matches(html, RegexItem, RegexOptions.Singleline + RegexOptions.IgnoreCase).ToArray
            Dim Package As String = __getValue(Tokens(0))
            Dim Maintainer As String = __getValue(Tokens(1))
            Dim Title As String = __getValue(Tokens(2))

            Return New Package With {
                .Package = Package,
                .Maintainer = Maintainer,
                .Title = Title
            }
        End Function

        Private Shared Function __getValue(s As String) As String
            Dim value As String = Regex.Match(s, ">[^<]+?</").Value
            value = Mid(value, 2, Len(value) - 3)
            Return value
        End Function
    End Class
End Namespace