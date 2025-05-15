namespace ApiService.Models;

public class Klient
{
    public int KlientId { get; set; }
    public int UzytkownikId { get; set; }
    public Uzytkownik Uzytkownik { get; set; } = null!;
    public int TypKlientaId { get; set; }
    public DicTypKlienta TypKlienta { get; set; } = null!;
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class KlientDto
{
    public int UzytkownikId { get; set; }
    public int TypKlientaId { get; set; }
}