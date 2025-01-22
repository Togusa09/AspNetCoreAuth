using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using WebApp.Authentication.CustomScheme;
using WebApp.Authorisation;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();


builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(sharedOptions =>
    {
        sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie(options =>
    {
        options.Cookie.Name = "WorkshopAuthCookie";
        // For these examples we just want to return error responses instead of redirecting
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.Headers["Location"] = context.RedirectUri;
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.Headers["Location"] = context.RedirectUri;
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) =>
    {
        // TODO: Strip out non-essential values where possible
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        var test = JsonWebTokenHandler.DefaultInboundClaimTypeMap;

        // Address of OIDC server
        options.Authority = "http://localhost:4012";
        // Name this client is registered with OIDC Server
        options.ClientId = "WebApp";

        options.ResponseType = OpenIdConnectResponseType.Code;
        options.ResponseMode = OpenIdConnectResponseMode.FormPost;

        // Scopes to request access to
        options.Scope.Add("email");
        options.Scope.Add("workshop");
        options.Scope.Add("workshop_api");

        options.GetClaimsFromUserInfoEndpoint = true;
        options.TokenValidationParameters.NameClaimType = "given_name";

        // Whether to require a https connection for server metadata.
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();

        // Map "craft" from token to user
        options.ClaimActions.MapJsonKey("craft", "craft");
        options.ClaimActions.MapJsonKey("given_name", "given_name");

        options.Events.OnTokenValidated = context =>
        {
            if (context.Principal is not { Identity: ClaimsIdentity claimsIdentity })
            {
                Debug.WriteLine("No claims identity found after token validation");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        };
        options.Events.OnRemoteSignOut = async context =>
        {
            await context.HttpContext.SignOutAsync();
        };
    })
    .AddCustomAuth(CustomAuthSchemeDefaults.AuthenticationScheme, "Custom Auth", o => { })
    .AddJwtBearer(options =>
    {
        //options.TokenValidationParameters.RoleClaimType
        options.Audience = "workshop_api";
        options.Authority = "http://localhost:4012";
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
        //options.MetadataAddress = config.ApiMetadataAddress;
        //options.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidateIssuer = true,
        //    ValidIssuer = config.Authority,
        //    ValidateAudience = true,
        //    ValidAudience = config.Audience,
        //    ValidateLifetime = true,
        //    ClockSkew = TimeSpan.Zero,
        //};
        //options.Events = new JwtBearerEvents
        //{
        //    OnAuthenticationFailed = context =>
        //    {
        //        Log.Logger.Warning(context.Exception, "OnAuthenticationFailed");
        //        return Task.CompletedTask;
        //    }
        //};
    });

builder.Services.AddSingleton(Craft.AllCraft);

builder.Services.AddSingleton<IAuthorizationHandler, IsAstronautAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, IsCertifiedForCraftAuthorizationResourceHandler>();

builder.Services.AddAuthorization(options =>
{
    // Are an astronaut if trained to fly a spacecraft
    options.AddPolicy("Astronaut", policy =>
    {
        //policy.RequireClaim(ClaimTypes.Role, "Pilot");
        //policy.RequireClaim("craft", Craft.Mercury.Name);
        policy.AddRequirements(new IsAstronautRequirement());
    });

    options.AddPolicy("IsCertifiedForCraft",
        policy => { policy.AddRequirements(new IsCertifiedForCraftRequirement()); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Undecided about including, in theory good, in practice didn't seem to handle exceptions from user code well
//app.UseExceptionHandler();
//app.UseStatusCodePages();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

// Essentially just a marker class so that the integration tests know what to hook into
public partial class Program
{
}