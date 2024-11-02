using Ardalis.Result;
using HelpPlatform.Core.RequestDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.AcceptClaim;

public class AcceptDonationRequestClaimHandler(IAcceptDonationRequestClaimService acceptClaimService) : ICommandHandler<AcceptDonationRequestClaimCommand, Result>
{
    public async Task<Result> Handle(AcceptDonationRequestClaimCommand request, CancellationToken cancellationToken)
    {
        return await acceptClaimService.AcceptClaim(request.RequestId, request.ClaimId, cancellationToken);
    }
}
