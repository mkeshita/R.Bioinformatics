Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic
Imports RDotNet.Extensions.VisualBasic

Namespace bnlearn

    Public Class NetworkParameters : Inherits bnlearn

        Dim BayesianNetworkObject As String
        Dim R As RDotNet.Extensions.VisualBasic.REngine

        ''' <summary>
        ''' 贝叶斯网络对象的变量名
        ''' </summary>
        ''' <param name="ObjectName"></param>
        ''' <remarks></remarks>
        Sub New(ObjectName As String, R As REngine)
            BayesianNetworkObject = ObjectName
            Me.R = R
        End Sub

        ''' <summary>
        ''' Get Bayesian network parameters
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        Public Function GetNetworkParameters(numberOfFactors As Integer) As ConditionalProbability()
            Call R.WriteLine("library(bnlearn); pdag = iamb(learning.test) ; dag = set.arc(pdag, from = ""A"", to = ""B"") ; fit = bn.fit(dag, learning.test, method = ""bayes"") ;")
            Call R.WriteLine("fit")
            Dim DataChunk As String() = R.StandardOutput
            Dim LQuery = (From strData As String In DataChunk Select ConditionalProbability.TryParse(strData.Replace(vbCr, "").Replace(vbLf, ""), numberOfFactors)).ToArray
            Return LQuery
        End Function

        Protected Overrides Function __R_script() As String
            Return BayesianNetworkObject
        End Function
    End Class
End Namespace