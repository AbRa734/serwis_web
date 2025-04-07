using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class DostepnosciSerwisantaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string DostepnosciSerwisantaPrefix = "/dostepnosci-serwisanta";

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

    public async Task<Result<List<DostepnoscSerwisanta>>> DostepnosciSerwisantaGet()
    {
        var result = new Result<List<DostepnoscSerwisanta>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<DostepnoscSerwisanta>>(DostepnosciSerwisantaPrefix);
                result.Data = response ?? new List<DostepnoscSerwisanta>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DostepnoscSerwisanta>> DostepnoscSerwisantaGet(int serwisantId)
    {
        var result = new Result<DostepnoscSerwisanta>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<DostepnoscSerwisanta>(DostepnosciSerwisantaPrefix + "/" + serwisantId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DostepnoscSerwisanta>> DostepnoscSerwisantaPost(DostepnoscSerwisantaDto dostepnoscSerwisanta)
    {
        var result = new Result<DostepnoscSerwisanta>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(DostepnosciSerwisantaPrefix, dostepnoscSerwisanta);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DostepnoscSerwisanta>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DostepnoscSerwisanta>> DostepnoscSerwisantaPut(int serwisantId, DostepnoscSerwisantaDto dostepnoscSerwisanta)
    {
        var result = new Result<DostepnoscSerwisanta>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(DostepnosciSerwisantaPrefix + "/" + serwisantId, dostepnoscSerwisanta);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DostepnoscSerwisanta>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> DostepnoscSerwisantaDelete(int serwisantId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(DostepnosciSerwisantaPrefix + "/" + serwisantId);
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