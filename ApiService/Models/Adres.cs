namespace ApiService.Models;

public class Adres
{
    public int AdresId { get; set; }
    public string Ulica { get; set; } = null!;
    public int NumerDomu { get; set; }
    public int? NumerMieszkania { get; set; }
    public string KodPocztowy { get; set; } = null!;
    public string Miasto { get; set; } = null!;
    public string Kraj { get; set; } = null!;
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class AdresDto
{
    public string Ulica { get; set; } = null!;
    public int NumerDomu { get; set; }
    public int? NumerMieszkania { get; set; }
    public string KodPocztowy { get; set; } = null!;
    public string Miasto { get; set; } = null!;
    public string Kraj { get; set; } = null!;
}