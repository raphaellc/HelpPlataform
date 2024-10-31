using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.UseCases.NotificationDomain.Dtos;

namespace HelpPlatform.UseCases.Notifications.List;

public record ListNotificationsQuery(int UserId) : IQuery<Result<IEnumerable<NotificationDto>>>;