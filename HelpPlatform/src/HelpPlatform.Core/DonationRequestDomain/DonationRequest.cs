using Ardalis.GuardClauses;
using Ardalis.Result;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.DonationRequestDomain;

public class DonationRequest(
    string? description,
    DateTime deadline,
    string location,
    string resourceType,
    int requestedQuantity,
    int userId) : EntityBase, IAggregateRoot
{
    public string? Description { get; private set; } = description;

    public DateTime Deadline { get; private set; } = Guard.Against.Null(deadline, nameof(deadline));

    public string Location { get; private set; } = Guard.Against.NullOrEmpty(location, nameof(location));

    // This will become reference to another entity
    public string ResourceType { get; private set; } = Guard.Against.NullOrEmpty(resourceType, nameof(resourceType));

    // All resources will have a measurement which defines a unit
    // And this will always be a unitary quantity of the resource
    public int RequestedQuantity { get; private set; } = Guard.Against.NegativeOrZero(requestedQuantity, nameof(requestedQuantity));

    public int FulfilledQuantity { get; private set; }

    // All donation requests start in "Open" status
    public DonationRequestStatusEnum Status { get; private set; } = DonationRequestStatusEnum.Open;

    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public int UserId { get; private set; } = Guard.Against.Negative(userId, nameof(userId));

    public User? User { get; init; }

    private readonly List<DonationRequestClaim> _claims = [];

    public IReadOnlyCollection<DonationRequestClaim> Claims => _claims.AsReadOnly();

    public Result AddClaim(string? message, int userId, int requestId, DateTime? deadline)
    {
        var hasOpenClaims =
            Claims.Any(claim => claim.UserId == userId && claim.Status == DonationRequestClaimStatusEnum.Waiting);
        if (hasOpenClaims)
        {
            return Result.Invalid(
                new ValidationError
                {
                    ErrorMessage = "User with open claim on request"
                }
            );
        }
        
        _claims.Add(new DonationRequestClaim(message, userId, requestId, deadline));
        
        return Result.Success();
    }
}
