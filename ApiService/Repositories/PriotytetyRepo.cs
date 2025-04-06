using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class PriotytetyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string PriotytetyPrefix = "/priorytety";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}