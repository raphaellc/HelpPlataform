using FastEndpoints;
using HelpPlatform.UseCases.Users.Create;
using MediatR;

namespace HelpPlatform.Web.Users;

public class Create(IMediator _mediator) : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure() {
        Post(CreateUserRequest.Route);
        AllowAnonymous();
        Summary(s => {
            // XML Docs are used by default but are overridden by these properties:
            //s.Summary = "Create a new Contributor.";
            //s.Description = "Create a new Contributor. A valid name is required.";
            s.ExampleRequest = new CreateUserRequest { Name = "User Name", Email = "email@email.com" };
        });
    }
    
    public override async Task HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(new CreateUserCommand(request.Name!, request.Email!), cancellationToken);

        if (result.IsSuccess) {
            Response = new CreateUserResponse(id: result.Value, name: request.Name!, email: request.Email);
        } else
        {
            foreach (var resultError in result.Errors)
            {
                AddError(resultError);
            }
            foreach (var resultError in result.ValidationErrors)
            {
                AddError(resultError.ErrorMessage);
            }

            ThrowIfAnyErrors();
        }
    }
}
