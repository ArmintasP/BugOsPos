using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;

namespace BugOsPos.Domain.FranchiseAggregate;

public sealed class Franchise : AggregateRoot<FranchiseId>
{
    public string Name { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    
    private Franchise(
        FranchiseId id,
        string email,
        string name,
        string phoneNumber)
        : base(id)
    {
        Email = email;
        Name = name;
        PhoneNumber = phoneNumber;
    }

    public static Franchise New(
        FranchiseId id,
        string email,
        string name,
        string phoneNumber)
    {
        return new Franchise(
            id,
            email,
            name,
            phoneNumber);
    }
}
