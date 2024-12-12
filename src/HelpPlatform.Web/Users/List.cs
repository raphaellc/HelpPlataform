using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.UseCases.Users;
using HelpPlatform.UseCases.Users.List;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.Users;

public class List(IMediator _mediator) : EndpointWithoutRequest<ListUserResponse> {
    public override void Configure() {
        Get("/Users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) {
        Result<IEnumerable<UserDto>> result = await _mediator.Send(new ListUsersQuery(null, null), cancellationToken);

        await this.SendResponse(result, r => new ListUserResponse {
            Users = r.Value.Select(user => new UserRecord(
                user.Id,
                user.Name,
                user.Email)).ToList()
        });
        
    }
}
