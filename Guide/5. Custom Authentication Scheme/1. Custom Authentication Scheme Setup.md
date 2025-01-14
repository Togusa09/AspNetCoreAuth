TODO: Spiel about custom auth setup

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-9.0#authentication-handler

1. Define options class and defaults for new auth scheme

	```csharp
	public static class CustomAuthSchemeDefaults
	{
	    public const string AuthenticationScheme = "CustomAuth";
	
	    public const string HeaderName = "Custom-Auth-Header";
	    public const string HeaderKey = "Please";
	}
	
	public class CustomAuthSchemeOptions : AuthenticationSchemeOptions
	{
	    public string HeaderName { get; set; } = CustomAuthSchemeDefaults.HeaderName;
	    public string HeaderKey { get; set; } = CustomAuthSchemeDefaults.HeaderKey;
	}
	```

1. Add auth handler

	```csharp
	public class CustomAuthSchemeHandler(
		IOptionsMonitor<CustomAuthSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder
	) : AuthenticationHandler<CustomAuthSchemeOptions>(options, logger, encoder)
	{
	    private readonly IOptionsMonitor<CustomAuthSchemeOptions> _options = options;
	
	    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
	    {
	        var currentOptions = _options.CurrentValue;
	
	        if (Request.Headers.TryGetValue(Options.HeaderName, out var headerValue))
	        {
	            if (headerValue != currentOptions.HeaderKey)
	            {
	                return Task.FromResult(AuthenticateResult.Fail($"Incorrect value for header: {Options.HeaderName}"));
	            }
	        }
	        else
	        {
	            return Task.FromResult(AuthenticateResult.Fail($"Missing header: {Options.HeaderName}"));
	        }
	
	        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
	            new List<Claim>
	            {
	                new(ClaimTypes.Name, "CustomAuth")
	            }, Scheme.Name //If principal scheme does not match the handler, asp.net will reject
	        ));
	
	        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, this.Scheme.Name)));
	    }
	}
	```
1. Add auth registration method. This is optional, but simplifies registration
	```csharp
	public static class CustomAuthExtensions
	{
	    public static AuthenticationBuilder AddCustomAuth(
		    this AuthenticationBuilder builder,
		    string authenticationScheme,
	        string? displayName,
	        Action<CustomAuthSchemeOptions> configureOptions)
	    {
	        builder.Services
	            .AddOptions<CustomAuthSchemeOptions>(authenticationScheme)
	            .Validate(o => !string.IsNullOrWhiteSpace(o.HeaderName)
	                , "Header name name must be specified");
	        return builder.AddScheme<CustomAuthSchemeOptions, CustomAuthSchemeHandler>(authenticationScheme, displayName,
	            configureOptions);
	    }
	}
	```
1. Register custom auth provider after OIDC in `program.cs`
	```csharp
	.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) =>
	{
	//...
	})
	.AddCustomAuth(CustomAuthSchemeDefaults.AuthenticationScheme, "Custom Auth", o => { });
	```
1. Add controller explicitly secured by the CustomAuthScheme
	```csharp
	[Authorize(AuthenticationSchemes = CustomAuthSchemeDefaults.AuthenticationScheme)]
	public class CustomAuthSchemeController : Controller
	{
	    [Authorize(AuthenticationSchemes = CustomAuthSchemeDefaults.AuthenticationScheme)]
	    public IActionResult GetData()
	    {
	        return Json(new
	        {
	            Data1 = "asdasfsfdsd",
	            Data2 = "12e23rwefesf",
	            User = User.Identity!.Name
	        });
	    }
	}
	```

1. Test out auth using rest query -  using `https://localhost:7089/CustomAuthScheme/` or .http request file
	1. Custom Auth Controller:
		1. Success - Using valid header and value
		![[CustomAuthClientSucceed.png]]

		1. Failure - Using incorrect header or value
		![[CustomAuthClientFail.png]]

	1. Test using `.http` (optional)

	```
	@host = localhost:7089
	
	### Unauthenticated request
	GET https://{{host}}/CustomAuthScheme/GetData
	
	### Authenticated request
	GET https://{{host}}/CustomAuthScheme/GetData
	Custom-Auth-Header: Please
	``` 
	The unauthenticated request will return a 401
	![[CustomAuthFailResponse.png]]
	The request with the authentication header will succeed and return your test data
	![[CustomAuthSucceedResponse.png]]