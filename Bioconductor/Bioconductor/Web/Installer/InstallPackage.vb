Imports System.Windows.Forms
Imports RDotNET.Extensions.VisualBasic
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
    Public ReadOnly Property Current As Package

    Private Sub InstallPackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not Repository Is Nothing Then
            Call TreeView1.Nodes.Add(__addNodes(Repository.softwares, BiocTypes.bioc))
            Call TreeView1.Nodes.Add(__addNodes(Repository.annotation, BiocTypes.annotation))
            Call TreeView1.Nodes.Add(__addNodes(Repository.experiment, BiocTypes.experiment))

            Text = $"BiocViews   master - {Repository.version.BiocLite}"
        End If

        LinkLabel1.Enabled = False
        LinkLabel2.Enabled = False
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

    Dim __currVer As String

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Dim sNode As TreeNode = e.Node
        Dim pack As Package = Repository.GetPackage(sNode.Text)

        If Not pack Is Nothing Then
            _Current = pack

            Label2.Text = pack.Package
            Label3.Text = pack.Title
            Label5.Text = pack.Maintainer

            __currVer = RSystem.packageVersion(pack.Package)

            If String.IsNullOrEmpty(__currVer) Then
                Label4.Text = "This package is not installed yet."
                LinkLabel1.Text = "Click to install!"
            Else
                Label4.Text = "Installed version: " & __currVer
                LinkLabel1.Text = "Click check update."
            End If

            LinkLabel2.Enabled = True
            LinkLabel1.Enabled = True
        Else
            LinkLabel2.Enabled = False
            LinkLabel1.Enabled = False
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim url As String = Current.GetURL
        Call Process.Start(url)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If Not Current Is Nothing Then
            Dim script As String = Current.InstallScript
            Call RSystem.REngine.Evaluate(script)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not String.IsNullOrEmpty(TextBox1.Text) Then
            Dim term As String = TextBox1.Text

        End If
    End Sub
End Class