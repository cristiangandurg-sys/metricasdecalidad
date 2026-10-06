using System;
using Application.Abstractions;

namespace Infrastructure.Logging;

public class ConsoleAppLogger : IAppLogger
{
    public void Log(string message)
    {
        Console.WriteLine("[LOG] " + DateTime.Now + " - " + message);
    }
}
