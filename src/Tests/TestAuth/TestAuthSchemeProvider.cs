using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Tests.TestAuth;

/// <summary>
/// Scheme provider for tests where a specific scheme is required
/// </summary>
public class TestSchemeProvider : AuthenticationSchemeProvider
{
    public TestSchemeProvider(IOptions<AuthenticationOptions> options)
        : base(options)
    {
    }

    protected TestSchemeProvider(
        IOptions<AuthenticationOptions> options,
        IDictionary<string, AuthenticationScheme> schemes
    )
        : base(options, schemes)
    {
    }

    public override Task<AuthenticationScheme?> GetSchemeAsync(string name) =>
        Task.FromResult<AuthenticationScheme?>(new AuthenticationScheme(
            name,
            name,
            typeof(TestAuthHandler)));
}