namespace Application.Common.Interfaces.Shared;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
