Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Language

Namespace VennDiagram.ModelAPI

    Public Class Serial : Inherits ClassObject
        <XmlAttribute> Public Property Name As String
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
            Console.WriteLine("{0}(color: {1}) {2} counts.", Me.Name, Me.Color, Me.Vector.Split(CChar(",")).Count)
            Return Me
        End Function
    End Class
End Namespace