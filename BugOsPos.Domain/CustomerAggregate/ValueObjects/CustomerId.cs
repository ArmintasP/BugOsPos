using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.CustomerAggregate.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public int Value { get; }

    private CustomerId(int value)
    {
        Value = value;
    }

    public static CustomerId New(int id) => new CustomerId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return string.Join('-', GetEqualityComponents());
    }
}
