using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class LogiRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string LogiPrefix = "/logi";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}