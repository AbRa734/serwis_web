using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class RoleUzytkownikaRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string RoleUzytkownikaPrefix = "/role-uzytkownika";

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

    public async Task<Result<List<DicRolaUzytkownika>>> RoleUzytkownikaGet()
    {
        var result = new Result<List<DicRolaUzytkownika>>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<DicRolaUzytkownika>>(RoleUzytkownikaPrefix);
                result.Data = response ?? new List<DicRolaUzytkownika>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicRolaUzytkownika>> RolaUzytkownikaGet(int rolaUzytkownikaId)
    {
        var result = new Result<DicRolaUzytkownika>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<DicRolaUzytkownika>(RoleUzytkownikaPrefix + "/" + rolaUzytkownikaId);
                result.Data = response;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicRolaUzytkownika>> RolaUzytkownikaPost(DicRolaUzytkownikaDto rolaUzytkownika)
    {
        var result = new Result<DicRolaUzytkownika>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(RoleUzytkownikaPrefix, rolaUzytkownika);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicRolaUzytkownika>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<DicRolaUzytkownika>> RolaUzytkownikaPut(int rolaUzytkownikaId, DicRolaUzytkownikaDto rolaUzytkownika)
    {
        var result = new Result<DicRolaUzytkownika>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(RoleUzytkownikaPrefix + "/" + rolaUzytkownikaId, rolaUzytkownika);
                response.EnsureSuccessStatusCode();
                result.Data = await response.Content.ReadFromJsonAsync<DicRolaUzytkownika>();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }

    public async Task<Result<bool>> RolaUzytkownikaDelete(int rolaUzytkownikaId)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.DeleteAsync(RoleUzytkownikaPrefix + "/" + rolaUzytkownikaId);
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