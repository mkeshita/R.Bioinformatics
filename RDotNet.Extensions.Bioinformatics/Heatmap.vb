Imports System.Text
Imports System.IO
Imports Microsoft.VisualBasic.DocumentFormat.Csv.DocumentStream.Tokenizer
Imports RDotNet.Extensions.VisualBasic
Imports RDotNet.Extensions.VisualBasic.utils.read.table
Imports RDotNet.Extensions.VisualBasic.stats
Imports RDotNet.Extensions.VisualBasic.Graphics

Public Class Heatmap : Inherits IRScript

    Const df As String = "df"

    ''' <summary>
    ''' Column name of the row factor in the csv file that represents the row name. Default is the first column.
    ''' </summary>
    ''' <returns></returns>
    Public Property rowNameMaps As String
    ''' <summary>
    ''' Csv文件的文件路径
    ''' </summary>
    ''' <returns></returns>
    Public Property dataset As readcsv

    Public Property kmeans As kmeans
    Public Property heatmap As stats.heatmap
    ''' <summary>
    ''' tiff文件的输出路径
    ''' </summary>
    ''' <returns></returns>
    Public Property tiff As String

    Private Function __getRowNames() As String
        Dim col As String = rowNameMaps

        If String.IsNullOrEmpty(rowNameMaps) Then
            ' 默认使用第一列，作为rows的名称
            Using file As FileStream = dataset.file.Open()
                col = New StreamReader(file).ReadLine
            End Using
            col = CharsParser(col).FirstOrDefault
        End If

        Return col
    End Function

    Sub New()
        Requires = {"RColorBrewer"}
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' http://joseph.yy.blog.163.com/blog/static/50973959201285102114376/
    ''' </remarks>
    Public Overrides Function RScript() As String
        Dim script As StringBuilder = New StringBuilder()
        Call script.AppendLine($"{df} <- " & dataset)
        Call script.AppendLine($"row.names({df}) <- {df}${__getRowNames()}")
        Call script.AppendLine($"{df}<-{df}[,-1]")

        kmeans.x = df
        kmeans.centers = 5

        Call script.AppendLine($"k <- {kmeans}")
        Call script.AppendLine($"dfc <- cbind ({df}, Cluster= k$cluster)")
        Call script.AppendLine("dfc <- dfc[order(dfc$Cluster),]")
        Call script.AppendLine("dfc.m <- data.matrix(dfc)")

        heatmap.x = "dfc.m"
        heatmap.Rowv = NA
        heatmap.Colv = NA
        heatmap.Colv = "rev(brewer.pal(10,""RdYlBu""))"
        heatmap.revC = [TRUE]
        heatmap.scale = "column"

        Call script.AppendLine(GraphicsDevice.tiff(heatmap, tiff))

        Return script.ToString
    End Function
End Class
