using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.FulfillClaim;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Fulfill(IMediator mediator) : Endpoint<FulfillDonationRequestClaimRequest>
{
    public override void Configure()
    {
        Patch(FulfillDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new FulfillDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x
            .Accepts<FulfillDonationRequestClaimRequest>()
            .Produces(204)
            .ClearDefaultProduces(200));
    }
    
    public override async Task HandleAsync(
        FulfillDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new FulfillDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

        await this.SendNoContent(result);
    }
}
