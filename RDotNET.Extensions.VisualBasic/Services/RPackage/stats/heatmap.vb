Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace stats

    ''' <summary>
    ''' A heat map is a false color image (basically image(t(x))) with a dendrogram added to the left side and to the top. Typically, reordering of the rows and columns according to some set of values (row or column means) within the restrictions imposed by the dendrogram is carried out.
    ''' </summary>
    ''' <remarks>
    ''' If either Rowv or Colv are dendrograms they are honored (and not reordered). Otherwise, dendrograms are computed as dd &lt;- as.dendrogram(hclustfun(distfun(X))) where X is either x or t(x).
    ''' If either Is a vector (Of 'weights’) then the appropriate dendrogram is reordered according to the supplied values subject to the constraints imposed by the dendrogram, by reorder(dd, Rowv), in the row case. If either is missing, as by default, then the ordering of the corresponding dendrogram is by the mean value of the rows/columns, i.e., in the case of rows, Rowv &lt;- rowMeans(x, na.rm = na.rm). If either is NA, no reordering will be done for the corresponding side.
    ''' By Default (scale = "row") the rows are scaled to have mean zero And standard deviation one. There Is some empirical evidence from genomic plotting that this Is useful.
    ''' The Default colors are Not pretty. Consider Using enhancements such As the RColorBrewer package.
    ''' </remarks>
    <RFunc("heatmap")> Public Class heatmap : Inherits IRToken

        ''' <summary>
        ''' numeric matrix of the values to be plotted.
        ''' </summary>
        ''' <returns></returns>
        Public Property x As RExpression
        ''' <summary>
        ''' determines if and how the row dendrogram should be computed and reordered. Either a dendrogram or a vector of values used to reorder the row dendrogram or NA to suppress any row dendrogram (and reordering) or by default, NULL, see ‘Details’ below.
        ''' </summary>
        ''' <returns></returns>
        Public Property Rowv As RExpression = NULL
        ''' <summary>
        ''' determines if and how the column dendrogram should be reordered. Has the same options as the Rowv argument above and additionally when x is a square matrix, Colv = "Rowv" means that columns should be treated identically to the rows (and so if there is to be no row dendrogram there will not be a column one either).
        ''' </summary>
        ''' <returns></returns>
        Public Property Colv As RExpression = "if(symm)""Rowv"" else NULL"
        ''' <summary>
        ''' function used to compute the distance (dissimilarity) between both rows and columns. Defaults to dist.
        ''' </summary>
        ''' <returns></returns>
        Public Property distfun As RExpression = "dist"
        ''' <summary>
        ''' function used to compute the hierarchical clustering when Rowv or Colv are not dendrograms. Defaults to hclust. Should take as argument a result of distfun and return an object to which as.dendrogram can be applied.
        ''' </summary>
        ''' <returns></returns>
        Public Property hclustfun As RExpression = "hclust"
        ''' <summary>
        ''' function(d, w) of dendrogram and weights for reordering the row and column dendrograms. The default uses reorder.dendrogram.
        ''' </summary>
        ''' <returns></returns>
        Public Property reorderfun As RExpression = "function(d, w) reorder(d, w)"
        ''' <summary>
        ''' expression that will be evaluated after the call to image. Can be used to add components to the plot.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("add.expr")> Public Property addExpr As RExpression
        ''' <summary>
        ''' logical indicating if x should be treated symmetrically; can only be true when x is a square matrix.
        ''' </summary>
        ''' <returns></returns>
        Public Property symm As Boolean = False
        ''' <summary>
        ''' logical indicating if the column order should be reversed for plotting, such that e.g., for the symmetric case, the symmetry axis is as usual.
        ''' </summary>
        ''' <returns></returns>
        Public Property revC As RExpression = "identical(Colv, ""Rowv"")"
        ''' <summary>
        ''' character indicating if the values should be centered and scaled in either the row direction or the column direction, or none. The default is "row" if symm false, and "none" otherwise.
        ''' </summary>
        ''' <returns></returns>
        Public Property scale As RExpression = c("row", "column", "none")
        ''' <summary>
        ''' logical indicating whether NA's should be removed.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("na.rm")> Public Property naRM As Boolean = True
        ''' <summary>
        ''' numeric vector of length 2 containing the margins (see par(mar = *)) for column and row names, respectively.
        ''' </summary>
        ''' <returns></returns>
        Public Property margins As RExpression = c(5, 5)
        ''' <summary>
        ''' (optional) character vector of length ncol(x) containing the color names for a horizontal side bar that may be used to annotate the columns of x.
        ''' </summary>
        ''' <returns></returns>
        Public Property ColSideColors As RExpression
        ''' <summary>
        ''' (optional) character vector of length nrow(x) containing the color names for a vertical side bar that may be used to annotate the rows of x.
        ''' </summary>
        ''' <returns></returns>
        Public Property RowSideColors As RExpression
        ''' <summary>
        ''' positive numbers, used as cex.axis in for the row or column axis labeling. The defaults currently only use number of rows or columns, respectively.
        ''' </summary>
        ''' <returns></returns>
        Public Property cexRow As RExpression = "0.2 + 1 / log10(nr)"
        ''' <summary>
        ''' positive numbers, used as cex.axis in for the row or column axis labeling. The defaults currently only use number of rows or columns, respectively.
        ''' </summary>
        ''' <returns></returns>
        Public Property cexCol As RExpression = "0.2 + 1 / log10(nc)"
        ''' <summary>
        ''' character vectors with row and column labels to use; these default to rownames(x) or colnames(x), respectively.
        ''' </summary>
        ''' <returns></returns>
        Public Property labRow As RExpression = NULL
        ''' <summary>
        ''' character vectors with row and column labels to use; these default to rownames(x) or colnames(x), respectively.
        ''' </summary>
        ''' <returns></returns>
        Public Property labCol As RExpression = NULL
        ''' <summary>
        ''' main, x- and y-axis titles; defaults to none.
        ''' </summary>
        ''' <returns></returns>
        Public Property main As RExpression = NULL
        ''' <summary>
        ''' main, x- and y-axis titles; defaults to none.
        ''' </summary>
        ''' <returns></returns>
        Public Property xlab As RExpression = NULL
        ''' <summary>
        ''' main, x- and y-axis titles; defaults to none.
        ''' </summary>
        ''' <returns></returns>
        Public Property ylab As RExpression = NULL
        ''' <summary>
        ''' logical indicating if the dendrogram(s) should be kept as part of the result (when Rowv and/or Colv are not NA).
        ''' </summary>
        ''' <returns></returns>
        <Parameter("keep.dendro")> Public Property keepDendro As Boolean = False
        ''' <summary>
        ''' logical indicating if information should be printed.
        ''' </summary>
        ''' <returns></returns>
        Public Property verbose As RExpression = getOption("verbose")
    End Class
End Namespace