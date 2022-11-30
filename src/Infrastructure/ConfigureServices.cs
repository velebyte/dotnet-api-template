using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Shared;
using Infrastructure.Authentication;
using Infrastructure.Authentication.JwtGenerator;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // Add Auth services
        services.AddAuth(configuration);

        // Setup dbcontext and initializer
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Filename=AppDatabase.db",
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        services.AddScoped<ApplicationDbContextInitialiser>();

        // Add FlowerRepository
        services.AddScoped<IFlowerRepository>(provider => provider.GetRequiredService<FlowerRepository>());

        // Add a DateTime provider
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // Setup the JWT Generator, Add Authentication service and Identity
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddIdentityCore<ApplicationUser>(options => options.User.RequireUniqueEmail = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Read and bind jwt settings from appsettings
        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        // Setup the JWT Bearer schema
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.Secret))
                        });

        services.AddAuthorization();

        return services;
    }
}
