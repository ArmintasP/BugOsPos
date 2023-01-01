using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    OrderId NextIdentity();
    Task<Order?> GetOrderById(OrderId id);
    Task Add(Order order);
}
