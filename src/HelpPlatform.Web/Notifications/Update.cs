using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.Notifications.MarkAsRead;
using MediatR;

namespace HelpPlatform.Web.Notifications;

public class Update(IMediator mediator) : Endpoint<UpdateNotificationAsReadRequest, Result>
{
    public override void Configure()
    {
        Patch(UpdateNotificationAsReadRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new UpdateNotificationAsReadRequest { NotificationId = 1, UserId = 1 };
        });
    }

    public override async Task HandleAsync(
        UpdateNotificationAsReadRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new MarkNotificationAsReadCommand(request.NotificationId, request.UserId, cancellationToken));

        if (result.Status == ResultStatus.NotFound){
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = Result.NoContent();
        }
        else
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