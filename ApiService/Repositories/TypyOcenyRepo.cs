using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class TypyOcenyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string TypyOcenyPrefix = "/typy-oceny";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}