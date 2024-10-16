using HelpPlatform.Infrastructure.Data.Config;
using FastEndpoints;
using FluentValidation;
using HelpPlatform.Web.Contributors;

namespace HelpPlatform.Web.ResourceTypes;

public class UpdateResourceTypeValidator : Validator<UpdateResourceTypeRequest>
{
    public UpdateResourceTypeValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(DataSchemaConstants.DefaultNameLength)
            .WithMessage("Name too long");
        
        RuleFor(request => request.Scale)
            .NotEmpty()
            .WithMessage("Scale is required")
            .MaximumLength(DataSchemaConstants.DefaultScaleLength)
            .WithMessage("Scale too long");

        RuleFor(request => request.ResourceTypeId)
            .Must((args, resourceTypeId) => args.Id == resourceTypeId)
            .WithMessage("Route and body Ids must match; cannot update Id of an existing resource.");
    }
}
