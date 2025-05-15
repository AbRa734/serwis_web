namespace ApiService.Models;

public class DicTypSerwisu
{
    public int TypSerwisuId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class DicTypSerwisuDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}