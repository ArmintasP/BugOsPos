using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;

namespace BugOsPos.Domain.DiscountAggregate;

public sealed class Discount : AggregateRoot<DiscountId>
{
    public decimal Amount { get; }
    public DiscountType DiscountType { get; }
    public DateTime FromDateTime { get; }
    public DateTime EndDateTime { get; }

    private Discount(
    DiscountId id,
    decimal amount,
    DiscountType discountType,
    DateTime fromDateTime,
    DateTime endDateTime)
    : base(id)
    {
        Amount = amount;
        DiscountType = discountType;
        FromDateTime = fromDateTime;
        EndDateTime = endDateTime;
    }

    public static Discount New(
        DiscountId id,
        decimal amount,
        DiscountType discountType,
        DateTime fromDateTime,
        DateTime endDateTime)
    {
        return new Discount(id, amount, discountType, fromDateTime, endDateTime);
    }
}

public enum DiscountType
{
    Percentage = 0,
    Amount = 1,
}
