namespace ApiService.Models;

public class Zamowienie
{
    public int ZamowienieId { get; set; }
    public int? SerwisantId { get; set; }
    public Serwisant? Serwisant { get; set; }
    public int KlientId { get; set; }
    public Klient Klient { get; set; } = null!;
    public Serwis Serwis { get; set; } = null!;
    public int StatusId { get; set; }
    public DicStatus Status { get; set; } = null!;
    public int PriorytetId { get; set; }
    public DicPriorytet Priorytet { get; set; } = null!;
    public int MetodaPlatnosciId { get; set; }
    public DicMetodaPlatnosci MetodaPlatnosci { get; set; } = null!;
    public ICollection<Komentarz>? Komentarze { get; set; }
    public int? NumerTelefonuId { get; set; }
    public NumerTelefonu? NumerTelefonu { get; set; }
    public int? AdresEmailId { get; set; }
    public AdresEmail? AdresEmail { get; set; }
    public int? AdresId { get; set; }
    public Adres? Adres { get; set; }
    public int Koszt100 { get; set; }
    public String Opis { get; set; } = "";
    public DateTimeOffset? PlanowanaDataRealizacjiOd { get; set; }
    public DateTimeOffset? PlanowanaDataRealizacjiDo { get; set; }
    public DateTimeOffset? ZrealizowanaDataRealizacjiOd { get; set; }
    public DateTimeOffset? ZrealizowanaDataRealizacjiDo { get; set; }
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; }
}

public class ZamowienieDto
{
    public int? SerwisantId { get; set; }
    public int KlientId { get; set; }
    public int SerwisId { get; set; }
    public int StatusId { get; set; }
    public int PriorytetId { get; set; }
    public int MetodaPlatnosciId { get; set; }
    public List<int>? KomentarzeIds { get; set; } = new List<int>();
    public int? NumerTelefonuId { get; set; }
    public int? AdresEmailId { get; set; }
    public int? AdresId { get; set; }
    public int Koszt100 { get; set; }
    public String Opis { get; set; } = "";
    public DateTimeOffset? PlanowanaDataRealizacjiOd { get; set; }
    public DateTimeOffset? PlanowanaDataRealizacjiDo { get; set; }
    public DateTimeOffset? ZrealizowanaDataRealizacjiOd { get; set; }
    public DateTimeOffset? ZrealizowanaDataRealizacjiDo { get; set; }
}