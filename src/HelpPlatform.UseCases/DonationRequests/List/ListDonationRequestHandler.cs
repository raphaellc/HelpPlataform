using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.List;

public class ListDonationRequestHandler(IRepository<DonationRequest> repository) : IQueryHandler<ListDonationRequestsQuery, Result<IEnumerable<DonationRequestDto>>>
{
    public async Task<Result<IEnumerable<DonationRequestDto>>> Handle(ListDonationRequestsQuery request, CancellationToken cancellationToken)
    {
        var donationRequests = await repository.ListAsync(new DonationRequestWithUserSpecification(), cancellationToken);

        var donationRequestDtos = donationRequests.Select(dr => new DonationRequestDto(
            id: dr.Id,
            description: dr.Description,
            deadline: dr.Deadline,
            location: dr.Location,
            resourceTypeId: dr.ResourceTypeId,
            requestedQuantity: dr.RequestedQuantity,
            fulfilledQuantity: dr.FulfilledQuantity,
            status: dr.Status.ToString(),
            userName: dr.User?.Name,
            createdAt: dr.CreatedAt
        )).ToList();

        return donationRequestDtos;
    }
}
