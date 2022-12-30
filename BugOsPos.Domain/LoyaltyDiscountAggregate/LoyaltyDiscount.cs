using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyDiscountAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate.ValueObjects;

namespace BugOsPos.Domain.LoyaltyDiscountAggregate;

public sealed class LoyaltyDiscount : AggregateRoot<LoyaltyDiscountId>
{
    public LoyaltyCardId LoyaltyCardId { get; }
    public ProductId ProductId { get; }
    public DiscountId DiscountId { get; }

    private LoyaltyDiscount(
        LoyaltyDiscountId id,
        LoyaltyCardId loyaltyCardId,
        ProductId productId,
        DiscountId discountId)
        : base(id)
    {
        LoyaltyCardId = loyaltyCardId;
        ProductId = productId;
        DiscountId = discountId;
    }

    public static LoyaltyDiscount New(
        LoyaltyDiscountId id,
        LoyaltyCardId loyaltyCardId,
        ProductId productId,
        DiscountId discountId)
    {
        return new LoyaltyDiscount(id, loyaltyCardId, productId, discountId);
    }
}
