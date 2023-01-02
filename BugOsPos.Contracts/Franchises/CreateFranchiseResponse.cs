namespace BugOsPos.Contracts.Franchises;
public sealed record CreateFranchiseResponse(
    int Id,
    string Name,
    string Email,
    Employee Employee);
