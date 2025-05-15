namespace ApiService.Models;

public class AdresEmail
{
    public int AdresEmailId { get; set; }
    public string Email { get; set; } = null!;
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class AdresEmailDto
{
    public string Email { get; set; } = null!;
}