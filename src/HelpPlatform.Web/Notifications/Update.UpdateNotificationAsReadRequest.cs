namespace HelpPlatform.Web.Notifications;

public class UpdateNotificationAsReadRequest
{
    public const string Route = "/notifications/{NotificationId:int}";

    public static string BuildRoute(int NotificationId) => Route.Replace("{NotificationId:int}", NotificationId.ToString());

    public int NotificationId { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
}