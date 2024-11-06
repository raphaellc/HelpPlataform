namespace HelpPlatform.Web.DonationRequests;

public class ListDonationRequestsRequest
{
    public const string Route = "/DonationRequests";

    public int? PageSize { get; set; }

    public int? PageIndex { get; set; }
}
