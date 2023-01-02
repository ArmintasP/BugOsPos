using BugOsPos.Domain.LocationAggregate;
using BugOsPos.Domain.LocationAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface ILocationRepository
{
    LocationId NextIdentity();
    Task<Location?> GetLocationById(LocationId id);
    Task Add(Location location);
    Task Update(Location location);
}