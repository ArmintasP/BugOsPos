using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.LoyaltyDiscountAggregate.ValueObjects;

public sealed class LoyaltyDiscountId : ValueObject
{
    public int Value { get; }

    private LoyaltyDiscountId(int value)
    {
        Value = value;
    }

    public static LoyaltyDiscountId New(int id) => new LoyaltyDiscountId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
