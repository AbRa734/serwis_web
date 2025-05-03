namespace ApiService.Models;

public class Serwisant
{
    public int SerwisantId { get; set; }
    public int UzytkownikId { get; set; }
    public Uzytkownik Uzytkownik { get; set; } = null!;
    public ICollection<Serwis> Serwisy { get; set; } = new List<Serwis>();
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
    public bool CzyAktywny { get; set; } = true;
}

public class SerwisantDto
{
    public int UzytkownikId { get; set; }
    public List<int> SerwisyIds { get; set; } = new List<int>();
    public bool CzyAktywny { get; set; }
}