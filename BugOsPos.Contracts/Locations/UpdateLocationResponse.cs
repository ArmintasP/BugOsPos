namespace BugOsPos.Contracts.Locations;
public sealed record UpdateLocationResponse(
    int Id,
    string Name,
    string Address,
    Rating Rating,
    List<string> PhotoPaths,
    List<NormalWorkingHours> NormalWorkingHours,
    List<OverriddenWorkingHours> OverriddenWorkingHours);
