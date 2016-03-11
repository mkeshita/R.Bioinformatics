Imports RDotNET.Extensions.VisualBasic

Namespace WGCNA

    ''' <summary>
    ''' Functions necessary to perform Weighted Correlation Network Analysis. 
    ''' WGCNA is also known as weighted gene co-expression network analysis when dealing with gene expression data. 
    ''' Many functions of WGCNA can also be used for general association networks specified by a symmetric adjacency matrix.
    ''' </summary>
    Public Class WGCNAFunction : Inherits IRToken

        Sub New()
            Requires = {"WGCNA"}
        End Sub
    End Class
End Namespace