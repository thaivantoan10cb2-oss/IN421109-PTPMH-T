using EduHub.Application.Abstractions;

namespace EduHub.Infrastructure.Notifications;

/// <summary>Shared behaviour for notification services that just print to the console.</summary>
public abstract class ConsoleNotificationServiceBase : INotificationService
{
    public abstract string Channel { get; }

    public virtual void Send(string message)
    {
        Console.WriteLine($"[{Channel.ToUpperInvariant()}] {message}");
    }
}
