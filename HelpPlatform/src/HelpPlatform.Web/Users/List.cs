using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.Contributors;
using HelpPlatform.UseCases.Contributors.List;
using HelpPlatform.UseCases.Users;
using HelpPlatform.UseCases.Users.List;
using HelpPlatform.Web.Contributors;
using MediatR;

namespace HelpPlatform.Web.Users;

public class List(IMediator _mediator) : EndpointWithoutRequest<UserListResponse> {
    public override void Configure() {
        Get("/Users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) {
        Result<IEnumerable<UserDTO>> result = await _mediator.Send(new ListUsersQuery(null, null), cancellationToken);

        if (result.IsSuccess){
            Response = new UserListResponse
            {
                Users = result.Value.Select(userDto => new UserRecord(userDto.Id, userDto.Name, userDto.Email)).ToList()
            };
        }
    }
}
