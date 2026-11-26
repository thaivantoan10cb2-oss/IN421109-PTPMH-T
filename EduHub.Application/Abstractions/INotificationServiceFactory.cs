namespace EduHub.Application.Abstractions;

public interface INotificationServiceFactory
{
    /// <exception cref="ArgumentException">The channel is not supported.</exception>
    INotificationService Create(string channel);
}
