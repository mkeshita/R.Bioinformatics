Imports RDotNet.Extensions.VisualBasic
Imports RDotNet.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNet.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace VennDiagram

    ''' <summary>
    ''' This function takes a list and creates a publication-quality TIFF Venn Diagram
    ''' </summary>
    <RFunc("venn.diagram")> Public Class vennDiagram : Inherits IRToken
        Public Property x As RExpression
        <Parameter("filename", ValueTypes.Path)> Public Property filename As String
        Public Property height As Integer = 3000
        Public Property width As Integer = 3000
        Public Property resolution As Integer = 500
        Public Property imagetype As String = "tiff"
        Public Property units As String = "px"
        Public Property compression As String = "lzw"
        Public Property na As String = "stop"
        Public Property main As RExpression = NULL
        Public Property [sub] As RExpression = NULL
        <Parameter("main.pos")> Public Property mainPos As RExpression = c(0.5, 1.05)
        <Parameter("main.fontface")> Public Property mainFontface As String = "plain"
        <Parameter("main.fontfamily")> Public Property mainFontfamily As String = "serif"
        <Parameter("main.col")> Public Property mainCol As String = "black"
        <Parameter("main.cex")> Public Property mainCex As Integer = 1
        <Parameter("main.just")> Public Property mainJust As RExpression = c(0.5, 1)
        <Parameter("sub.pos")> Public Property subPos As RExpression = c(0.5, 1.05)
        <Parameter("sub.fontface")> Public Property subFontface As String = "plain"
        <Parameter("sub.fontfamily")> Public Property subFontfamily As String = "serif"
        <Parameter("sub.col")> Public Property subCol As String = "black"
        <Parameter("sub.cex")> Public Property subCex As Integer = 1
        <Parameter("sub.just")> Public Property subJust As RExpression = c(0.5, 1)
        <Parameter("category.names")> Public Property categoryNames As RExpression = names(x)
        <Parameter("force.unique")> Public Property forceUnique As Boolean = True
        <Parameter("print.mode")> Public Property printMode As String = "raw"
        Public Property sigdigs As Integer = 3
        <Parameter("direct.area")> Public Property directArea As Boolean = False
        <Parameter("area.vector")> Public Property areaVector As Integer = 0
        <Parameter("hyper.test")> Public Property hyperTest As Boolean = False
        <Parameter("total.population")> Public Property totalPopulation As RExpression = NULL
    End Class
End Namespace