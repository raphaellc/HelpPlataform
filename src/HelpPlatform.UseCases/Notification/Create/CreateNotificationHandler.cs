using Ardalis.Result;
using HelpPlatform.Core.NotificationDomain;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.Notifications.Create;

public class CreateNotificationHandler(IRepository<Notification> repository) : ICommandHandler<CreateNotificationCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification(request.UserId, request.Message);
        var createdNotification = await repository.AddAsync(notification, cancellationToken);

        return createdNotification.Id;
    }
}