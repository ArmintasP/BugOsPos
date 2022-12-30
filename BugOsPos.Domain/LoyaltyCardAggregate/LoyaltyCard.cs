using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;

namespace BugOsPos.Domain.LoyaltyCardAggregate;

public sealed class LoyaltyCard : AggregateRoot<LoyaltyCardId>
{
    public CustomerId CustomerId { get; }
    public string Code { get; }

    private LoyaltyCard(
        LoyaltyCardId id,
        CustomerId customerId,
        string code)
        : base(id)
    {
        CustomerId = customerId;
        Code = code;
    }

    public static LoyaltyCard New(
        LoyaltyCardId id,
        CustomerId customerId,
        string code)
    {
        return new LoyaltyCard(id, customerId, code);
    }
}
