namespace HelpPlatform.Web.Notifications;
public class CreateNotificationResponse
{
    public int NotificationId { get; set; }

    public CreateNotificationResponse(int notificationId)
    {
        NotificationId = notificationId;
    }
}