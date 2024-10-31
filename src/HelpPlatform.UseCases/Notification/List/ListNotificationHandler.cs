using Ardalis.Result;
using HelpPlatform.Core.NotificationDomain;
using HelpPlatform.UseCases.NotificationDomain.Dtos;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.Notifications.List;

public class ListNotificationHandler(IRepository<Notification> repository) : IQueryHandler<ListNotificationsQuery, Result<IEnumerable<NotificationDto>>>
{
    public async Task<Result<IEnumerable<NotificationDto>>> Handle(ListNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await repository.ListAsync(cancellationToken);

        var notificationDtos = notifications.Select(n => new NotificationDto(
            userId: n.UserId,
            message: n.Message,
            read: n.Read,
            createdAt: n.CreatedAt
        )).ToList();

        return notificationDtos;
    }
}