namespace ApiService.Models;

public class DicMetodaPlatnosci
{
    public int MetodaPlatnosciId { get; init; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class DicMetodaPlatnosciDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}