namespace ApiService.Models;

public class NumerTelefonu
{
    public int NumerTelefonuId { get; set; }
    public string Numer { get; set; } = null!;
    public DateTime? DataAktualizacji { get; set; }
    public DateTime DataDodania { get; init; } 
}

public class NumerTelefonuDto
{
    public string Numer { get; set; } = null!;
}