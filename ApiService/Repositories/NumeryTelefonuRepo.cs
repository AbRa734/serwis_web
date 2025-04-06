using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class NumeryTelefonuRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string NumeryTelefonuPrefix = "/numery-telefonu";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}