using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.LoyaltyDiscountAggregate;
using BugOsPos.Domain.LoyaltyDiscountAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class LoyaltyDiscountRepository : ILoyaltyDiscountRepository
{
    private static readonly List<LoyaltyDiscount> _loyaltyDiscounts = PrefilledData.SampleLoyaltyDiscounts();
    private int _nextId = _loyaltyDiscounts.Count + 1;

    public LoyaltyDiscountId NextIdentity()
    {
        return LoyaltyDiscountId.New(_nextId);
    }

    public Task Add(LoyaltyDiscount loyaltyDiscount)
    {
        _loyaltyDiscounts.Add(loyaltyDiscount);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<LoyaltyDiscount?> GetLoyaltyDiscountById(LoyaltyDiscountId id)
    {
        var LoyaltyDiscount = _loyaltyDiscounts.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(LoyaltyDiscount);
    }

    public Task Update(LoyaltyDiscount loyaltyDiscount)
    {
        var index = _loyaltyDiscounts.FindIndex(p => p.Id == loyaltyDiscount.Id);
        _loyaltyDiscounts[index] = loyaltyDiscount;
        return Task.CompletedTask;
    }

    public List<LoyaltyDiscount>? GetLoyaltyDiscountsByLoyaltyCardId(int loyaltyCardId)
    {
        return _loyaltyDiscounts.Where(loyaltyDiscount => loyaltyDiscount.LoyaltyCardId.Value == loyaltyCardId).ToList();
    }
}