Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Parallel
Imports SMRUCC.R.CRAN.Bioconductor.Web
Imports RDotNET.Extensions.VisualBasic
Imports SMRUCC.R.CRAN.Bioconductor.Web.Packages
Imports System.Threading

Module Program

    Public Sub Main()
        Call RSystem.InitDefault()
        Call Test.Main()

        Dim splash As New bioc
        Call RunTask(AddressOf splash.ShowDialog)
        Dim repo As Repository = Repository.LoadDefault
        Call Thread.Sleep(500)
        Call splash.Close()
        Call New InstallPackage(repo).ShowDialog()
    End Sub
End Module
