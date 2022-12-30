using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.LocationAggregate.ValueObjects;

public sealed class OverriddenWorkingHoursId : ValueObject
{
    public DayOfWeek DayOfWekk { get; }
    public LocationId LocationId { get; }

    private OverriddenWorkingHoursId(DayOfWeek dayoOfWeek, LocationId locationId)
    {
        DayOfWekk = dayoOfWeek;
        LocationId = locationId;
    }

    public static OverriddenWorkingHoursId New(DayOfWeek dayoOfWeek, LocationId locationId) => new OverriddenWorkingHoursId(dayoOfWeek, locationId);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return DayOfWekk;
        yield return LocationId;
    }
}
