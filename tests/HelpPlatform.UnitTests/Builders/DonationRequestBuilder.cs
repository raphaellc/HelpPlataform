using System.Runtime.CompilerServices;
using HelpPlatform.Core.DonationRequestDomain;

namespace HelpPlatform.UnitTests.Builders;

public class DonationRequestBuilder
{
    private DonationRequest? _donationRequest;

    public string TestDescription { get; private set; } = "test description";

    public DateTime TestDeadline { get; private set; } = DateTime.Now.AddHours(8);

    public string TestLocation { get; private set; } = "test location";

    public int TestResourceTypeId { get; private set; } = 1;

    public int TestRequestedQuantity { get; private set; } = 10;

    public int TestUserId { get; private set; } = 1;

    public List<DonationRequestClaimBuilder> TestClaims { get; private set; } = [DonationRequestClaimBuilder.WithDefaultValues()];

    public static DonationRequestBuilder WithDefaultValues()
    {
        return new DonationRequestBuilder();
    }

    public static DonationRequestBuilder WithNoClaims()
    {
        return new DonationRequestBuilder { TestClaims = [] };
    }

    public DonationRequestBuilder SetTestDescription(string description)
    {
        this.TestDescription = description;
        return this;
    }
    
    public DonationRequestBuilder SetTestLocation(string location)
    {
        this.TestLocation = location;
        return this;
    }
    
    public DonationRequestBuilder SetTestResourceTypeId(int resourceTypeId)
    {
        this.TestResourceTypeId = resourceTypeId;
        return this;
    }
    
    public DonationRequestBuilder SetTestRequestedQuantity(int requestedQuantity)
    {
        this.TestRequestedQuantity = requestedQuantity;
        return this;
    }
    
    public DonationRequestBuilder SetTestUserId(int userId)
    {
        this.TestUserId = userId;
        return this;
    }

    public DonationRequest Build()
    {
        this._donationRequest = new DonationRequest(
        TestDescription,
        TestDeadline,
        TestLocation,
        TestResourceTypeId,
        TestRequestedQuantity,
        TestUserId
        );

        this.TestClaims.ForEach(claimBuilder => this._donationRequest.AddClaim(
            claimBuilder.TestMessage,
            claimBuilder.TestUserId,
            claimBuilder.TestRequestId,
            claimBuilder.TestQuantity,
            claimBuilder.TestDeadline
        ));

        return this._donationRequest;
    }
}
