using Application.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebApi.Contracts;

namespace WebApi.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () => Results.Ok("ok"));

        app.MapPost("/orders", (CreateOrderRequest request, CreateOrderUseCase useCase) =>
        {
            if (string.IsNullOrWhiteSpace(request.Customer)
                || string.IsNullOrWhiteSpace(request.Product)
                || request.Quantity <= 0
                || request.UnitPrice < 0)
            {
                return Results.BadRequest("Datos de la orden invalidos.");
            }

            var order = useCase.Execute(request.Customer, request.Product, request.Quantity, request.UnitPrice);
            return Results.Ok(order);
        });

        app.MapGet("/orders/last", (GetLastOrdersUseCase useCase) => Results.Ok(useCase.Execute()));

        return app;
    }
}
