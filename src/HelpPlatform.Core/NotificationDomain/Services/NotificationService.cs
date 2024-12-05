using Ardalis.Result;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.NotificationDomain.Interfaces;
using HelpPlatform.Core.NotificationDomain.Specifications;

namespace HelpPlatform.Core.NotificationDomain.Services;

public class NotificationService(
    IRepository<Notification> repository,
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
            Console.WriteLine("======================= Not found =======================");
            return Result.NotFound("Notification not found.");
        }

        if (notification.UserId != userId)
        {
            Console.WriteLine("======================= Forbidden =======================");
            return Result.Forbidden();
        }

        notification.Read = true;
        await repository.UpdateAsync(notification, cancellationToken);
        Console.WriteLine("======================= Success =======================");
        return Result.Success();
    }    

     public async Task<Result<List<Notification>>> ListNotificationsByUserAsync(int userId, CancellationToken cancellationToken = default)
    {
        var notifications = await repository.ListAsync(new NotificationsByUserSpecification(userId), cancellationToken);

        return Result<List<Notification>>.Success(notifications);
    }
    
    public async Task<Result<List<Notification>>> GetUnreadNotificationsForUserAsync(int userId, CancellationToken cancellationToken = default)
    {
        var notifications = await repository.ListAsync(new UnreadNotificationsForUserSpecification(userId), cancellationToken);
        return Result<List<Notification>>.Success(notifications);
    }
}
