Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports SMRUCC.R.CRAN.Bioconductor.Web
Imports RDotNET.Extensions.VisualBasic

Module Program

    Public Sub Main()
        Call RSystem.InitDefault()

        Dim nn = RSystem.packageVersion("snow")
        nn = RSystem.packageVersion("snowx")

        Call New InstallPackage(Repository.LoadDefault).ShowDialog()

        Dim bc As WebService = WebService.Default
        Console.WriteLine(bc.Version)
    End Sub
End Module
