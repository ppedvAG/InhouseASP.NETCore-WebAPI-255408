using HelloDependencyInjection.Services.Payment;
using HelloDependencyInjection.Services.Shopping;
using Microsoft.Extensions.DependencyInjection;

namespace HelloDependencyInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // DIP: Dependency Inversion Principle
            // Die Abhangigkeit wird von aussen gesetzt.
            var paymentService = new PaymentService();
            var shoppingCartService = new ShoppingCartService(paymentService, new StoreSettings());
            shoppingCartService.PayOrder();

            // Mit Dependency Injection Package
            // Install-Package Microsoft.Extensions.DependencyInjection
            ServiceProvider serviceProvider = RegisterTypesOnInitialAppStartup();

            // Bei der Verwendung wird der sog. Dependency Tree automatisch aufgeloest
            var shoppingCart2 = serviceProvider.GetService<IShoppingService>();
            shoppingCart2.PayOrder();

            using (var scope = serviceProvider.CreateScope())
            {
                var shoppingCart3 = scope.ServiceProvider.GetService<IShoppingService>();
                shoppingCart3.PayOrder();

                // Hat dieselbe PaymentService Instanz
                var shoppingCart4 = scope.ServiceProvider.GetService<IShoppingService>();
                shoppingCart4.PayOrder();
            }
        }

        private static ServiceProvider RegisterTypesOnInitialAppStartup()
        {
            var collection = new ServiceCollection();

            // 3 varianten Dependencies zu registrieren (Lebenszyklus für die Instanzen)

            // Jedes mal wird das Objekt neu erzeugt
            collection.AddTransient<IShoppingService, ShoppingCartService>();

            // Jedes Objekt wird innerhalb eines Scopes nur einmal verwendet
            collection.AddScoped<IPaymentService, PaymentService>();

            // Ein Objekt wird nur ein einziges Mal erzeugt
            collection.AddSingleton<StoreSettings>();

            var serviceProvider = collection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
