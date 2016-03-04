Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports SMRUCC.R.CRAN.Bioconductor.Web
Imports RDotNET.Extensions.VisualBasic
Imports SMRUCC.R.CRAN.Bioconductor.Web.Packages

Module Program

    Public Sub Main()

        Call Test.Main()


        Call RSystem.InitDefault()
        Call New InstallPackage(Repository.LoadDefault).ShowDialog()
    End Sub
End Module
