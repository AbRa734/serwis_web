using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiService.Helpers;
using ApiService.Models;

namespace ApiService.Repositories;

public class KontaktEmailRepo(HttpClient httpClient, TokenService.TokenService tokenService)
{
    private const string KontaktEmailPrefix = "/wyslij-email";

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

    public async Task<Result<bool>> KontaktEmailPost(KontaktEmailRequest kontaktEmail)
    {
        var result = new Result<bool>();
        await SetAuthorizationAndExecute(async () =>
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync(KontaktEmailPrefix, kontaktEmail);
                response.EnsureSuccessStatusCode();
                result.Data = true;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
        });
        return result;
    }
}