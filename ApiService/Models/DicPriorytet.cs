namespace ApiService.Models;

public class DicPriorytet
{
    public int PriorytetId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class DicPriorytetDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}