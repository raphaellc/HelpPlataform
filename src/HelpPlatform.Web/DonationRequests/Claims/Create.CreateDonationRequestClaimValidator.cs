using FastEndpoints;
using FluentValidation;
using HelpPlatform.Infrastructure.Data.Config;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class CreateDonationRequestClaimValidator : Validator<CreateDonationRequestClaimRequest>
{
    public CreateDonationRequestClaimValidator()
    {
        RuleFor(request => request.RequestId)
            .Must((args, requestId) => args.RequestId == requestId)
            .WithMessage("Route and body request IDs must match")
            .GreaterThan(0)
            .WithMessage("Invalid donation request");

        RuleFor(request => request.UserId)
            .NotEmpty()
            .WithMessage("User is required")
            .GreaterThan(0)
            .WithMessage("Invalid user");

        RuleFor(request => request.Message)
            .MaximumLength(DataSchemaConstants.DefaultClaimMessageLength)
            .WithMessage("Claim message must be less than " + DataSchemaConstants.DefaultClaimMessageLength);

        RuleFor(request => request.Quantity)
            .NotEmpty()
            .WithMessage("Quantity is required")
            .GreaterThan(0)
            .WithMessage("Quantity must be positive");

        RuleFor(request => request.Deadline)
            .GreaterThan(DateTime.Now.AddMinutes(15))
            .When(request => request.Deadline.HasValue)
            .WithMessage("Deadline must not be less than 15 minutes away");
    }
}
