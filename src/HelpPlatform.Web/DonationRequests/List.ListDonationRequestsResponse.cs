namespace HelpPlatform.Web.DonationRequests;

public class ListDonationRequestResponse
{
    public List<DonationRequestRecord> DonationRequests { get; set; } = [];
    public int PageCount { get; set; }
}
