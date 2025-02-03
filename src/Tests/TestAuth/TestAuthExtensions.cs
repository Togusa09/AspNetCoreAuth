using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.TestAuth;

public static class TestAuthExtensions
{
    public static AuthenticationBuilder AddTestAuth(this IServiceCollection services, string name, IEnumerable<Claim> claims, string scheme = "TestScheme")
    {
        var auth = services
            .AddAuthentication(defaultScheme: scheme)
            .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(
                scheme, options =>
                {
                    options.UserName = name;
                    options.Claims = claims;
                });

        return auth;
    }

    public static IServiceCollection OverrideAuthSchemeProvider(this IServiceCollection services)
    {
        return services.AddTransient<IAuthenticationSchemeProvider, TestSchemeProvider>();
    }
}