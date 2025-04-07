using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class MetodyPlatnosciRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string MetodyPlatnosciPrefix = "/metody-platnosci";

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

    public async Task<Result<List<DicMetodaPlatnosci>>> MetodyPlatnosciGet()
    {
        var result = new Result<List<DicMetodaPlatnosci>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<DicMetodaPlatnosci>>(MetodyPlatnosciPrefix);
                result.Data = response ?? new List<DicMetodaPlatnosci>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicMetodaPlatnosci>> MetodaPlatnosciGet(int metodaPlatnosciId)
    {
        var result = new Result<DicMetodaPlatnosci>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<DicMetodaPlatnosci>(MetodyPlatnosciPrefix + "/" + metodaPlatnosciId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicMetodaPlatnosci>> MetodaPlatnosciPost(DicMetodaPlatnosciDto metodaPlatnosci)
    {
        var result = new Result<DicMetodaPlatnosci>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(MetodyPlatnosciPrefix, metodaPlatnosci);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicMetodaPlatnosci>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicMetodaPlatnosci>> MetodaPlatnosciPut(int metodaPlatnosciId, DicMetodaPlatnosciDto metodaPlatnosci)
    {
        var result = new Result<DicMetodaPlatnosci>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(MetodyPlatnosciPrefix + "/" + metodaPlatnosciId, metodaPlatnosci);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicMetodaPlatnosci>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> MetodaPlatnosciDelete(int metodaPlatnosciId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(MetodyPlatnosciPrefix + "/" + metodaPlatnosciId);
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