using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class UzytkownicyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string UzytkownicyPrefix = "/uzytkownicy";

    private void SetAuthorizationHeader()
    {
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenService.GetToken());
    }

    public async Task<Result<List<Uzytkownik>>> UzytkownicyGet()
    {
        SetAuthorizationHeader();

        var result = new Result<List<Uzytkownik>>();
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<Uzytkownik>>(UzytkownicyPrefix);
            result.Data = response ?? [];
        }
        catch (Exception ex)
        {
            result.Error = ex.Message;
        }

        return result;
    }
}