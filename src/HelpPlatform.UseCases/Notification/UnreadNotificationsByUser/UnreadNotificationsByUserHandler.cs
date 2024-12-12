using HelpPlatform.Core.NotificationDomain;
using HelpPlatform.Core.NotificationDomain.Interfaces;
using HelpPlatform.SharedKernel;
using Ardalis.Result;
using HelpPlatform.UseCases.NotificationDomain.Dtos;
using HelpPlatform.UseCases.Notifications.UnreadNotificationsByUser;

namespace HelpPlatform.UseCases.Notifications.UnreadNotificationsByUser;

public class UnreadNotificationsByUserHandler(INotificationService notificationService) : IQueryHandler<UnreadNotificationsByUserQuery, Result<IEnumerable<NotificationDto>>>
{
    public async Task<Result<IEnumerable<NotificationDto>>> Handle(UnreadNotificationsByUserQuery request, CancellationToken cancellationToken)
    {
        var result = await notificationService.GetUnreadNotificationsForUserAsync(request.UserId, cancellationToken);

        return result.Map(notifications => notifications.Select(n => new NotificationDto(
            notificationID: n.Id,
            userId: n.UserId,
            message: n.Message,
            read: n.Read,
            createdAt: n.CreatedAt
        )));
    }
}
