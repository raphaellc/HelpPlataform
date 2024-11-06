using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain;

public static class DonationRequestErrors
{
    public const string DonationRequestNotFound = "DONATION_REQUEST_NOT_FOUND";
    
    public const string ClaimNotFound = "DONATION_REQUEST_CLAIM_NOT_FOUND";


    public const string ClaimQuantityOutOfRangeErrorMessage =
        "Claim quantity was greater than the request's remaining quantity or less than 1";

    public const string IllegalClaimAcceptingErrorMessage = "Illegal claim accepting";
    public const string IllegalClaimCancellingErrorMessage = "Illegal claim cancelling";
    public const string IllegalClaimRejectingErrorMessage = "Illegal claim rejecting";
    public const string IllegalClaimFulfillingErrorMessage = "Illegal claim fulfilling";
    public const string IllegalClaimUnfulfillingErrorMessage = "Illegal claim unfulfilling";
    public const string IllegalNotNeededClaimErrorMessage = "Illegal 'not needed' claim transition";
    public const string IllegalRequestClosedClaimErrorMessage = "Illegal 'request closed' claim transition";
    

    public static ValidationError AcceptClaimPastDeadline =>
        new ValidationError
        {
            ErrorCode = "ACCEPT_CLAIM_PAST_DEADLINE", ErrorMessage = "Cannot accept claim past its deadline"
        };

    public static ValidationError ModifyingClosedRequest =>
        new ValidationError
        {
            ErrorCode = "MODIFYING_CLOSED_REQUEST", ErrorMessage = "Cannot modify a closed request"
        };

    public static ValidationError CannotCancelClaim =>
        new ValidationError
        {
            ErrorCode = "CANNOT_CANCEL_CLAIM", ErrorMessage = "Claim is not cancellable in its current status"
        };

    public static ValidationError NotOneHourBeforeUnfulfilled =>
        new ValidationError
        {
            ErrorCode = "NOT_ONE_HOUR_BEFORE_UNFULFILLED",
            ErrorMessage = "Claim cannot be marked unfulfilled before one hour has passed since acceptance" 
        };
}
