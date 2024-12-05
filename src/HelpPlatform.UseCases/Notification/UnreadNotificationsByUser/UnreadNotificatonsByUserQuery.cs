using HelpPlatform.SharedKernel;
using Ardalis.Result;
using HelpPlatform.Core.NotificationDomain;
using HelpPlatform.UseCases.NotificationDomain.Dtos;

namespace HelpPlatform.UseCases.Notifications.UnreadNotificationsByUser;

public record UnreadNotificationsByUserQuery(int UserId) : IQuery<Result<IEnumerable<NotificationDto>>>;
