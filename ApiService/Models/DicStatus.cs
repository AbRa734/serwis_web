namespace ApiService.Models;

public class DicStatus
{
    public int StatusId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; } 
}

public class DicStatusDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}