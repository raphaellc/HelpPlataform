using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.FulfillClaim;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Fulfill(IMediator mediator) : Endpoint<FulfillDonationRequestClaimRequest, Result>
{
    public override void Configure()
    {
        Patch(FulfillDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new FulfillDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
    }
    
    public override async Task HandleAsync(
        FulfillDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new FulfillDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

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
