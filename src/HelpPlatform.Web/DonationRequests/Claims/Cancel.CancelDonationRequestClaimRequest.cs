namespace HelpPlatform.Web.DonationRequests.Claims;

public class CancelDonationRequestClaimRequest
{
    public const string Route = "DonationRequests/{RequestId:int}/Claims/{ClaimId:int}/Cancel";
    
    public static string BuildRoute(int requestId, int claimId) => Route
        .Replace("{RequestId:int}", requestId.ToString())
        .Replace("{ClaimId:int}", claimId.ToString());
    
    public int RequestId { get; set; }
    
    public int ClaimId { get; set; }
}
