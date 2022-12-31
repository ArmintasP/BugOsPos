using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IEmployeeRepository
{
    EmployeeId NextIdentity();
    Task Add(Employee employee);
    Task Delete(EmployeeId employeeId);
    Task<Employee?> GetEmployeeById(EmployeeId employeeId);
    Task<Employee?> GetEmployeeByEmployeeCode(string code, int franchiseId);
}
