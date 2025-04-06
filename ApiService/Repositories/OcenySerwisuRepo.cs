using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class OcenySerwisuRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string OcenySerwisuPrefix = "/oceny-serwisu";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}