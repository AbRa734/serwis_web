using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class TypySerwisuRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string TypySerwisuPrefix = "/typy-serwisu";

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

    public async Task<Result<List<DicTypSerwisu>>> TypySerwisuGet()
    {
        var result = new Result<List<DicTypSerwisu>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<DicTypSerwisu>>(TypySerwisuPrefix);
                result.Data = response ?? new List<DicTypSerwisu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypSerwisu>> TypSerwisuGet(int typSerwisuId)
    {
        var result = new Result<DicTypSerwisu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<DicTypSerwisu>(TypySerwisuPrefix + "/" + typSerwisuId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypSerwisu>> TypSerwisuPost(DicTypSerwisuDto typSerwisu)
    {
        var result = new Result<DicTypSerwisu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(TypySerwisuPrefix, typSerwisu);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicTypSerwisu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypSerwisu>> TypSerwisuPut(int typSerwisuId, DicTypSerwisuDto typSerwisu)
    {
        var result = new Result<DicTypSerwisu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(TypySerwisuPrefix + "/" + typSerwisuId, typSerwisu);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicTypSerwisu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> TypSerwisuDelete(int typSerwisuId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(TypySerwisuPrefix + "/" + typSerwisuId);
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