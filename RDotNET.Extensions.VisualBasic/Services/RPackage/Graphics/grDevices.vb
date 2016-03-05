Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace grDevices

    Public MustInherit Class grDevice : Inherits IRToken
        Public Property filename As String = "Rplot%03d.bmp"
        Public Property width As Integer = 480
        Public Property height As Integer = 480
        Public Property units As String = "px"
        Public Property pointsize As Integer = 12
        Public Property bg As String = "white"
        Public Property res As RExpression = NA
        Public Property family As String = ""
        Public Property restoreConsole As Boolean = True
        Public Overridable Property type As RExpression = c("windows", "cairo")
        Public Property antialias As RExpression
    End Class

    <RFunc("bmp")> Public Class bmp : Inherits grDevice
    End Class

    <RFunc("jpeg")> Public Class jpeg : Inherits grDevice

        Public Property quality As Integer = 75
    End Class

    <RFunc("png")> Public Class png : Inherits grDevice
        Public Overrides Property type As RExpression = c("windows", "cairo", "cairo-png")
    End Class

    Public Class tiff : Inherits grDevice

        Public Property compression As RExpression = c("none", "rle", "lzw", "jpeg", "zip", "lzw+p", "zip+p")
    End Class
End Namespace
