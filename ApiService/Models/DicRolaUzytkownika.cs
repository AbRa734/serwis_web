namespace ApiService.Models;

public class DicRolaUzytkownika
{
    public int RolaUzytkownikaId { get; set; }
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; } 
}

public class DicRolaUzytkownikaDto
{
    public string Nazwa { get; set; } = null!;
    public string? Opis { get; set; }
}