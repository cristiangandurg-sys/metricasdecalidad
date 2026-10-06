using Application.Abstractions;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data;

public class SqlOrderRepository : IOrderWriter
{
    private const string InsertSql =
        "INSERT INTO Orders(Id, Customer, Product, Qty, Price) VALUES (@Id, @Customer, @Product, @Qty, @Price)";

    private readonly string _connectionString;

    public SqlOrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Save(Order order)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(InsertSql, connection);

        // Consulta parametrizada: elimina la inyeccion SQL.
        command.Parameters.AddWithValue("@Id", order.Id);
        command.Parameters.AddWithValue("@Customer", order.CustomerName);
        command.Parameters.AddWithValue("@Product", order.ProductName);
        command.Parameters.AddWithValue("@Qty", order.Quantity);
        command.Parameters.AddWithValue("@Price", order.UnitPrice);

        connection.Open();
        command.ExecuteNonQuery();
    }
}
