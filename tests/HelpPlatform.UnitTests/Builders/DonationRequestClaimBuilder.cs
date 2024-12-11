using HelpPlatform.Core.DonationRequestDomain;

namespace HelpPlatform.UnitTests.Builders;

public class DonationRequestClaimBuilder
{
    private DonationRequestClaim? _drClaim;
    public string TestMessage { get; private set; } = "test message";
    public int TestUserId { get; private set; } = 1;
    public int TestRequestId { get; private set; } = 1;
    public int TestQuantity { get; private set; } = 5;
    public DateTime TestDeadline { get; private set; } = DateTime.Now.AddHours(4);
    
    public DonationRequestClaimBuilder SetTestMessage(string message)
    {
        this.TestMessage = message;
        return this;
    }
    
    public DonationRequestClaimBuilder SetTestUserId(int userId)
    {
        this.TestUserId = userId;
        return this;
    }
    
    public DonationRequestClaimBuilder SetTestRequestId(int requestId)
    {
        this.TestRequestId = requestId;
        return this;
    }
    
    public DonationRequestClaimBuilder SetTestQuantity(int quantity)
    {
        this.TestQuantity = quantity;
        return this;
    }
    
    public DonationRequestClaimBuilder SetTestDeadline(DateTime deadline)
    {
        this.TestDeadline = deadline;
        return this;
    }
    
    public static DonationRequestClaimBuilder WithDefaultValues()
    {
        return new DonationRequestClaimBuilder();
    }

    public DonationRequestClaim Build()
    {
        this._drClaim = new DonationRequestClaim(
            TestMessage,
            TestUserId,
            TestRequestId,
            TestQuantity,
            TestDeadline
        );
        
        return this._drClaim;
    }
}
