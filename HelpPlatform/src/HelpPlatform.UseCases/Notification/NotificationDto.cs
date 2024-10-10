namespace HelpPlatform.Core.NotificationDomain.Dtos;

public class NotificationDto(int userId, string message, bool read, DateTime createdAt)
{
    public int UserId { get; set; } = userId;
    public string Message { get; set; } = message;
    public bool Read { get; set; } = read;
    public DateTime CreatedAt { get; set; } = createdAt;
}