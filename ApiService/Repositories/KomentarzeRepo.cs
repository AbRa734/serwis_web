using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class KomentarzeRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string KomentarzePrefix = "/komentarze";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}