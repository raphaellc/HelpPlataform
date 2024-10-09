namespace HelpPlatform.Web.DonationRequests;

public class CreateDonationRequestRequest
{
    public const string Route = "/DonationRequests";
    
    public String? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public String? Location { get; set; }
    public String? ResourceType { get; set; }

    public int? RequestedQuantity { get; set; }

    public int? UserId { get; set; }
}
