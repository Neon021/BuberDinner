using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BuberDinner.api.Common.Mapping;

public static class DependenctInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper> ();
        return services;
    }
}