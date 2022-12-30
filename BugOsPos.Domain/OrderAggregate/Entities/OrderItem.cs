using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Domain.OrderAggregate.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public DiscountId DiscountId { get; }
    public int Quantity { get; }
    public OrderItemStatus Status { get; }

    private OrderItem(
        OrderItemId id,
        DiscountId discountId,
        int quantity,
        OrderItemStatus status)
        : base(id)
    {
        DiscountId = discountId;
        Quantity = quantity;
        Status = status;
    }

    public static OrderItem New(
        OrderItemId id,
        DiscountId discountId,
        int quantity,
        OrderItemStatus status)
    {
        return new OrderItem(id, discountId, quantity, status);
    }
}

public enum OrderItemStatus
{
    Pending,
    Completed,
    Cancelled
}
