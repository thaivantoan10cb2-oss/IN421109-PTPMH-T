namespace EduHub.Application.Abstractions;

public interface INotificationService
{
    /// <summary>Channel name this service handles, e.g. "email" or "sms".</summary>
    string Channel { get; }

    void Send(string message);
}
