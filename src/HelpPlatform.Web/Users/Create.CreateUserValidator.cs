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
            .WithMessage("Insira o Nome")
            .MaximumLength(DataSchemaConstants.DefaultNameLength);

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Insira o Email")
            .EmailAddress()
            .WithMessage("Email invalido");

        RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage("Insira a Senha")
                .MinimumLength(6)
                .WithMessage("A senha deve ter pelo menos 6 caracteres");
    }
}
