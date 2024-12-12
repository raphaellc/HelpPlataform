using FastEndpoints;
using FluentValidation;

namespace HelpPlatform.Web.DonationRequests;

public class ListDonationRequestsValidator : Validator<ListDonationRequestsRequest>
{
    public ListDonationRequestsValidator()
    {
        RuleFor(request => request.PageSize)
            .GreaterThanOrEqualTo(0)
            .Unless(request => request.PageSize == null)
            .WithMessage("Page size must be null or positive");
        
        RuleFor(request => request.PageIndex)
            .GreaterThanOrEqualTo(0)
            .Unless(request => request.PageIndex == null)
            .WithMessage("Page index must be null or positive");
    }
}
