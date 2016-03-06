Imports RDotNET.Extensions.Bioinformatics
Imports RDotNET.Extensions.VisualBasic
Imports RDotNET.Extensions.VisualBasic.gplots
Imports RDotNET.Extensions.VisualBasic.grDevices
Imports RDotNET.Extensions.VisualBasic.utils.read.table
Imports SMRUCC.R.CRAN.Bioconductor.Web
Imports SMRUCC.R.CRAN.Bioconductor.Web.Packages

Module Test

    Dim heatmap As String = <R>

                                library(RColorBrewer)

df &lt;- read.csv(file="F:\\1.13.RegPrecise_network\\FBA\\xcam314565\\19.0\\data\\metabolic-reactions.rFBA\\heatmap\\objfunc-30__scales.csv", 
header=TRUE, 
sep=",", 
quote="\"", 
dec=".", 
fill=TRUE, 
comment.char="")
row.names(df) &lt;- df$Locus
df &lt;- df[,-1]
df &lt;- data.matrix(df)

library(gplots)

tiff(compression=c("none", "rle", "lzw", "jpeg", "zip", "lzw+p", "zip+p"), 
filename="F:/1.13.RegPrecise_network/FBA/xcam314565/19.0/data/metabolic-reactions.rFBA/heatmap/objfunc-30__scales.tiff", 
width=4000, 
height=3200, 
units="px", 
pointsize=12, 
bg="white", 
res=NA, 
family="", 
restoreConsole=TRUE, 
type=c("windows", "cairo"))

result &lt;- heatmap.2(df, col=redgreen(75), scale="row",   margin=c(15,15)  ,    key=TRUE, symkey=FALSE, density.info="none", trace="none", cexRow=2,cexCol=2)
dev.off()




                            </R>

    Sub Main()

        Call RSystem.REngine.WriteLine(Test.heatmap)

        Dim result = RSystem.REngine.WriteLine("result")


        Pause()



        Dim hm As New Heatmap With {
            .dataset = New readcsv("E:\R.Bioinformatics\datasets\ppg2008.csv"),
            .heatmap = heatmap2.Puriney,
            .kmeans = New stats.kmeans,
            .image = New tiff("x:/ffff.tiff")
        }

        hm.kmeans.centers = 5

        Dim r As String = hm.RScript

        Call r.SaveTo("x:\dddd.r")




        Dim rp = Repository.LoadDefault
        Dim pp = rp.softwares.First
        pp.GetDetails("E:\R.Bioinformatics\Bioconductor\ParserTest.html".ReadAllText)
    End Sub

End Module
