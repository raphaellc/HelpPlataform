using Ardalis.Result;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.NotificationDomain.Interfaces;

namespace HelpPlatform.Core.NotificationDomain.Services;

public class NotificationService(
    INotificationRepository repository,
    IRepository<User> userRepository
    ) : INotificationService
{
    public async Task<Result<Notification>> CreateNotificationAsync(int userId, string message, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            return Result<Notification>.NotFound("User not found.");
        }

        var notification = new Notification(userId, message);
        await repository.AddAsync(notification, cancellationToken);

        return Result<Notification>.Success(notification);
    }

    public async Task<Result> MarkNotificationAsReadAsync(int notificationId, int userId, CancellationToken cancellationToken = default)
    {
        var notification = await repository.GetByIdAsync(notificationId, cancellationToken);
        if (notification == null)
        {
            return Result.NotFound("Notification not found.");
        }

        if (notification.UserId != userId)
        {
            return Result.Forbidden("User is not authorized to modify this notification.");
        }

        notification.Read = true;
        await repository.UpdateAsync(notification, cancellationToken);

        return Result.Success();
    }    
}
