Imports RDotNet.Extensions.VisualBasic
Imports RDotNet.Extensions.VisualBasic.Services.ScriptBuilder

Namespace VennDiagram

    ''' <summary>
    ''' This function takes a list and creates a publication-quality TIFF Venn Diagram
    ''' </summary>
    <RFunc("venn.diagram")> Public Class vennDiagram : Inherits IRToken
        Public Property x
        Public Property filename
        Public Property height = 3000
        Public Property width = 3000
        Public Property resolution = 500
        Public Property imagetype = "tiff"
        Public Property units = "px"
        Public Property compression = "lzw"
        Public Property na = "stop"
        Public Property main = NULL
        Public Property [sub] = NULL
        Public Property main.pos    = c(0.5, 1.05)
                Public Property main.fontface = "plain"
                Public Property main.fontfamily = "serif"
                Public Property main.col = "black"
                Public Property main.cex = 1
                Public Property main.just = c(0.5, 1)
                Public Property sub.pos = c(0.5, 1.05)
        Public Property sub.fontface = "plain"
        Public Property sub.fontfamily = "serif"
        Public Property sub.col = "black"
        Public Property sub.cex = 1
        Public Property sub.just = c(0.5, 1)
        Public Property category.names = names(x)
                Public Property force.unique =    True
                Public Property print.mode = "raw"
                Public Property sigdigs = 3
        Public Property direct.area =    False
                Public Property area.vector = 0
                Public Property hyper.test = False
                Public Property total.population = NULL
    End Class
End Namespace