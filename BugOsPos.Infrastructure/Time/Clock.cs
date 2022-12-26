using BugOsPos.Application.Common.Interfaces.Clock;

namespace BugOsPos.Infrastructure.Time;

public class Clock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
