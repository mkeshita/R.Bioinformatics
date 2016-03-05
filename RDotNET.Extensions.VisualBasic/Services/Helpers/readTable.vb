Imports Microsoft.VisualBasic.DocumentFormat.Csv.StorageProvider.Reflection

Namespace utils.read.table

    ' Reads a file in table format and creates a data frame from it, with cases corresponding to lines and variables to fields in the file.

    Public Class readTable

        Public Property file As String
        Public Property header As bool = bool.[FALSE]
        Public Property sep = ""
        Public Property quote = Rstring("\""'")
        Public Property dec = Rstring(".")
        Public Property numerals = c("allow.loss", "warn.loss", "no.loss")
        <Parameter("row.names")> Public Property rowNames
        <Parameter("col.names")> Public Property colNames
        <Parameter("as.is")> Public Property asIs = "!stringsAsFactors"
        <Parameter("na.strings")> Public Property naStrings = "NA"
        Public Property colClasses = na
        Public Property nrows = -1
        Public Property skip = 0
        Public Property check.names = True
                Public Property fill = !blank.lines.skip
        Public Property strip.white = False
                Public Property blank.lines.skip = True
                Public Property comment.Char = "#"
                Public Property allowEscapes = False
        Public Property flush = False
        Public Property stringsAsFactors = default.stringsAsFactors()
                Public Property fileEncoding = ""
        Public Property encoding = "unknown"
        Public Property text
        Public Property skipNul = False
    End Class
read.csv(file, header = TRUE, sep = ",", quote = "\"",
         dec = ".", fill = TRUE, comment.char = "", ...)

read.csv2(file, header = TRUE, sep = ";", quote = "\"",
          dec = ",", fill = TRUE, comment.char = "", ...)

read.delim(file, header = TRUE, sep = "\t", quote = "\"",
           dec = ".", fill = TRUE, comment.char = "", ...)

read.delim2(file, header = TRUE, sep = "\t", quote = "\"",
            dec = ",", fill = TRUE, comment.char = "", ...)
End Namespace