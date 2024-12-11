using FastEndpoints;
using HelpPlatform.UseCases.Users.Create;
using HelpPlatform.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace HelpPlatform.Web.Users;

public class Create(IMediator _mediator) : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        Post(CreateUserRequest.Route);
        AllowAnonymous();
        Summary(s => {
            s.ExampleRequest = new CreateUserRequest { Name = "User Name", Email = "email@email.com" };
        });
    }
    
    public override async Task HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(new CreateUserCommand(request.Name!, request.Email!), cancellationToken);

        await this.SendResponse(result, r => new CreateUserResponse(
            result.Value,
            request.Name!,
            request.Email
        ));
    }
}
