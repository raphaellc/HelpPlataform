
using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.DonationRequestDomain.Services;

public class AcceptDonationRequestClaimService(IRepository<DonationRequest> repository) : IAcceptDonationRequestClaimService
{
    public async Task<Result> AcceptClaim(int requestId, int claimId, CancellationToken cancellationToken)
    {
        Console.WriteLine("CHEGOU AQUI");
        
        var donationRequest = await repository
            .FirstOrDefaultAsync(new DonationRequestWithClaimsSpecification(requestId), cancellationToken);

        if (donationRequest == null)
        {
            return Result.NotFound("Donation request not found");
        }
        
        Console.WriteLine("CHEGOU AQUI 2");

        var result = donationRequest.AcceptClaim(claimId);
        if (!result.IsSuccess) return result;
        
        Console.WriteLine("CHEGOU AQUI 3");
        
        await repository.UpdateAsync(donationRequest, cancellationToken);
        // TODO - Notify affected users
        // int donorId = donationRequest.GetClaimDonorId(claimId);
        // use mediatr to send commands for creating notifications
        
        Console.WriteLine("CHEGOU AQUI 4");

        return result;
    }
}
