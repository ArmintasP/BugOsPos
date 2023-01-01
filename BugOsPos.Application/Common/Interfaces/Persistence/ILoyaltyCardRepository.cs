using BugOsPos.Domain.LoyaltyCardAggregate;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;
public interface ILoyaltyCardRepository
{
    LoyaltyCardId NextIdentity();
    Task<LoyaltyCard?> GetLoyaltyCardById(LoyaltyCardId id);
    Task Add(LoyaltyCard loyaltyCard);
}
