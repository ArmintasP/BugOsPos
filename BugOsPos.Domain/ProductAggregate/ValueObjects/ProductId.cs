using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.ProductAggregate.ValueObjects;

public sealed class ProductId : ValueObject
{
    public int Value { get; }

    private ProductId(int value)
    {
        Value = value;
    }

    public static ProductId New(int id) => new ProductId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
