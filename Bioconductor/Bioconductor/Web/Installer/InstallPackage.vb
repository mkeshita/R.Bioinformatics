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

        ToolStripManager.Renderer = New ChromeUIRender
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

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Call Close()
    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        Call Process.Start("http://master.bioconductor.org/")
    End Sub

    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Call Process.Start("https://github.com/SMRUCC/R.Bioinformatics")
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect

    End Sub

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Dim sNode As TreeNode = e.Node
    End Sub
End Class