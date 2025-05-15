namespace ApiService.Models;

public class ResetHasla
{
    public int ResetHaslaId { get; set; }
    public int UzytkownikId { get; set; }
    public String Token { get; set; } = null!;
    public bool CzyLinkKlikniety { get; set; } = false;
    public bool CzyZrestartowanoHaslo { get; set; } = false;
    public DateTimeOffset DataDodania { get; init; } = DateTimeOffset.UtcNow;
}

public class ResetHaslaDto
{
    public String Token { get; set; } = null!;
    public bool CzyLinkKlikniety { get; set; } = false;
    public bool CzyZrestartowanoHaslo { get; set; } = false;
}

public class ZmianaHaslaRequest
{
    public string Haslo { get; set; } = null!;
    public int UzytkownikId { get; set; }
}