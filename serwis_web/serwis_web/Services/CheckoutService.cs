using Stripe.Checkout;

namespace serwis_web.Services;

public class CheckoutSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutSessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Session> CreateCheckoutSessionAsync()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        var domain = $"{request?.Scheme}://{request?.Host}";

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 1000,
                        Currency = "pln",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Test",
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = $"{domain}/",
            CancelUrl = $"{domain}/",
        };

        var service = new SessionService();
        return await service.CreateAsync(options);
    }
}