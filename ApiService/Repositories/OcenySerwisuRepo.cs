using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class OcenySerwisuRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string OcenySerwisuPrefix = "/oceny-serwisu";

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

    public async Task<Result<List<OcenaSerwisu>>> OcenySerwisuGet()
    {
        var result = new Result<List<OcenaSerwisu>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<OcenaSerwisu>>(OcenySerwisuPrefix);
                result.Data = response ?? new List<OcenaSerwisu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<OcenaSerwisu>> OcenaSerwisuGet(int ocenaSerwisuId)
    {
        var result = new Result<OcenaSerwisu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<OcenaSerwisu>(OcenySerwisuPrefix + "/" + ocenaSerwisuId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<OcenaSerwisu>> OcenaSerwisuPost(OcenaSerwisuDto ocenaSerwisu)
    {
        var result = new Result<OcenaSerwisu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(OcenySerwisuPrefix, ocenaSerwisu);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<OcenaSerwisu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<OcenaSerwisu>> OcenaSerwisuPut(int ocenaSerwisuId, OcenaSerwisuDto ocenaSerwisu)
    {
        var result = new Result<OcenaSerwisu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(OcenySerwisuPrefix + "/" + ocenaSerwisuId, ocenaSerwisu);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<OcenaSerwisu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> OcenaSerwisuDelete(int ocenaSerwisuId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(OcenySerwisuPrefix + "/" + ocenaSerwisuId);
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