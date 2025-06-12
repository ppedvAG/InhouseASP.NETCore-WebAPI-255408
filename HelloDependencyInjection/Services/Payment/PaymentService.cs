namespace HelloDependencyInjection.Services.Payment;

internal class PaymentService : IPaymentService
{
    public void MakePayment()
    {
        Console.WriteLine($"Payment made:\t[{GetHashCode()}]");
    }
}
