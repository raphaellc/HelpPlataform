using System.Collections.ObjectModel;
using Ardalis.Result;
using HelpPlatform.Core.NotificationDomain;
using HelpPlatform.Core.NotificationDomain.Services;
using HelpPlatform.Core.NotificationDomain.Interfaces;
using HelpPlatform.UseCases.NotificationDomain.Dtos;
using HelpPlatform.SharedKernel;
using MediatR;

namespace HelpPlatform.UseCases.Notifications.List;

public class ListNotificationHandler(INotificationService notificationService) : IQueryHandler<ListNotificationsQuery, Result<IEnumerable<NotificationDto>>>
{
    public async Task<Result<IEnumerable<NotificationDto>>> Handle(ListNotificationsQuery request, CancellationToken cancellationToken)
    {
        var result = await notificationService.ListNotificationsByUserAsync(request.UserId,cancellationToken);
        
        return result.Map(notifications => notifications.Select(n => new NotificationDto(
            userId: n.UserId,
            message: n.Message,
            read: n.Read,
            createdAt: n.CreatedAt
        )));
    }
}
