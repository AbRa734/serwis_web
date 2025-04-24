using Microsoft.AspNetCore.Components.Server.Circuits;
using MudBlazor.Services;
using QuestPDF.Infrastructure;
using serwis_web;
using serwis_web.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddBootstrapBlazor();
builder.Services.AddMudServices();

var app = builder.Build();

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

QuestPDF.Settings.License = LicenseType.Community;
StripeConfiguration.ApiKey = "sk_test_51RHTepH8Eh2h5xHth0gIqcvTUQINF5t2pEPF6J8ghuJUYJ7SheubGn1FdUMISm09sPhVvYWse0d0wWuOiwAHFLNI00ph5Q9zsZ";

app.MapPost("/platnosc", async (CheckoutSessionService checkoutService) =>
{
    var session = await checkoutService.CreateCheckoutSessionAsync();
    return Results.Ok(new { id = session.Id });
});

app.Run();