using System;
using Application.Abstractions;
using Domain.Entities;

namespace Application.UseCases;

public class CreateOrderUseCase
{
    private readonly IAppLogger _logger;
    private readonly IIdGenerator _idGenerator;
    private readonly IOrderWriter _orderWriter;
    private readonly IRecentOrdersWriter _recentOrders;

    public CreateOrderUseCase(
        IAppLogger logger,
        IIdGenerator idGenerator,
        IOrderWriter orderWriter,
        IRecentOrdersWriter recentOrders)
    {
        _logger = logger;
        _idGenerator = idGenerator;
        _orderWriter = orderWriter;
        _recentOrders = recentOrders;
    }

    public Order Execute(string customer, string product, int quantity, decimal unitPrice)
    {
        _logger.Log("CreateOrderUseCase starting");

        var order = new Order
        {
            Id = _idGenerator.NextId(),
            CustomerName = customer,
            ProductName = product,
            Quantity = quantity,
            UnitPrice = unitPrice
        };

        _recentOrders.Add(order);
        _logger.Log("Created order " + order.Id + " for " + customer + " (total " + order.CalculateTotal() + ")");

        try
        {
            _orderWriter.Save(order);
        }
        catch (Exception ex)
        {
            // El error ya no se traga en silencio: queda registrado.
            _logger.Log("Could not persist order " + order.Id + ": " + ex.Message);
        }

        return order;
    }
}
