using BusinessLogic.Contracts;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class SetupExtensions
{
    /// <summary>
    /// Wir haben noch keine Datenbank, weswegen wir vorerst die Daten im Memory halten
    /// und deswegen hier Singleton verwenden muessen! Allerdings ganz schlechte Idee bei
    /// einer Anwendung mit mehreren Clients. NUR ZU TESTZWECKEN!
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInMemoryServices(this IServiceCollection services)
        => services.AddSingleton<IStaticRecipeService, StaticRecipeService>();
}
