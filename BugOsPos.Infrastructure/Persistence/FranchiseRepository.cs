using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.FranchiseAggregate;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class FranchiseRepository : IFranchiseRepository
{
    private static readonly List<Franchise> _franchises = PrefilledData.SampleFranchises();
    private int _nextId = _franchises.Count + 1;

    public FranchiseId NextIdentity()
    {
        return FranchiseId.New(_nextId);
    }

    public Task Add(Franchise franchise)
    {
        _franchises.Add(franchise);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Franchise?> GetFranchiseById(FranchiseId id)
    {
        var Franchise = _franchises.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(Franchise);
    }

    public Task Update(Franchise franchise)
    {
        var index = _franchises.FindIndex(p => p.Id == franchise.Id);
        _franchises[index] = franchise;
        return Task.CompletedTask;
    }
}
