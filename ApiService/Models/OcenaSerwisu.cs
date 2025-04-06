namespace ApiService.Models;

public class OcenaSerwisu
{
    public int OcenaSerwisuId { get; set; }

    public int SerwisId { get; set; }
    public Serwis Serwis { get; set; } = null!;
    public int SerwisantId { get; set; }
    public Serwisant Serwisant { get; set; } = null!;
    public int TypOcenyId { get; set; }
    public DicTypOceny TypOceny { get; set; } = null!;
    public string? Komentarz { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class OcenaSerwisuDto
{
    public int SerwisId { get; set; }
    public int SerwisantId { get; set; }
    public int TypOcenyId { get; set; }
    public string? Komentarz { get; set; }
}