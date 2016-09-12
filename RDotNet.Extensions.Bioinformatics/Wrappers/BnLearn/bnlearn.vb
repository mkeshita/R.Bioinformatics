Imports RDotNet.Extensions.VisualBasic

Namespace bnlearn

    ''' <summary>
    ''' ```R
    ''' require(bnlearn)
    ''' ```
    ''' </summary>
    Public MustInherit Class bnlearn : Inherits IRScript

        Sub New()
            Requires = {"bnlearn"}
        End Sub

    End Class
End Namespace