using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.ShiftAggregate.ValueObjects;

public sealed class ShiftId : ValueObject
{
    public int Value { get; }

    private ShiftId(int value)
    {
        Value = value;
    }

    public static ShiftId New(int id) => new ShiftId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
