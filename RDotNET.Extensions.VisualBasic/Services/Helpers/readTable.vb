Imports Microsoft.VisualBasic.DocumentFormat.Csv.StorageProvider.Reflection

Namespace utils.read.table

    ''' <summary>
    ''' [read.table]
    ''' Reads a file in table format and creates a data frame from it, with cases corresponding to lines and variables to fields in the file.
    ''' </summary>
    Public Class readTable : Inherits IRProvider

        Public Property file As String
        Public Property header As bool = bool.[FALSE]
        Public Property sep = Rstring("")
        Public Property quote = Rstring("\""'")
        Public Property dec = Rstring(".")
        Public Property numerals = c("allow.loss", "warn.loss", "no.loss")
        <Parameter("row.names")> Public Property rowNames
        <Parameter("col.names")> Public Property colNames
        <Parameter("as.is")> Public Property asIs = "!stringsAsFactors"
        <Parameter("na.strings")> Public Property naStrings = Rstring("NA")
        Public Property colClasses = NA
        Public Property nrows = -1
        Public Property skip = 0
        <Parameter("check.names")> Public Property checkNames As bool = bool.TRUE
        Public Property fill = "!blank.lines.skip"
        <Parameter("strip.white")> Public Property stripWhite As bool = bool.FALSE
        <Parameter("blank.lines.skip")> Public Property blankLinesSkip As bool = bool.TRUE
        <Parameter("comment.char")> Public Property commentChar = Rstring("#")
        Public Property allowEscapes As bool = bool.FALSE
        Public Property flush As bool = bool.FALSE
        Public Property stringsAsFactors = "default.stringsAsFactors()"
        Public Property fileEncoding = Rstring("")
        Public Property encoding = Rstring("unknown")
        Public Property text
        Public Property skipNul As bool = bool.FALSE

        Public Overrides Function RScript() As String

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