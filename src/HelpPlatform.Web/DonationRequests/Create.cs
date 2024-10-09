using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests.Create;
using MediatR;

namespace HelpPlatform.Web.DonationRequests;

public class Create(IMediator _mediator) : Endpoint<CreateDonationRequestRequest, CreateDonationRequestResponse>
{
    public override void Configure() {
        Post(CreateDonationRequestRequest.Route);
        AllowAnonymous();
        Summary(s => {
            // XML Docs are used by default but are overridden by these properties:
            //s.Summary = "Create a new Contributor.";
            //s.Description = "Create a new Contributor. A valid name is required.";
            s.ExampleRequest = new CreateDonationRequestRequest
            {
                Description = "Donation Request Example",
                RequestedQuantity = 10,
                Deadline = DateTime.Now.AddHours(5),
                Location = "Example Location",
                ResourceType = "Example Resource",
                UserId = 0
            };
        });
    }
    
    public override async Task HandleAsync(
        CreateDonationRequestRequest request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(new CreateDonationRequestCommand(
        Description: request.Description,
        Deadline: request.Deadline!.Value,
        Location: request.Location!,
        ResourceType: request.ResourceType!,
        RequestedQuantity: request.RequestedQuantity!.Value,
        UserId: request.UserId!.Value),
        cancellationToken);

        if (result.IsSuccess){
            Response = new CreateDonationRequestResponse(result.Value);
            return;
        }
        
        foreach (var resultError in result.Errors)
        {
            AddError(resultError);
        }

        ThrowIfAnyErrors();
    }
}
