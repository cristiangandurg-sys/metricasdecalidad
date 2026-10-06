namespace WebApi.Contracts;

public record CreateOrderRequest(string Customer, string Product, int Quantity, decimal UnitPrice);
