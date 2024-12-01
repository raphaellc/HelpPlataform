using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.ListClaim;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class List(IMediator mediator) : Endpoint<ListDonationRequestClaimsRequest, ListDonationRequestClaimsResponse>
{
    public override void Configure()
    {
        Get(ListDonationRequestClaimsRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new ListDonationRequestClaimsRequest { RequestId = 1 };
        });
    }

    public override async Task HandleAsync(
        ListDonationRequestClaimsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListDonationRequestClaimQuery(request.RequestId), cancellationToken);
        
        await this.SendResponse(result, r => new ListDonationRequestClaimsResponse
        {
            Claims = r.Value.Select(claim => new DonationRequestClaimRecord(
                claim.Id,
                claim.Message,
                claim.Quantity,
                claim.CreatedAt,
                claim.Deadline,
                claim.Status,
                claim.UserId,
                claim.RequestId)).ToList()
        });
    }
}
