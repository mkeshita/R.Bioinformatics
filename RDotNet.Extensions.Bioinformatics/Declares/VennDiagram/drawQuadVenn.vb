Imports RDotNet.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNet.Extensions.VisualBasic
Imports RDotNet.Extensions.VisualBasic.Services.ScriptBuilder.RTypes
Imports RDotNet.Extensions.VisualBasic.Services

Namespace VennDiagram

    ''' <summary>
    '''Creates a Venn diagram with four sets.
    ''' </summary>
    ''' <remarks>
    ''' The function defaults to placing the ellipses so that area1 corresponds to lower left,
    ''' area2 corresponds to lower right, area3 corresponds to middle left and area4 corresponds
    ''' to middle right. Refer to the example below to see how the 31 partial areas are ordered.
    ''' Arguments with length of 15 (label.col, cex, fontface, fontfamily) will follow the order
    ''' in the example.
    '''
    ''' Value
    '''
    ''' Returns an Object Of Class gList containing the grid objects that make up the diagram.
    ''' Also displays the diagram In a graphical device unless specified With ind = False.
    ''' Grid:grid.draw can be used to draw the gList object in a graphical device.
    ''' </remarks>
    <RFunc("draw.quad.venn")> Public Class drawQuadVenn : Inherits IRToken

        ''' <summary>
        '''
        ''' </summary>
        ''' <returns></returns>
        Public Property area1 As RExpression
        Public Property area2 As RExpression
        Public Property area3 As RExpression
        Public Property area4 As RExpression
        Public Property n12 As RExpression
        Public Property n13 As RExpression
        Public Property n14 As RExpression
        Public Property n23 As RExpression
        Public Property n24 As RExpression
        Public Property n34 As RExpression
        Public Property n123 As RExpression
        Public Property n124 As RExpression
        Public Property n134 As RExpression
        Public Property n234 As RExpression
        Public Property n1234 As RExpression
        Public Property category As RExpression = rep("", 4)
        Public Property lwd As RExpression = rep(2, 4)
        Public Property lty As RExpression = rep("solid", 4)
        Public Property col As RExpression = rep("black", 4)
        Public Property fill As RExpression = NULL
        Public Property alpha As RExpression = rep(0.5, 4)
        <Parameter("label.col")> Public Property labelCol As RExpression = rep("black", 15)
        Public Property cex As RExpression = rep(1, 15)
        Public Property fontface As RExpression = rep("plain", 15)
        Public Property fontfamily As RExpression = rep("serif", 15)
        <Parameter("cat.pos")> Public Property catPos = c(-15, 15, 0, 0)
        <Parameter("cat.dist")> Public Property catDist As RExpression = c(0.22, 0.22, 0.11, 0.11)
        <Parameter("cat.col")> Public Property catCol As RExpression = rep("black", 4)
        <Parameter("cat.cex")> Public Property catCex As RExpression = rep(1, 4)
        <Parameter("cat.fontface")> Public Property catFontface As RExpression = rep("plain", 4)
        <Parameter("cat.fontfamily")> Public Property catFontfamily = rep("serif", 4)
        <Parameter("cat.just")> Public Property catJust As RExpression = rep(List(c(0.5, 0.5)), 4)
        <Parameter("rotation.degree")> Public Property rotationDegree As RExpression = 0
        <Parameter("rotation.centre")> Public Property rotationCentre As RExpression = c(0.5, 0.5)
        Public Property ind As Boolean = True
        <Parameter("cex.prop")> Public Property cexProp As RExpression = NULL
        <Parameter("print.mode")> Public Property printMode As String = "raw"
        Public Property sigdigs As Integer = 3
        <Parameter("direct.area")> Public Property directArea As Boolean = False
        <Parameter("area.vector")> Public Property areaVector As Integer = 0
    End Class
End Namespace