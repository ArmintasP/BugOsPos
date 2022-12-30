using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.LocationAggregate.ValueObjects;

public sealed class LocationId : ValueObject
{
    public int Value { get; }

    private LocationId(int value)
    {
        Value = value;
    }

    public static LocationId New(int id) => new LocationId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
