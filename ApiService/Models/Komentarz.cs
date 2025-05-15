namespace ApiService.Models;

public class Komentarz
{
    public int KomentarzId { get; set; }
    public string Tresc { get; set; } = null!;
    public int UzytkownikId { get; set; }
    public Uzytkownik Uzytkownik { get; set; } = null!;
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; } 
}

public class KomentarzDto
{
    public string Tresc { get; set; } = null!;
    public int UzytkownikId { get; set; }
}