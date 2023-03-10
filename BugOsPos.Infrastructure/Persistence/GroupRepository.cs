using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class GroupRepository : IGroupRepository
{
    private static readonly List<Group> _groups = PrefilledData.SampleGroups();
    private int _nextId = _groups.Count + 1;
        
    public GroupId NextIdentity()
    {
        return GroupId.New(_nextId);
    }
    
    public Task Add(Group group)
    {
        _groups.Add(group);
        _nextId++;
        return Task.CompletedTask;
    }
    
    public Task<Group?> GetGroupById(GroupId id)
    {
        var group = _groups.FirstOrDefault(group => group.Id == id);
        return Task.FromResult(group);
    }

    public Task Update(Group group)
    {
        var index = _groups.FindIndex(p => p.Id == group.Id);
        _groups[index] = group;
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Group>> GetGroupsByFranchiseId(FranchiseId id)
    {
         var groups = _groups.Where(group => group.FranchiseId == id);
        return Task.FromResult(groups);
    }
}
