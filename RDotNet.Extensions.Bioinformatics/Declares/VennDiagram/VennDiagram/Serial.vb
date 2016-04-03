Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Language

Namespace VennDiagram.ModelAPI

    ''' <summary>
    ''' A partition in the venn diagram.
    ''' </summary>
    Public Class Serial : Inherits ClassObject

        ''' <summary>
        ''' The name of this partition
        ''' </summary>
        ''' <returns></returns>
        <XmlAttribute> Public Property Name As String
        ''' <summary>
        ''' The color string of the partition
        ''' </summary>
        ''' <returns></returns>
        <XmlAttribute> Public Property Color As String

        ''' <summary>
        ''' 使用数字来表示成员的一个向量
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <XmlElement> Public Property Vector As String

        Sub New()
        End Sub

        Sub New(Name As String)
            Me.Name = Name
        End Sub

        Public Function ApplyOptions([Option] As String()) As Serial
            Name = [Option].First
            Color = [Option].Last
            Console.WriteLine("{0}(color: {1}) {2} counts.", Me.Name, Me.Color, Me.Vector.Split(CChar(",")).Length)
            Return Me
        End Function
    End Class
End Namespace