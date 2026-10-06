using System.Collections.Generic;
using Application.Abstractions;
using Domain.Entities;

namespace Application.UseCases;

public class GetLastOrdersUseCase
{
    private readonly IRecentOrdersReader _recentOrders;

    public GetLastOrdersUseCase(IRecentOrdersReader recentOrders)
    {
        _recentOrders = recentOrders;
    }

    public IReadOnlyList<Order> Execute() => _recentOrders.GetLast();
}
