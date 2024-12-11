using FastEndpoints;
using FluentValidation;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class ListUserClaimsValidator : Validator<ListUserClaimsRequest>
{
    public ListUserClaimsValidator()
    {
        RuleFor(request => request.UserId)
            .GreaterThan(0)
            .WithMessage("Invalid user");
    }
}
