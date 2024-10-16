using FastEndpoints;
using FluentValidation;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class ListDonationRequestClaimsValidator : Validator<ListDonationRequestClaimsRequest>
{
    public ListDonationRequestClaimsValidator()
    {
        RuleFor(request => request.RequestId)
            .GreaterThan(0)
            .WithMessage("Invalid donation request");
    }
}
