using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.CreateClaim;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Create(IMediator mediator) : Endpoint<CreateDonationRequestClaimRequest, Result>
{
    public override void Configure()
    {
        Post(CreateDonationRequestClaimRequest.Route);
        AllowAnonymous();
        Summary(s => {
            s.ExampleRequest = new CreateDonationRequestClaimRequest
            {
                Message = "Example donation request claim.",
                Deadline = DateTime.Now.AddHours(8),
                RequestId = 1,
                UserId = 1
            };
        });
    }

    public override async Task HandleAsync(
        CreateDonationRequestClaimRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateDonationRequestClaimCommand(
            message: request.Message,
            userId: request.UserId,
            requestId: request.RequestId,
            quantity: request.Quantity,
            deadline: request.Deadline),
            cancellationToken
        );

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
