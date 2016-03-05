Imports Microsoft.VisualBasic.DocumentFormat.Csv.StorageProvider.Reflection
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Namespace utils.read.table

    ''' <summary>
    ''' [read.table]
    ''' Reads a file in table format and creates a data frame from it, with cases corresponding to lines and variables to fields in the file.
    ''' </summary>
    ''' 
    <RFunc("read.table")>
    Public Class readTable : Inherits IRToken

        <Parameter("file", ValueTypes.Path)> Public Property file As String
        Public Property header As Boolean = False
        Public Property sep As String = ""
        Public Property quote As String = "\""'"
        Public Property dec As String = "."
        Public Property numerals As RExpression = c("allow.loss", "warn.loss", "no.loss")
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
    End Class

    <RFunc("read.csv")>
    Public Class readcsv : Inherits IRToken

        <Parameter("file", ValueTypes.Path)> Public Property file As String
        Public Property header As Boolean = True
        Public Property sep As String = ","
        Public Property quote As String = "\"""
        Public Property dec As String = "."
        Public Property fill As Boolean = True
        <Parameter("comment.char")> Public Property commentChar As String = ""
    End Class

    <RFunc("read.csv2")>
    Public Class readcsv2 : Inherits IRToken

        <Parameter("file", ValueTypes.Path)> Public Property file As String
        Public Property header As Boolean = True
        Public Property sep As String = ";"
        Public Property quote As String = "\"""
        Public Property dec As String = ","
        Public Property fill As Boolean = True
        <Parameter("comment.char")> Public Property commentChar As String = ""
    End Class

    <RFunc("read.delim")>
    Public Class readdelim : Inherits IRToken

        <Parameter("file", ValueTypes.Path)> Public Property file As String
        Public Property header As Boolean = True
        Public Property sep As String = "\t"
        Public Property quote As String = "\"""
        Public Property dec As String = "."
        Public Property fill As Boolean = True
        <Parameter("comment.char")> Public Property commentChar As String = ""
    End Class

    <RFunc("read.delim2")>
    Public Class readdelim2 : Inherits IRToken

        <Parameter("file", ValueTypes.Path)> Public Property file As String
        Public Property header As Boolean = True
        Public Property sep As String = "\t"
        Public Property quote As String = "\"""
        Public Property dec As String = ","
        Public Property fill As Boolean = True
        <Parameter("comment.char")> Public Property commentChar As String = ""
    End Class
End Namespace