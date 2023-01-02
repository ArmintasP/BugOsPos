using BugOsPos.Domain.LoyaltyDiscountAggregate;
using BugOsPos.Domain.LoyaltyDiscountAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface ILoyaltyDiscountRepository
{
    LoyaltyDiscountId NextIdentity();
    Task<LoyaltyDiscount?> GetLoyaltyDiscountById(LoyaltyDiscountId id);
    List<LoyaltyDiscount>? GetLoyaltyDiscountsByLoyaltyCardId(int loyaltyCardId);
    Task Add(LoyaltyDiscount loyaltyDiscount);
}
