using Ardalis.Result;
using FastEndpoints;
using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.SharedKernel;
using HelpPlatform.UseCases.DonationRequests;
using HelpPlatform.UseCases.DonationRequests.List;
using MediatR;

namespace HelpPlatform.Web.DonationRequests;

public class List(IMediator mediator, IRepository<DonationRequest> repository) : Endpoint<ListDonationRequestsRequest, ListDonationRequestResponse> {
    public override void Configure() {
        Get("/DonationRequests");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        ListDonationRequestsRequest request,
        CancellationToken cancellationToken)
    {
        Result<IEnumerable<DonationRequestDto>> result =
            await mediator.Send(new ListDonationRequestsQuery(request.PageSize, request.PageIndex), cancellationToken);

        int itemCount = await repository.CountAsync(cancellationToken);
        int pageCount;
        if (request.PageSize is > 0)
        {
            pageCount = (int)decimal.Ceiling(itemCount / (decimal)request.PageSize!);
        } else
        {
            pageCount = itemCount > 0 ? 1 : 0;
        }

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
                createdAt: dto.CreatedAt)).ToList(),
                PageCount = pageCount
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
