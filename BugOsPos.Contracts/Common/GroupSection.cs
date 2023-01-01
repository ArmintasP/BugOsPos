namespace BugOsPos.Contracts.Common;

public sealed record GroupSection(
    int Id,
    int FranchiseId,
    string Name,
    string Description);
