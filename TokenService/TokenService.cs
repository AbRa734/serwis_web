namespace TokenService;

public class TokenService
{
    private string? _token;
    private string? _userEmail;

    public void SetToken(string token)
    {
        _token = token;
    }

    public string? GetToken()
    {
        return _token;
    }

    public void SetUserEmail(string email)
    {
        _userEmail = email;
    }

    public string? GetUserEmail()
    {
        return _userEmail;
    }


    public void ClearToken()
    {
        _token = null;
    }

    public bool HasToken => !string.IsNullOrWhiteSpace(_token);
}