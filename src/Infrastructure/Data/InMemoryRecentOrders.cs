using System.Collections.Generic;
using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Data;

public class InMemoryRecentOrders : IRecentOrdersReader, IRecentOrdersWriter
{
    private const int MaxItems = 50;

    private readonly object _lock = new object();
    private readonly List<Order> _orders = new List<Order>();

    public void Add(Order order)
    {
        lock (_lock)
        {
            _orders.Add(order);
            if (_orders.Count > MaxItems)
            {
                _orders.RemoveAt(0);
            }
        }
    }

    public IReadOnlyList<Order> GetLast()
    {
        lock (_lock)
        {
            return _orders.ToArray();
        }
    }
}
