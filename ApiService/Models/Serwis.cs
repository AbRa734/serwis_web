namespace ApiService.Models;

public class Serwis
{
    public int SerwisId { get; set; }
    public int TypSerwisuId { get; set; }
    public DicTypSerwisu TypSerwisu { get; set; } = null!;
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class SerwisDto
{
    public int TypSerwisuId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}