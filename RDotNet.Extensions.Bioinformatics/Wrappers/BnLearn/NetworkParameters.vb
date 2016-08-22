Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Language
Imports RDotNET.Extensions.VisualBasic

Namespace bnlearn

    Public Class NetworkParameters : Inherits bnlearn

        Dim BayesianNetworkObject As String

        ''' <summary>
        ''' 贝叶斯网络对象的变量名
        ''' </summary>
        ''' <param name="ObjectName"></param>
        ''' <remarks></remarks>
        Sub New(ObjectName As String)
            BayesianNetworkObject = ObjectName
        End Sub

        ''' <summary>
        ''' Get Bayesian network parameters
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        Public Function GetNetworkParameters(numberOfFactors As Integer) As ConditionalProbability()

            Call "library(bnlearn); 
pdag = iamb(learning.test); 
dag = set.arc(pdag, from = ""A"", to = ""B""); 
fit = bn.fit(dag, learning.test, method = ""bayes"");".ζ

            Dim LQuery = LinqAPI.Exec(Of ConditionalProbability) <=
                From s As String
                In "fit".ζ.AsCharacter.ToArray
                Let Data As String = s.Replace(vbCr, "").Replace(vbLf, "")
                Select ConditionalProbability.TryParse(Data, numberOfFactors)
            Return LQuery
        End Function

        Protected Overrides Function __R_script() As String
            Return BayesianNetworkObject
        End Function
    End Class
End Namespace