using Ardalis.GuardClauses;
using Ardalis.Result;
using HelpPlatform.Core.ResourceTypeDomain;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.RequestDomain.DonationRequestDomain;

public class DonationRequest(
    string? description,
    DateTime deadline,
    string location,
    int resourceTypeId,
    int requestedQuantity,
    int userId) : EntityBase, IAggregateRoot, IRequest
{
    public string? Description { get; private set; } = description;

    public DateTime Deadline { get; private set; } =
        Guard.Against.Null(deadline, nameof(deadline));

    public string Location { get; private set; } =
        Guard.Against.NullOrEmpty(location, nameof(location));

    public int ResourceTypeId { get; private set; } =
        Guard.Against.NegativeOrZero(resourceTypeId, nameof(resourceTypeId));
    public ResourceType? ResourceType { get; private set; }
    
    public int RequestedQuantity { get; private set; } =
        Guard.Against.NegativeOrZero(requestedQuantity, nameof(requestedQuantity));

    public int FulfilledQuantity { get; private set; }

    public int RemainingQuantity => RequestedQuantity - FulfilledQuantity;

    // All donation requests start in "Open" status
    public DonationRequestStatusEnum Status { get; private set; } =
        DonationRequestStatusEnum.Open;

    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public int UserId { get; private set; } =
        Guard.Against.Negative(userId, nameof(userId));
    public User? User { get; private set; }

    private readonly List<DonationRequestClaim> _claims = [];
    public IReadOnlyCollection<DonationRequestClaim> Claims => _claims.AsReadOnly();

    public bool IsEditable => Status is
        not DonationRequestStatusEnum.Completed
        and not DonationRequestStatusEnum.Closed;

    public Result AddClaim(string? message, int userId, int requestId, int quantity, DateTime? deadline)
    {
        if (!IsEditable) return Result.Invalid(new ValidationError { ErrorMessage = "Cannot modify a closed request" });
        
        var hasOpenClaims =
            Claims.Any(claim => claim.UserId == userId && claim.Status
                                    is DonationRequestClaimStatusEnum.Waiting
                                    or DonationRequestClaimStatusEnum.Accepted);
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
            return Result.Invalid
            (
                new ValidationError { ErrorMessage = "User with multiple rejected claims on donation request" }
            );
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

        if (quantity > RemainingQuantity)
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
        if (!IsEditable) return Result.Invalid(DonationRequestErrors.ModifyingClosedRequest);
        
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);
        if (claim == null) return Result.NotFound(DonationRequestErrors.ClaimNotFound);

        if (claim.Deadline != null && claim.Deadline < DateTime.Now)
        {
            return Result.Invalid(DonationRequestErrors.AcceptClaimPastDeadline);
        }

        Guard.Against.OutOfRange(
            claim.Quantity, nameof(claim.Quantity),
            1, RemainingQuantity,
            DonationRequestErrors.ClaimQuantityOutOfRangeErrorMessage);

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
        if (!IsEditable) return Result.Invalid(DonationRequestErrors.ModifyingClosedRequest);
        
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);
        if (claim == null) return Result.NotFound(DonationRequestErrors.ClaimNotFound);

        claim.SetRejected();

        return Result.Success();
    }

    public Result MarkClaimAsFulfilled(int claimId)
    {
        if (!IsEditable) return Result.Invalid(DonationRequestErrors.ModifyingClosedRequest);
        
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);
        if (claim == null) return Result.NotFound(DonationRequestErrors.ClaimNotFound);
        
        claim.SetFulfilled();

        var hasOtherClaimsToFulfill = Claims.Any(otherClaim => otherClaim.Status == DonationRequestClaimStatusEnum.Accepted);
        if (!hasOtherClaimsToFulfill)
        {
            Status = DonationRequestStatusEnum.Completed;
        }

        return Result.Success();
    }

    public Result MarkClaimAsUnfulfilledAndRevertQuantity(int claimId)
    {
        if (!IsEditable) return Result.Invalid(DonationRequestErrors.ModifyingClosedRequest);
        
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);
        if (claim == null) return Result.NotFound(DonationRequestErrors.ClaimNotFound);

        var result = claim.SetUnfulfilled();
        if (!result.IsSuccess) return result;
        
        _revertClaimChanges(claim.Quantity);

        return Result.Success();
    }

    private void _revertClaimChanges(int claimedQuantity)
    {
        FulfilledQuantity -= claimedQuantity;
        Status = FulfilledQuantity == 0 ?
            DonationRequestStatusEnum.Open : DonationRequestStatusEnum.PartiallyClaimed;
    }

    public Result CancelClaim(int claimId)
    {
        if (!IsEditable) return Result.Invalid(DonationRequestErrors.ModifyingClosedRequest);
        
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);
        if (claim == null) return Result.NotFound(DonationRequestErrors.ClaimNotFound);

        switch (claim.Status)
        {
            case DonationRequestClaimStatusEnum.Waiting:
                claim.SetCancelled();
                break;
            case DonationRequestClaimStatusEnum.Accepted:
                claim.SetCancelled();
                _revertClaimChanges(claim.Quantity);
                break;
            case DonationRequestClaimStatusEnum.Rejected:
            case DonationRequestClaimStatusEnum.Fulfilled:
            case DonationRequestClaimStatusEnum.Unfulfilled:
            case DonationRequestClaimStatusEnum.Cancelled:
            case DonationRequestClaimStatusEnum.NotNeeded:
            case DonationRequestClaimStatusEnum.RequestClosed:
                return Result.Invalid(DonationRequestErrors.CannotCancelClaim);
            default:
                throw new ArgumentOutOfRangeException(nameof(claim.Status));
        }
        
        return Result.Success();
    }

    public Result<int> GetClaimDonorId(int claimId)
    {
        var claim = Claims.FirstOrDefault(claim => claim.Id == claimId);
        return claim == null ? Result.NotFound(DonationRequestErrors.ClaimNotFound) : Result.Success(claim.UserId);
    }
    
    public Result Close()
    {
        if (!IsEditable) return Result.Invalid(DonationRequestErrors.ModifyingClosedRequest);
        
        var claims = Claims.Where(claim =>
            claim.Status is DonationRequestClaimStatusEnum.Accepted or DonationRequestClaimStatusEnum.Waiting);
        
        foreach (var claim in claims) {
            claim.SetRequestClosed();
        }

        Status = DonationRequestStatusEnum.Closed;

        return Result.Success();
    }
}
