using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Domain.GroupAggregate;

public sealed class Group : AggregateRoot<GroupId>
{
    public string Name { get; }
    public string Description { get; }
    
    private Group(GroupId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static Group New(GroupId id, string name, string description)
    {
        return new Group(id, name, description);
    }
}