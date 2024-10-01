using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.DonationRequestDomain.Services;

public class CreateDonationRequestClaimService(
    IRepository<DonationRequest> repository,
    IRepository<User> userRepository
    ) : ICreateDonationRequestClaimService
{
    public async Task<Result> CreateClaim(string? message, int userId, int requestId, DateTime? deadline, CancellationToken cancellationToken)
    {
        var donationRequest = await repository.FirstOrDefaultAsync(new DonationRequestWithClaimsSpecification(requestId), cancellationToken);
        if (donationRequest == null)
        {
            return Result.NotFound("Request not found");
        }
        if (deadline != null && deadline > donationRequest.Deadline)
        {
            return Result.Invalid(
                new ValidationError { ErrorMessage = "Claim deadline must not be greater than request deadline" }
            );
        }

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            return Result.NotFound("User not found");
        }
        
        var result = donationRequest.AddClaim(message: message, userId: userId, requestId: requestId, deadline: deadline);
        if (result.IsSuccess)
        {
            await repository.UpdateAsync(donationRequest, cancellationToken);
        }
        
        return result;
    }
}
