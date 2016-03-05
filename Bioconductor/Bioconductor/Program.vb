﻿Imports System.Runtime.CompilerServices
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

        Dim tb As New utils.read.table.readTable
        Dim sss = tb.RScript

        '  Call Test.Main()
        Call RSystem.InitDefault()

        Dim splash As New bioc
        Call RunTask(AddressOf splash.ShowDialog)
        Dim repo As Repository = Repository.LoadDefault
        Call Thread.Sleep(500)
        Call splash.Close()
        Call New InstallPackage(repo).ShowDialog()
    End Sub
End Module
