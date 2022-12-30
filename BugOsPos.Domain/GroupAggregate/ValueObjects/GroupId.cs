using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.GroupAggregate.ValueObjects;

public sealed class GroupId : ValueObject
{
    public int Value { get; }

    private GroupId(int value)
    {
        Value = value;
    }

    public static GroupId New(int id) => new GroupId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
