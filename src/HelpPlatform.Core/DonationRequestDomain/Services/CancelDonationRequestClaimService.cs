using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.DonationRequestDomain.Services;

public class CancelDonationRequestClaimService(IRepository<DonationRequest> repository) : ICancelDonationRequestClaimService
{
    public async Task<Result> CancelClaim(int requestId, int claimId, CancellationToken cancellationToken)
    {
        var donationRequest = await repository.FirstOrDefaultAsync
        (
            new DonationRequestWithClaimsSpecification(requestId), cancellationToken
        );

        if (donationRequest == null)
        {
            return Result.NotFound("Donation request not found");
        }

        var result = donationRequest.CancelClaim(claimId);
        if (!result.IsSuccess) return result;
        
        // TODO - Notify request owner

        return Result.Success();
    }
}
