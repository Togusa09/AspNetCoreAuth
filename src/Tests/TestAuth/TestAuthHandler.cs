using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tests.TestAuth;


public class TestAuthHandlerOptions : AuthenticationSchemeOptions
{
    public bool AllowLogin { get; set; } = true;
    //public string AuthName { get; set; } = "TestScheme";
    public string UserName { get; set; } = "TestUser";

    public IEnumerable<Claim> Claims { get; set; } = [];
}

public class TestAuthHandler(
    IOptionsMonitor<TestAuthHandlerOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : SignInAuthenticationHandler<TestAuthHandlerOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Options.AllowLogin)
        {
            return Task.FromResult(AuthenticateResult.Fail($"Login blocked for provider {this.Scheme.Name} by config"));
        }

        var claims = Options.Claims.ToList();

        if (!string.IsNullOrWhiteSpace(Options.UserName))
        {
            claims.Add(new Claim(ClaimTypes.Name, Options.UserName));
        }

        var identity = new ClaimsIdentity(claims, this.Scheme.Name, "name", "roles");

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    protected override Task HandleSignOutAsync(AuthenticationProperties? properties)
    {
        return Task.CompletedTask;
    }

    protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
    {
        return Task.CompletedTask;
    }
}