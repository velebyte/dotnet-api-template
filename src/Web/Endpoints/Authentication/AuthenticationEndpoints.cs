using Application.Features.Authentication;
using Application.Features.Authentication.LoginUser;
using Application.Features.Authentication.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Web.Endpoints;

public static class AuthenticationEndpoints
{
    /// <summary>
    /// Maps authentication endpoints register and login
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MapAuthenticationEndpoints(this WebApplication app)
    {
        _ = app.MapPost(
            "/api/register",
            async (ISender mediator, HttpRequest httpRequest, RegisterUserCommand command) =>
            {
                return Results.Ok(await mediator.Send(command));

            })
            .WithTags("Authentication")
            .WithMetadata(new SwaggerOperationAttribute("Register a user", "\n    POST /api/register"))
            .Produces<AuthenticationResponse>(StatusCodes.Status200OK)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        _ = app.MapPost(
            "/api/login",
            async (ISender mediator, HttpRequest httpRequest, LoginUserCommand command) =>
            {
                return Results.Ok(await mediator.Send(command));

            })
            .WithTags("Authentication")
            .WithMetadata(new SwaggerOperationAttribute("Log In a user", "\n    POST /api/login"))
            .Produces<AuthenticationResponse>(StatusCodes.Status200OK)
            .Produces<ValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        return app;
    }
}
