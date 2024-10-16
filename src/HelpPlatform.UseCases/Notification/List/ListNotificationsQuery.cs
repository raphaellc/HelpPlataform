using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.NotificationDomain.Dtos;
namespace HelpPlatform.UseCases.Notifications.List;

public record ListNotificationsQuery : IQuery<Result<IEnumerable<NotificationDto>>>;