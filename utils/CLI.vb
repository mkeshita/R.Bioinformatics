Imports Microsoft.VisualBasic.CommandLine.Reflection

Module CLI

    <ExportAPI("/heatmap",
               Info:="Drawing a heatmap by using a matrix.",
               Example:="",
               Usage:="/heatmap /in <dataset.csv> [/out <out.tiff> /width 4000 /height 3000 /colors <RExpression>]")>
    <ParameterInfo("/in", False,
                   Description:="A matrix dataset, and first row in this csv file needs to be the property of the object and rows are the object entity. 
                   Example can be found at datasets: .../datasets/ppg2008.csv")>
    <ParameterInfo("/colors", True,
                   Description:="The color schema of your heatmap, default this parameter is null and using brewer.pal(10,""RdYlBu"") from RColorBrewer.
                   This value should be an R expression.")>
    Public Function heatmap(args As CommandLine.CommandLine) As Integer

    End Function
End Module
