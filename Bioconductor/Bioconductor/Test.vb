Imports RDotNET.Extensions.Bioinformatics
Imports RDotNET.Extensions.VisualBasic
Imports RDotNET.Extensions.VisualBasic.gplots
Imports RDotNET.Extensions.VisualBasic.grDevices
Imports RDotNET.Extensions.VisualBasic.utils.read.table
Imports SMRUCC.R.CRAN.Bioconductor.Web

Imports Microsoft.VisualBasic.Serialization
Imports SMRUCC.R.CRAN.Bioconductor.Web.Packages
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.ComponentModel.DataStructures
Imports Microsoft.VisualBasic.ComponentModel.DataStructures.BinaryTree

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

    Public Class heatmap2OUT

        Public Property rowInd As Integer()
        Public Property colInd As Integer()

        Public Property [call] As String

        Public Property rowMeans As Double()
        Public Property rowSDs As Double()
        Public Property carpet As Double()
        Public Property rowDendrogram As BinaryTree(Of Integer)
        Public Property colDendrogram As BinaryTree(Of Integer)
        Public Property breaks As Double()
        Public Property col As String()
        Public Property colorTable

        Public Shared Function IndParser(result As String) As Integer()
            Return Regex.Matches(result, "\d+").ToArray(Function(s) Scripting.CastInteger(s))
        End Function

        Public Shared Function MeansParser(result As String) As Double()
            Return Regex.Matches(result, "(-?\d+(\.\d+)?)|(NaN)").ToArray(Function(s) Scripting.CastDouble(s))
        End Function

        Public Shared Function TreeBuilder(result As String) As BinaryTree(Of Integer)
            result = result.Replace("list", "")



        End Function

    End Class


    Sub Main()

        Dim xx As Pointer = 5

        Dim ndd As Integer = +xx

        Dim xxsss = +ndd


        Dim ddd As String() = LoadJsonFile(Of String())("E:\R.Bioinformatics\datasets\heatmap_testOUT.json")

        Dim i As New Pointer(Of String)


        Dim dddrt As New heatmap2OUT With {
            .rowInd = heatmap2OUT.IndParser(ddd + i),   ' i++
            .colInd = heatmap2OUT.IndParser(ddd + i),
            .call = ddd + i,
            .rowMeans = heatmap2OUT.MeansParser(ddd + i),
            .rowSDs = heatmap2OUT.MeansParser(ddd + i),
            .carpet = heatmap2OUT.MeansParser(ddd + i),
            .rowDendrogram = heatmap2OUT.TreeBuilder(ddd + i),
            .colDendrogram = heatmap2OUT.TreeBuilder(ddd + i)
        }





        Call RSystem.REngine.WriteLine(Test.heatmap)

        Dim result = RSystem.REngine.WriteLine("result")

        Call result.GetJson.SaveTo("E:\R.Bioinformatics\datasets\heatmap_testOUT.json")

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
