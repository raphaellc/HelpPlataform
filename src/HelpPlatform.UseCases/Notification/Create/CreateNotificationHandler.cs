using Ardalis.GuardClauses;
using Ardalis.Result;
using HelpPlatform.Core.NotificationDomain;
using HelpPlatform.SharedKernel;
using HelpPlatform.UseCases.Notifications.Create;

namespace HelpPlatform.UseCases.Notifications.Create;

public class CreateNotificationHandler(IRepository<Notification> repository) : ICommandHandler<CreateNotificationCommand, Result>
{
    public async Task<Result> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification(request.UserId, request.Message);

        var createdNotification = await repository.AddAsync(notification, cancellationToken);

        return Result.Success();
    }
}