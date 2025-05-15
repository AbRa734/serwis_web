namespace ApiService.Models;

public class NumerTelefonu
{
    public int NumerTelefonuId { get; set; }
    public string Numer { get; set; } = null!;
    public DateTimeOffset? DataAktualizacji { get; set; }
    public DateTimeOffset DataDodania { get; init; } 
}

public class NumerTelefonuDto
{
    public string Numer { get; set; } = null!;
}