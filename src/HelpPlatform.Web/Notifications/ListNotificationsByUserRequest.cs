namespace HelpPlatform.Web.Notifications;
public class ListNotificationsByUserRequest
{
    public const string Route = "/notifications/{UserId}";
    public int? UserId { get; set; }
}