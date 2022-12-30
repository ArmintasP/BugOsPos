using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.OrderAggregate.ValueObjects;

public sealed class PaymentId : ValueObject
{
    public int Value { get; }

    private PaymentId(int value)
    {
        Value = value;
    }

    public static PaymentId New(int id) => new PaymentId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}