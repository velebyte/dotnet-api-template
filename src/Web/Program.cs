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

var app = await builder
            .Build()
            .ConfigureApplication();

// Mapping MinimalApi endpoints
// app.MapAuthenticationEndpoints();
// app.MapFlowersEndpoints();

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

// Making Program class accessible for testing
public partial class Program { }