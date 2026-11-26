namespace EduHub.Infrastructure.Notifications;

public class SmsNotificationService : ConsoleNotificationServiceBase
{
    public override string Channel => "sms";
}
