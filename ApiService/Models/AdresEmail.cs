namespace ApiService.Models;

public class AdresEmail
{
    public int AdresEmailId { get; set; }
    public string Email { get; set; } = null!;
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class AdresEmailDto
{
    public string Email { get; set; } = null!;
}