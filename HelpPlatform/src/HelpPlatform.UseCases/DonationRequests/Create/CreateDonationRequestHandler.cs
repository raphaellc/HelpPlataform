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
        Guard.Against.Null(user, nameof(user), "User not found.");

        var donationRequest = new DonationRequest(
            request.Description,
            request.Deadline,
            request.Location,
            request.ResourceType,
            request.RequestedQuantity,
            user.Id
        );

        var createdDr = await drRepository.AddAsync(donationRequest, cancellationToken);

        return createdDr.Id;
    }
}
