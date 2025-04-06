using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class KlienciRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string KlienciPrefix = "/klienci";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}