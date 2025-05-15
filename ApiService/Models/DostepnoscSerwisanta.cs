namespace ApiService.Models;

public class DostepnoscSerwisanta
{
    public int DostepnoscSerwisantaId { get; set; }
    public int SerwisantId { get; set; }
    public Serwisant Serwisant { get; set; } = null!;
    public bool CzyDostepny { get; set; }
    public ICollection<Serwis> Serwisy { get; set; } = new List<Serwis>();
    public DateTimeOffset DataOd { get; set; }
    public DateTimeOffset DataDo { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class DostepnoscSerwisantaDto
{
    public int SerwisantId { get; set; }
    public bool CzyDostepny { get; set; }
    public List<int> SerwisyIds { get; set; } = new List<int>();
    public DateTimeOffset DataOd { get; set; }
    public DateTimeOffset DataDo { get; set; }
}