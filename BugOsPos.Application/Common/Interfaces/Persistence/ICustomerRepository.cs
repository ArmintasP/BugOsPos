using BugOsPos.Domain.CustomerAggregate;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    CustomerId NextIdentity();
    Task<Customer?> GetCustomerByUsername(string username, int franchiseId);
    Task<Customer?> GetCustomerByEmail(string email, int franchiseId);
    Task<Customer?> GetCustomerById(int id);
    Task Add(Customer customer);
    Task Update(Customer customer);
}
