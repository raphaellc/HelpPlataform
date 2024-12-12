using System.Data;
using FastEndpoints;
using FluentValidation;
using HelpPlatform.Infrastructure.Data.Config;

namespace HelpPlatform.Web.DonationRequests;

public class CreateDonationRequestValidator : Validator<CreateDonationRequestRequest>
{
    public CreateDonationRequestValidator()
    {
        RuleFor(request => request.Description)
            .MaximumLength(DataSchemaConstants.DefaultDescriptionLength)
            .WithMessage("Description length must be less than " + DataSchemaConstants.DefaultDescriptionLength);

        RuleFor(request => request.Deadline)
            .NotEmpty()
            .WithMessage("Deadline is required")
            .GreaterThan(DateTime.Now.AddHours(1))
            .WithMessage("Deadline must be at least 1 hour away from now")
            .LessThanOrEqualTo(DateTime.Now.AddMonths(6))
            .WithMessage("Deadline must not be more than 6 months away");

        RuleFor(request => request.RequestedQuantity)
            .NotEmpty()
            .WithMessage("Requested quantity is required")
            .GreaterThan(-1)
            .WithMessage("Requested quantity must be a positive number");

        RuleFor(request => request.ResourceTypeId)
            .NotEmpty()
            .WithMessage("ResourceType is required");

        RuleFor(request => request.UserId)
            .NotEmpty()
            .WithMessage("User is required");

        RuleFor(request => request.Location)
            .NotEmpty()
            .WithMessage("Location is required")
            .MaximumLength(DataSchemaConstants.DefaultLocationLength)
            .WithMessage("Location length must be less than " + DataSchemaConstants.DefaultLocationLength);
    }
}
