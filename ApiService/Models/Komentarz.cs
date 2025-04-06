namespace ApiService.Models;

public class Komentarz
{
    public int KomentarzId { get; set; }
    public string Tresc { get; set; } = null!;
    public int UzytkownikId { get; set; }
    public Uzytkownik Uzytkownik { get; set; } = null!;
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; } 
}

public class KomentarzDto
{
    public string Tresc { get; set; } = null!;
    public int UzytkownikId { get; set; }
}