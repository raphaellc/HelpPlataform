
using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.DonationRequestDomain.Services;

public class AcceptDonationRequestClaimService(IRepository<DonationRequest> repository) : IAcceptDonationRequestClaimService
{
    public async Task<Result> AcceptClaim(int requestId, int claimId, CancellationToken cancellationToken)
    {
        var donationRequest = await repository
            .FirstOrDefaultAsync(new DonationRequestWithClaimsSpecification(requestId), cancellationToken);

        if (donationRequest == null)
        {
            return Result.NotFound(DonationRequestErrors.DonationRequestNotFound);
        }

        var result = donationRequest.AcceptClaim(claimId);
        if (!result.IsSuccess) return result;
        
        await repository.UpdateAsync(donationRequest, cancellationToken);
        // TODO - Notify affected users
        // int donorId = donationRequest.GetClaimDonorId(claimId);
        // use mediatr to send commands for creating notifications

        return result;
    }
}
