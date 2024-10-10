using Ardalis.Specification;


namespace HelpPlatform.Core.NotificationDomain.Specifications;

public sealed class NotificationsContainingKeywordSpecification : Specification<Notification>
{
    public NotificationsContainingKeywordSpecification(string keyword)
    {
        Query
            .Where(n => n.Message.Contains(keyword));
    }
}