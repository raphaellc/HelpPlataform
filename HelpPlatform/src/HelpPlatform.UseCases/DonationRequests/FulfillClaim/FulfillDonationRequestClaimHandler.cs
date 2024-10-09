using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.FulfillClaim;

public class FulfillDonationRequestClaimHandler(IFulfillDonationRequestClaimService service) : ICommandHandler<FulfillDonationRequestClaimCommand, Result>
{
    public async Task<Result> Handle(FulfillDonationRequestClaimCommand request, CancellationToken cancellationToken)
    {
        return await service.FulfillClaim(request.requestId, request.claimId, cancellationToken);
    }
}
