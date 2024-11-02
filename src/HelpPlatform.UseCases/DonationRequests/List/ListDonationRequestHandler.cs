using Ardalis.Result;
using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.Core.RequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.List;

public class ListDonationRequestHandler(IRepository<DonationRequest> repository) : IQueryHandler<ListDonationRequestsQuery, Result<IEnumerable<DonationRequestDto>>>
{
    public async Task<Result<IEnumerable<DonationRequestDto>>> Handle(ListDonationRequestsQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.Size ?? 0) * (request.Index ?? 0);
        int take = request.Size is null or 0 ? int.MaxValue : request.Size.Value;
        
        var donationRequests = await repository.ListAsync
        (
            new PaginatedDonationRequestsWithUserSpecification(skip, take), cancellationToken
        );

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
