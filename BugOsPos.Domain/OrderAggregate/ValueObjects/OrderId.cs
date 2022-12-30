using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.OrderAggregate.ValueObjects;

public sealed class OrderId : ValueObject
{
    public int Value { get; }

    private OrderId(int value)
    {
        Value = value;
    }

    public static OrderId New(int id) => new OrderId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
