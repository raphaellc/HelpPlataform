using FastEndpoints;
using FluentValidation;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class AcceptDonationRequestClaimValidator : Validator<AcceptDonationRequestClaimRequest>
{
    public AcceptDonationRequestClaimValidator()
    {
        RuleFor(request => request.RequestId)
            .GreaterThan(0)
            .WithMessage("Invalid donation request");
        
        RuleFor(request => request.ClaimId)
            .GreaterThan(0)
            .WithMessage("Invalid claim");
    }
}
