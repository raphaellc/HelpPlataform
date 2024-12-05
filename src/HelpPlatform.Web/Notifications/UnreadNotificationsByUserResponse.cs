using HelpPlatform.UseCases.NotificationDomain.Dtos;

namespace HelpPlatform.Web.Notifications;

public class UnreadNotificationsByUserResponse
{
    public IEnumerable<NotificationDto> Notifications { get; set; }

    public UnreadNotificationsByUserResponse(IEnumerable<NotificationDto> notifications)
    {
        Notifications = notifications;
    }
}
