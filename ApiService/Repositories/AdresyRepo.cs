using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class AdresyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string AdresyPrefix = "/adresy";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}