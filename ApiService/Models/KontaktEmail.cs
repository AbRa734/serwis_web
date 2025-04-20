namespace ApiService.Models;

public class KontaktEmailRequest
{
    public string Imie { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Temat { get; set; } = null!;
    public string Tresc { get; set; } = null!;
}