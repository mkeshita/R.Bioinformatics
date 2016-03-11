Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports RDotNET.Extensions.VisualBasic.base
Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder.RTypes

Public Module RScripts

    Public Const NULL As String = "NULL"
    ''' <summary>
    ''' "NA" 字符串，而不是NA空值常量
    ''' </summary>
    Public ReadOnly Property NA As RExpression = New RExpression("NA")
    Public Const [TRUE] As String = "TRUE"
    Public Const [FALSE] As String = "FALSE"

    ''' <summary>
    ''' Retrieve or set the dimension of an object.
    ''' </summary>
    ''' <param name="x">
    ''' an R object, for example a matrix, array or data frame.
    ''' For the default method, either NULL or a numeric vector, which is coerced to integer (by truncation).
    ''' </param>
    ''' <returns>
    ''' For an array (And hence in particular, for a matrix) dim retrieves the dim attribute of the object. It Is NULL Or a vector of mode integer.
    ''' The replacement method changes the "dim" attribute (provided the New value Is compatible) And removes any "dimnames" And "names" attributes.
    ''' </returns>
    ''' <remarks>
    ''' Details
    '''
    ''' The functions Dim And Dim&lt;- are internal generic primitive functions.
    ''' Dim has a method For data.frames, which returns the lengths Of the row.names attribute Of x And Of x (As the numbers Of rows And columns respectively).
    ''' </remarks>
    Public Function [dim](x As String) As String
        Return $"dim({x})"
    End Function

    ''' <summary>
    ''' Given a matrix or data.frame x, t returns the transpose of x.
    ''' </summary>
    ''' <param name="x">a matrix or data frame, typically.</param>
    ''' <returns>A matrix, with dim and dimnames constructed appropriately from those of x, and other attributes except names copied across.</returns>
    ''' <remarks>
    ''' This is a generic function for which methods can be written. The description here applies to the default and "data.frame" methods.
    ''' A data frame Is first coerced To a matrix: see as.matrix. When x Is a vector, it Is treated as a column, i.e., the result Is a 1-row matrix.
    ''' </remarks>
    Public Function t(x As String) As String
        Return $"t({x})"
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="file"></param>
    ''' <param name="extendsFull">是否转换为全路径？默认不转换</param>
    ''' <returns></returns>
    <Extension>
    Public Function UnixPath(file As String, Optional extendsFull As Boolean = False) As String
        If String.IsNullOrEmpty(file) Then
            Return ""
        End If
        If extendsFull Then
            file = FileIO.FileSystem.GetFileInfo(file).FullName
        End If
        Return file.Replace("\"c, "/"c)
    End Function

    ''' <summary>
    ''' c(....)
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Public Function c(ParamArray x As String()) As String
        Dim cx As String = String.Join(", ", x.ToArray(Function(s) $"""{s}"""))
        Return $"c({cx})"
    End Function

    ''' <summary>
    ''' c(....)
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Public Function c(ParamArray x As Object()) As String
        Dim cx As String = String.Join(",", x.ToArray(Function(o) Scripting.ToString(o)))
        Return $"c({cx})"
    End Function

    Public Function getOption(verbose As String) As String
        Return $"getOption(""{verbose}"")"
    End Function

    Public Function Rstring(s As String) As String
        Return $"""{s}"""
    End Function

    Public Function par(x As String) As String
        Return $"par(""{x}"")"
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="x">是一个对象，不是字符串</param>
    ''' <returns></returns>
    Public Function median(x As String) As String
        Return $"media({x})"
    End Function

    Public Function library([lib] As String) As String
        Return $"library({[lib]})"
    End Function

    Public Function names(x As String) As String
        Return $"names({x})"
    End Function

    ''' <summary>
    ''' ifelse returns a value with the same shape as test which is filled with elements selected from either yes or no depending on whether the element of test is TRUE or FALSE.
    ''' 
    ''' If yes or no are too short, their elements are recycled. yes will be evaluated if and only if any element of test is true, and analogously for no.
    ''' Missing values In test give missing values In the result.
    ''' </summary>
    ''' <param name="test">an object which can be coerced to logical mode.</param>
    ''' <param name="yes">Return values For True elements Of test.</param>
    ''' <param name="no">return values for false elements of test.</param>
    ''' <returns>
    ''' A vector of the same length and attributes (including dimensions and "class") as test and data values from the values of yes or no. 
    ''' The mode of the answer will be coerced from logical to accommodate first any values taken from yes and then any values taken from no.
    ''' </returns>
    Public Function ifelse(test As String, yes As String, no As String) As String
        Return New ifelse(test, yes, no)
    End Function

    ''' <summary>
    ''' Provides access to a copy of the command line arguments supplied when this R session was invoked.
    ''' </summary>
    ''' <param name="trailingOnly">logical. Should only arguments after --args be returned?</param>
    ''' <returns>
    ''' A character vector containing the name of the executable and the user-supplied command line arguments. 
    ''' The first element is the name of the executable by which R was invoked. 
    ''' The exact form of this element is platform dependent: it may be the fully qualified name, or simply the last component (or basename) of the application, or for an embedded R it can be anything the programmer supplied.
    ''' If trailingOnly = True, a character vector Of those arguments (If any) supplied after --args.
    ''' </returns>
    ''' <remarks>
    ''' These arguments are captured before the standard R command line processing takes place. This means that they are the unmodified values. 
    ''' This is especially useful with the --args command-line flag to R, as all of the command line after that flag is skipped.
    ''' </remarks>
    Public Function commandArgs(Optional trailingOnly As Boolean = False) As String
        Return $"commandArgs(trailingOnly = {New RBoolean(trailingOnly)})"
    End Function
End Module
