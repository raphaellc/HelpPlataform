using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.Close;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests;

public class Close(IMediator mediator) : Endpoint<CloseDonationRequestRequest>
{
    public override void Configure()
    {
        Patch(CloseDonationRequestRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CloseDonationRequestRequest { RequestId = 1 };
        });
        Description(x => x.Accepts<CloseDonationRequestRequest>()
        .Produces(204)
        .ClearDefaultProduces(200));
    }
    
    public override async Task HandleAsync(
        CloseDonationRequestRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CloseDonationRequestCommand(request.RequestId, cancellationToken));

        await this.SendNoContent(result);
    }
}
