using Stripe.Checkout;

namespace serwis_web.Services;

public class CheckoutSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutSessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Session> CreateCheckoutSessionAsync(PaymentRequest paymentRequest)
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        var domain = $"{request?.Scheme}://{request?.Host}";
    
        // Get the referrer (previous page)
        var referer = _httpContextAccessor.HttpContext?.Request.Headers.Referer.ToString();
        var returnUrl = !string.IsNullOrEmpty(referer) ? referer : $"{domain}";

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = paymentRequest.AmountInCents,
                        Currency = paymentRequest.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = paymentRequest.ProductName,
                            Description = paymentRequest.Description
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = $"{returnUrl}?payment_status=success",
            CancelUrl = $"{returnUrl}?payment_status=cancel",
        };

        var service = new SessionService();
        return await service.CreateAsync(options);
    }
}

public class PaymentRequest
{
    public long AmountInCents { get; set; }
    public string Currency { get; set; } = "pln";
    public string ProductName { get; set; } = "Usługa serwisowa";
    public string Description { get; set; } = "";
}