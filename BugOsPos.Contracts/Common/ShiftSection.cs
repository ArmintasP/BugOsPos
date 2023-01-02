namespace BugOsPos.Contracts.Common;

public sealed record ShiftSection(
    int Id,
    int LocationId,
    DateTime Start,
    DateTime End);
