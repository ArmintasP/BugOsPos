using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IEmployeeRepository
{
    EmployeeId NextIdentity();
    Task Add(Employee employee);
    Task Update(Employee employee);
    Task Delete(EmployeeId employeeId);
    Task<Employee?> GetEmployeeById(EmployeeId employeeId);
    Task<IEnumerable<Employee>> GetEmployeesByGroupId(GroupId groupId);
    Task<Employee?> GetEmployeeByEmployeeCode(string code, int franchiseId);
}
