using BugOsPos.Contracts.Common;

namespace BugOsPos.Contracts.Locations;

public sealed record GetLocationEmployeesResponse(List<EmployeeSection> Employees);
