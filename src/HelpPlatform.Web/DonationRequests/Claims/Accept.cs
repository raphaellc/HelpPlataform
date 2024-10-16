using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.AcceptClaim;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Accept(IMediator mediator) : Endpoint<AcceptDonationRequestClaimRequest, Result>
{
    public override void Configure()
    {
        Patch(AcceptDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            s.ExampleRequest = new AcceptDonationRequestClaimRequest { RequestId = 1, ClaimId = 1 };
        });
        Description(x => x.Accepts<AcceptDonationRequestClaimRequest>());
    }

    public override async Task HandleAsync(
        AcceptDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AcceptDonationRequestClaimCommand(request.RequestId, request.ClaimId), cancellationToken);

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
