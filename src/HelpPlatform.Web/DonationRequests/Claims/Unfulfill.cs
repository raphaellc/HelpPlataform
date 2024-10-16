using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.UnfulfillClaim;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Unfulfill(IMediator mediator) : Endpoint<UnfulfillDonationRequestClaimRequest, Result>
{
    public override void Configure()
    {
        Patch(UnfulfillDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new UnfulfillDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x.Accepts<AcceptDonationRequestClaimRequest>());
    }
    
    public override async Task HandleAsync(
        UnfulfillDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UnfulfillDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

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
