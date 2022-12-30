using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Infrastructure.Authentication;
using BugOsPos.Infrastructure.Persistence;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace BugOsPos.Api.Mappings;

public static class SwaggerInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}
