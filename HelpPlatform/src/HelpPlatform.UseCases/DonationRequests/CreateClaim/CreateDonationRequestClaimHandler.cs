using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.CreateClaim;

public class CreateDonationRequestClaimHandler(ICreateDonationRequestClaimService drClaimService) : ICommandHandler<CreateDonationRequestClaimCommand, Result>
{
    public async Task<Result> Handle(CreateDonationRequestClaimCommand request, CancellationToken cancellationToken)
    {
        var result = await drClaimService.CreateClaim(request.message, request.userId, request.requestId, request.deadline, cancellationToken); 
        Console.WriteLine(string.Join(";", result.ValidationErrors.Select(valErr => valErr.ErrorMessage)));
        return result;
    }
}
