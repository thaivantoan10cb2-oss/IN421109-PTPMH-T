namespace EduHub.Infrastructure.Notifications;

public class EmailNotificationService : ConsoleNotificationServiceBase
{
    public override string Channel => "email";
}
