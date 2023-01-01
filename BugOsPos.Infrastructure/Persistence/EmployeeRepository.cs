using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private static readonly List<Employee> _employees = PrefilledData.SampleEmployees();
    private int _nextId = _employees.Count + 1;
        
    public EmployeeId NextIdentity()
    {
        return EmployeeId.New(_nextId);
    }
    
    public Task Add(Employee employee)
    {
        _employees.Add(employee);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Employee?> GetEmployeeByEmployeeCode(string code, int franchiseId)
    {
        var employee = _employees.SingleOrDefault(
            e => e.FranchiseId.Value == franchiseId &&
            e.EmployeeCode == code);
        
        return Task.FromResult(employee);
    }

    public Task Delete(EmployeeId employeeId)
    {
        _employees.Remove(_employees.Single(e => e.Id == employeeId));
        return Task.CompletedTask;
    }

    public Task<Employee?> GetEmployeeById(EmployeeId employeeId)
    {
        var employee = _employees.SingleOrDefault(
            e => e.Id == employeeId);

        return Task.FromResult(employee);
    }
    
    public Task Update(Employee employee)
    {
        var index = _employees.FindIndex(c => c.Id == employee.Id);
        _employees[index] = employee;
        return Task.CompletedTask;
    }
}
