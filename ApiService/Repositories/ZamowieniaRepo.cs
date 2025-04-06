using System.Net.Http.Headers;

namespace ApiService.Repositories;

public class ZamowieniaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string ZamowieniaPrefix = "/zamowienia";

    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }
}