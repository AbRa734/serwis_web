using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class StatusyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string StatusyPrefix = "/statusy";

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

    public async Task<Result<List<DicStatus>>> StatusyGet()
    {
        var result = new Result<List<DicStatus>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<DicStatus>>(StatusyPrefix);
                result.Data = response ?? new List<DicStatus>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicStatus>> StatusGet(int statusId)
    {
        var result = new Result<DicStatus>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<DicStatus>(StatusyPrefix + "/" + statusId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicStatus>> StatusPost(DicStatusDto status)
    {
        var result = new Result<DicStatus>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(StatusyPrefix, status);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicStatus>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicStatus>> StatusPut(int statusId, DicStatusDto status)
    {
        var result = new Result<DicStatus>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(StatusyPrefix + "/" + statusId, status);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicStatus>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> StatusDelete(int statusId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(StatusyPrefix + "/" + statusId);
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