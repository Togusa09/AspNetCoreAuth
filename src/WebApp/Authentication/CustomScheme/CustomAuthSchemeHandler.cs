using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace WebApp.Authentication.CustomScheme;

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

public class CustomAuthSchemeHandler(IOptionsMonitor<CustomAuthSchemeOptions> options,
                                     ILoggerFactory loggerFactory,
                                     UrlEncoder encoder
) : AuthenticationHandler<CustomAuthSchemeOptions>(options, loggerFactory, encoder)
{
    private readonly IOptionsMonitor<CustomAuthSchemeOptions> _options = options;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Logger.LogInformation("Handling authentication request");

        var currentOptions = _options.CurrentValue;

        if (Request.Headers.TryGetValue(Options.HeaderName, out var headerValue))
        {
            if (headerValue != currentOptions.HeaderKey)
            {
                Logger.LogWarning("Incorrect value for header {headerName} not found", Options.HeaderName);
                return Task.FromResult(AuthenticateResult.Fail($"Incorrect value for header: {Options.HeaderName}"));
            }
        }
        else
        {
            Logger.LogWarning("Expected auth header {headerName} not found", Options.HeaderName);
            return Task.FromResult(AuthenticateResult.Fail($"Missing header: {Options.HeaderName}"));
        }

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>
            {
                new(ClaimTypes.Name, "CustomAuth")
            }, Scheme.Name //If principal scheme does not match the handler, asp.net will reject
        ));

        Logger.LogInformation("Successfully authenticated using header");
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, this.Scheme.Name)));
    }
}

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