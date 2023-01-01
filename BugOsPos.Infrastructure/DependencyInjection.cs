using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Clock;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Infrastructure.Authentication;
using BugOsPos.Infrastructure.Persistence;
using BugOsPos.Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BugOsPos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddPersistence();
        services.AddAuthentication(configuration);
        
        services.Configure<PasswordProviderSettings>(configuration.GetSection("PasswordProviderSettings"));
        services.AddScoped<IPasswordProvider, PasswordProvider>();

        services.AddSingleton<IClock, Clock>();
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IShiftRepository, ShiftRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ILoyaltyDiscountRepository, LoyaltyDiscountRepository>();
        services.AddScoped<ILoyaltyCardRepository, LoyaltyCardRepository>();
        services.AddScoped<IFranchiseRepository, FranchiseRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind("JwtSettings", jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        
        return services;
    }
}
