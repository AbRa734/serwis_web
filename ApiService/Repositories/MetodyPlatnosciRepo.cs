using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class MetodyPlatnosciRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string MetodyPlatnosciPrefix = "/metody-platnosci";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}