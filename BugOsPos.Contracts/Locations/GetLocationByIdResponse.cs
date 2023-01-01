namespace BugOsPos.Contracts.Locations;
public sealed record GetLocationByIdResponse(
    int Id,
    string Name,
    string Address,
    Rating Rating,
    List<string> PhotoPaths,
    List<NormalWorkingHours> NormalWorkingHours,
    List<OverriddenWorkingHours> OverriddenWorkingHours);


public sealed record Rating(
    decimal Value,
    int NumberOrRatings);


public sealed record NormalWorkingHours(
    TimeOnly OpeningTime,
    TimeOnly ClosingTime);

public sealed record OverriddenWorkingHours(
    TimeOnly AltOpeningTime,
    TimeOnly AltClosingTime,
    DateTime ApplyAltTimesStartDate,
    DateTime ApplyAltTimesEndDate,
    bool Closed);