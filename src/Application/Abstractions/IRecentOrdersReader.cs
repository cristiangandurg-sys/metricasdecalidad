using System.Collections.Generic;
using Domain.Entities;

namespace Application.Abstractions;

public interface IRecentOrdersReader
{
    IReadOnlyList<Order> GetLast();
}
