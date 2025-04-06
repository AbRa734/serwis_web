using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class DostepnosciSerwisantaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string DostepnosciSerwisantaPrefix = "/dostepnosci-serwisanta";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}