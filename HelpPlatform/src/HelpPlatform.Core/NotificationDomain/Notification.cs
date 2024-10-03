using Ardalis.GuardClauses;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.NotificationDomain;

public class Notification(int userId, string message) : EntityBase
{
    public int NotificationId { get; private set; } // PK

    public int UserId { get; private set; } = Guard.Against.Negative(userId, nameof(userId)); // PK

    public string Message { get; private set; } = Guard.Against.NullOrEmpty(message, nameof(message));

    public bool Read { get; private set; } = false;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}