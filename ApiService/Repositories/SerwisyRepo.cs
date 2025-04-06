using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class SerwisyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string SerwisyPrefix = "/serwisy";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
    
}