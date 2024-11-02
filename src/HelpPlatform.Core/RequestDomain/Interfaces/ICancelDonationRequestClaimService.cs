using Ardalis.Result;

namespace HelpPlatform.Core.RequestDomain.Interfaces;

public interface ICancelDonationRequestClaimService
{
    public Task<Result> CancelClaim(int requestId, int claimId, CancellationToken cancellationToken);
}
