namespace ApiService.Models;

public class KontaktEmailRequest
{
    public string EmailOdbiorcy { get; set; } = null!;
    public string Temat { get; set; } = null!;
    public string Tresc { get; set; } = null!;
}