using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private static readonly List<Employee> _employees = PrefilledData.SampleEmployees();
        
    public EmployeeId NextIdentity()
    {
        return EmployeeId.New(_employees.Count + 1);
    }
    
    public Task Add(Employee employee)
    {
        _employees.Add(employee);
        return Task.CompletedTask;
    }

    public Task<Employee?> GetEmployeeByEmployeeCode(string code, int franchiseId)
    {
        var employee = _employees.SingleOrDefault(
            e => e.FranchiseId.Value == franchiseId &&
            e.EmployeeCode == code);
        
        return Task.FromResult(employee);
    }
}
