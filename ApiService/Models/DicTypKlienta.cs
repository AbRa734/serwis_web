namespace ApiService.Models;

public class DicTypKlienta
{
    public int TypKlientaId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class DicTypKlientaDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}