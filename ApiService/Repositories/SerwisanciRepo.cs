using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class SerwisanciRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string SerwisanciPrefix = "/serwisanci";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}