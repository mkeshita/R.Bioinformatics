Imports System.Drawing
Imports System.Text
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.DocumentFormat.Csv
Imports Microsoft.VisualBasic.DocumentFormat.Csv.DocumentStream
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Linq.Extensions
Imports RDotNet.Extensions.VisualBasic
Imports RDotNet.Extensions.VisualBasic.Services.ScriptBuilder

Namespace VennDiagram.ModelAPI

    ''' <summary>
    ''' The data model of the venn diagram.(文氏图的数据模型)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VennDiagram : Inherits IRScript

        Dim __plot As vennDiagramPlot

        Public Property plot As vennDiagramPlot
            Get
                If __plot Is Nothing Then
                    __plot = New vennDiagramPlot("input_data", "fill_color", "title", "output_image_file")
                End If
                Return __plot
            End Get
            Set(value As vennDiagramPlot)
                __plot = value
            End Set
        End Property

        ''' <summary>
        ''' The title of the diagram.
        ''' </summary>
        ''' <returns></returns>
        <XmlIgnore> Public Property Title As String
            Get
                Return plot.main
            End Get
            Set(value As String)
                plot.main = Rstring(value)
            End Set
        End Property

        ''' <summary>
        ''' vennDiagram tiff file saved path.(所生成的文氏图的保存文件名)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlIgnore> Public Property saveTiff As String
            Get
                Return plot.filename
            End Get
            Set(value As String)
                plot.filename = value
            End Set
        End Property

        <XmlElement> Public Property Serials As Serial()
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

        ''' <summary>
        ''' Applying the diagram options
        ''' </summary>
        ''' <param name="venn"></param>
        ''' <param name="opts"></param>
        ''' <returns></returns>
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
            Dim R As ScriptBuilder = New ScriptBuilder(capacity:=5 * 1024)
            Dim dataList As StringBuilder = New StringBuilder(capacity:=128)
            Dim color As StringBuilder = New StringBuilder(capacity:=128)

            For i As Integer = 0 To Serials.Length - 1
                Dim x As Serial = Serials(i)
                Dim objName As String = x.Name.NormalizePathString

                R += $"d{i} <- c({x.Vector})"
                objName = objName.Replace(" ", "_")

                Call color.AppendFormat("""{0}"",", x.Color)
                Call dataList.AppendFormat("{0}=d{1},", objName, i)

                If Not String.Equals(x.Name, objName) Then
                    Call $"{x.Name} => '{objName}'".__DEBUG_ECHO
                End If
            Next
            Call color.RemoveLast
            Call dataList.RemoveLast

            R += $"input_data <- list({dataList.ToString})"
            R += $"fill_color <- c({color.ToString})"
            R += plot.Copy("input_data", "fill_color")

            Return R.ToString
        End Function
    End Class
End Namespace