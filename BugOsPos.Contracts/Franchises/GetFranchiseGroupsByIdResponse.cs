using BugOsPos.Contracts.Common;

namespace BugOsPos.Contracts.Franchises;
public sealed record GetFranchiseGroupsByIdResponse(List<GroupSection> Groups);
