using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class AdministracjaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string RejestracjaPrefix = "/rejestracja";
    private const string LoginPrefix = "/logowanie";

    public async Task<Result<bool>> RejestracjaPost(RegisterRequest autoryzacja)
    {
        var response = await httpClient.PostAsJsonAsync(RejestracjaPrefix, autoryzacja);

        if (response.IsSuccessStatusCode)
        {
            return new Result<bool> { Data = true};
        }
        
        return new Result<bool> { Error = response.ToString() };
    }

    public async Task<Result<Token>> LoginPost(LoginRequest autoryzacja)
    {
        var response = await httpClient.PostAsJsonAsync(LoginPrefix, autoryzacja);
        if (!response.IsSuccessStatusCode)
        {
            return new Result<Token> { Error = response.ToString() };
        }

        var result = response.Content.ReadFromJsonAsync<Token>().Result;
        if (result?.AccessToken != null)
        {
            tokenService.SetToken(result.AccessToken);
            tokenService.SetUserEmail(autoryzacja.Email);
        }

        return new Result<Token>
        {
            Data = result,
        };
    }
}