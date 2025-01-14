using System.Diagnostics;
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
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        };
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) =>
    {
        // TODO: Strip out non-essential values where possible
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        options.Authority = "http://localhost:4012";
        options.ClientId = "WebApp";

        options.ResponseType = OpenIdConnectResponseType.Code;
        options.ResponseMode = OpenIdConnectResponseMode.FormPost;

        options.Scope.Add("email");
        options.Scope.Add("workshop");
        options.Scope.Add("workshop_api");

        options.TokenValidationParameters.NameClaimType = "name";
        //options.TokenValidationParameters.RoleClaimType = "role";
        //options.GetClaimsFromUserInfoEndpoint = true;

        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();

        options.ClaimActions.MapJsonKey("craft", "craft");

        var test = JsonWebTokenHandler.DefaultInboundClaimTypeMap;

        options.Events.OnTokenValidated = context =>
        {
            if (context.Principal is not { Identity: ClaimsIdentity claimsIdentity })
            {
                Debug.WriteLine("No claims identity found after token validation");
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
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

builder.Services.AddSingleton<IAuthorizationHandler, IsTracyFamilyAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, IsAstronautAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, IsCertifiedForCraftAuthorizationResourceHandler>();

builder.Services.AddAuthorization(options =>
{
    // Are a member of the Tracy family if surname is Tracy
    options.AddPolicy("TracyFamily", policy =>
        //policy.RequireClaim(ClaimTypes.Surname, "Tracy")
        policy.AddRequirements(new IsTracyFamilyRequirement())
    );

    // Are an astronaut if trained to fly a spacecraft
    options.AddPolicy("Astronaut", policy =>
    {
        //policy.RequireClaim(ClaimTypes.Role, "Pilot");
        //policy.RequireClaim("craft", Craft.Mercury.Name, Craft.ThunderBird1.Name, Craft.ThunderBird3.Name,
        //    Craft.ThunderBird5.Name);
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

app.UseExceptionHandler();
app.UseStatusCodePages();

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