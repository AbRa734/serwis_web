using ApiService.Models;
using ApiService.Repositories;

namespace ApiService;

public class ApiService
{
    private string BaseUrl { get; set; } = Environment.GetEnvironmentVariable("SERWIS_API_URL") ?? "";

    private readonly TokenService.TokenService _tokenService;
    private readonly IHttpClientFactory _httpClientFactory;
    public UzytkownicyRepo UzytkownicyRepo { get; set; }
    public AdministracjaRepo AdministracjaRepo { get; set; }
    public ZamowieniaRepo ZamowieniaRepo { get; set; }
    public KlienciRepo KlienciRepo { get; set; }
    public LogiRepo LogiRepo { get; set; }
    public KomentarzeRepo KomentarzeRepo { get; set; }
    public TypySerwisuRepo TypySerwisuRepo { get; set; }
    public TypyOcenyRepo TypyOcenyRepo { get; set; }
    public TypyKlientaRepo TypyKlientaRepo { get; set; }
    public StatusyRepo StatusyRepo { get; set; }
    public SerwisyRepo SerwisyRepo { get; set; }
    public SerwisanciRepo SerwisanciRepo { get; set; }
    public RoleUzytkownikaRepo RoleUzytkownikaRepo { get; set; }
    public PriotytetyRepo PriotytetyRepo { get; set; }
    public OcenySerwisuRepo OcenySerwisuRepo { get; set; }
    public NumeryTelefonuRepo NumeryTelefonuRepo { get; set; }
    public MetodyPlatnosciRepo MetodyPlatnosciRepo { get; set; }
    public DostepnosciSerwisantaRepo DostepnosciSerwisantaRepo { get; set; }
    public AdresyEmailRepo AdresyEmailRepo { get; set; }
    public AdresyRepo AdresyRepo { get; set; }
    public KontaktEmailRepo KontaktEmailRepo { get; set; }

    public ApiService(IHttpClientFactory httpClientFactory, TokenService.TokenService tokenService)
    {
        this._httpClientFactory = httpClientFactory;
        _tokenService = tokenService;

        var httpClient = httpClientFactory.CreateClient("ApiWithAuth");

        UzytkownicyRepo = new UzytkownicyRepo(httpClient, tokenService);
        AdministracjaRepo = new AdministracjaRepo(httpClient, tokenService);
        ZamowieniaRepo = new ZamowieniaRepo(httpClient, tokenService);
        KlienciRepo = new KlienciRepo(httpClient, tokenService);
        LogiRepo = new LogiRepo(httpClient, tokenService);
        KomentarzeRepo = new KomentarzeRepo(httpClient, tokenService);
        TypySerwisuRepo = new TypySerwisuRepo(httpClient, tokenService);
        TypyOcenyRepo = new TypyOcenyRepo(httpClient, tokenService);
        TypyKlientaRepo = new TypyKlientaRepo(httpClient, tokenService);
        StatusyRepo = new StatusyRepo(httpClient, tokenService);
        SerwisyRepo = new SerwisyRepo(httpClient, tokenService);
        SerwisanciRepo = new SerwisanciRepo(httpClient, tokenService);
        RoleUzytkownikaRepo = new RoleUzytkownikaRepo(httpClient, tokenService);
        PriotytetyRepo = new PriotytetyRepo(httpClient, tokenService);
        OcenySerwisuRepo = new OcenySerwisuRepo(httpClient, tokenService);
        NumeryTelefonuRepo = new NumeryTelefonuRepo(httpClient, tokenService);
        MetodyPlatnosciRepo = new MetodyPlatnosciRepo(httpClient, tokenService);
        DostepnosciSerwisantaRepo = new DostepnosciSerwisantaRepo(httpClient, tokenService);
        AdresyRepo = new AdresyRepo(httpClient, tokenService);
        AdresyEmailRepo = new AdresyEmailRepo(httpClient, tokenService);
        KontaktEmailRepo = new KontaktEmailRepo(httpClient, tokenService);
    }

    public  async Task<Uzytkownik> GetUzytkownik()
    {
        var uzytkownikEmail = _tokenService.GetUserEmail();
        Uzytkownik uzytkownik = null!;
        var uzytkownicy = await UzytkownicyRepo.UzytkownicyGet();
        if (uzytkownicy.Data != null)
        {
            foreach (var uzytkownikItem in uzytkownicy.Data)
            {
                if (uzytkownikItem.AdresEmail.Email == uzytkownikEmail)
                {
                    uzytkownik = uzytkownikItem;
                    break;
                }
            }
        }
        else
        {
            throw new Exception("Nie można pobrać użytkowników");
        }

        return uzytkownik;
    }
}