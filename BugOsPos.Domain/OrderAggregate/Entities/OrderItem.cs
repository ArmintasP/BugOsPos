using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate.ValueObjects;

namespace BugOsPos.Domain.OrderAggregate.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; }
    public DiscountId? DiscountId { get; }
    public int Quantity { get; }
    public OrderItemStatus Status { get; }

    private OrderItem(
        OrderItemId id,
        ProductId productId,
        DiscountId? discountId,
        int quantity,
        OrderItemStatus status)
        : base(id)
    {
        ProductId = productId;
        DiscountId = discountId;
        Quantity = quantity;
        Status = status;
    }

    public static OrderItem New(
        OrderItemId id,
        ProductId productId,
        DiscountId? discountId,
        int quantity,
        OrderItemStatus status)
    {
        return new OrderItem(
            id,
            productId,
            discountId,
            quantity,
            status);
    }
}

public enum OrderItemStatus
{
    Pending,
    Completed,
    Cancelled
}
