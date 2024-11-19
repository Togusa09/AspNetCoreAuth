using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using WebApp.Authentication.CustomScheme;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(sharedOptions =>
    {
        sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddCookie(options =>
    {
        options.Cookie.Name = "WorkshopAuthCookie";
        //options.LoginPath = "/Login/Login";
        //options.LogoutPath = "/Login/Logut";
        options.Events.OnValidatePrincipal = (context) =>
        {
            return Task.CompletedTask;
        };
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        options.Authority = "http://localhost:4012";
        options.ClientId = "WebApp";

        options.ResponseType = OpenIdConnectResponseType.Code;
        options.ResponseMode = OpenIdConnectResponseMode.FormPost;

        options.Scope.Add("email");
        options.Scope.Add("workshop");

        options.TokenValidationParameters.NameClaimType = "name";

        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
        options.Events.OnTokenValidated = (context) =>
        {
            Debug.WriteLine("Test");
            return Task.CompletedTask;
        };
    })
    .AddCustomAuth(CustomAuthSchemeDefaults.AuthenticationScheme, "Custom Auth", o =>
    {
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
