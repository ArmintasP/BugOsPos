﻿using BugOsPos.Application.Common.Interfaces.Persistence;
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

    public Task Add(Order Order)
    {
        _orders.Add(Order);
        _nextId++;
        return Task.CompletedTask;
    }

    public Task<Order?> GetOrderById(OrderId id)
    {
        var Order = _orders.SingleOrDefault(
            e => e.Id == id);

        return Task.FromResult(Order);
    }

    public Task Update(Order Order)
    {
        var index = _orders.FindIndex(p => p.Id == Order.Id);
        _orders[index] = Order;
        return Task.CompletedTask;
    }
}
