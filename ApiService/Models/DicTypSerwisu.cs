namespace ApiService.Models;

public class DicTypSerwisu
{
    public int TypSerwisuId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class DicTypSerwisuDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}