using Ardalis.Result;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.Core.RequestDomain.Interfaces;
using HelpPlatform.Core.RequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.RequestDomain.Services;

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

        await repository.UpdateAsync(donationRequest, cancellationToken);

        return Result.Success();
    }
}
