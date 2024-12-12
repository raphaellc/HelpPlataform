using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.Notifications.MarkAsRead;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.Notifications;

public class Update(IMediator mediator) : Endpoint<UpdateNotificationAsReadRequest>
{
    public override void Configure()
    {
        Patch(UpdateNotificationAsReadRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new UpdateNotificationAsReadRequest { NotificationId = 1, UserId = 1 };
        });
        Description(x => x
        .Produces(204)
        .ClearDefaultProduces(200));
    }

    public override async Task HandleAsync(
        UpdateNotificationAsReadRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new MarkNotificationAsReadCommand(request.NotificationId, request.UserId), cancellationToken);

        if (result.Status == ResultStatus.NotFound){
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        await this.SendNoContent(result);
    }
}