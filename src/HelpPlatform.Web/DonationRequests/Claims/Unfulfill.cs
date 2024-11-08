using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.UnfulfillClaim;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Unfulfill(IMediator mediator) : Endpoint<UnfulfillDonationRequestClaimRequest>
{
    public override void Configure()
    {
        Patch(UnfulfillDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new UnfulfillDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x
        .Accepts<AcceptDonationRequestClaimRequest>()
        .Produces(204)
        .ClearDefaultProduces(200));
    }
    
    public override async Task HandleAsync(
        UnfulfillDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UnfulfillDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

        await this.SendNoContent(result);
        
    }
}
