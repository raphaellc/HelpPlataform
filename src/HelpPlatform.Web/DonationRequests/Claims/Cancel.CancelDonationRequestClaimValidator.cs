using FastEndpoints;
using FluentValidation;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class CancelDonationRequestClaimValidator : Validator<CancelDonationRequestClaimRequest>
{
    public CancelDonationRequestClaimValidator()
    {
        RuleFor(request => request.RequestId)
            .GreaterThan(0)
            .WithMessage("Invalid donation request");
    }
}
