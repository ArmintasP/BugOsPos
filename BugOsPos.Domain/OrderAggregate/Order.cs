using BugOsPos.Domain.Common.Models;
using BugOsPos.Domain.CustomerAggregate.ValueObjects;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.LocationAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    public CustomerId? CustomerId { get; }
    public EmployeeId? CashierId { get; }
    public EmployeeId? CourierId { get; }
    public LocationId LocationId { get; }
    public Payment? Payment { get;  }
    public List<OrderItem> OrderItems { get; }
    public OrderStatus Status { get; }
    public bool IsForDelivery { get; }
    public TimeSpan? EstimatedTime { get; }
    public decimal? Tips { get; }
    public decimal TotalPrice { get; }
    public string? CustomerComment { get; }

    private Order(
        OrderId id,
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
        CustomerId? customerId,
        EmployeeId? cashierId,
        EmployeeId? courierId,
        LocationId locationId,
        bool isForDelivery)
    {
        return new Order(
            id,
            customerId,
            cashierId,
            courierId,
            locationId,
            isForDelivery);
    }
}

public enum OrderStatus
{
    NotPlaced = 0,
    InMaking,
    Ready,
    InDelivery,
    Delivered,
    Closed
}