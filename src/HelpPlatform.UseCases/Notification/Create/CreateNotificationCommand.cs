using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.Notifications.Create;

public record CreateNotificationCommand(
    int UserId,
    string Message
) : ICommand<Result<int>>;