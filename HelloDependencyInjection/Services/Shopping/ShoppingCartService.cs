using HelloDependencyInjection.Services.Payment;

namespace HelloDependencyInjection.Services.Shopping;

internal class ShoppingCartService : IShoppingService
{
    private readonly IPaymentService _paymentService;
    private readonly StoreSettings _storeSettings;

    public ShoppingCartService(IPaymentService paymentService, StoreSettings storeSettings)
    {
        _paymentService = paymentService;
        _storeSettings = storeSettings;
    }

    public void PayOrder()
    {
        Console.WriteLine($"Store settings:\t{_storeSettings.Foo}");
        Console.WriteLine($"Paying order:\t[{GetHashCode()}]");
        _paymentService.MakePayment();
        Console.WriteLine();
    }
}
