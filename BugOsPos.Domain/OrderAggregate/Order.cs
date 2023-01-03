using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using BugOsPos.Domain.LoyaltyCardAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    public CustomerId? CustomerId { get; }
    public EmployeeId? CashierId { get; }
    public EmployeeId? CourierId { get; }
    public LocationId LocationId { get; }
    public LoyaltyCardId? LoyaltyCardId { get; }
    public Payment? Payment { get; private set; }
    public List<OrderItem> OrderItems { get; }
    public OrderStatus Status { get; private set; }
    public bool IsForDelivery { get; }
    public TimeSpan? EstimatedTime { get; }
    public decimal? Tips { get; }
    public decimal TotalPrice { get; }
    public string? CustomerComment { get; set; }

    private Order(
        OrderId id,
        LoyaltyCardId? loyaltyCardId,
        CustomerId? customerId,
        EmployeeId? cashierId,
        EmployeeId? courierId,
        LocationId locationId,
        bool isForDelivery)
        : base(id)
    {
        CustomerId = customerId;
        CashierId = cashierId;
        CourierId = courierId;
        LoyaltyCardId = loyaltyCardId;
        LocationId = locationId;
        Status = OrderStatus.NotPlaced;
        IsForDelivery = isForDelivery;
        EstimatedTime = null;
        Tips = null;
        OrderItems = new List<OrderItem>();
        TotalPrice = 0;
        CustomerComment = null;
        Payment = null;
    }

    public static Order New(
        OrderId id,
        LoyaltyCardId? loyaltyCardId,
        CustomerId? customerId,
        EmployeeId? cashierId,
        EmployeeId? courierId,
        LocationId locationId,
        bool isForDelivery)
    {
        return new Order(
            id,
            loyaltyCardId,
            customerId,
            cashierId,
            courierId,
            locationId,
            isForDelivery);
    }


    public static Order New(
        int id,
        int? loyaltyCardId,
        int? customerId,
        int? cashierId,
        int? courierId,
        int locationId,
        bool isForDelivery)
    {
        return new Order(
            OrderId.New(id),
            loyaltyCardId == null ? null : LoyaltyCardId.New(loyaltyCardId.Value),
            customerId == null ? null : CustomerId.New(customerId.Value),
            cashierId == null ? null : EmployeeId.New(cashierId.Value),
            courierId == null ? null : EmployeeId.New(courierId.Value),
            LocationId.New(locationId),
            isForDelivery);
    }

    public void AddOrderItem(OrderItem item)
    {
        OrderItems.Add(item);
    }

    public void Confirm()
    {
        Status = OrderStatus.Confirmed;
    }

    public void Complete()
    {
        Status = OrderStatus.Closed;
    }
    public void Pay(PaymentType type, DateTime time)
    {
        Payment = Payment.New(PaymentId.New(Id.Value), type, time);
    }
}

public enum OrderStatus
{
    NotPlaced = 0,
    Confirmed,
    InMaking,
    Ready,
    InDelivery,
    Delivered,
    Closed
}