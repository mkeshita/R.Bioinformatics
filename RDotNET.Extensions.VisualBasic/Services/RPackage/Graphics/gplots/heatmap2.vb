Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace gplots

    ''' <summary>
    ''' A heat map is a false color image (basically image(t(x))) with a dendrogram added to the left side and/or to the top. Typically, reordering of the rows and columns according to some set of values (row or column means) within the restrictions imposed by the dendrogram is carried out.
    ''' This heatmap provides a number Of extensions To the standard R heatmap Function.
    ''' </summary>
    <RFunc("heatmap.2")> Public Class heatmap2 : Inherits IRToken

        ''' <summary>
        ''' numeric matrix of the values to be plotted.
        ''' </summary>
        ''' <returns></returns>
        Public Property x As RExpression

        ' # dendrogram control

        ''' <summary>
        ''' determines if and how the row dendrogram should be reordered.	By default, it is TRUE, which implies dendrogram is computed and reordered based on row means. If NULL or FALSE, then no dendrogram is computed and no reordering is done. If a dendrogram, then it is used "as-is", ie without any reordering. If a vector of integers, then dendrogram is computed and reordered based on the order of the vector.
        ''' </summary>
        ''' <returns></returns>
        Public Property Rowv As Boolean = True
        ''' <summary>
        ''' determines if and how the column dendrogram should be reordered.	Has the options as the Rowv argument above and additionally when x is a square matrix, Colv="Rowv" means that columns should be treated identically to the rows.
        ''' </summary>
        ''' <returns></returns>
        Public Property Colv As RExpression = "if(symm)""Rowv"" else TRUE"
        ''' <summary>
        ''' function used to compute the distance (dissimilarity) between both rows and columns. Defaults to dist.
        ''' </summary>
        ''' <returns></returns>
        Public Property distfun As RExpression = "dist"
        ''' <summary>
        ''' function used to compute the hierarchical clustering When Rowv Or Colv are Not dendrograms. Defaults To hclust.
        ''' </summary>
        ''' <returns></returns>
        Public Property hclustfun As RExpression = "hclust"
        ''' <summary>
        ''' character string indicating whether to draw 'none', 'row', 'column' or 'both' dendrograms. Defaults to 'both'. However, if Rowv (or Colv) is FALSE or NULL and dendrogram is 'both', then a warning is issued and Rowv (or Colv) arguments are honoured.
        ''' </summary>
        ''' <returns></returns>
        Public Property dendrogram As String = c("both", "row", "column", "none")
        ''' <summary>
        ''' function(d, w) of dendrogram and weights for reordering the row and column dendrograms. The default uses stats{reorder.dendrogram}
        ''' </summary>
        ''' <returns></returns>
        Public Property reorderfun As RExpression = "function(d, w) reorder(d, w)"
        ''' <summary>
        ''' logical indicating if x should be treated symmetrically; can only be true when x is a square matrix.
        ''' </summary>
        ''' <returns></returns>
        Public Property symm As Boolean = False

        ' # data scaling

        ''' <summary>
        ''' character indicating if the values should be centered and scaled in either the row direction or the column direction, or none. The default is "none".
        ''' </summary>
        ''' <returns></returns>
        Public Property scale As RExpression = c("none", "row", "column")
        ''' <summary>
        ''' logical indicating whether NA's should be removed.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("na.rm")> Public Property naRM As Boolean = True

        ' # image plot

        ''' <summary>
        ''' logical indicating if the column order should be reversed for plotting, such that e.g., for the symmetric case, the symmetry axis is as usual.
        ''' </summary>
        ''' <returns></returns>
        Public Property revC As RExpression = "identical(Colv, ""Rowv"")"
        ''' <summary>
        ''' expression that will be evaluated after the call to image. Can be used to add components to the plot.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("add.expr")> Public Property addExpr As RExpression

        ' # mapping data to colors
        ''' <summary>
        ''' (optional) Either a numeric vector indicating the splitting points for binning x into colors, or a integer number of break points to be used, in which case the break points will be spaced equally between min(x) and max(x).
        ''' </summary>
        ''' <returns></returns>
        Public Property breaks As RExpression
        ''' <summary>
        ''' Boolean indicating whether breaks should be made symmetric about 0. Defaults to TRUE if the data includes negative values, and to FALSE otherwise.
        ''' </summary>
        ''' <returns></returns>
        Public Property symbreaks As RExpression = "any(x < 0, na.rm = TRUE) || scale!=""none"""

        ' # colors
        ''' <summary>
        ''' colors used for the image. Defaults to heat colors (heat.colors).
        ''' </summary>
        ''' <returns></returns>
        Public Property col As String = "heat.colors"

        ' # block sepration
        ''' <summary>
        ''' (optional) vector of integers indicating which columns or rows should be separated from the preceding columns or rows by a narrow space of color sepcolor.
        ''' </summary>
        ''' <returns></returns>
        Public Property colsep As RExpression
        ''' <summary>
        ''' (optional) vector of integers indicating which columns or rows should be separated from the preceding columns or rows by a narrow space of color sepcolor.
        ''' </summary>
        ''' <returns></returns>
        Public Property rowsep As RExpression
        ''' <summary>
        ''' (optional) vector of integers indicating which columns or rows should be separated from the preceding columns or rows by a narrow space of color sepcolor.
        ''' </summary>
        ''' <returns></returns>
        Public Property sepcolor As String = "white"
        ''' <summary>
        ''' (optional) Vector of length 2 giving the width (colsep) or height (rowsep) the separator box drawn by colsep and rowsep as a function of the width (colsep) or height (rowsep) of a cell. Defaults to c(0.05, 0.05)
        ''' </summary>
        ''' <returns></returns>
        Public Property sepwidth As RExpression = c(0.05, 0.05)

        ' # cell labeling
        ''' <summary>
        ''' (optional) matrix of character strings which will be placed within each color cell, e.g. p-value symbols.
        ''' </summary>
        ''' <returns></returns>
        Public Property cellnote As RExpression
        ''' <summary>
        ''' (optional) numeric scaling factor for cellnote items.
        ''' </summary>
        ''' <returns></returns>
        Public Property notecex As Double = 1.0
        ''' <summary>
        ''' (optional) character string specifying the color for cellnote text. Defaults to "cyan".
        ''' </summary>
        ''' <returns></returns>
        Public Property notecol As String = "cyan"
        ''' <summary>
        ''' Color to use for missing value (NA). Defaults to the plot background color.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("na.color")> Public Property naColor As RExpression = par("bg")

        ' # level trace
        ''' <summary>
        ''' character string indicating whether a solid "trace" line should be drawn across 'row's or down 'column's, 'both' or 'none'. The distance of the line from the center of each color-cell is proportional to the size of the measurement. Defaults to 'column'.
        ''' </summary>
        ''' <returns></returns>
        Public Property trace As RExpression = c("column", "row", "both", "none")
        ''' <summary>
        ''' character string giving the color for "trace" line. Defaults to "cyan".
        ''' </summary>
        ''' <returns></returns>
        Public Property tracecol As String = "cyan"
        ''' <summary>
        ''' Vector of values within cells where a horizontal or vertical dotted line should be drawn. The color of the line is controlled by linecol. Horizontal lines are only plotted if trace is 'row' or 'both'. Vertical lines are only drawn if trace 'column' or 'both'. hline and vline default to the median of the breaks, linecol defaults to the value of tracecol.
        ''' </summary>
        ''' <returns></returns>
        Public Property hline As RExpression = median("breaks")
        ''' <summary>
        ''' Vector of values within cells where a horizontal or vertical dotted line should be drawn. The color of the line is controlled by linecol. Horizontal lines are only plotted if trace is 'row' or 'both'. Vertical lines are only drawn if trace 'column' or 'both'. hline and vline default to the median of the breaks, linecol defaults to the value of tracecol.
        ''' </summary>
        ''' <returns></returns>
        Public Property vline As RExpression = median("breaks")
        ''' <summary>
        ''' Vector of values within cells where a horizontal or vertical dotted line should be drawn. The color of the line is controlled by linecol. Horizontal lines are only plotted if trace is 'row' or 'both'. Vertical lines are only drawn if trace 'column' or 'both'. hline and vline default to the median of the breaks, linecol defaults to the value of tracecol.
        ''' </summary>
        ''' <returns></returns>
        Public Property linecol As RExpression = "tracecol"

        ' # Row/Column Labeling
        ''' <summary>
        ''' numeric vector of length 2 containing the margins (see par(mar= *)) for column and row names, respectively.
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
        '''	angle of row/column labels, in degrees from horizontal
        ''' </summary>
        ''' <returns></returns>
        Public Property srtRow As RExpression = NULL
        ''' <summary>
        ''' angle of row/column labels, in degrees from horizontal
        ''' </summary>
        ''' <returns></returns>
        Public Property srtCol As RExpression = NULL
        ''' <summary>
        ''' 2-element vector giving the (left-right, top-bottom) justification of row/column labels (relative to the text orientation).
        ''' </summary>
        ''' <returns></returns>
        Public Property adjRow As RExpression = c(0, NA)
        ''' <summary>
        ''' 2-element vector giving the (left-right, top-bottom) justification of row/column labels (relative to the text orientation).
        ''' </summary>
        ''' <returns></returns>
        Public Property adjCol As RExpression = c(NA, 0)
        ''' <summary>
        ''' Number of character-width spaces to place between row/column labels and the edge of the plotting region.
        ''' </summary>
        ''' <returns></returns>
        Public Property offsetRow As Double = 0.5
        ''' <summary>
        ''' Number of character-width spaces to place between row/column labels and the edge of the plotting region.
        ''' </summary>
        ''' <returns></returns>
        Public Property offsetCol As Double = 0.5
        ''' <summary>
        ''' color of row/column labels, either a scalar to set the color of all labels the same, or a vector providing the colors of each label item
        ''' </summary>
        ''' <returns></returns>
        Public Property colRow As RExpression = NULL
        ''' <summary>
        ''' color of row/column labels, either a scalar to set the color of all labels the same, or a vector providing the colors of each label item
        ''' </summary>
        ''' <returns></returns>
        Public Property colCol As RExpression = NULL

        ' # color key + density info
        ''' <summary>
        ''' logical indicating whether a color-key should be shown.
        ''' </summary>
        ''' <returns></returns>
        Public Property key As Boolean = True
        ''' <summary>
        ''' numeric value indicating the size of the key
        ''' </summary>
        ''' <returns></returns>
        Public Property keysize As Double = 1.5
        ''' <summary>
        ''' character string indicating whether to superimpose a 'histogram', a 'density' plot, or no plot ('none') on the color-key.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("density.info")> Public Property densityInfo As RExpression = c("histogram", "density", "none")
        ''' <summary>
        ''' character string giving the color for the density display specified by density.info, defaults to the same value as tracecol.
        ''' </summary>
        ''' <returns></returns>
        Public Property denscol As RExpression = "tracecol"
        ''' <summary>
        ''' Boolean indicating whether the color key should be made symmetric about 0. Defaults to TRUE if the data includes negative values, and to FALSE otherwise.
        ''' </summary>
        ''' <returns></returns>
        Public Property symkey As RExpression = "any(x < 0, na.rm = TRUE) || symbreaks"
        ''' <summary>
        ''' Numeric scaling value for tuning the kernel width when a density plot is drawn on the color key. (See the adjust parameter for the density function for details.) Defaults to 0.25.
        ''' </summary>
        ''' <returns></returns>
        Public Property densadj As Double = 0.25
        ''' <summary>
        ''' main title of the color key. If set to NA no title will be plotted.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("key.title")> Public Property keyTitle As RExpression = NULL
        ''' <summary>
        ''' x axis label of the color key. If set to NA no label will be plotted.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("key.xlab")> Public Property keyxlab As RExpression = NULL
        ''' <summary>
        ''' y axis label of the color key. If set to NA no label will be plotted.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("key.ylab")> Public Property keyylab As RExpression = NULL
        ''' <summary>
        ''' function computing tick location and labels for the xaxis of the color key. Returns a named list containing parameters that can be passed to axis. See examples.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("key.xtickfun")> Public Property keyxtickfun As RExpression = NULL
        ''' <summary>
        ''' function computing tick location and labels for the y axis of the color key. Returns a named list containing parameters that can be passed to axis. See examples.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("key.ytickfun")> Public Property keyytickfun As RExpression = NULL
        ''' <summary>
        ''' graphical parameters for the color key. Named list that can be passed to par.
        ''' </summary>
        ''' <returns></returns>
        <Parameter("key.par")> Public Property keyPar As RExpression = "list()"

        ' # plot labels
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

        ' # plot layout
        ''' <summary>
        ''' visual layout: position matrix, column height, column width. See below for details
        ''' </summary>
        ''' <returns></returns>
        Public Property lmat As RExpression = NULL
        ''' <summary>
        ''' visual layout: position matrix, column height, column width. See below for details
        ''' </summary>
        ''' <returns></returns>
        Public Property lhei As RExpression = NULL
        ''' <summary>
        ''' visual layout: position matrix, column height, column width. See below for details
        ''' </summary>
        ''' <returns></returns>
        Public Property lwid As RExpression = NULL

        ' # extras
        ''' <summary>
        ''' A function to be called after all other work. See examples.
        ''' </summary>
        ''' <returns></returns>
        Public Property extrafun As RExpression = NULL

    End Class
End Namespace