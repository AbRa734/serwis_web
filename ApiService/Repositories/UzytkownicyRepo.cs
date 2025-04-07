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

    private async Task SetAuthorizationAndExecute(Func<Task> action)
    {
        SetAuthorizationHeader();
        await action();
    }

    public async Task<Result<List<Uzytkownik>>> UzytkownicyGet()
    {
        var result = new Result<List<Uzytkownik>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Uzytkownik>>(UzytkownicyPrefix);
                result.Data = response ?? new List<Uzytkownik>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Uzytkownik>> UzytkownikGet(int uzytkownikId)
    {
        var result = new Result<Uzytkownik>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Uzytkownik>(UzytkownicyPrefix + "/" + uzytkownikId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Uzytkownik>> UzytkownikPost(UzytkownikDto uzytkownik)
    {
        var result = new Result<Uzytkownik>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(UzytkownicyPrefix, uzytkownik);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Uzytkownik>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Uzytkownik>> UzytkownikPut(int uzytkownikId, UzytkownikDto uzytkownik)
    {
        var result = new Result<Uzytkownik>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(UzytkownicyPrefix + "/" + uzytkownikId, uzytkownik);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Uzytkownik>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> UzytkownikDelete(int uzytkownikId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(UzytkownicyPrefix + "/" + uzytkownikId);
                result.Data = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    result.Error = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.Error = ex.Message;
            }
        });
        return result;
    }
}