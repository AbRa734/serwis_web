namespace ApiService.Models;

public class DicPriorytet
{
    public int PriorytetId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class DicPriorytetDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}