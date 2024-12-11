using FastEndpoints;
using HelpPlatform.UseCases.Notifications.Create;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.Notifications;

public class Create : Endpoint<CreateNotificationRequest, CreateNotificationResponse>
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(CreateNotificationRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Create a new Notification.";
            s.Description = "Create a new Notification. A valid user ID and message are required.";
            s.ExampleRequest = new CreateNotificationRequest
            {
                UserId = 1,
                Message = "This is a notification message!"
            };
        });
    }

    public override async Task HandleAsync(CreateNotificationRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateNotificationCommand(
            UserId: request.UserId!.Value,
            Message: request.Message!), cancellationToken);

        await this.SendResponse(result, r => new CreateNotificationResponse(result.Value));
    }
}
