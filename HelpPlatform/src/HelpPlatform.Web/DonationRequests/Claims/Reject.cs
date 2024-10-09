using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.RejectClaim;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Reject(IMediator mediator) : Endpoint<RejectDonationRequestClaimRequest, Result>
{
    public override void Configure()
    {
        Patch(RejectDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new RejectDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
    }

    public override async Task HandleAsync(
        RejectDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RejectDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

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
