using FluentValidation.Results;

namespace Web.Common;

public static class EndpointExtensions
{
    /// <summary>
    /// Ext
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static IDictionary<string, string[]> ToProblemDetailsValidationDictionary(this IEnumerable<ValidationFailure> failures)
    {
        return failures.ToDictionary(key => $"{key.PropertyName} - {key.ErrorCode}", element => new string[] { element.ErrorMessage });
    }
}
