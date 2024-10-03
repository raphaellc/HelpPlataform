using Ardalis.Specification;
using HelpPlatform.Core.NotificationDomain;

namespace HelpPlatform.Core.NotificationDomain.Specifications;

public sealed class UnreadNotificationsForUserSpecification : Specification<Notification>
{
    public UnreadNotificationsForUserSpecification(int userId)
    {
        Query
            .Where(n => n.UserId == userId && !n.Read);
    }
}