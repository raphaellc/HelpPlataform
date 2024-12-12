namespace HelpPlatform.Web.Notifications;
public class CreateNotificationRequest
{
    public const string Route = "/notifications";
    public int? UserId { get; set; }
    public string? Message { get; set; }
}