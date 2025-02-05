using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.Notifications.MarkAsRead;

public record MarkNotificationAsReadCommand(int notificationId, int userId) : ICommand<Result>;