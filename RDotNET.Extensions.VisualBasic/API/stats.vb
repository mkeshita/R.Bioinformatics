Namespace API

    Public Module stats

        ''' <summary>
        ''' Fit an ARIMA model to a univariate time series.
        ''' </summary>
        ''' <param name="x">a univariate time series</param>
        ''' <param name="order">A specification of the non-seasonal part of the ARIMA model: the three integer components (p, d, q) are the AR order, the degree of differencing, and the MA order.</param>
        ''' <param name="seasonal">A specification of the seasonal part of the ARIMA model, plus the period (which defaults to frequency(x)). This should be a list with components order and period, but a specification of just a numeric vector of length 3 will be turned into a suitable list with the specification as the order.</param>
        ''' <param name="xreg">Optionally, a vector or matrix of external regressors, which must have the same number of rows as x.</param>
        ''' <param name="includemean">Should the ARMA model include a mean/intercept term? The default is TRUE for undifferenced series, and it is ignored for ARIMA models with differencing.</param>
        ''' <param name="transformpars">logical; if true, the AR parameters are transformed to ensure that they remain in the region of stationarity. Not used for method = "CSS". For method = "ML", it has been advantageous to set transform.pars = FALSE in some cases, see also fixed.</param>
        ''' <param name="fixed">optional numeric vector of the same length as the total number of parameters. If supplied, only NA entries in fixed will be varied. transform.pars = TRUE will be overridden (with a warning) if any AR parameters are fixed. It may be wise to set transform.pars = FALSE when fixing MA parameters, especially near non-invertibility.</param>
        ''' <param name="init">optional numeric vector of initial parameter values. Missing values will be filled in, by zeroes except for regression coefficients. Values already specified in fixed will be ignored.</param>
        ''' <param name="method">fitting method: maximum likelihood or minimize conditional sum-of-squares. The default (unless there are missing values) is to use conditional-sum-of-squares to find starting values, then maximum likelihood. Can be abbreviated.</param>
        ''' <param name="ncond">only used if fitting by conditional-sum-of-squares: the number of initial observations to ignore. It will be ignored if less than the maximum lag of an AR term.</param>
        ''' <param name="SSinit">a string specifying the algorithm to compute the state-space initialization of the likelihood; see KalmanLike for details. Can be abbreviated.</param>
        ''' <param name="optimmethod">The value passed as the method argument to optim.</param>
        ''' <param name="optimcontrol">List of control parameters for optim.</param>
        ''' <param name="kappa">the prior variance (as a multiple of the innovations variance) for the past observations in a differenced model. Do not reduce this.</param>
        ''' <returns>
        ''' A list of class "Arima" with components:
        '''
        ''' coef	
        ''' a vector Of AR, MA And regression coefficients, which can be extracted by the coef method.
        '''
        ''' sigma2	
        ''' the MLE Of the innovations variance.
        '''
        ''' var.coef	
        ''' the estimated variance matrix Of the coefficients coef, which can be extracted by the vcov method.
        '''
        ''' loglik	
        ''' the maximized log-likelihood (Of the differenced data), Or the approximation To it used.
        '''
        ''' arma	
        ''' A compact form Of the specification, As a vector giving the number Of AR, MA, seasonal AR And seasonal MA coefficients, plus the period And the number Of non-seasonal And seasonal differences.
        '''
        ''' aic	
        ''' the AIC value corresponding To the log-likelihood. Only valid For method = "ML" fits.
        '''
        ''' residuals	
        ''' the fitted innovations.
        '''
        ''' Call    
        ''' the matched Call.
        '''
        ''' series	
        ''' the name Of the series x.
        '''
        ''' code	
        ''' the convergence value returned by optim.
        '''
        ''' n.cond	
        ''' the number Of initial observations Not used In the fitting.
        '''
        ''' nobs	
        ''' the number Of “used” observations For the fitting, can also be extracted via nobs() And Is used by BIC.
        '''
        ''' model	
        ''' A list representing the Kalman Filter used In the fitting. See KalmanLike.
        ''' </returns>
        ''' <remarks>
        ''' Different definitions of ARMA models have different signs for the AR and/or MA coefficients. The definition used here has
        ''' ```
        ''' X[t] = a[1]X[t-1] + … + a[p]X[t-p] + e[t] + b[1]e[t-1] + … + b[q]e[t-q]
        ''' ```
        ''' And so the MA coefficients differ in sign from those of S-PLUS. Further, if include.mean Is true (the default for an ARMA model), this formula applies to X - m rather than X. 
        ''' For ARIMA models with differencing, the differenced series follows a zero-mean ARMA model. If am xreg term Is included, a linear regression (with a constant term if include.mean 
        ''' Is true And there Is no differencing) Is fitted with an ARMA model for the error term.
        ''' The variance matrix Of the estimates Is found from the Hessian Of the log-likelihood, And so may only be a rough guide.
        ''' Optimization Is done by optim. It will work best if the columns in xreg are roughly scaled to zero mean And unit variance, but does attempt to estimate suitable scalings.
        ''' </remarks>
        Public Function arima(x As String,
                              Optional order As String = "c(0L, 0L, 0L)",
                              Optional seasonal As String = "list(order = c(0L, 0L, 0L), period = NA)",
                              Optional xreg As String = NULL,
                              Optional includemean As Boolean = True,
                              Optional transformpars As Boolean = True,
                              Optional fixed As String = NULL,
                              Optional init As String = NULL,
                              Optional method As String = "c(""CSS-ML"", ""ML"", ""CSS"")",
                              Optional ncond As String = NULL,
                              Optional SSinit As String = "c(""Gardner1980"", ""Rossignol2011"")",
                              Optional optimmethod As String = "BFGS",
                              Optional optimcontrol As String = "list()",
                              Optional kappa As String = "1e6")
        End Function

    End Module
End Namespace