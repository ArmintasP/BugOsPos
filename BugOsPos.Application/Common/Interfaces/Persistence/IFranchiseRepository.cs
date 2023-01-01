using BugOsPos.Domain.FranchiseAggregate;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IFranchiseRepository
{
    FranchiseId NextIdentity();
    Task<Franchise?> GetFranchiseById(FranchiseId id);
    Task Add(Franchise franchise);
}
