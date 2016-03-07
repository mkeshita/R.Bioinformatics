Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData

Namespace WGCNA

    <PackageNamespace("bioc.WGCNA")>
    Public Module ScriptAPI

        <ExportAPI("WGCNA")>
        Public Function BuildScript() As WGCNAScript

        End Function
    End Module
End Namespace
