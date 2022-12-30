using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.LocationAggregate.ValueObjects;

namespace BugOsPos.Domain.LocationAggregate.Entities;

public sealed class OverriddenWorkingHours : Entity<OverriddenWorkingHoursId>
{
    public TimeOnly AltOpeningTime { get; }
    public TimeOnly AltClosingTime { get; }
    public DateTime ApplyAltTimesStartDate { get; }
    public DateTime ApplyAltTimesEndDate { get; }
    public bool Closed { get; }

    private OverriddenWorkingHours(
        OverriddenWorkingHoursId id,
        TimeOnly openingTime,
        TimeOnly closingTime,
        DateTime applyAltTimesStartDate,
        DateTime applyAltTimesEndDate,
        bool closed) : base(id)
    {
        AltOpeningTime = openingTime;
        AltClosingTime = closingTime;
        ApplyAltTimesStartDate = applyAltTimesStartDate;
        ApplyAltTimesEndDate = applyAltTimesEndDate;
        Closed = closed;
    }

    public static OverriddenWorkingHours New(
        OverriddenWorkingHoursId id,
        TimeOnly openingTime,
        TimeOnly closingTime,
        DateTime applyAltTimesStartDate,
        DateTime applyAltTimesEndDate,
        bool closed)
    {
        return new OverriddenWorkingHours(id, openingTime, closingTime, applyAltTimesStartDate, applyAltTimesEndDate, closed);
    }
}
