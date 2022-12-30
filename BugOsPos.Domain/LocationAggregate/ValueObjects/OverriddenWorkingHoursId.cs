using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.LocationAggregate.ValueObjects;

public sealed class NormalWorkingHoursId : ValueObject
{
    public DayOfWeek DayOfWekk { get; }
    public LocationId LocationId { get; }

    private NormalWorkingHoursId(DayOfWeek dayoOfWeek, LocationId locationId)
    {
        DayOfWekk = dayoOfWeek;
        LocationId = locationId;
    }

    public static NormalWorkingHoursId New(DayOfWeek dayoOfWeek, LocationId locationId) => new NormalWorkingHoursId(dayoOfWeek, locationId);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return DayOfWekk;
        yield return LocationId;
    }
}
