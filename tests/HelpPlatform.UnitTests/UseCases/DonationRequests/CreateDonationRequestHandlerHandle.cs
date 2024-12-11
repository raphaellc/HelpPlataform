using System.Runtime.CompilerServices;
using Ardalis.Result;
using FluentAssertions;
using HelpPlatform.Core.DonationRequestDomain;
using HelpPlatform.Core.DonationRequestDomain.Services;
using HelpPlatform.Core.DonationRequestDomain.Specifications;
using HelpPlatform.Core.UserDomain;
using HelpPlatform.SharedKernel;
using HelpPlatform.UnitTests.Builders;
using HelpPlatform.UseCases.DonationRequests.Create;
using NSubstitute;
using Xunit;

namespace HelpPlatform.UnitTests.UseCases.DonationRequests;

public class CreateDonationRequestHandlerHandle
{
    private readonly IRepository<DonationRequest> _mockDrRepository = Substitute.For<IRepository<DonationRequest>>();
    private readonly IRepository<User> _mockUserRepository = Substitute.For<IRepository<User>>();

    private CreateDonationRequestHandler _handler;

    public CreateDonationRequestHandlerHandle()
    {
        _handler = new CreateDonationRequestHandler(_mockDrRepository, _mockUserRepository);
    }
    
    [Fact]
    public async Task ReturnsSuccessGivenValidParameters()
    {
        var user = UserBuilder.WithDefaultValues().Build();
        var drBuilder = DonationRequestBuilder.WithDefaultValues();
        _mockUserRepository.GetByIdAsync(Arg.Any<int>()).Returns(user);

        _mockDrRepository.AddAsync(Arg.Any<DonationRequest>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(drBuilder.Build()));

        var result = await _handler.Handle(new CreateDonationRequestCommand(
            drBuilder.TestDescription,
            drBuilder.TestDeadline,
            drBuilder.TestLocation,
            drBuilder.TestResourceTypeId,
            drBuilder.TestRequestedQuantity,
            drBuilder.TestUserId), 
            default
        );

        result.IsSuccess.Should().BeTrue();
    }
}
