using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.EmployeeAggregate.ValueObjects;

public sealed class EmployeeId : ValueObject
{
    public int Value { get; }

    private EmployeeId(int value)
    {
        Value = value;
    }

    public static EmployeeId New(int id) => new EmployeeId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
