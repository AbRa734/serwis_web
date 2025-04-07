using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class KlienciRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string KlienciPrefix = "/klienci";

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

    public async Task<Result<List<Klient>>> KlienciGet()
    {
        var result = new Result<List<Klient>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Klient>>(KlienciPrefix);
                result.Data = response ?? new List<Klient>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Klient>> KlientGet(int klientId)
    {
        var result = new Result<Klient>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Klient>(KlienciPrefix + "/" + klientId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Klient>> KlientPost(KlientDto klient)
    {
        var result = new Result<Klient>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(KlienciPrefix, klient);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Klient>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Klient>> KlientPut(int klientId, KlientDto klient)
    {
        var result = new Result<Klient>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(KlienciPrefix + "/" + klientId, klient);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Klient>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> KlientDelete(int klientId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(KlienciPrefix + "/" + klientId);
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