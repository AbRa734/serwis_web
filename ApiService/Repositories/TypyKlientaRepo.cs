using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class TypyKlientaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string TypyKlientaPrefix = "/typy-klienta";

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

    public async Task<Result<List<DicTypKlienta>>> TypyKlientaGet()
    {
        var result = new Result<List<DicTypKlienta>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<DicTypKlienta>>(TypyKlientaPrefix);
                result.Data = response ?? new List<DicTypKlienta>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypKlienta>> TypKlientaGet(int typKlientaId)
    {
        var result = new Result<DicTypKlienta>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<DicTypKlienta>(TypyKlientaPrefix + "/" + typKlientaId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypKlienta>> TypKlientaPost(DicTypKlientaDto typKlienta)
    {
        var result = new Result<DicTypKlienta>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(TypyKlientaPrefix, typKlienta);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicTypKlienta>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypKlienta>> TypKlientaPut(int typKlientaId, DicTypKlientaDto typKlienta)
    {
        var result = new Result<DicTypKlienta>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(TypyKlientaPrefix + "/" + typKlientaId, typKlienta);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicTypKlienta>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> TypKlientaDelete(int typKlientaId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(TypyKlientaPrefix + "/" + typKlientaId);
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