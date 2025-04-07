using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class TypyOcenyRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string TypyOcenyPrefix = "/typy-oceny";

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

    public async Task<Result<List<DicTypOceny>>> TypyOcenyGet()
    {
        var result = new Result<List<DicTypOceny>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<DicTypOceny>>(TypyOcenyPrefix);
                result.Data = response ?? new List<DicTypOceny>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypOceny>> TypOcenyGet(int typOcenyId)
    {
        var result = new Result<DicTypOceny>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<DicTypOceny>(TypyOcenyPrefix + "/" + typOcenyId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypOceny>> TypOcenyPost(DicTypOcenyDto typOceny)
    {
        var result = new Result<DicTypOceny>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(TypyOcenyPrefix, typOceny);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicTypOceny>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicTypOceny>> TypOcenyPut(int typOcenyId, DicTypOcenyDto typOceny)
    {
        var result = new Result<DicTypOceny>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(TypyOcenyPrefix + "/" + typOcenyId, typOceny);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicTypOceny>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> TypOcenyDelete(int typOcenyId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(TypyOcenyPrefix + "/" + typOcenyId);
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