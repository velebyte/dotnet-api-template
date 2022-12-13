using Application.Features.Flowers;
using Application.Features.Flowers.CreateFlower;
using Application.Features.Flowers.GetFlower;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Endpoints;

public static class FlowersEndpoints
{
    /// <summary>
    /// Map Flower endpoints
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MapFlowersEndpoints(this WebApplication app)
    {
        _ = app.MapPost(
            "/api/flowers",
            async (ISender mediator, HttpRequest httpRequest, CreateFlowerCommand command) =>
            {
                var entityId = await mediator.Send(command);

                return Results.CreatedAtRoute(UriHelper.GetEncodedUrl(httpRequest), entityId);
                
            })
            .WithTags("Flowers")
            .WithMetadata(new SwaggerOperationAttribute("Creat a flower", "\n    POST /flowers"))
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        _ = app.MapGet(
            "/api/flowers/{id:guid}",
            async (ISender mediator, HttpContext context, Guid id) =>
            {
                return Results.Ok(await mediator.Send(new GetFlowerQuery(id)));
            })
            .WithTags("Flowers")
            .WithMetadata(new SwaggerOperationAttribute("Lookup flower by id", "\n    GET /flowers/{id}"))
            .Produces<FlowerResponse>(StatusCodes.Status200OK)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        return app;
    }
}
