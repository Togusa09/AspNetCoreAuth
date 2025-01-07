
## Basic Cookie Authentication
https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-9.0

1. Add to `Program.cs` 
	```csharp
	builder.Services.AddAuthentication(sharedOptions =>
	{
		sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		}).AddCookie(options =>
		{
		// Set custom cookie name
		options.Cookie.Name = "WorkshopAuthCookie";
		// Sets URL to direct login challenge requests to
		options.LoginPath = "/Login/Login";
		// Sets URL to direct logout requests to
		options.LogoutPath = "/Login/Logut";
	});

	//////////////////////////////////////////////////

	builder.Build();

	//////////////////////////////////////////////////

	app.UseAuthentication();
	app.UseAuthorization();
	```
1. Create `LoginController.cs`
	```csharp
	public class LoginController : Controller
	{
		public async Task<IActionResult> Login()
		{
			// Sign user in with an empty identity using Cookie Auth Scheme
			await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme)));
			return RedirectToAction("Index", "Home");
		}
		
		public async Task<IActionResult> Logout()
		{
			// Sign user out from any authentication schemes
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
	```
1. Add login/out link to menu
	  ```razor
	@if (User.Identity!.IsAuthenticated)
	{
		<li class="nav-item">
			<a class="nav-link text-dark" asp-area="" asp-controller="Login" asp-action="Logout">Logout</a>
		</li>
	}
	else
	{
		<li class="nav-item">
			<a class="nav-link text-dark" asp-area="" asp-controller="Login" asp-action="Login">Login</a>
		</li>
	}
	```
1. Run the WebApp project and open it in your browser

	![[CookieAuth2.png]]
1. Use your browsers dev tools to view the cookie attached to requests
	![[CookieAuth1.png]]
## Cookie Auth Events
https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.cookies.cookieauthenticationevents?view=aspnetcore-8.0

| Callback                 | Description                                                              |
| ------------------------ | ------------------------------------------------------------------------ |
| OnCheckSlidingExpiration | Invoked to check if the cookie should be renewed.                        |
| OnRedirectToAccessDenied | Invoked when the client needs to be redirected to the access denied url. |
| OnRedirectToLogin        | Invoked when the client needs to be redirected to the sign in url.       |
| OnRedirectToLogout       | Invoked when the client is to be redirected to logout.                   |
| OnRedirectToReturnUrl    | Invoked when the client is to be redirected after logout.                |
| OnSignedIn               | Invoked after sign in has completed.                                     |
| OnSigningIn              | Invoked on signing in.                                                   |
| OnSigningOut             | Invoked on signing out.                                                  |
| OnValidatePrincipal      | Invoked to validate the principal.                                       |
Workshop actions for these?
- Could log on sign in/out - requires logging
- Could perform validate, but need something to validate on. Maybe move login/out onto index page with list of users?

## Sharing Cookie Authentication Between Apps

https://learn.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-8.0

Useful to show off, but not sure of value, as it's usually SSO handling this