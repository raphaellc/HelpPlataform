using HelpPlatform.Core.DonationRequestDomain;
using HelpPlatform.UnitTests.Builders;
using Xunit;

namespace HelpPlatform.UnitTests.Core.DonationRequests.EntityTests;

public class DonationRequestAddClaim
{
    [Fact]
    public void AddsDonationRequestClaim()
    {
        var drBuilder = DonationRequestBuilder.WithNoClaims();
        var donationRequest = drBuilder.Build();
        var claimBuilder = DonationRequestClaimBuilder.WithDefaultValues();
        donationRequest.AddClaim(
            claimBuilder.TestMessage,
            claimBuilder.TestUserId,
            claimBuilder.TestRequestId,
            claimBuilder.TestQuantity,
            claimBuilder.TestDeadline
        );

        var firstClaim = donationRequest.Claims.Single();
        Assert.Equal(claimBuilder.TestMessage, firstClaim.Message);
        Assert.Equal(claimBuilder.TestUserId, firstClaim.UserId);
        Assert.Equal(claimBuilder.TestRequestId, firstClaim.RequestId);
        Assert.Equal(claimBuilder.TestMessage, firstClaim.Message);
        Assert.Equal(claimBuilder.TestQuantity, firstClaim.Quantity);
        Assert.Equal(claimBuilder.TestDeadline, firstClaim.Deadline);
        Assert.Equal(DonationRequestClaimStatusEnum.Waiting, firstClaim.Status);
        Assert.Null(firstClaim.AcceptedAt);
    }
}
