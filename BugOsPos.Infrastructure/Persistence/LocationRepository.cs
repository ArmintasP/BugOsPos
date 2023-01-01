using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LocationAggregate;
using BugOsPos.Domain.LocationAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class LocationRepository : ILocationRepository
{
    private static readonly List<Location> _locations = PrefilledData.SampleLocations();
    private int _nextId = _locations.Count + 1;
        
    public LocationId NextIdentity()
    {
        return LocationId.New(_nextId);
    }

    public Task<Location?> GetLocationById(LocationId id)
    {
        var location = _locations.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(location);
    }

    public Task Add(Location location)
    {
        _locations.Add(location);
        _nextId++;
        return Task.CompletedTask;
    }
}
