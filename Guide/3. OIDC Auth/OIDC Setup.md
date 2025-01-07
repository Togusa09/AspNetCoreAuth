1. User docker compose file `Docker/docker-compose.json`
	1. Add command, maybe note for podman?
2. Add OIDC auth config to `Program.cs` after `.AddCookie()`
	```csharp
	.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) =>
	{
	    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	
	    options.Authority = "http://localhost:4012";
	    options.ClientId = "WebApp";
	
	    options.ResponseType = OpenIdConnectResponseType.Code;
	    options.ResponseMode = OpenIdConnectResponseMode.FormPost;
	
	    options.Scope.Add("email");
	    options.Scope.Add("workshop");
	    options.Scope.Add("workshop_api");
	
	    options.TokenValidationParameters.NameClaimType = "name";
	    options.TokenValidationParameters.RoleClaimType = "role";
	
	    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
	    
	    options.ClaimActions.MapJsonKey("craft", "craft");
	    options.ClaimActions.MapJsonKey("role", "role");
	})
	```
1. Add a method to the `LoginController` to trigger an OIDC challenge. Alternatively the `DefaultChallengeScheme` can be set to use OIDC (`OpenIdConnectDefaults.AuthenticationScheme` by default), and in event of a HTTP 401, the user will be redirected to the OIDC provider.
```csharp
	public IActionResult LoginOidc(string returnUrl = "/")
	{
		var param = new AuthenticationProperties
		{
		  RedirectUri = returnUrl
		};
		
		return Challenge(param, OpenIdConnectDefaults.AuthenticationScheme);
	}
```
1. Log into the Identity Server. The full list of credentials are in `Docker/users-config.json`, but for the moment you can use `scott`/`pwd`
	![[IdentityServerLogin.png]]
1. The displayed user information will now be populated from the Identity Server response, instead of the hard coded user values from the cookie login.
	![[LoggedInOIDC.png]]
1. When using the developer tools you will notice that the authentication cookie specified in `AddCookie()` is still around. ASP.net core receives the users information as part of the initial login, then stores that information in the cookie for use in subsequent requests. This can be altered by setting `SignInScheme` on the OIDC config options, but if unset will use the default auth scheme.
	![[AuthCookie.png]]

## Additional notes
The OIDC scheme also has events that can be set to perform actions as part of it's pipeline, which can also be useful for debugging the OIDC id and access response data, or performing additional operations.