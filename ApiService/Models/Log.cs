namespace ApiService.Models;

public class Log
{
    public int LogId { get; set; }
    public int ZamowienieId { get; set; }
    public Zamowienie Zamowienie { get; set; } = null!;
    public int? StatusId { get; set; }
    public DicStatus? Status { get; set; }
    public string? Komentarz { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; } 
}

public class LogDto
{
    public int ZamowienieId { get; set; }
    public int? StatusId { get; set; }
    public string? Komentarz { get; set; }
}