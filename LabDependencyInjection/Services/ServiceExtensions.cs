using Lab002_DependencyInjection.Services;

namespace LabDependencyInjection.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddOperationServices(this IServiceCollection services) 
        => services.AddSingleton<IOperationSingleton, OperationService>()
                .AddScoped<IOperationScoped, OperationService>()
                .AddTransient<IOperationTransient, OperationService>();
}
