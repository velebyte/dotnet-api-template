using Application.Common.Exceptions;
using Web.Common;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Web.Endpoints;

public static class ErrorEndpoint
{
    /// <summary>
    /// Maps exception handling error endpoint
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MapErrorEndpoint(this WebApplication app)
    {
        app.Map("/error", (HttpContext context) =>
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature is null)
                return Results.Problem();

            return contextFeature.Error switch
            {
                ValidationException ex => Results.ValidationProblem(ex.Errors.ToProblemDetailsValidationDictionary()),
                NotFoundException ex => Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound),
                DuplicateException ex => Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status409Conflict),
                UserCreationFailureException ex => Results.Problem(detail: ex.Message),
                DatabaseException ex => Results.Problem(ex.Message),
                _ => Results.Problem()
            };
        });

        return app;
    }
}
