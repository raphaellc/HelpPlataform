using Ardalis.Result;
using HelpPlatform.Core.RequestDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.CreateClaim;

public class CreateDonationRequestClaimHandler(ICreateDonationRequestClaimService drClaimService) : ICommandHandler<CreateDonationRequestClaimCommand, Result>
{
    public async Task<Result> Handle(CreateDonationRequestClaimCommand request, CancellationToken cancellationToken)
    {
        return await drClaimService.CreateClaim(request.message, request.userId, request.requestId, request.quantity, request.deadline, cancellationToken);
    }
}
