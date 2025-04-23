namespace ApiService.Models;

public class Uzytkownik
{
    public int UzytkownikId { get; set; }
    public DicRolaUzytkownika RolaUzytkownika { get; set; } 
    public string? Imie { get; set; }
    public string? Nazwisko { get; set; }
    public int AdresEmailId { get; set; }
    public AdresEmail AdresEmail { get; set; } = null!;
    public int? NumerTelefonuId { get; set; }
    public NumerTelefonu? NumerTelefonu { get; set; }
    public Adres? Adres { get; set; }
    Autoryzacja? Autoryzacja { get; set; }
    public bool CzyAktywny { get; set; } = true;
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; }
}

public class UzytkownikDto
{
    public int RolaUzytkownikaId { get; set; }
    public string? Imie { get; set; }
    public string? Nazwisko { get; set; }
    public int AdresEmailId { get; set; }
    public int? NumerTelefonuId { get; set; }
    public bool CzyAktywny { get; set; }
    public int? AdresId { get; set; }
}

public class Autoryzacja
{
    public string Email { get; set; } = null!;
    public string Haslo { get; set; } = null!;
}

public class LoginRequest
{
    public string Email { get; set; } = null!;
    public string Haslo { get; set; } = null!;
}

public class RegisterRequest
{
    public string Email { get; set; } = null!;
    public string Haslo { get; set; } = null!;
    public string? Numer { get; set; }
    public string? Imie { get; set; }
    public string? Nazwisko { get; set; }
}

public class RegisterRequestIdentity
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