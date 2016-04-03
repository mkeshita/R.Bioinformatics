Imports System.Drawing
Imports System.Text
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.DocumentFormat.Csv
Imports Microsoft.VisualBasic.DocumentFormat.Csv.DocumentStream
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Linq.Extensions
Imports RDotNET.Extensions.VisualBasic

Namespace VennDiagram.ModelAPI

    ''' <summary>
    ''' The data model of the venn diagram.(文氏图的数据模型)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VennDiagram : Inherits IRScript

        ''' <summary>
        ''' The title of the diagram.
        ''' </summary>
        ''' <returns></returns>
        Public Property Title As String
        ''' <summary>
        ''' vennDiagram tiff file saved path.(所生成的文氏图的保存文件名)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property saveTiff As String
        Public Property Serials As Serial()
            Get
                Return __partitions.Values.ToArray
            End Get
            Set(value As Serial())
                If value Is Nothing Then
                    __partitions = New Dictionary(Of Serial)
                Else
                    __partitions = value.ToDictionary
                End If
            End Set
        End Property

        Dim __partitions As Dictionary(Of Serial)

        Sub New()
            Requires = {"VennDiagram"}
        End Sub

        Public Overrides Function ToString() As String
            Return Title
        End Function

        Public Sub RandomColors()
            Dim colors As String() = RSystem.RColors.Randomize

            For i As Integer = 0 To Serials.Length - 1
                Serials(i).Color = colors(i)
            Next
        End Sub

        Public Property Resolution As Size = New Size(5000, 3000)

        Public Shared Operator +(venn As VennDiagram, opts As IEnumerable(Of String())) As VennDiagram
            For Each opt As String() In opts
                Dim name As String = opt.First
                Call venn.__partitions.Find(name).ApplyOptions(opt)
            Next

            Return venn
        End Operator

        ''' <summary>
        ''' 将本数据模型对象转换为R脚本
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overrides Function __R_script() As String
            Dim R As StringBuilder = New StringBuilder(capacity:=5 * 1024)
            Dim dataList As StringBuilder = New StringBuilder(capacity:=128)
            Dim color As StringBuilder = New StringBuilder(capacity:=128)

            For i As Integer = 0 To Serials.Length - 1
                Dim x As Serial = Serials(i)
                Dim objName As String = x.Name.NormalizePathString
                objName = objName.Replace(" ", "_")

                Call R.AppendLine(String.Format("d{0} <- c({1});", i, x.Vector))
                Call color.AppendFormat("""{0}"",", x.Color)
                Call dataList.AppendFormat("{0}=d{1},", objName, i)

                If Not String.Equals(x.Name, objName) Then
                    Call $"{x.Name} => '{objName}'".__DEBUG_ECHO
                End If
            Next
            Call color.Remove(color.Length - 1, 1)
            Call dataList.Remove(dataList.Length - 1, 1)

            Call R.AppendLine(String.Format("input_data <- list({0});", dataList.ToString))
            Call R.AppendLine(String.Format("output_image_file <- ""{0}"";", saveTiff.Replace("\", "/")))
            Call R.AppendLine(String.Format("title <- ""{0}"";", Title))
            Call R.AppendLine(String.Format("fill_color <- c({0});", color.ToString))
            Call R.AppendLine($"venn.diagram(input_data,fill=fill_color,filename=output_image_file,width={Resolution.Width},height={Resolution.Height},main=title);")

            Return R.ToString
        End Function
    End Class
End Namespace