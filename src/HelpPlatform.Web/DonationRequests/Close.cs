using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.Close;
using MediatR;

namespace HelpPlatform.Web.DonationRequests;

public class Close(IMediator mediator) : Endpoint<CloseDonationRequestRequest, Result>
{
    public override void Configure()
    {
        Patch(CloseDonationRequestRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CloseDonationRequestRequest { RequestId = 1 };
        });
    }
    
    public override async Task HandleAsync(
        CloseDonationRequestRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CloseDonationRequestCommand(request.RequestId, cancellationToken));

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
