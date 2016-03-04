Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.Serialization
Imports SMRUCC.R.CRAN.Bioconductor.Web.Packages

Namespace Web

    Public Class Repository : Implements ISaveHandle

        Public Property version As Version
        Public Property softwares As Package()
        Public Property annotation As Package()
        Public Property experiment As Package()

        Public Shared ReadOnly Property DefaultFile As String =
            App.ProductSharedDIR & "/biocLite.json"

        Public Overrides Function ToString() As String
            Return version.ToString
        End Function

        Public Shared Function Load(file As String) As Repository
            Try
                Return LoadJsonFile(Of Repository)(file)
            Catch ex As Exception
                ex = New Exception(file, ex)

                Dim __new As New Repository
                Call __new.Save(file, Encodings.ASCII)
                Return __new
            End Try
        End Function

        Public Shared Function LoadDefault() As Repository
            Return Load(DefaultFile)
        End Function

        Public Function Save(Optional Path As String = "", Optional encoding As Encoding = Nothing) As Boolean Implements ISaveHandle.Save
            Return Me.GetJson.SaveTo(Path, encoding)
        End Function

        Public Function Save(Optional Path As String = "", Optional encoding As Encodings = Encodings.UTF8) As Boolean Implements ISaveHandle.Save
            Return Me.Save(Path, encoding.GetEncodings)
        End Function
    End Class
End Namespace