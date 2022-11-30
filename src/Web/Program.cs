using Web.Common;
using Web.Endpoints;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateBootstrapLogger();

var builder = WebApplication
                .CreateBuilder(args)
                .ConfigureBuilder();

var app = builder
            .Build()
            .ConfigureApplication();

// Healthcheck endpoint
app.MapHealthChecks("/ping");

// Error endpoint where the integrated
// exception handling middleware reroutes
// thrown exception
app.MapErrorEndpoint();

// Mapping our endpoints
app.MapAuthenticationEndpoints();
app.MapFlowersEndpoints();


try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
