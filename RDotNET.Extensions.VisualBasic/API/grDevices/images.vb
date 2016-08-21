
Imports RDotNET.Extensions.VisualBasic.grDevices

Namespace API.grDevices

    Public Module images

        Public Sub tiff(Optional filename As String = "Rplot%03d.tif",
                        Optional width As Integer = 480,
                        Optional height As Integer = 480,
                        Optional units As String = "px",
                        Optional pointsize As Integer = 12,
                        Optional compression As String = "c(""none"", ""rle"", ""lzw"", ""jpeg"", ""zip"", ""lzw+p"", ""zip+p"")",
                        Optional bg As String = "white",
                        Optional res As String = "NA",
                        Optional family As String = "",
                        Optional restoreConsole As Boolean = True,
                        Optional type As String = "c(""windows"", ""cairo"")",
                        Optional antialias As String = NULL)

            Call New tiff With {
                .antialias = antialias,
                .filename = filename,
                .restoreConsole = restoreConsole,
                .family = family,
                .compression = compression,
                .bg = bg,
                .height = height,
                .pointsize = pointsize,
                .res = res,
                .type = type,
                .units = units,
                .width = width
            }.__call
        End Sub
    End Module
End Namespace