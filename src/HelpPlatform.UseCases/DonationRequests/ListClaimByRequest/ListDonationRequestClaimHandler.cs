using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.ListClaimByRequest;

public class ListDonationRequestClaimHandler(IListClaimsByDonationRequestService service) :
    IQueryHandler<ListDonationRequestClaimQuery, Result<IEnumerable<DonationRequestClaimDto>>>
{
    public async Task<Result<IEnumerable<DonationRequestClaimDto>>> Handle(
        ListDonationRequestClaimQuery request,
        CancellationToken cancellationToken)
    {
        var result = await service.ListAsync(request.RequestId, request.UserId);

        return Result.Success(result);
    }
}
