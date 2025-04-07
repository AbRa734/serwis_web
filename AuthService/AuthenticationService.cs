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
        if (e.Location != _navigationManager.BaseUri && 
            e.Location != _navigationManager.BaseUri + "/" && 
            !_tokenService.HasToken)
        {
            _navigationManager.NavigateTo("/access-denied", forceLoad: true);
        }
    }

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        _navigationManager.LocationChanged -= CheckAccess;
        return base.OnCircuitClosedAsync(circuit, cancellationToken);
    }
}