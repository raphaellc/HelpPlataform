namespace HelpPlatform.Web.DonationRequests.Claims;

public class ListDonationRequestClaimsRequest
{
    public const string Route = "/DonationRequests/{RequestId:int}/Claims";
    
    public static string BuildRoute(int requestId) => Route
        .Replace("{RequestId:int}", requestId.ToString());
    
    public int RequestId { get; set; }
}
