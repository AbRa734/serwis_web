using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class LogiRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string LogiPrefix = "/logi";

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

    public async Task<Result<List<Log>>> LogiGet()
    {
        var result = new Result<List<Log>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Log>>(LogiPrefix);
                result.Data = response ?? new List<Log>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Log>> LogGet(int logId)
    {
        var result = new Result<Log>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Log>(LogiPrefix + "/" + logId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Log>> LogPost(LogDto log)
    {
        var result = new Result<Log>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(LogiPrefix, log);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Log>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Log>> LogPut(int logId, LogDto log)
    {
        var result = new Result<Log>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(LogiPrefix + "/" + logId, log);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Log>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> LogDelete(int logId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(LogiPrefix + "/" + logId);
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