using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;

public sealed class LoyaltyCardId : ValueObject
{
    public int Value { get; }

    private LoyaltyCardId(int value)
    {
        Value = value;
    }

    public static LoyaltyCardId New(int id) => new LoyaltyCardId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
