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

    // This will become a reference to another entity
    public string ResourceType { get; private set; } = Guard.Against.NullOrEmpty(resourceType, nameof(resourceType));

    // All resources will have a measurement which defines a unit
    // And this will always be a unitary quantity of the resource
    public int RequestedQuantity { get; private set; } = Guard.Against.NegativeOrZero(requestedQuantity, nameof(requestedQuantity));

    public int FulfilledQuantity { get; private set; }

    public int RemainingQuantity => RequestedQuantity - FulfilledQuantity;

    // All donation requests start in "Open" status
    public DonationRequestStatusEnum Status { get; private set; } = DonationRequestStatusEnum.Open;

    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public int UserId { get; private set; } = Guard.Against.Negative(userId, nameof(userId));

    public User? User { get; init; }

    private readonly List<DonationRequestClaim> _claims = [];

    public IReadOnlyCollection<DonationRequestClaim> Claims => _claims.AsReadOnly();

    public Result AddClaim(string? message, int userId, int requestId, int quantity, DateTime? deadline)
    {
        var hasOpenClaims =
            Claims.Any(claim => claim.UserId == userId && claim.Status == DonationRequestClaimStatusEnum.Waiting);
        if (hasOpenClaims)
        {
            return Result.Invalid
            (
                new ValidationError { ErrorMessage = "User with open claim on request" }
            );
        }
        
        // This will stop users from spamming the requester with unwanted claims
        var hasMultipleRejectedClaims =
            Claims.Count(claim => claim.UserId == userId && claim.Status == DonationRequestClaimStatusEnum.Rejected) >= 2;
        if (hasMultipleRejectedClaims)
        {
            return Result.Invalid(new ValidationError { ErrorMessage = "User with multiple rejected claims on donation request" });
        }
        
        if (DateTime.Now > Deadline)
        {
            return Result.Invalid
            (
                new ValidationError { ErrorMessage = "Claim cannot be made after donation request deadline is passed"}
            );
        }
        
        if (deadline > Deadline)
        {
            return Result.Invalid
            (
                new ValidationError { ErrorMessage = "Claim deadline must not be greater than request deadline" }
            );
        }

        if (quantity > RequestedQuantity - FulfilledQuantity)
        {
            return Result.Invalid
            (
                new ValidationError { ErrorMessage = "Claim quantity must not be greater than remaining requested quantity" }
            );
        }
        
        _claims.Add(new DonationRequestClaim(message, userId, requestId, quantity, deadline));
        
        return Result.Success();
    }

    public Result AcceptClaim(int claimId)
    {
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);

        if (claim == null) return Result.NotFound("Claim not found");

        if (claim.Deadline != null && claim.Deadline < DateTime.Now)
        {
            Result.Invalid(new ValidationError { ErrorMessage = "Cannot accept claim past its deadline" });
        }

        Guard.Against.OutOfRange(
            claim.Quantity, nameof(claim.Quantity),
            1, RemainingQuantity,
            "Claim quantity was greater than the request's remaining quantity or less than 1");

        FulfilledQuantity += claim.Quantity;
        claim.SetAccepted();
            
        if (RemainingQuantity == 0)
        {
            Status = DonationRequestStatusEnum.Claimed;
            _updateNotNeededClaims();
        }
        else
        {
            Status = DonationRequestStatusEnum.PartiallyClaimed;
            _updateClaimQuantities();
        }

        return Result.Success();
    }

    private void _updateClaimQuantities()
    {
        foreach (var claim in _claims)
        {
            if (claim.Quantity > RemainingQuantity)
            {
                claim.ReduceQuantity(claim.Quantity - RemainingQuantity);
            }
        }
    }

    private void _updateNotNeededClaims()
    {
        foreach (var claim in _claims)
        {
            if (claim.Status == DonationRequestClaimStatusEnum.Waiting)
            {
                claim.SetNotNeeded();
            }
        }
    }

    public Result RejectClaim(int claimId)
    {
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);

        if (claim == null) return Result.NotFound("Claim not found");

        claim.SetRejected();

        return Result.Success();
    }

    public Result MarkClaimAsFulfilled(int claimId)
    {
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);

        if (claim == null) return Result.NotFound("Claim not found");

        claim.SetFulfilled();

        var hasOtherClaimsToFulfill = Claims.Any(otherClaim => otherClaim.Status == DonationRequestClaimStatusEnum.Accepted);
        if (!hasOtherClaimsToFulfill)
        {
            Status = DonationRequestStatusEnum.Completed;
        }

        return Result.Success();
    }

    public Result<int> GetClaimDonorId(int claimId)
    {
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);

        if (claim == null) return Result.NotFound("Claim not found");

        return Result.Success(claim.UserId);
    }
}
