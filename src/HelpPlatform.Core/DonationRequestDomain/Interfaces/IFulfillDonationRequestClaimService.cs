using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface IFulfillDonationRequestClaimService
{
    public Task<Result> FulfillClaim(int requestId, int claimId, CancellationToken cancellationToken);
}
