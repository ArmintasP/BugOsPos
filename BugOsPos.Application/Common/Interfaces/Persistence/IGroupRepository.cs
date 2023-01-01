﻿using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IGroupRepository
{
    GroupId NextIdentity();
    Task<Group?> GetGroupById(GroupId id);
    Task Update(Group group);
    Task Add(Group group);
}
