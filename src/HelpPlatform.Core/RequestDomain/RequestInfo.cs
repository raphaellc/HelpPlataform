using Ardalis.GuardClauses;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.Core.ResourceTypeDomain;
using HelpPlatform.Core.UserDomain;

namespace HelpPlatform.Core.RequestDomain;

public class RequestInfo(
    string? description,
    DateTime deadline,
    string location,
    int resourceTypeId,
    int userId)
{
    public string? Description { get; private set; } = description;

    public DateTime Deadline { get; private set; } =
        Guard.Against.Null(deadline, nameof(deadline));

    public string Location { get; private set; } =
        Guard.Against.NullOrEmpty(location, nameof(location));

    public int ResourceTypeId { get; private set; } =
        Guard.Against.NegativeOrZero(resourceTypeId, nameof(resourceTypeId));
    public ResourceType? ResourceType { get; private set; }

    // All donation requests start in "Open" status
    public DonationRequestStatusEnum Status { get; private set; } =
        DonationRequestStatusEnum.Open;

    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public int UserId { get; private set; } =
        Guard.Against.Negative(userId, nameof(userId));
    public User? User { get; private set; }
}
