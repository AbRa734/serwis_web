using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class AdresyEmailRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string AdresyEmailPrefix = "/adresy-email";

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

    public async Task<Result<List<AdresEmail>>> AdresyEmailGet()
    {
        var result = new Result<List<AdresEmail>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<AdresEmail>>(AdresyEmailPrefix);
                result.Data = response ?? new List<AdresEmail>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<AdresEmail>> AdresEmailGet(int adresEmailId)
    {
        var result = new Result<AdresEmail>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<AdresEmail>(AdresyEmailPrefix + "/" + adresEmailId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<AdresEmail>> AdresEmailPost(AdresEmailDto adresEmail)
    {
        var result = new Result<AdresEmail>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(AdresyEmailPrefix, adresEmail);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<AdresEmail>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<AdresEmail>> AdresEmailPut(int adresEmailId, AdresEmailDto adresEmail)
    {
        var result = new Result<AdresEmail>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(AdresyEmailPrefix + "/" + adresEmailId, adresEmail);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<AdresEmail>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> AdresEmailDelete(int adresEmailId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(AdresyEmailPrefix + "/" + adresEmailId);
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