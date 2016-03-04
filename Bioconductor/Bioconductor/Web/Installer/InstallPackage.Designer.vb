<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InstallPackage
    Inherits Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <DebuggerNonUserCodeAttribute()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As Global.System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <Global.System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ListView1 = New Global.System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Location = New Global.System.Drawing.Point(73, 125)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New Global.System.Drawing.Size(461, 378)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'InstallPackage
        '
        Me.AutoScaleDimensions = New Global.System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = Global.System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New Global.System.Drawing.Size(723, 652)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "InstallPackage"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListView1 As Global.System.Windows.Forms.ListView
End Class
