using FastEndpoints;
using FluentValidation;
using HelpPlatform.Infrastructure.Data.Config;

namespace HelpPlatform.Web.Users;

public class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(DataSchemaConstants.DefaultNameLength);

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email invalid");
    }
}
