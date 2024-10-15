using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface ICancelDonationRequestClaimService
{
    public Task<Result> CancelClaim(int requestId, int claimId, CancellationToken cancellationToken);
}
