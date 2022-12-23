using Serilog;

namespace Web.Common;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, string appName)
    {
        // Add services to the container.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = appName,
                Version = "v1"
            });
        });

        services.AddHealthChecks();

        services.AddControllers();

        return services;
    }

    public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
    {
        // Setup Serilog
        hostBuilder.UseSerilog((context, services, configuration) =>
                                configuration
                                .ReadFrom.Configuration(context.Configuration)
                                .ReadFrom.Services(services)
                                .Enrich.FromLogContext());

        return hostBuilder;
    }
}
