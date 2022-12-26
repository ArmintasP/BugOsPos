using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate;

namespace BugOsPos.Infrastructure.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private static readonly List<Customer> _customers = new();

    public Task Add(Customer customer)
    {
        customer.Id = _customers.Count + 1;
        _customers.Add(customer);
        return Task.CompletedTask;
    }

    public Task<Customer?> GetCustomerByEmail(string email, int franchiseId)
    {
        var customer = _customers.SingleOrDefault(c =>
            c.Email == email &&
            c.FranchiseId == franchiseId);

        return Task.FromResult(customer);
    }

    public Task<Customer?> GetCustomerByUsername(string username, int franchiseId)
    {
        var customer = _customers.SingleOrDefault(c =>
            c.Username == username &&
            c.FranchiseId == franchiseId);

        return Task.FromResult(customer);
    }
}
