using Application.Common.Interfaces.Shared;

namespace Infrastructure.Shared;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
