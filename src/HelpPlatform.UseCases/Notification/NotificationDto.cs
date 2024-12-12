using MediatR.Wrappers;

namespace HelpPlatform.UseCases.NotificationDomain.Dtos;

public class NotificationDto(int notificationID, int userId, string message, bool read, DateTime createdAt)
{
    public int NotificationID { get; set; } = notificationID;
    public int UserId { get; set; } = userId;
    public string Message { get; set; } = message;
    public bool Read { get; set; } = read;
    public DateTime CreatedAt { get; set; } = createdAt;
}
