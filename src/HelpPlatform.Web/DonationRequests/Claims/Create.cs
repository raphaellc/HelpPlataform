using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.CreateClaim;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class Create(IMediator mediator) : Endpoint<CreateDonationRequestClaimRequest>
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
        Description(x => x
            .Accepts<CreateDonationRequestClaimRequest>()
            .Produces(204)
            .ClearDefaultProduces(200));
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

        await this.SendNoContent(result);
    }
}
