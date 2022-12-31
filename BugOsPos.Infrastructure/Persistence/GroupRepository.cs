using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class GroupRepository : IGroupRepository
{
    private static readonly List<Group> _groups = PrefilledData.SampleGroups();
        
    public GroupId NextIdentity()
    {
        return GroupId.New(_groups.Count + 1);
    }

    public Task<Group?> GetGroupById(int id)
    {
        var group = _groups.FirstOrDefault(group => group.Id.Value == id);
        return Task.FromResult(group);
    }

    public Task Add(Group group)
    {
        _groups.Add(group);
        return Task.CompletedTask;
    }
}
