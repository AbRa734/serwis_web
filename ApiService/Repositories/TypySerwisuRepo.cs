using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class TypySerwisuRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string TypySerwisuPrefix = "/typy-serwisu";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}