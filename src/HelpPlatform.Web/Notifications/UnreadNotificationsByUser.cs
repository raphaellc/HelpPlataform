using FastEndpoints;
using HelpPlatform.UseCases.Notifications.UnreadNotificationsByUser;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.Notifications;

public class UnreadNotificationsByUser : Endpoint<UnreadNotificationsByUserRequest, UnreadNotificationsByUserResponse>
{
    private readonly IMediator _mediator;

    public UnreadNotificationsByUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(UnreadNotificationsByUserRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get unread notifications for a user.";
            s.Description = "Retrieve all unread notifications for the specified user ID.";
            s.ExampleRequest = new UnreadNotificationsByUserRequest
            {
                UserId = 1
            };
        });
    }

    public override async Task HandleAsync(UnreadNotificationsByUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UnreadNotificationsByUserQuery(request.UserId), cancellationToken);
        
        await this.SendResponse(result, r => new UnreadNotificationsByUserResponse(result.Value));
    }
}
