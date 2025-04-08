using serwis_web.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<TokenService.TokenService>(); //FIXME: zmienić na scoped
builder.Services.AddSingleton<ApiService.ApiService>(); //FIXME: zmienić na scoped
//zakomentować w sytuacji gdy nie ma potrzeby sprawdzania tokenu
//builder.Services.AddScoped<CircuitHandler, AuthService.AuthenticationService>();

builder.Services.AddBootstrapBlazor();
builder.Services.AddMudServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    //app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(serwis_web.Client._Imports).Assembly);

app.Run();