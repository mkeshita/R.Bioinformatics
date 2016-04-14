Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder

Namespace gplots

    Public Class jetColors : Inherits IRScript

        Protected Overrides Function __R_script() As String
            Dim script As ScriptBuilder = New ScriptBuilder
            Return script
        End Function
    End Class
End Namespace