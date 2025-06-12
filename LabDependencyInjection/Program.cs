using LabDependencyInjection.Services;

namespace LabDependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = RegisterTypes(args);

        // Frueher gab es eine separate Startup-Klasse
        // Die Logik sollte mindestens in einer eigenen Methode erfolgen
        Startup(builder.Build());
    }

    private static WebApplicationBuilder RegisterTypes(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddOperationServices()
            .AddControllers();

        return builder;
    }

    private static void Startup(WebApplication app)
    {
        app.MapControllerRoute("default", "{controller=Test}");

        app.Run();
    }
}
