using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.UseCases.DonationRequests;
using HelpPlatform.UseCases.DonationRequests.List;
using MediatR;

namespace HelpPlatform.Web.DonationRequests;

public class List(IMediator _mediator) : EndpointWithoutRequest<ListDonationRequestResponse> {
    public override void Configure() {
        Get("/DonationRequests");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken) {
        Result<IEnumerable<DonationRequestDto>> result = await _mediator.Send(new ListDonationRequestsQuery(null, null), cancellationToken);

        if (result.IsSuccess){
            Response = new ListDonationRequestResponse
            {
                DonationRequests = result.Value.Select(dto => new DonationRequestRecord(
                id: dto.Id,
                description: dto.Description,
                deadline: dto.Deadline,
                location: dto.Location,
                resourceTypeId: dto.ResourceTypeId,
                requestedQuantity: dto.RequestedQuantity,
                fulfilledQuantity: dto.FulfilledQuantity,
                status: dto.Status,
                userName: dto.UserName,
                createdAt: dto.CreatedAt)).ToList()
            };
            return;
        }
        
        foreach (var resultError in result.Errors)
        {
            AddError(resultError);
        }

        ThrowIfAnyErrors();
    }
}
