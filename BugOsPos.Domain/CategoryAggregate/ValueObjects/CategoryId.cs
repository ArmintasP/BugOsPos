using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.CategoryAggregate.ValueObjects;

public sealed class CategoryId : ValueObject
{
    public int Value { get; }

    private CategoryId(int value)
    {
        Value = value;
    }

    public static CategoryId New(int id) => new CategoryId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
