using Ardalis.Result;
using HelpPlatform.Core.NotificationDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.Notifications.MarkAsRead;

public class MarkNotificationAsReadHandler(INotificationService service) : ICommandHandler<MarkNotificationAsReadCommand, Result>
{
    public async Task<Result> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        return await service.MarkNotificationAsReadAsync(request.notificationId, request.userId, cancellationToken);
    }
}