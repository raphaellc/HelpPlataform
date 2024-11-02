
using Ardalis.Result;
using HelpPlatform.Core.RequestDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.CancelClaim;

public class CancelDonationRequestClaimHandler(ICancelDonationRequestClaimService service) : ICommandHandler<CancelDonationRequestClaimCommand, Result>
{
    public async Task<Result> Handle(CancelDonationRequestClaimCommand request, CancellationToken cancellationToken)
    {
        return await service.CancelClaim(request.requestId, request.claimId, cancellationToken);
    }
}
