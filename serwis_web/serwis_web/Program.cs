using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using MudBlazor.Services;
using QuestPDF.Infrastructure;
using serwis_web;
using serwis_web.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient("ApiWithAuth",
    (sp, client) => { client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("SERWIS_API_URL") ?? ""); });
builder.Services.AddScoped<TokenService.TokenService>();
builder.Services.AddScoped<ApiService.ApiService>();
builder.Services.AddScoped<CheckoutSessionService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CircuitHandler, AuthService.AuthenticationService>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie();

builder.Services.AddBootstrapBlazor();
builder.Services.AddMudServices();


var app = builder.Build();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(serwis_web.Client._Imports).Assembly);

app.UseStatusCodePagesWithRedirects("/Error");

app.Use(async (context, next) =>
{
    var email = context.Session.GetString("email");
    if (!string.IsNullOrEmpty(email))
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Email, email)
        };
        var identity = new ClaimsIdentity(claims, "Clerk");
        context.User = new ClaimsPrincipal(identity);
    }

    await next();
});


QuestPDF.Settings.License = LicenseType.Community;
StripeConfiguration.ApiKey =
    "sk_test_51RHTepH8Eh2h5xHth0gIqcvTUQINF5t2pEPF6J8ghuJUYJ7SheubGn1FdUMISm09sPhVvYWse0d0wWuOiwAHFLNI00ph5Q9zsZ";

app.MapPost("/platnosc", async ([FromBody] PaymentRequest paymentRequest, CheckoutSessionService checkoutService) =>
{
    if (paymentRequest.AmountInCents <= 0)
    {
        return Results.BadRequest(new { error = "Invalid payment amount" });
    }
    
    var session = await checkoutService.CreateCheckoutSessionAsync(paymentRequest);
    return Results.Ok(new { id = session.Id });
});

app.Run();