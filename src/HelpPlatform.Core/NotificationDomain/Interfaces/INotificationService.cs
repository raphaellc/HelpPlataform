using Ardalis.Result;

namespace HelpPlatform.Core.NotificationDomain.Interfaces;

public interface INotificationService
{
    Task<Result<Notification>> CreateNotificationAsync(int userId, string message, CancellationToken cancellationToken = default);
    Task<Result> MarkNotificationAsReadAsync(int notificationId, int userId, CancellationToken cancellationToken = default);
    Task<Result<List<Notification>>> ListNotificationsByUserAsync(int userId, CancellationToken cancellationToken = default);
    Task<Result<List<Notification>>> GetUnreadNotificationsForUserAsync(int userId, CancellationToken cancellationToken = default);

    // Outros métodos da interface, como GetNotificationsForUserAsync, DeleteNotificationAsync, etc.
}
