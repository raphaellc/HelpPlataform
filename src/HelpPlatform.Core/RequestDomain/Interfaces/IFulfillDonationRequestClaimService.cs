using Ardalis.Result;

namespace HelpPlatform.Core.RequestDomain.Interfaces;

public interface IFulfillDonationRequestClaimService
{
    public Task<Result> FulfillClaim(int requestId, int claimId, CancellationToken cancellationToken);
}
