using Web.Common;
using Web.Endpoints;
using Serilog;
using Serilog.Events;


var builder = WebApplication
                .CreateBuilder(args)
                .ConfigureBuilder();

var app = builder
            .Build()
            .ConfigureApplication();

// Mapping MinimalApi endpoints
// app.MapAuthenticationEndpoints();
// app.MapFlowersEndpoints();
app.Run();

// Making Program class accessible for testing
public partial class Program { }