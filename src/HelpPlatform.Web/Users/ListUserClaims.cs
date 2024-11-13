using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.ListClaimByRequest;
using HelpPlatform.Web.DonationRequests.Claims;
using MediatR;

namespace HelpPlatform.Web.Users;

public class ListUserClaims(IMediator mediator) : Endpoint<ListUserClaimsRequest, ListUserClaimsResponse>
{
    public override void Configure()
    {
        Get(ListUserClaimsRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new ListUserClaimsRequest { UserId = 1 };
        });
    }
    
    public override async Task HandleAsync(
        ListUserClaimsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListDonationRequestClaimQuery(RequestId: null, UserId: request.UserId), cancellationToken);
        if (result.IsSuccess)
        {
            Response = new ListUserClaimsResponse
            {
                Claims = result.Value.Select(claim => new DonationRequestClaimRecord
                (
                claim.Id,
                claim.Message,
                claim.Quantity, 
                claim.CreatedAt,
                claim.Deadline,
                claim.Status,
                claim.UserId,
                claim.RequestId
                )).ToList()
            };
        }
    }
}
