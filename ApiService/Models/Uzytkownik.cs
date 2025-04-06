namespace ApiService.Models;

public class Uzytkownik
{
    public int UzytkownikId { get; set; }
    public ICollection<DicRolaUzytkownika> RoleUzytkownika { get; set; } = new List<DicRolaUzytkownika>();
    public string? Imie { get; set; }
    public string? Nazwisko { get; set; }
    public int AdresEmailId { get; set; }
    public AdresEmail AdresEmail { get; set; } = null!;
    public int? NumerTelefonuId { get; set; }
    public NumerTelefonu? NumerTelefonu { get; set; }
    public ICollection<Adres>? Adresy { get; set; }
    Autoryzacja? Autoryzacja { get; set; }
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class UzytkownikDto
{
    public List<int> RoleUzytkownikaIds { get; set; } = new List<int>();
    public string? Imie { get; set; }
    public string? Nazwisko { get; set; }
    public int AdresEmailId { get; set; }
    public int? NumerTelefonuId { get; set; }
    public List<int>? AdresyIds { get; set; } = new List<int>();
}

public class Autoryzacja
{
    public string Email { get; set; } = null!;
    public string Haslo { get; set; } = null!;
}

public class Token
{
    public string TokenType { get; set; } = null!;
    public string? AccessToken { get; set; } = null!;
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = null!;
}