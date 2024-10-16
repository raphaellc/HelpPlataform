using FastEndpoints;
using FluentValidation;
using HelpPlatform.Infrastructure.Data.Config;

namespace HelpPlatform.Web.ResourceTypes;

public class CreateResourceTypeValidator : Validator<CreateResourceTypeRequest>
{
    public CreateResourceTypeValidator()
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

    }
}
