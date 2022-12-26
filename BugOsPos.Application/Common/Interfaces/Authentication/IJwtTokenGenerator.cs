using BugOsPos.Domain.CustomerAggregate;

namespace BugOsPos.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Customer customer);
}
