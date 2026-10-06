using System.Security.Cryptography;
using Application.Abstractions;

namespace Infrastructure.Services;

public class SecureIdGenerator : IIdGenerator
{
    public int NextId() => RandomNumberGenerator.GetInt32(1, 9999999);
}
