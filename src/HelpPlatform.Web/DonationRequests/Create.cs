using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.Create;
using HelpPlatform.Web.Extensions;
using MediatR;

namespace HelpPlatform.Web.DonationRequests;

public class Create(IMediator _mediator) : Endpoint<CreateDonationRequestRequest, CreateDonationRequestResponse>
{
    public override void Configure() {
        Post(CreateDonationRequestRequest.Route);
        AllowAnonymous();
        Summary(s => {
            s.ExampleRequest = new CreateDonationRequestRequest
            {
                Description = "Donation Request Example",
                RequestedQuantity = 10,
                Deadline = DateTime.Now.AddHours(5),
                Location = "Example Location",
                ResourceTypeId = 1,
                UserId = 1
            };
        });
    }
    
    public override async Task HandleAsync(CreateDonationRequestRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateDonationRequestCommand(
        Description: request.Description,
        Deadline: request.Deadline!.Value,
        Location: request.Location!,
        ResourceTypeId: request.ResourceTypeId!.Value,
        RequestedQuantity: request.RequestedQuantity!.Value,
        UserId: request.UserId!.Value),
        cancellationToken);

        await this.SendResponse(result, r => new CreateDonationRequestResponse(result.Value));
    }
}
