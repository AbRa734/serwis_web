namespace ApiService.Models;

public class DostepnoscSerwisanta
{
    public int DostepnoscSerwisantaId { get; set; }
    public int SerwisantId { get; set; }
    public Serwisant Serwisant { get; set; } = null!;
    public bool CzyDostepny { get; set; }
    public ICollection<Serwis> Serwisy { get; set; } = new List<Serwis>();
    public DateTime DataOd { get; set; }
    public DateTime DataDo { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class DostepnoscSerwisantaDto
{
    public int SerwisantId { get; set; }
    public bool CzyDostepny { get; set; }
    public List<int> SerwisyIds { get; set; } = new List<int>();
    public DateTime DataOd { get; set; }
    public DateTime DataDo { get; set; }
}