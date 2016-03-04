Imports SMRUCC.R.CRAN.Bioconductor.Web
Imports SMRUCC.R.CRAN.Bioconductor.Web.Packages

Module Test

    Sub Main()
        Dim rp = Repository.LoadDefault
        Dim pp = rp.softwares.First
        pp.GetDetails("E:\R.Bioinformatics\Bioconductor\ParserTest.html".ReadAllText)
    End Sub

End Module
