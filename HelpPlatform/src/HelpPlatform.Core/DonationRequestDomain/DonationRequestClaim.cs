using Ardalis.GuardClauses;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.UserDomain;

namespace HelpPlatform.Core.DonationRequestDomain;

public class DonationRequestClaim(
    string? message,
    int userId,
    int requestId,
    DateTime? deadline
    ) : EntityBase
{
    public string? Message { get; private set; } = message;
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public DateTime? Deadline { get; private set; } = deadline;

    public DonationRequestClaimStatusEnum Status { get; private set; } = DonationRequestClaimStatusEnum.Waiting;
    
    public int UserId { get; private set; } = Guard.Against.NegativeOrZero(userId);
    public User? User { get; private set; }

    public int RequestId { get; private set; } = Guard.Against.NegativeOrZero(requestId);
}
