using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class SerwisyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string SerwisyPrefix = "/serwisy";

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

    public async Task<Result<List<Serwis>>> SerwisyGet()
    {
        var result = new Result<List<Serwis>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Serwis>>(SerwisyPrefix);
                result.Data = response ?? new List<Serwis>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Serwis>> SerwisGet(int serwisId)
    {
        var result = new Result<Serwis>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Serwis>(SerwisyPrefix + "/" + serwisId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Serwis>> SerwisPost(SerwisDto serwis)
    {
        var result = new Result<Serwis>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(SerwisyPrefix, serwis);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Serwis>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Serwis>> SerwisPut(int serwisId, SerwisDto serwis)
    {
        var result = new Result<Serwis>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(SerwisyPrefix + "/" + serwisId, serwis);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Serwis>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> SerwisDelete(int serwisId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(SerwisyPrefix + "/" + serwisId);
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