Imports System.Text
Imports RDotNet.Extensions.VisualBasic

Public Class Heatmap : Inherits IRScript

    Const df As String = "df"

    ''' <summary>
    ''' Column name of the row factor in the csv file that represents the row name. Default is the first column.
    ''' </summary>
    ''' <returns></returns>
    Public Property rowNameMaps As String
    ''' <summary>
    ''' Csv文件的文件路径
    ''' </summary>
    ''' <returns></returns>
    Public Property dataset As String

    ''' <summary>
    ''' Csv文件的分隔符，默认为逗号
    ''' </summary>
    ''' <returns></returns>
    Public Property delimiter As String = ","

    Private Function __getRowNames() As String
        If String.IsNullOrEmpty(rowNameMaps) Then

        End If
    End Function

    Public Overrides Function RScript() As String
        Dim script As StringBuilder = New StringBuilder()

    End Function
End Class
