using BugOsPos.Domain.Common.Models;

namespace BugOsPos.Domain.FranchiseAggregate.ValueObjects;

public sealed class FranchiseId : ValueObject
{
    public int Value { get; }
    
    private FranchiseId(int value)
    {
        Value = value;
    }

    public static FranchiseId New(int id) => new FranchiseId(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
