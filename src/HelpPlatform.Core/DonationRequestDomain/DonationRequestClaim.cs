using System.Runtime.InteropServices.JavaScript;
using Ardalis.GuardClauses;
using Ardalis.Result;
using HelpPlatform.SharedKernel;
using HelpPlatform.Core.UserDomain;

namespace HelpPlatform.Core.DonationRequestDomain;

public class DonationRequestClaim(
    string? message,
    int userId,
    int requestId,
    int quantity,
    DateTime? deadline
    ) : EntityBase
{
    public string? Message { get; private set; } = message;

    public int Quantity { get; set; } = quantity;
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public DateTime? Deadline { get; private set; } = deadline;

    public DateTime? AcceptedAt { get; private set; } = null;

    public DonationRequestClaimStatusEnum Status { get; private set; } = DonationRequestClaimStatusEnum.Waiting;
    
    public int UserId { get; private set; } = Guard.Against.NegativeOrZero(userId);
    public User? User { get; private set; }

    public int RequestId { get; private set; } = Guard.Against.NegativeOrZero(requestId);
    public DonationRequest? DonationRequest { get; private set; }

    public void ReduceQuantity(int quantity)
    {
        Guard.Against.NegativeOrZero(quantity, nameof(quantity));
        Quantity -= quantity;
    }

    public void SetAccepted()
    {
        Guard.Against.Expression(status => status != DonationRequestClaimStatusEnum.Waiting, Status, DonationRequestErrors.IllegalClaimAcceptingErrorMessage);
        Status = DonationRequestClaimStatusEnum.Accepted;
        AcceptedAt = DateTime.Now;
    }
    
    public void SetRejected()
    {
        Guard.Against.Expression(status => status != DonationRequestClaimStatusEnum.Waiting, Status, DonationRequestErrors.IllegalClaimRejectingErrorMessage);
        Status = DonationRequestClaimStatusEnum.Rejected;
    }

    public void SetNotNeeded()
    {
        Guard.Against.Expression(status => status != DonationRequestClaimStatusEnum.Waiting, Status, DonationRequestErrors.IllegalNotNeededClaimErrorMessage);
        Status = DonationRequestClaimStatusEnum.NotNeeded;
    }
    public void SetFulfilled()
    {
        Guard.Against.Expression(status => status != DonationRequestClaimStatusEnum.Accepted, Status, DonationRequestErrors.IllegalClaimFulfillingErrorMessage);
        Status = DonationRequestClaimStatusEnum.Fulfilled;
    }

    public Result SetUnfulfilled()
    {
        Guard.Against.Null(AcceptedAt, nameof(AcceptedAt));
        
        if (AcceptedAt.Value.AddHours(1) > DateTime.Now)
        {
            var res = Result.Invalid(DonationRequestErrors.NotOneHourBeforeUnfulfilled);
        }
        
        Guard.Against.Expression(status => status != DonationRequestClaimStatusEnum.Accepted, Status, DonationRequestErrors.IllegalClaimUnfulfillingErrorMessage);
        Status = DonationRequestClaimStatusEnum.Unfulfilled;

        return Result.Success();
    }

    public void SetCancelled()
    {
        Guard.Against.Expression
        (
            status => status is
                DonationRequestClaimStatusEnum.Fulfilled or
                DonationRequestClaimStatusEnum.Unfulfilled or
                DonationRequestClaimStatusEnum.Rejected or
                DonationRequestClaimStatusEnum.NotNeeded or
                DonationRequestClaimStatusEnum.RequestClosed,
            Status, DonationRequestErrors.IllegalClaimCancellingErrorMessage
        );

        Status = DonationRequestClaimStatusEnum.Cancelled;
    }

    public void SetRequestClosed()
    {
        Guard.Against.Expression
        (
            status => status is
                DonationRequestClaimStatusEnum.Cancelled or
                DonationRequestClaimStatusEnum.NotNeeded or
                DonationRequestClaimStatusEnum.Fulfilled or
                DonationRequestClaimStatusEnum.Unfulfilled or
                DonationRequestClaimStatusEnum.Rejected or 
                DonationRequestClaimStatusEnum.RequestClosed,
            Status, DonationRequestErrors.IllegalRequestClosedClaimErrorMessage
        );

        Status = DonationRequestClaimStatusEnum.RequestClosed;
    }
}
