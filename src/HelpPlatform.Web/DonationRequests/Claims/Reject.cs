using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.RejectClaim;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Reject(IMediator mediator) : Endpoint<RejectDonationRequestClaimRequest>
{
    public override void Configure()
    {
        Patch(RejectDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new RejectDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x.Accepts<AcceptDonationRequestClaimRequest>());
    }

    public override async Task HandleAsync(
        RejectDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RejectDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

        await this.SendNoContent(result);
    }
}
