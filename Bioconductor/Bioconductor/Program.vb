Imports System.Runtime.CompilerServices
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports SMRUCC.R.CRAN.Bioconductor.Web

Module Program

    Public Sub Main()
        Call New InstallPackage().ShowDialog()

        Dim bc = New WebService
        Console.WriteLine(bc.Version)
        Call bc.Initialize()
    End Sub
End Module
