using HelpPlatform.Core.DonationRequestDomain;
using HelpPlatform.Core.DonationRequestDomain.Services;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;
using HelpPlatform.UnitTests.Builders;
using NSubstitute;
using Xunit;

namespace HelpPlatform.UnitTests.Core.DonationRequests.ServiceTests;

public class CreateDonationRequestClaim
{
    private readonly IRepository<DonationRequest> _mockDrRepository = Substitute.For<IRepository<DonationRequest>>();
    private readonly IRepository<User> _mockUserRepository = Substitute.For<IRepository<User>>();

    [Fact]
    public async Task InvokesDonationRequestRepositoryUpdateAsyncOnce()
    {
        var donationRequest = DonationRequestBuilder.WithNoClaims().Build();
        var user = UserBuilder.WithDefaultValues().Build();
        _mockDrRepository.FirstOrDefaultAsync(Arg.Any<DonationRequestWithClaimsSpecification>()).Returns(donationRequest);
        _mockUserRepository.GetByIdAsync(Arg.Any<int>()).Returns(user);

        var createClaimService = new CreateDonationRequestClaimService(_mockDrRepository, _mockUserRepository);

        await createClaimService.CreateClaim(default, 1, 1, 1, default, default);

        await _mockDrRepository.Received().UpdateAsync(donationRequest);
    }
}
