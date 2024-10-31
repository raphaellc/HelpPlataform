using HelpPlatform.UseCases.NotificationDomain.Dtos;

namespace HelpPlatform.Web.Notifications;

public class ListNotificationsByUserResponse
{
    public IEnumerable<NotificationDto> Notifications { get; set; }

    public ListNotificationsByUserResponse(IEnumerable<NotificationDto> notifications)
    {
        Notifications = notifications;
    }
}
