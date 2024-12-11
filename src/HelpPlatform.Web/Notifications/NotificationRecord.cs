namespace HelpPlatform.Web.Notifications;

public record NotificationRecord(int UserId, string Message, bool Read, DateTime CreatedAt);