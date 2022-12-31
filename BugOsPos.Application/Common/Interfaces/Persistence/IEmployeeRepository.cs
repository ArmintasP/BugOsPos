using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IEmployeeRepository
{
    EmployeeId NextIdentity();
    Task Add(Employee employee);
    Task<Employee?> GetEmployeeByEmployeeCode(string code, int franchiseId);
}
