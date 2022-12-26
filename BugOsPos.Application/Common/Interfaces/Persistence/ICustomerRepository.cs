using BugOsPos.Domain.CustomerAggregate;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerByUsername(string username, int franchiseId);
    Task<Customer?> GetCustomerByEmail(string email, int franchiseId);
    Task Add(Customer customer);
}
