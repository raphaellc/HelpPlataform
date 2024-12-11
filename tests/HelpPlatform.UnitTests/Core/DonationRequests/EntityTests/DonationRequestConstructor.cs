using HelpPlatform.Core.DonationRequestDomain;
using Xunit;

namespace HelpPlatform.UnitTests.Core.DonationRequests.EntityTests;

public class DonationRequestConstructor
{
    private readonly string _description = "description test";
    private readonly DateTime _deadline = DateTime.Now;
    private readonly string _location = "location test";
    private readonly int _resourceTypeId = 99;
    private readonly int _requestedQuantity = 99;
    private readonly int _userId = 99;
    
    private DonationRequest? _donationRequest;

    [Fact]
    public void InitializesDonationRequest()
    {
        _donationRequest = new DonationRequest(
            _description,
            _deadline,
            _location,
            _resourceTypeId,
            _requestedQuantity,
            _userId);

        Assert.Equal(_description, _donationRequest.Description);
        Assert.Equal(_deadline, _donationRequest.Deadline);
        Assert.Equal(_location, _donationRequest.Location);
        Assert.Equal(_resourceTypeId, _donationRequest.ResourceTypeId);
        Assert.Equal(_userId, _donationRequest.UserId);
        Assert.Equal(_requestedQuantity, _donationRequest.RequestedQuantity);
        Assert.Equal(_requestedQuantity, _donationRequest.RemainingQuantity);
        Assert.Equal(default, _donationRequest.FulfilledQuantity);
        Assert.Equal(DonationRequestStatusEnum.Open, _donationRequest.Status);
        Assert.Empty(_donationRequest.Claims);
        Assert.True(_donationRequest.IsEditable);
    }

    [Fact]
    public void CannotInitializeWithNullOrNegativeQuantity()
    {
        Assert.Throws<ArgumentException>(() => new DonationRequest(
        _description,
        _deadline,
        _location,
        _resourceTypeId,
        0,
        _userId));
        
        Assert.Throws<ArgumentException>(() => new DonationRequest(
        _description,
        _deadline,
        _location,
        _resourceTypeId,
        -99,
        _userId));
    }
    
    // TODO - implement tests for other validations
}
