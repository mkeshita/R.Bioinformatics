Imports System.Text
Imports System.IO
Imports Microsoft.VisualBasic.DocumentFormat.Csv.DocumentStream.Tokenizer
Imports Microsoft.VisualBasic.Linq
Imports RDotNet.Extensions.VisualBasic
Imports RDotNet.Extensions.VisualBasic.utils.read.table
Imports RDotNet.Extensions.VisualBasic.stats
Imports RDotNet.Extensions.VisualBasic.Graphics
Imports RDotNet.Extensions.VisualBasic.grDevices

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
    Public Property heatmap As heatmap_plot
    ''' <summary>
    ''' tiff文件的输出路径
    ''' </summary>
    ''' <returns></returns>
    Public Property image As grDevice

    Private Function __getRowNames() As String
        Dim col As String = rowNameMaps

        If String.IsNullOrEmpty(rowNameMaps) Then
            ' 默认使用第一列，作为rows的名称
            Using file As FileStream = dataset.file.Open()
                col = New StreamReader(file).ReadLine
            End Using
            col = CharsParser(col).FirstOrDefault
        End If

        _locusId = IO.File.ReadAllLines(dataset.file) _
            .Skip(1).ToArray(Function(x) x.Split(","c).First)

        Return col
    End Function

    Dim __output As String()

    ''' <summary>
    ''' 在执行完了脚本之后调用本方法才能够得到结果，否则返回空值
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property output As String()
        Get
            If __output.IsNullOrEmpty Then
                Try
                    __output = RSystem.REngine.WriteLine("result")
                Catch ex As Exception
                    Return Nothing
                End Try
            End If
            Return __output
        End Get
    End Property
    Public ReadOnly Property locusId As String()

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
    Protected Overrides Function __R_script() As String
        Dim script As StringBuilder = New StringBuilder()
        Call script.AppendLine($"{df} <- " & dataset)
        Call script.AppendLine($"row.names({df}) <- {df}${__getRowNames()}")
        Call script.AppendLine($"{df}<-{df}[,-1]")
        Call script.AppendLine("df <- data.matrix(df)")

        heatmap.x = df

        If Not heatmap.Requires Is Nothing Then
            For Each ns As String In heatmap.Requires
                Call script.AppendLine(RScripts.library(ns))
            Next
        End If

        Call script.AppendLine(image.Plot("result <- " & heatmap))

        Return script.ToString
    End Function
End Class
