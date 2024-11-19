using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WebApp.Authentication.CustomScheme
{
    public static class CustomAuthExtensions
    {
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, string authenticationScheme, string? displayName, Action<CustomAuthSchemeOptions> configureOptions)
        {
            builder.Services
                .AddOptions<CustomAuthSchemeOptions>(authenticationScheme)
                .Validate(o => !string.IsNullOrWhiteSpace(o.HeaderName) || !string.IsNullOrWhiteSpace(o.TokenName),
                    "Header name or token name must be specified");
            return builder.AddScheme<CustomAuthSchemeOptions, CustomAuthSchemeHandler>(authenticationScheme, displayName, configureOptions);
        }
    }

    public static class CustomAuthSchemeDefaults
    {
        public const string AuthenticationScheme = "CustomAuth";

        public const string HeaderName = "Custom-Auth-Header";
        public const string HeaderKey = "Please";

        public const string TokenName = "Custom-Auth-Token";
        public const string TokenKey = "OpenSesame";
    }

    public class CustomAuthSchemeOptions : AuthenticationSchemeOptions
    {
        public string HeaderName { get; set; } = CustomAuthSchemeDefaults.HeaderName;
        public string HeaderKey { get; set; } = CustomAuthSchemeDefaults.HeaderKey;

        public string TokenName { get; set; } = CustomAuthSchemeDefaults.TokenName;
        public string TokenKey { get; set; } = CustomAuthSchemeDefaults.TokenKey;
    }

    public class CustomAuthSchemeHandler(IOptionsMonitor<CustomAuthSchemeOptions> options,
                                         ILoggerFactory logger,
                                         UrlEncoder encoder
    ) : AuthenticationHandler<CustomAuthSchemeOptions>(options, logger, encoder)
    {
        private readonly IOptionsMonitor<CustomAuthSchemeOptions> _options = options;

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Endpoint endpoint = Context.GetEndpoint();

            if (endpoint != null)
            {
                IAllowAnonymous allowAnonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>();

                if (allowAnonymous != null)
                {
                    return Task.FromResult(AuthenticateResult.NoResult());
                }
            }

            var currentOptions = _options.CurrentValue;

            if (Request.Query.TryGetValue(Options.TokenName, out var tokenValue))
            {
                if (tokenValue != currentOptions.TokenKey)
                {
                    return Task.FromResult(AuthenticateResult.Fail($"Incorrect value for query parameter: {Options.TokenName}"));
                }
            }
            else
            {
                return Task.FromResult(AuthenticateResult.Fail($"Missing query parameter: {Options.TokenName}"));
            }

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
}
