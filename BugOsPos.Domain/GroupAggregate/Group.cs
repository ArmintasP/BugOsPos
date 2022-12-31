using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Domain.GroupAggregate;

public sealed class Group : AggregateRoot<GroupId>
{
    public FranchiseId FranchiseId { get; }
    public string Name { get; }
    public string Description { get; }
    
    private Group(
        GroupId id,
        FranchiseId franchiseId,
        string name,
        string description) : base(id)
    {
        FranchiseId = franchiseId;
        Name = name;
        Description = description;
    }

    public static Group New(GroupId id, FranchiseId franchiseId, string name, string description)
    {
        return new Group(id, franchiseId, name, description);
    }
}