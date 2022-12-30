using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;

namespace BugOsPos.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId>
{
    public string Username { get; }
    public string Password { get; }
    public string Email { get; }
    public string Name { get; }
    public string Surname { get; }
    public string? Address { get; }
    public FranchiseId FranchiseId { get; }
    public bool IsBlocked { get; }

    private Customer(
        CustomerId id,
        string username,
        string password,
        string email,
        string name,
        string surname,
        string? address,
        FranchiseId franchiseId)
        : base(id)
    {
        Username = username;
        Password = password;
        Email = email;
        Name = name;
        Surname = surname;
        Address = address;
        FranchiseId = franchiseId;
    }

    public static Customer New(
        CustomerId id,
        string username,
        string password,
        string email,
        string name,
        string surname,
        string? address,
        int franchiseId)
    {
        return new Customer(
            id,
            username,
            password,
            email,
            name,
            surname,
            address,
            FranchiseId.New(franchiseId));
    }
}
