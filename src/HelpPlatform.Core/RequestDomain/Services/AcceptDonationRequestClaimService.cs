using Ardalis.Result;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.Core.RequestDomain.Interfaces;
using HelpPlatform.Core.RequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.RequestDomain.Services;

public class AcceptDonationRequestClaimService<TRequest>(IRepository<TRequest> repository) : IAcceptDonationRequestClaimService where TRequest : class, IRequest
{
    public async Task<Result> AcceptClaim(int requestId, int claimId, CancellationToken cancellationToken)
    {
        var donationRequest = await repository
            .FirstOrDefaultAsync(new DonationRequestTestSpecification<TRequest>(requestId), cancellationToken);

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
