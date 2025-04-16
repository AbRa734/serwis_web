using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace AuthService;

public class AuthenticationService : CircuitHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly TokenService.TokenService _tokenService;

    public AuthenticationService(NavigationManager navigationManager, TokenService.TokenService tokenService)
    {
        _navigationManager = navigationManager;
        _tokenService = tokenService;
        _navigationManager.LocationChanged += CheckAccess;
    }

    private void CheckAccess(object? sender, LocationChangedEventArgs e)
    {
        CheckCurrentPage(e.Location);
    }

    private void CheckCurrentPage(string currentUrl)
    {
        if (currentUrl != _navigationManager.BaseUri && 
            currentUrl != _navigationManager.BaseUri + "/" && 
            !_tokenService.HasToken)
        {
            _navigationManager.NavigateTo("/", forceLoad: true);
        }
    }

    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        CheckCurrentPage(_navigationManager.Uri);
        return base.OnCircuitOpenedAsync(circuit, cancellationToken);
    }

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        _navigationManager.LocationChanged -= CheckAccess;
        return base.OnCircuitClosedAsync(circuit, cancellationToken);
    }
}