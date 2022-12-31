using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.CustomerAggregate;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class CustomerRepository : ICustomerRepository
{
    private static readonly List<Customer> _customers = PrefilledData.SampleCustomers();
    private int _nextId = _customers.Count + 1;

    // If it was a db, we would get generated id from there.
    // Ideally, it would be better to have keys as GUID instead of ints (as it is required by the documentation).
    
    public CustomerId NextIdentity()
    {
        return CustomerId.New(_nextId);
    }

    public Task Add(Customer customer)
    {
        _customers.Add(customer);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Customer?> GetCustomerByEmail(string email, int franchiseId)
    {
        var customer = _customers.SingleOrDefault(c =>
            c.Email == email &&
            c.FranchiseId.Value == franchiseId);

        return Task.FromResult(customer);
    }

    public Task<Customer?> GetCustomerByUsername(string username, int franchiseId)
    {
        var customer = _customers.SingleOrDefault(c =>
            c.Username == username &&
            c.FranchiseId.Value == franchiseId);

        return Task.FromResult(customer);
    }

    public Task<Customer?> GetCustomerById(int id)
    {
        var customer = _customers.SingleOrDefault(c => c.Id.Value == id);

        return Task.FromResult(customer);
    }

    public Task Update(Customer customer)
    {
        var index = _customers.FindIndex(c => c.Id == customer.Id);
        _customers[index] = customer;
        return Task.CompletedTask;
    }
}
