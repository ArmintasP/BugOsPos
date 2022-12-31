using BugOsPos.Domain.CustomerAggregate;
using BugOsPos.Domain.EmployeeAggregate;

namespace BugOsPos.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Customer customer);
    string GenerateToken(Employee employee);
}
