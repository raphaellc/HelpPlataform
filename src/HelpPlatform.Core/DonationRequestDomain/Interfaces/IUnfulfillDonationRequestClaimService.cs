using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface IUnfulfillDonationRequestClaimService
{
    public Task<Result> UnfulfillClaim(int requestId, int claimId, CancellationToken cancellationToken);
}
