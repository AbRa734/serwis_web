using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class StatusyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string StatusyPrefix = "/statusy";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}