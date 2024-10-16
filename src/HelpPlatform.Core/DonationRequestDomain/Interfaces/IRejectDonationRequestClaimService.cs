using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface IRejectDonationRequestClaimService
{
    public Task<Result> RejectClaim(int requestId, int claimId, CancellationToken cancellationToken);
}
