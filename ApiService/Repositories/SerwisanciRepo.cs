using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class SerwisanciRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string SerwisanciPrefix = "/serwisanci";

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

    public async Task<Result<List<Serwisant>>> SerwisanciGet()
    {
        var result = new Result<List<Serwisant>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Serwisant>>(SerwisanciPrefix);
                result.Data = response ?? new List<Serwisant>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Serwisant>> SerwisantGet(int serwisantId)
    {
        var result = new Result<Serwisant>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Serwisant>(SerwisanciPrefix + "/" + serwisantId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Serwisant>> SerwisantPost(SerwisantDto serwisant)
    {
        var result = new Result<Serwisant>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(SerwisanciPrefix, serwisant);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Serwisant>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Serwisant>> SerwisantPut(int serwisantId, SerwisantDto serwisant)
    {
        var result = new Result<Serwisant>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(SerwisanciPrefix + "/" + serwisantId, serwisant);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Serwisant>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> SerwisantDelete(int serwisantId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(SerwisanciPrefix + "/" + serwisantId);
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