using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class AdministracjaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string RejestracjaPrefix = "/rejestracja";
    private const string LoginPrefix = "/logowanie";
    private const string RestartHaslaPrefix = "/restart-hasla";
    private const string ZmianaHaslaPrefix = "/zmien-haslo";

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
    
    public async Task<Result<bool>> RestartHaslaPost(string email)
    {
        var url = $"{RestartHaslaPrefix}?email={Uri.EscapeDataString(email)}";
        var response = await httpClient.PostAsync(url, null);
        if (!response.IsSuccessStatusCode)
        {
            return new Result<bool> { Error = response.ToString() };
        }

        return new Result<bool>
        {
            Data = true,
        };
    }
    
    public async Task<Result<ResetHasla>> RestartHaslaGet(string token)
    {
        var url = $"{RestartHaslaPrefix}?token={Uri.EscapeDataString(token)}";
        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            return new Result<ResetHasla> { Error = response.ToString() };
        }

        var content = await response.Content.ReadFromJsonAsync<ResetHasla>();

        return new Result<ResetHasla>
        {
            Data = content,
        };
    }
    
    public async Task<Result<bool>> RestartHaslaPut(string id, ResetHaslaDto resetHaslaDto)
    {
        var url = $"{RestartHaslaPrefix}/{Uri.EscapeDataString(id)}";
        var response = await httpClient.PutAsJsonAsync(url, resetHaslaDto);
        if (!response.IsSuccessStatusCode)
        {
            return new Result<bool> { Error = response.ToString() };
        }

        return new Result<bool>
        {
            Data = true,
        };
    }
    
    public async Task<Result<bool>> ZrestartujHaslo(string token, ZmianaHaslaRequest zmianaHaslaRequest)
    {
        var url = $"{ZmianaHaslaPrefix}?token={Uri.EscapeDataString(token)}";
        var response = await httpClient.PostAsJsonAsync(url, zmianaHaslaRequest);
        if (!response.IsSuccessStatusCode)
        {
            return new Result<bool> { Error = response.ToString() };
        }

        return new Result<bool>
        {
            Data = true,
        };
    }
}