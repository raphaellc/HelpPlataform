using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.AcceptClaim;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Accept(IMediator mediator) : Endpoint<AcceptDonationRequestClaimRequest>
{
    public override void Configure()
    {
        Patch(AcceptDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new AcceptDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x
            .Accepts<AcceptDonationRequestClaimRequest>()
            .Produces(204)
            .ClearDefaultProduces(200));
    }

    public override async Task HandleAsync(
        AcceptDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AcceptDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

        await this.SendNoContent(result);
    }
}
