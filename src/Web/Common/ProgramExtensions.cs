using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Serilog;

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

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        // Adds middleware for streamlined request logging
        app.UseSerilogRequestLogging(configure =>
        {
            configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000} ms";
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                                $"{app.Environment.ApplicationName} v1"));

            // Initialise database
            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                _ = initialiser.InitialiseAsync();
            }
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Redirect HTTP requests to HTTPS
        app.UseHttpsRedirection();
        // Global exception handler
        app.UseExceptionHandler("/error");

        return app;
    }
}
