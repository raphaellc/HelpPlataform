using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;

namespace HelpPlatform.UseCases.DonationRequests.UnfulfillClaim;

public class UnfulfillDonationRequestClaimHandler(IUnfulfillDonationRequestClaimService service)
{
    public async Task<Result> Handle(UnfulfillDonationRequestClaimCommand request, CancellationToken cancellationToken)
    {
        return await service.UnfulfillClaim(request.requestId, request.claimId, cancellationToken);
    }
}
