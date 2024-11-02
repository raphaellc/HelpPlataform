using Ardalis.Result;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.Core.RequestDomain.Interfaces;
using HelpPlatform.Core.RequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.RequestDomain.Services;

public class RejectDonationRequestClaimService(IRepository<DonationRequest> repository) : IRejectDonationRequestClaimService
{
    public async Task<Result> RejectClaim(int requestId, int claimId, CancellationToken cancellationToken)
    {
        var donationRequest = await repository
            .FirstOrDefaultAsync(
            new DonationRequestWithClaimsSpecification(requestId), cancellationToken
            );

        if (donationRequest == null)
        {
            return Result.NotFound("Donation request not found");
        }

        var result = donationRequest.RejectClaim(claimId);

        if (!result.IsSuccess) return result;
        
        
        await repository.UpdateAsync(donationRequest, cancellationToken);
        
        // TODO - Notify donor
        // int donorId = donationRequest.GetClaimDonorId(claimId);
        // use mediatr to send command for creating notifications
        
        return Result.Success();
    }
}
