using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.Entities;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    OrderId NextIdentity();
    Task<Order?> GetOrderById(OrderId id);
    Task UpdateOrderItem(OrderItem orderItem);
    Task<OrderItem?> GetOrderItemById(OrderItemId id);
    Task<IEnumerable<Order>> GetOrdersByCourierId(EmployeeId id);
    Task Add(Order order);
    Task Update(Order order);
}
