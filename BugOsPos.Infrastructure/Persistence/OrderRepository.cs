using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
using BugOsPos.Domain.OrderAggregate;
using BugOsPos.Domain.OrderAggregate.ValueObjects;

namespace BugOsPos.Infrastructure.Persistence;

public sealed class OrderRepository : IOrderRepository
{
    private static readonly List<Order> _orders = PrefilledData.SampleOrders();
    private int _nextId = _orders.Count + 1;

    public OrderId NextIdentity()
    {
        return OrderId.New(_nextId);
    }

    public Task Add(Order order)
    {
        _orders.Add(order);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Order?> GetOrderById(OrderId id)
    {
        var order = _orders.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(order);
    }

    public Task Update(Order order)
    {
        var index = _orders.FindIndex(p => p.Id == order.Id);
        _orders[index] = order;
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Order>> GetOrdersByCourierId(EmployeeId id)
    {
        var orders = _orders.Where(order => id.Equals(order.CourierId));

        return Task.FromResult(orders);
    }
}
