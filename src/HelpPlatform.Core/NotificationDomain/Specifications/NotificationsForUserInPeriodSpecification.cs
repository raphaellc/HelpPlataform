using Ardalis.Specification;
using HelpPlatform.Core.NotificationDomain;

namespace HelpPlatform.Core.NotificationDomain.Specifications;

public sealed class NotificationsForUserInPeriodSpecification : Specification<Notification>
{
    public NotificationsForUserInPeriodSpecification(int userId, DateTime startDate, DateTime endDate)
    {
        Query
            .Where(n => n.UserId == userId && n.CreatedAt >= startDate && n.CreatedAt <= endDate);
    }
}