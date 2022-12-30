using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.DiscountAggregate.ValueObjects;
using BugOsPos.Domain.ProductAggregate.ValueObjects;

namespace BugOsPos.Domain.OrderAggregate.ValueObjects;

public sealed class OrderItemId : ValueObject
{
    public OrderId OrderId { get; }
    public ProductId ProductId { get; }

    private OrderItemId(OrderId orderId, ProductId productId)
    {
        OrderId = orderId;
        ProductId = productId;
    }

    public static OrderItemId New(
        OrderId orderId,
        ProductId productId)
    {
        return new OrderItemId(orderId, productId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return OrderId;
        yield return ProductId;
    }
}
