using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class AdresyEmailRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string AdresyEmailPrefix = "/adresy-email";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}