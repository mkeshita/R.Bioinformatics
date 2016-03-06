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
        Public Property rowDendrogram As TreeNode(Of Integer)
        Public Property colDendrogram As TreeNode(Of Integer)
        Public Property breaks As Double()
        Public Property col As String()
        Public Property colorTable

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
            Call newickParser(result, New Dictionary(Of String, String), node)
            Return node
        End Function
    End Class

    ' oct 20, 2013: some regular expression stuff
    Friend RegExpFloat As New Regex("(\d+\.?\d*)")
    Friend MatchLeftBracket As New Regex("\[")

    ''' <summary>
    ''' created: Oct 20, 2013 : a better and easier to maintain parser for newick and nexus trees
    ''' NOTE: this is a recursive function </summary>
    ''' <param name="inputstr"> : input tree string </param>
    ''' <param name="hashTranslate"> : aliases for lead nodes (for nexsus format) </param>
    ''' <param name="iNode"> : current internal node; == rootNode the first time 'newickParser' is called  </param>
    Private Sub newickParser(inputstr As String, hashTranslate As Dictionary(Of String, String), iNode As TreeNode(Of Integer))
        inputstr = inputstr.Trim()

        ' NOTE: the input string should look like this: (A,B,(C,D)E)F
        ' the first char has to be (
        ' first, get what's between the first and the last Parentheses, and what's after the last right Parentheses
        ' for example, your tree : (A,B,(C,D)E)F will be split into two parts:
        '   A,B,(C,D)E = ...
        '   F = tail string
        Dim tailString As String = ""

        If Not inputstr.Length = 0 Then
            ' remove trailing ';'
            While inputstr.EndsWith(";")

                ' is this really necessary???
                inputstr = inputstr.Substring(0, inputstr.Length - 1)
            End While
            For idx As Integer = inputstr.Length - 1 To 0 Step -1
                If inputstr(idx) = ")"c Then
                    tailString = inputstr.Substring(idx + 1)

                    ' change input str from (A,B,(C,D)E)F to A,B,(C,D)E
                    inputstr = inputstr.Substring(1, idx - 1)
                    ' !!!!!
                    Exit For
                End If
            Next
        End If
        ' if string is not empty
        ' parse the tail string and get information for current internal node
        ' possibilities
        ' 1. nothing ... 
        ' 2. bootstrap only: )99, or )0.99
        ' 3. branch length only: ):0.456
        ' 4. both bootstrap and branch length )99:0.456
        ' 5. internal ID : )str
        ' 6. internal ID with branch length : )str:0.456
        ' 7. itol style, internalID, bootstrap and branch length: )internalID:0.2[95]
        ' 8. nexus style bootstrap: )[&label=99]:0.456
        If Not tailString.Length = 0 Then
            ' first of all, split string into two parts by ':'
            Dim parts As String() = tailString.StringSplit(":", True)

            '  deal with the first part
            '  first, check case 8. Square brackets [&label=99]
            If parts.Length >= 1 Then
                Dim part1 As String = parts(0)
                If Not part1.Length = 0 Then
                    ' if not case 3
                    If MatchLeftBracket.Match(part1) IsNot Nothing Then
                        ' get the float number from a string like this: [&label=99]
                        Dim m As MatchResult = RegExpFloat.Matches(part1)
                        If (m IsNot Nothing) Then
                            Dim bootstrap As Single = Val(m.Item(0).Value)

                        End If
                    Else
                        ' check if it is a string or numeric 
                        Try
                            Dim bootstrap As Single = Val(part1)
                            ' if success; case 2, 4

                        Catch
                            ' if fail, i assume the string is internal note name; case 5, 7

                        End Try
                    End If
                    ' if part 1 is not empty
                End If
            End If

            ' if there is a part 2 and it's not empty --
            If parts.Length >= 2 Then
                Dim part2 As String = parts(1)
                If Not part2.Length = 0 Then
                    If MatchLeftBracket.Match(part2) IsNot Nothing Then
                        ' if it is the itol style

                        ' split it into two parts by '[', the first part should contain the branch lenth, while the second contains the bootstrap
                        ' of cource, both could be empty,
                        ' valid inputs are:
                        ' :[] - both are empty,
                        ' :[99]
                        ' :0.456[]
                        ' :0.456[99]
                        ' NOTE: bootstrap value can also be float number
                        Dim iparts As String() = part2.StringSplit("\[", True)
                        If iparts.Length > 0 Then
                            ' the first part: branch length
                            Dim ipart1 As String = iparts(0)
                            Dim m As MatchResult = RegExpFloat.Matches(ipart1)
                            If (m IsNot Nothing) Then
                                Dim branchlength As Single = Val(m.Item(0).Value)

                            End If
                            ' branch length
                            ' the second part, bootstrap
                            If iparts.Length >= 2 Then
                                Dim ipart2 As String = iparts(1)
                                Dim m2 As MatchResult = RegExpFloat.Matches(ipart2)
                                If (m IsNot Nothing) Then
                                    Dim bootstrap As Single = Val(m2.Item(0).Value)

                                End If
                                ' bootstrap
                            End If
                        End If
                    Else
                        ' parse branch length value; case 3,4,6,8
                        Try
                            Dim branchlength As Single = Val(part2)

                            ' do nothing
                        Catch
                        End Try
                        ' if ... else ...
                    End If
                    ' if part2 is not empty
                End If
                ' if part2 is there
            End If
        End If
        ' if the string4internalNode string is not empty

        ' now go through what's between the parentheses and get the leaf nodes
        '   (A,B,(C,D)E)F = original tree
        '   A,B,(C,D)E = the part the following codes will deal with
        If Not inputstr.Length = 0 Then

            ' split current input string into substrings, each is a daughtor node of current internal node
            ' if your input string is like this: A,B,(C,D)E
            ' it will be split into the following three daughter strings:
            '  A
            '  B
            '  (C,D)E
            ' accordingly, three daughter nodes will be created, two are leaf nodes and one is an internal node 
            Dim brackets As Integer = 0, leftParenthesis As Integer = 0, commas As Integer = 0
            Dim sb As New StringBuilder()
            For Each c As Char In inputstr.ToCharArray()
                If (c = ","c OrElse c = ")"c) AndAlso brackets = 0 Then
                    ' ',' usually indicates the end of an node; is || c == ')' really necessary ???
                    ' make daugher nodes
                    Dim daughter = sb.ToString()
                    If leftParenthesis > 0 AndAlso commas > 0 Then
                        newickParser(daughter, hashTranslate, MakeNewInternalNode("", False, iNode))
                    Else
                        ' a leaf daughter 
                        ' parse information for current daughter node
                        parseInforAndMakeNewLeafNode(daughter, hashTranslate, iNode)
                    End If

                    ' reset some variables
                    sb = New StringBuilder()
                    leftParenthesis = 0
                Else
                    sb.Append(c)
                    ' ',' will not be recored
                    If c = ","c Then
                        commas += 1
                    End If
                End If

                '  brackets is used to find the contents between a pair of matching ()s
                '  how does this work???
                '  
                '  here is how the value of brackets changes if your input string is like this :
                '  (A,B,(C,D)E)F
                '  1    2   1 0 # value of brackets ... 
                '  +    +   - - # operation
                '  ^          ^ # contents between these two () will be extracted = A,B,(C,D)E
                '  
                '  --- 
                '  variable 'leftParenthesis' is used to indicate whether current daughter node is likely a internal node; 
                '  however, this alone cannot garrentee this, because the name of a leaf node may contain Parenthesis
                '  therefore I use 'leftParenthesis' and 'commas' together to indicate an internal node  
                If c = "("c Then
                    brackets += 1
                    leftParenthesis += 1
                ElseIf c = ")"c Then
                    brackets -= 1
                End If
            Next
            ' deal with the last daughter 
            Dim LastDaughter As String = sb.ToString()
            If leftParenthesis > 0 AndAlso commas > 0 Then
                newickParser(LastDaughter, hashTranslate, MakeNewInternalNode("", False, iNode))
            Else
                ' a leaf daughter 

                ' parse information for current daughter node
                parseInforAndMakeNewLeafNode(LastDaughter, hashTranslate, iNode)

            End If
        End If
        ' new recursive parser
    End Sub

    ''' <summary>
    ''' Dec 5, 2011; can be used to make rootnode
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="isroot"></param>
    ''' <param name="parentnode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MakeNewInternalNode(id As String, isroot As Boolean, ByRef parentnode As PhyloNode) As PhyloNode
        Dim NewNodeObject As New PhyloNode(id, -1)

        ' dec 5, 2011
        If Not isroot AndAlso parentnode IsNot Nothing Then
            parentnode += NewNodeObject
        End If

        Return NewNodeObject
    End Function


    ' end of current function
    ''' <summary>
    ''' created on Oct 20, 2013 
    ''' input: the leafstr to be parsed, the internal node the leaf node has to be added to 
    ''' </summary>
    Private Sub parseInforAndMakeNewLeafNode(leafstr As String, hashTranslate As Dictionary(Of String, String), iNode As PhyloNode)
        leafstr = leafstr.Trim()

        ' parse a leaf node,
        ' possibilities are:
        ' 1. ,, - leaf node is not named (???)
        ' 2. A  - named leaf node
        ' 3. :0.1 - unamed leaf node with branch length
        ' 4. A:0.1 - named leaf node with branch length
        If leafstr.Length = 0 Then
            ' case 1
            makeNewLeafNode("", iNode)
        Else
            ' split it into two parts
            Dim parts As String() = leafstr.StringSplit(":", True)

            ' now deal with part 1, two possibilities: named / unamed leaf node
            Dim part1 As String = parts(0)
            If part1.Length = 0 Then
                makeNewLeafNode("", iNode)
            Else
                Dim leafNodeName As String = part1.Replace("'", "").Replace("""", "")
                leafNodeName = If((hashTranslate IsNot Nothing AndAlso hashTranslate.ContainsKey(leafNodeName)), hashTranslate(leafNodeName), leafNodeName)
                makeNewLeafNode(leafNodeName, iNode)
            End If
        End If
    End Sub


    Private Function makeNewLeafNode(id As String, parentnode As PhyloNode) As PhyloNode
        Dim leafnode As New PhyloNode(id, -1)

        parentnode += leafnode

        ' dec 30, 2010
        Return leafnode
    End Function


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
