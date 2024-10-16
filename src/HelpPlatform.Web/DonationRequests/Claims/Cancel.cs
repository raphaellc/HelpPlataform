using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.CancelClaim;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Cancel(IMediator mediator) : Endpoint<CancelDonationRequestClaimRequest, Result>
{
    public override void Configure()
    {
        Patch(CancelDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new CancelDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x.Accepts<AcceptDonationRequestClaimRequest>());
    }
    
    public override async Task HandleAsync(
        CancelDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CancelDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

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
