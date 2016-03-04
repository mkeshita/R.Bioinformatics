Imports System.Windows.Forms
Imports SMRUCC.R.CRAN.Bioconductor.Web
Imports SMRUCC.R.CRAN.Bioconductor.Web.Packages

Public Class InstallPackage

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Sub New(repository As Repository)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Repository = repository
    End Sub

    Public ReadOnly Property Repository As Repository

    Private Sub InstallPackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not Repository Is Nothing Then
            Call TreeView1.Nodes.Add(__addNodes(Repository.softwares, BiocTypes.bioc))
            Call TreeView1.Nodes.Add(__addNodes(Repository.annotation, BiocTypes.annotation))
            Call TreeView1.Nodes.Add(__addNodes(Repository.experiment, BiocTypes.experiment))

            Text = $"BiocViews   master - {Repository.version.BiocLite}"
        End If
    End Sub

    Private Function __addNodes(packs As Package(), type As BiocTypes) As TreeNode
        Dim root As New TreeNode With {
            .Text = type.Description
        }
        Dim childs = (From x As Package In packs.AsParallel Select New TreeNode With {.Text = x.Package}).ToArray

        For Each child In childs
            Call root.Nodes.Add(child)
        Next

        Return root
    End Function
End Class