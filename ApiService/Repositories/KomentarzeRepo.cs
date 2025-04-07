using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class KomentarzeRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string KomentarzePrefix = "/komentarze";

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

    public async Task<Result<List<Komentarz>>> KomentarzeGet()
    {
        var result = new Result<List<Komentarz>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<Komentarz>>(KomentarzePrefix);
                result.Data = response ?? new List<Komentarz>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Komentarz>> KomentarzGet(int komentarzId)
    {
        var result = new Result<Komentarz>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Komentarz>(KomentarzePrefix + "/" + komentarzId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Komentarz>> KomentarzPost(KomentarzDto komentarz)
    {
        var result = new Result<Komentarz>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(KomentarzePrefix, komentarz);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Komentarz>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<Komentarz>> KomentarzPut(int komentarzId, KomentarzDto komentarz)
    {
        var result = new Result<Komentarz>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(KomentarzePrefix + "/" + komentarzId, komentarz);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<Komentarz>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> KomentarzDelete(int komentarzId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(KomentarzePrefix + "/" + komentarzId);
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