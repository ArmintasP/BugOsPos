using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.DiscountAggregate.ValueObjects;

public sealed class DiscountId : ValueObject
{
    public int Value { get; }

    private DiscountId(int value)
    {
        Value = value;
    }

    public static DiscountId New(int id) => new DiscountId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
