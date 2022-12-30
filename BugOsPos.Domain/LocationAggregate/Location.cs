using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.Common.ValueObjects;
using BugOsPos.Domain.LocationAggregate.Entities;
using BugOsPos.Domain.LocationAggregate.ValueObjects;

namespace BugOsPos.Domain.LocationAggregate;

public sealed class Location : AggregateRoot<LocationId>
{
    public string Name { get; }
    public string Address { get; }
    public Rating Rating { get; }
    public List<string> PhotoPaths { get; }
    public List<NormalWorkingHours> NormalWorkingHours { get; }
    public List<OverriddenWorkingHours> OverriddenWorkingHours { get; }

    private Location(
        LocationId id,
        string name,
        string address) : base(id)
    {
        Name = name;
        Address = address;
        Rating = Rating.New();
        PhotoPaths = new List<string>();
        NormalWorkingHours = new List<NormalWorkingHours>();
        OverriddenWorkingHours = new List<OverriddenWorkingHours>();
    }

    public static Location New(
        LocationId id,
        string name,
        string address)
    {
        return new Location(id, name, address);
    }
}
