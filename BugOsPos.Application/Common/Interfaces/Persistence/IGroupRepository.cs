using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IGroupRepository
{
    GroupId NextIdentity();
    Task<Group?> GetGroupById(int id);
    Task Add(Group group);
}
