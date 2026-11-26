using EduHub.Application.Abstractions;

namespace EduHub.Infrastructure.Notifications;

/// <summary>
/// Picks the notification service whose Channel matches the requested one.
/// Adding a new channel only needs a new INotificationService registration (open/closed).
/// </summary>
public class NotificationServiceFactory : INotificationServiceFactory
{
    private readonly IEnumerable<INotificationService> _services;

    public NotificationServiceFactory(IEnumerable<INotificationService> services)
    {
        _services = services;
    }

    public INotificationService Create(string channel)
    {
        if (string.IsNullOrWhiteSpace(channel))
            throw new ArgumentException("Channel must not be empty.", nameof(channel));

        var service = _services.FirstOrDefault(s =>
            string.Equals(s.Channel, channel.Trim(), StringComparison.OrdinalIgnoreCase));

        return service
               ?? throw new ArgumentException($"Unsupported notification channel: '{channel}'.", nameof(channel));
    }
}
