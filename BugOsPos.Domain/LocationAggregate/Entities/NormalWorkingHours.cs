using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.LocationAggregate.ValueObjects;

namespace BugOsPos.Domain.LocationAggregate.Entities;

public sealed class NormalWorkingHours : Entity<NormalWorkingHoursId>
{
    public TimeOnly OpeningTime { get; }
    public TimeOnly ClosingTime { get; }

    private NormalWorkingHours(
        NormalWorkingHoursId id,
        TimeOnly openingTime,
        TimeOnly closingTime) : base(id)
    {
        OpeningTime = openingTime;
        ClosingTime = closingTime;
    }

    public static NormalWorkingHours New(
        NormalWorkingHoursId id,
        TimeOnly openingTime,
        TimeOnly closingTime)
    {
        return new NormalWorkingHours(id, openingTime, closingTime);
    }
}
