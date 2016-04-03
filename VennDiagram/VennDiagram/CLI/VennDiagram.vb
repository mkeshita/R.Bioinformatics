Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ConsoleDevice.STDIO

Imports RDotNET.Extensions.VisualBasic.RSystem
Imports RDotNET
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports Microsoft.VisualBasic.Linq.Extensions
Imports Microsoft.VisualBasic.DocumentFormat.Csv
Imports RDotNET.Extensions.VisualBasic
Imports RDotNET.Extensions.Bioinformatics.VennDiagram.ModelAPI

<PackageNamespace("VennTools.CLI", Category:=APICategories.CLI_MAN,
                  Description:="Tools for creating venn diagram model for the R program and venn diagram visualize drawing.",
                  Publisher:="xie.guigang@gmail.com",
                  Url:="http://gcmodeller.org")>
Public Module CLI

    <ExportAPI(".Draw",
               Info:="Draw the venn diagram from a csv data file, you can specific the diagram drawing options from this command switch value. " &
                     "The generated venn dragram will be saved as tiff file format.",
        Usage:=".Draw -i <csv_file> [-t <diagram_title> -o <_diagram_saved_path> -s <serials_option_pairs> -rbin <r_bin_directory>]",
        Example:=".Draw -i /home/xieguigang/Desktop/genomes.csv -t genome-compared -o ~/Desktop/xcc8004.tiff -s ""xcc8004,blue;ecoli,green;pa14,yellow;ftn,black;aciad,red""")>
    <ParameterInfo("-i",
        Description:="The csv data source file for drawing the venn diagram graph.",
        Example:="/home/xieguigang/Desktop/genomes.csv")>
    <ParameterInfo("-t", True,
        Description:="Optional, the venn diagram title text",
        Example:="genome-compared")>
    <ParameterInfo("-o", True,
        Description:="Optional, the saved file location for the venn diagram, if this switch value is not specific by the user then \n" &
                     "the program will save the generated venn diagram to user desktop folder and using the file name of the input csv file as default.",
        Example:="~/Desktop/xcc8004.tiff")>
    <ParameterInfo("-s", True,
        Description:="Optional, the profile settings for the serials in the venn diagram, each serial profile data is\n " &
                     "in a key value paired like: name,color, and each serial profile pair is seperated by a ';' character.\n" &
                     "If this switch value is not specific by the user then the program will trying to parse the serial name\n" &
                     "from the column values and apply for each serial a randomize color.",
        Example:="xcc8004,blue;ecoli,green;pa14,yellow;ftn,black;aciad,red")>
    <ParameterInfo("-rbin", True,
        Description:="Optional, Set up the r bin path for drawing the venn diagram, if this switch value is not specific by the user then \n" &
                     "the program just output the venn diagram drawing R script file in a specific location, or if this switch \n" &
                     "value is specific by the user and is valid for call the R program then will output both venn diagram tiff image " &
                     "file and R script for drawing the output venn diagram.\n" &
                     "This switch value is just for the windows user, when this program was running on a LINUX/UNIX/MAC platform operating \n" &
                     "system, you can ignore this switch value, but you should install the R program in your linux/MAC first if you wish to\n " &
                     "get the venn diagram directly from this program.",
        Example:="C:\\R\\bin\\")>
    Public Function VennDiagramA(args As CommandLine.CommandLine) As Integer
        Dim inds As String = args("-i")
        Dim title As String = args.GetValue("-t", inds.BaseName)
        Dim SerialsOption As String = args("-s")
        Dim out As String = args.GetValue("-o", App.Desktop & $"/{title}_venn.tiff")

        If Not inds.FileExists Then '-i开关参数无效
            Printf("Could not found the source file!")
            Return -1
        Else
            out = UnixPath(out, True)
        End If

        Dim dataset As DocumentStream.File = New DocumentStream.File(inds)
        Dim VennDiagram As VennDiagram = RModelAPI.Generate(source:=dataset)

        If String.IsNullOrEmpty(SerialsOption) Then '从原始数据中进行推测
            VennDiagram += From idx As Integer In dataset.Width.Sequence
                           Let column = (From s As String In dataset.Column(Index:=idx).AsParallel
                                         Where Not String.IsNullOrEmpty(s)
                                         Select s).ToArray
                           Select {column.ParseName(Serial:=idx), GetRandomColor()} '
        Else '从用户输入之中进行解析
            VennDiagram += From s As String In SerialsOption.Split(CChar(";")) Select s.Split(CChar(",")) '
        End If

        VennDiagram.Title = title
        VennDiagram.SaveFile = out

        Dim RBin As String = args("-rbin"), RScript As String = VennDiagram.RScript
        Dim EXPORT As String = FileIO.FileSystem.GetParentPath(out)
        EXPORT = $"{EXPORT}/{title.NormalizePathString}_venn.r"

        If String.IsNullOrEmpty(RBin) OrElse Not FileIO.FileSystem.DirectoryExists(RBin) Then
            Call TryInit()
        Else
            Call TryInit(RBin)
        End If

        Call RScript.SaveTo(EXPORT, Encodings.ASCII)
        Call RServer.source(EXPORT)
        Printf("The venn diagram r script were saved at location:\n '%s'", EXPORT)
        Call Process.Start(out)

        Return 0
    End Function

    Private Function GetRandomColor() As String
        Call VBMath.Randomize()
        Return RSystem.RColors(Rnd() * (RSystem.RColors.Length - 1))
    End Function
End Module
