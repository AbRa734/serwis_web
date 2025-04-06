using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class RoleUzytkownikaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string RoleUzytkownikaPrefix = "/role-uzytkownika";
    
    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
    
}