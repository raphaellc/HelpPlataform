using Ardalis.Result;
using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.Core.RequestDomain.Specifications;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.RejectClaim;

public class RejectDonationRequestClaimHandler(IRepository<DonationRequest> repository)
    : ICommandHandler<RejectDonationRequestClaimCommand, Result>
{
    public async Task<Result> Handle(
        RejectDonationRequestClaimCommand request,
        CancellationToken cancellationToken)
    {
        var donationRequest = await repository
            .FirstOrDefaultAsync(
                new DonationRequestWithClaimsSpecification(request.RequestId), cancellationToken
            );

        if (donationRequest == null)
        {
            return Result.NotFound("Donation request not found");
        }

        var result = donationRequest.RejectClaim(request.ClaimId);

        if (result.IsSuccess)
        {
            await repository.UpdateAsync(donationRequest, cancellationToken);
            // Notify donor
        }

        return result;
    }
}
