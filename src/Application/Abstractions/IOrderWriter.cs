using Domain.Entities;

namespace Application.Abstractions;

public interface IOrderWriter
{
    void Save(Order order);
}
