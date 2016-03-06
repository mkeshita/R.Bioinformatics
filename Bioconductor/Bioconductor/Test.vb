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
Imports Microsoft.VisualBasic
Imports System.Text

Imports PhyloNode = Microsoft.VisualBasic.ComponentModel.DataStructures.BinaryTree.TreeNode(Of Integer)

Imports RegExp = System.Text.RegularExpressions.Regex
Imports MatchResult = System.Text.RegularExpressions.MatchCollection

Imports Node = System.Xml.XmlNode
Imports Element = System.Xml.XmlElement
Imports NodeList = System.Xml.XmlNodeList

Imports Microsoft.VisualBasic.Linq


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

        ''' <summary>
        ''' 基因的排列顺序
        ''' </summary>
        ''' <returns></returns>
        Public Property rowInd As Integer()
        Public Property colInd As Integer()

        Public Property [call] As String

        Public Property rowMeans As Double()
        Public Property rowSDs As Double()
        Public Property carpet As Double()
        ''' <summary>
        ''' heatmap.2行聚类的结果，(基因)
        ''' </summary>
        ''' <returns></returns>
        Public Property rowDendrogram As TreeNode(Of Integer)
        Public Property colDendrogram As TreeNode(Of Integer)

        ''' <summary>
        ''' 进行<see cref="col"/>映射的数值等级
        ''' </summary>
        ''' <returns></returns>
        Public Property breaks As Double()
        ''' <summary>
        ''' 热图里面的颜色代码
        ''' </summary>
        ''' <returns></returns>
        Public Property col As String()
        Public Property colorTable As colorTable()

        Public Shared Function IndParser(result As String) As Integer()
            Return Regex.Matches(result, "\d+").ToArray(Function(s) Scripting.CastInteger(s))
        End Function

        Public Shared Function MeansParser(result As String) As Double()
            Return Regex.Matches(result, "(-?\d+(\.\d+)?)|(NaN)").ToArray(Function(s) Scripting.CastDouble(s))
        End Function

        Public Shared Function TreeBuilder(result As String) As TreeNode(Of Integer)
            result = result.Replace("list", "")
            Call result.__DEBUG_ECHO
            Dim node As TreeNode(Of Integer) = New TreeNode(Of Integer)
            Call NewickParser.TreeParser(result, New Dictionary(Of String, String), node)
            Return node
        End Function

        Public Shared Function ColorParser(result As String) As String()
            Return Regex.Matches(result, "#[0-9A-Za-z]+").ToArray
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="out">heatmap.2输出结果</param>
        ''' <returns></returns>
        Public Shared Function RParser(out As String()) As heatmap2OUT
            Dim i As Pointer(Of String) = New Pointer(Of String)
            Dim mapResult As New heatmap2OUT With {
                .rowInd = heatmap2OUT.IndParser(out + i),   ' i++
                .colInd = heatmap2OUT.IndParser(out + i),
                .call = out + i,
                .rowMeans = heatmap2OUT.MeansParser(out + i),
                .rowSDs = heatmap2OUT.MeansParser(out + i),
                .carpet = heatmap2OUT.MeansParser(out + i),
                .rowDendrogram = heatmap2OUT.TreeBuilder(out + i),
                .colDendrogram = heatmap2OUT.TreeBuilder(out + i),
                .breaks = heatmap2OUT.MeansParser(out + i),
                .col = heatmap2OUT.ColorParser(out + i),
                .colorTable = colorTableParser(out + i)
            }
            Return mapResult
        End Function

        Public Shared Function colorTableParser(result As String) As colorTable()
            Dim vectors As String() = Regex.Matches(result, "\(.+?\)", RegexOptions.Singleline).ToArray
            Dim i As New Pointer(Of String)
            Dim low As String = vectors + i      ' Pointer operations 
            Dim high As String = vectors + i
            Dim color As String = vectors + i

            low = Mid(low, 2, low.Length - 2)
            high = Mid(high, 2, high.Length - 2)
            color = Mid(color, 2, color.Length - 2)

            Dim lows As Double() = low.Split(","c).ToArray(Function(s) Scripting.CastDouble(s.Trim))
            Dim highs As Double() = high.Split(","c).ToArray(Function(s) Scripting.CastDouble(s.Trim))
            Dim colors As String() = color.Split(","c).ToArray(Function(s) s.Trim)

            Return (From lp As SeqValue(Of String)
                    In colors.SeqIterator
                    Select New colorTable With {
                        .color = lp.obj,
                        .low = lows(lp.Pos),
                        .high = highs(lp.Pos)}).ToArray
        End Function
    End Class

    Public Structure colorTable
        Dim low As Double
        Dim high As Double
        Dim color As String

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function
    End Structure

    Sub Main()

        Dim xx As Pointer = 5

        Dim ndd As Integer = +xx

        Dim xxsss = +ndd


        Dim ddd As String() = LoadJsonFile(Of String())("E:\R.Bioinformatics\datasets\heatmap_testOUT.json")


        Dim hhhhh = heatmap2OUT.RParser(ddd)

        Call hhhhh.SaveAsXml("x:\ddd.xml")

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
