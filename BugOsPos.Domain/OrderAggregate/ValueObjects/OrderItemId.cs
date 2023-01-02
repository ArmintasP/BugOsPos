using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.OrderAggregate.ValueObjects;

public sealed class OrderItemId : ValueObject
{
    public int Value { get; }

    private OrderItemId(int value)
    {
        Value = value;
    }

    public static OrderItemId New(int id)
    {
        return new OrderItemId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
