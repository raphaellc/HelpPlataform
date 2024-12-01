using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.CancelClaim;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Cancel(IMediator mediator) : Endpoint<CancelDonationRequestClaimRequest>
{
    public override void Configure()
    {
        Patch(CancelDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CancelDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x
            .Accepts<CancelDonationRequestClaimRequest>()
            .Produces(204)
            .ClearDefaultProduces(200));
    }
    
    public override async Task HandleAsync(
        CancelDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CancelDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

        await this.SendNoContent(result);
    }
}
