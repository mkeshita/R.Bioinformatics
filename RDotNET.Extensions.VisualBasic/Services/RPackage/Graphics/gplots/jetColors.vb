Imports RDotNET.Extensions.VisualBasic.Services.ScriptBuilder

Namespace gplots

    ''' <summary>
    ''' Using function call 'get.jetColors()' to gets the colors vector
    ''' </summary>
    ''' <remarks>
    ''' 旭仔-畜牧 564307915@qq.com
    ''' </remarks>
    Public NotInheritable Class jetColors : Inherits IRScript

        ''' <summary>
        ''' ``get.jetColors()``
        ''' </summary>
        Public Shared ReadOnly Property [Call] As String = "get.jetColors()"

        Private Shared ReadOnly __call As jetColors

        Shared Sub New()
            __call = New jetColors
            Call __call.RScript.ζ
        End Sub

        Private Sub New()
        End Sub

        ''' <summary>
        ''' R script for generates the color pattern profiles.
        ''' 
        ''' ```R
        ''' jetColors.f0 &lt;- function(rgb1,rgb2,n) {
        '''      return(mapply(seq, rgb1, rgb2, len = n));
        ''' }
        ''' 
        ''' get.jetColors &lt;- function() {
        '''
        '''    red    &lt;- c(0.60,0.00,0.00)
        '''    yellow &lt;- c(1.00,0.86,0.10)
        '''    mid    &lt;- c(0.85,1.00,0.85)
        '''    cyan   &lt;- c(0.10,0.86,1.00)
        '''    blue   &lt;- c(0.00,0.00,0.50)
        '''    r2y    &lt;- jetColors.f0(red,yellow,400)
        '''    c2b    &lt;- jetColors.f0(cyan,blue,400)
        '''    y2m    &lt;- jetColors.f0(yellow, mid, 202)
        '''    y2m    &lt;- y2m[-c(1,nrow(y2m)),]
        '''    m2c    &lt;- jetColors.f0(mid,cyan,202)
        '''    m2c    &lt;- m2c[-nrow(m2c),]
        '''    color  &lt;- rbind(r2y,y2m,m2c,c2b)
        '''    color  &lt;- color[nrow(color):1,]
        '''
        '''    jetColors &lt;- character(len=nrow(color))
        '''
        '''    for(i in 1:nrow(color)) {
        '''        jetColors[i] = RGB(color[i, 1], color[i, 2], color[i, 3])
        '''    }
        '''
        '''    return(jetColors)
        ''' }
        ''' ```
        ''' </summary>
        Public Const getJetColors As String = "
jetColors.f0 <- function(rgb1,rgb2,n) {
    return (mapply(seq,rgb1,rgb2,len=n));
}

get.jetColors <- function() {

    red    <- c(0.60,0.00,0.00)
    yellow <- c(1.00,0.86,0.10)
    mid    <- c(0.85,1.00,0.85)
    cyan   <- c(0.10,0.86,1.00)
    blue   <- c(0.00,0.00,0.50)
    r2y    <- jetColors.f0(red,yellow,400)
    c2b    <- jetColors.f0(cyan,blue,400)
    y2m    <- jetColors.f0(yellow, mid, 202)
    y2m    <- y2m[-c(1,nrow(y2m)),]
    m2c    <- jetColors.f0(mid,cyan,202)
    m2c    <- m2c[-nrow(m2c),]
    color  <- rbind(r2y,y2m,m2c,c2b)
    color  <- color[nrow(color):1,]

    jetColors <- character(len=nrow(color))

    for (i in 1:nrow(color)) {
        jetColors[i] = rgb(color[i,1],color[i,2],color[i,3])
    }

    return (jetColors)
}"

        Protected Overrides Function __R_script() As String
            Return getJetColors
        End Function
    End Class
End Namespace