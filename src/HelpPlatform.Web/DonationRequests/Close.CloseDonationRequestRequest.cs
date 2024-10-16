namespace HelpPlatform.Web.DonationRequests;

public class CloseDonationRequestRequest
{
    public const string Route = "/DonationRequests/{RequestId:int}/Close";
    
    public static string BuildRoute(int requestId) => Route
        .Replace("{RequestId:int}", requestId.ToString());
    
    public int RequestId { get; set; }
}
