Imports Microsoft.VisualBasic.DocumentFormat.Csv.StorageProvider.Reflection
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace utils.read.table

    ''' <summary>
    ''' [read.table]
    ''' Reads a file in table format and creates a data frame from it, with cases corresponding to lines and variables to fields in the file.
    ''' </summary>
    Public Class readTable : Inherits IRProvider

        Public Property file As String
        Public Property header As Boolean = False
        Public Property sep As String = ""
        Public Property quote As String = "\""'"
        Public Property dec As String = "."
        Public Property numerals As String = c("allow.loss", "warn.loss", "no.loss")
        <Parameter("row.names")> Public Property rowNames As String
        <Parameter("col.names")> Public Property colNames As String
        <Parameter("as.is")> Public Property asIs As RExpression = "!stringsAsFactors"
        <Parameter("na.strings")> Public Property naStrings As String = NA
        Public Property colClasses As RExpression = NA
        Public Property nrows As Integer = -1
        Public Property skip As Integer = 0
        <Parameter("check.names")> Public Property checkNames As Boolean = True
        Public Property fill As RExpression = "!blank.lines.skip"
        <Parameter("strip.white")> Public Property stripWhite As Boolean = False
        <Parameter("blank.lines.skip")> Public Property blankLinesSkip As Boolean = True
        <Parameter("comment.char")> Public Property commentChar As String = "#"
        Public Property allowEscapes As Boolean = False
        Public Property flush As Boolean = False
        Public Property stringsAsFactors As RExpression = "default.stringsAsFactors()"
        Public Property fileEncoding As String = ""
        Public Property encoding As String = "unknown"
        Public Property text As String
        Public Property skipNul As Boolean = False

        Public Overrides Function RScript() As String
            Return Me.GetScript
        End Function
    End Class

    Public Class readcsv : Inherits IRProvider

        Public Property file
        Public Property header = True
        Public Property sep = Rstring(",")
        Public Property quote = Rstring("\""")
        Public Property dec = Rstring(".")
        Public Property fill As bool = bool.TRUE
        <Parameter("comment.char")> Public Property commentChar As String = Rstring("")

        Public Overrides Function RScript() As String

        End Function
    End Class

    Public Class readcsv2
        Public Property file
        Public Property header = True
        Public Property sep = ";"
        Public Property quote = "\"""
        Public Property dec = Rstring(",")
        Public Property fill = True
        <Parameter("comment.char")> Public Property commentChar = Rstring("")
    End Class

    Public Class readdelim
        Public Property file
        Public Property header = True
        Public Property sep = "\t"
        Public Property quote = "\"""
        Public Property dec = "."
        Public Property fill = True
        <Parameter("comment.char")> Public Property commentChar = ""
    End Class

    Public Class readdelim2
        Public Property file
        Public Property header = True
        Public Property sep = "\t"
        Public Property quote = "\"""
        Public Property dec = ","
        Public Property fill = True
        <Parameter("comment.char")> Public Property commentChar = ""
    End Class
End Namespace