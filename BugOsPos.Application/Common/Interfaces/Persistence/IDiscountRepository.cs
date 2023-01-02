using BugOsPos.Domain.DiscountAggregate;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IDiscountRepository
{
    DiscountId NextIdentity();
    Task<Discount?> GetDiscountById(int id);
    Task Add(Discount discount);
}
