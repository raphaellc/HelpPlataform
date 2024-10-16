using Ardalis.GuardClauses;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.NotificationDomain;

public class Notification(int userId, string message) : EntityBase, IAggregateRoot
{
    public int UserId { get; private set; } = Guard.Against.Negative(userId, nameof(userId)); 

    public string Message { get; private set; } = Guard.Against.NullOrEmpty(message, nameof(message));

    public bool Read { get; set; } = false;
    public void MarkAsRead() 
    {
        Read = true; // Pode ser definido dentro da classe
    }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}