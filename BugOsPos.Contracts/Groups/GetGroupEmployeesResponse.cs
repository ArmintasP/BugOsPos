using BugOsPos.Contracts.Common;

namespace BugOsPos.Contracts.Groups;

public sealed record GetGroupEmployeesResponse(List<EmployeeSection> Employees);
