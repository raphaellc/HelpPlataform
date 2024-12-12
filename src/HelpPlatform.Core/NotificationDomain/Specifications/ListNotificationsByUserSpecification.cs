using Ardalis.Specification;
using HelpPlatform.Core.NotificationDomain;

namespace HelpPlatform.Core.NotificationDomain.Specifications;

public sealed class NotificationsByUserSpecification : Specification<Notification>
{
    public NotificationsByUserSpecification(int userId)
    {
        Query.Where(n => n.UserId == userId);
    }
}
