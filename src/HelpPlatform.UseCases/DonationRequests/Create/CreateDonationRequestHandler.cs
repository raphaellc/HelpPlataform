using Ardalis.GuardClauses;
using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.Create;

public class CreateDonationRequestHandler(IRepository<DonationRequest> drRepository, IRepository<User> userRepository) : ICommandHandler<CreateDonationRequestCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateDonationRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.NotFound(UserErrors.UserNotFound);
        }
        
        var donationRequest = new DonationRequest(
            request.Description,
            request.Deadline,
            request.Location,
            request.ResourceTypeId,
            request.RequestedQuantity,
            user.Id
        );

        var createdDr = await drRepository.AddAsync(donationRequest, cancellationToken);

        return createdDr.Id;
    }
}
