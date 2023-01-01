using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.DiscountAggregate;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class DiscountRepository : IDiscountRepository
{
    private static readonly List<Discount> _discounts = PrefilledData.SampleDiscounts();
    private int _nextId = _discounts.Count + 1;

    public DiscountId NextIdentity()
    {
        return DiscountId.New(_nextId);
    }

    public Task Add(Discount discount)
    {
        _discounts.Add(discount);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Discount?> GetDiscountById(DiscountId id)
    {
        var Discount = _discounts.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(Discount);
    }

    public Task Update(Discount discount)
    {
        var index = _discounts.FindIndex(p => p.Id == discount.Id);
        _discounts[index] = discount;
        return Task.CompletedTask;
    }
}
