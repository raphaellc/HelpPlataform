using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.DonationRequestDomain.Services;

public class CloseDonationRequestService(IRepository<DonationRequest> repository) : ICloseDonationRequestService
{
    public async Task<Result> CloseRequest(int requestId, CancellationToken cancellationToken)
    {
        var donationRequest = await repository.FirstOrDefaultAsync
        (
            new DonationRequestWithClaimsSpecification(requestId), cancellationToken
        );
        
        if (donationRequest == null) return Result.NotFound("Donation request not found");

        var result = donationRequest.Close();
        
        if (!result.IsSuccess) return result;
        
        await repository.UpdateAsync(donationRequest, cancellationToken);
        
        return Result.Success();
    }
}
