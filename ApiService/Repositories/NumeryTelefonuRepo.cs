using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class NumeryTelefonuRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string NumeryTelefonuPrefix = "/numery-telefonu";

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

    public async Task<Result<List<NumerTelefonu>>> NumeryTelefonuGet()
    {
        var result = new Result<List<NumerTelefonu>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<NumerTelefonu>>(NumeryTelefonuPrefix);
                result.Data = response ?? new List<NumerTelefonu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<NumerTelefonu>> NumerTelefonuGet(int numerTelefonuId)
    {
        var result = new Result<NumerTelefonu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<NumerTelefonu>(NumeryTelefonuPrefix + "/" + numerTelefonuId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<NumerTelefonu>> NumerTelefonuPost(NumerTelefonuDto numerTelefonu)
    {
        var result = new Result<NumerTelefonu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(NumeryTelefonuPrefix, numerTelefonu);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<NumerTelefonu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<NumerTelefonu>> NumerTelefonuPut(int numerTelefonuId, NumerTelefonuDto numerTelefonu)
    {
        var result = new Result<NumerTelefonu>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(NumeryTelefonuPrefix + "/" + numerTelefonuId, numerTelefonu);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<NumerTelefonu>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> NumerTelefonuDelete(int numerTelefonuId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(NumeryTelefonuPrefix + "/" + numerTelefonuId);
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