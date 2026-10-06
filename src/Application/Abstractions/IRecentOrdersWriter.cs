using Domain.Entities;

namespace Application.Abstractions;

public interface IRecentOrdersWriter
{
    void Add(Order order);
}
