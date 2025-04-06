namespace ApiService.Models;

public class DicTypOceny
{
    public int TypOcenyId { get; set; }
    public int Ocena { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class DicTypOcenyDto
{
    public int Ocena { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}