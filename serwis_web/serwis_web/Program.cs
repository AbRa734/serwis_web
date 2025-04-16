using Microsoft.AspNetCore.Components.Server.Circuits;
using MudBlazor.Services;
using serwis_web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient("ApiWithAuth", (sp, client) =>
{
    client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("SERWIS_API_URL") ?? ""); 
});
builder.Services.AddScoped<TokenService.TokenService>(); 
builder.Services.AddScoped<ApiService.ApiService>();

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

app.Run();