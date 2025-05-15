namespace ApiService.Models;

public class DicStatus
{
    public int StatusId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; } 
}

public class DicStatusDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}