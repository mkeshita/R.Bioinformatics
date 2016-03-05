Imports System.Text
Imports Microsoft.VisualBasic.Linq.Extensions

''' <summary>
''' R脚本的数据模型对象的接口
''' </summary>
''' <remarks></remarks>
Public MustInherit Class IRScript : Inherits IRProvider
    Implements IDisposable
    Implements IScriptProvider

    ''' <summary>
    ''' The package names that required of this script file.
    ''' (需要加载的R的包的列表)
    ''' </summary>
    ''' <returns></returns>
    Public Overridable ReadOnly Property Requires As String()

    ''' <summary>
    ''' 保存脚本文件到文件系统之上
    ''' </summary>
    ''' <param name="FilePath"></param>
    ''' <returns></returns>
    Public Overridable Function SaveTo(FilePath As String) As Boolean
        If String.IsNullOrEmpty(FilePath) Then
            Return False
        Else
            Return __save(FilePath)
        End If
    End Function

    Private Function __save(path As String) As Boolean
        Dim libraries As String() = Requires.ToArray(Function(name) $"library({name})")
        Dim Rscript As String = libraries.JoinBy(vbCrLf) & vbCrLf & vbCrLf & Me.RScript
        Return Rscript.SaveTo(path, Encoding.ASCII)  ' 好像R只能够识别ASCII的脚本文件
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose( disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose( disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
