using Application;
using Infrastructure;
using Serilog;
using Web.Endpoints;

namespace Web.Common;

public static class ProgramExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Host.AddSerilog();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddPresentationServices(builder.Environment.ApplicationName);

        return builder;
    }

    public static async Task<WebApplication> ConfigureApplication(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        // Adds middleware for streamlined request logging
        app.UseSerilogRequestLogging(configure =>
            configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000} ms");

        app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                                $"{app.Environment.ApplicationName} v1"));


        // Redirect HTTP requests to HTTPS
        app.UseHttpsRedirection();
        // Global exception handler
        app.UseExceptionHandler("/error");

        // Map controllers
        app.MapControllers();

        // Healthcheck endpoint
        app.MapHealthChecks("/ping");

        // Error endpoint where the integrated
        // exception handling middleware reroutes
        // thrown exception
        app.MapErrorEndpoint();

        return app;
    }
}
