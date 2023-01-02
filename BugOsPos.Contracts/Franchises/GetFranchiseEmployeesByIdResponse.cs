using BugOsPos.Contracts.Common;

namespace BugOsPos.Contracts.Franchises;


public sealed record GetFranchiseEmployeesByIdResponse(List<Employee> Employees);
