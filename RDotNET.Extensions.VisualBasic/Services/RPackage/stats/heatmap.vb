Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace stats

    <RFunc("heatmap")> Public Class heatmap : Inherits IRToken

        Public Property x As RExpression
        Public Property Rowv As RExpression = NULL
        Public Property Colv As RExpression = "if(symm)""Rowv"" else NULL"
        Public Property distfun As RExpression = "dist"
        Public Property hclustfun As RExpression = "hclust"
        Public Property reorderfun As RExpression = "function(d, w) reorder(d, w)"
        <Parameter("add.expr")> Public Property addExpr As RExpression
        Public Property symm As Boolean = False
        Public Property revC As RExpression = "identical(Colv, ""Rowv"")"
        Public Property scale As RExpression = c("row", "column", "none")
        <Parameter("na.rm")> Public Property naRM As Boolean = True
        Public Property margins As RExpression = c(5, 5)
        Public Property ColSideColors As RExpression
        Public Property RowSideColors As RExpression
        Public Property cexRow As RExpression = "0.2 + 1 / log10(nr)"
        Public Property cexCol As RExpression = "0.2 + 1 / log10(nc)"
        Public Property labRow As RExpression = NULL
        Public Property labCol As RExpression = NULL
        Public Property main As RExpression = NULL
        Public Property xlab As RExpression = NULL
        Public Property ylab As RExpression = NULL
        <Parameter("keep.dendro")> Public Property keepDendro As Boolean = False
        Public Property verbose As RExpression = getOption("verbose")
    End Class
End Namespace