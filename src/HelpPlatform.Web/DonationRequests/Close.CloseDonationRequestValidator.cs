using FastEndpoints;
using FluentValidation;

namespace HelpPlatform.Web.DonationRequests;

public class CloseDonationRequestValidator : Validator<CloseDonationRequestRequest>
{
    public CloseDonationRequestValidator()
    {
        RuleFor(request => request.RequestId)
            .GreaterThan(0)
            .WithMessage("Invalid donation request");
    }
}
