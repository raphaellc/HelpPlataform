namespace HelpPlatform.Web.DonationRequests.Claims;

public class ListUserClaimsRequest
{
    public const string Route = "Users/{UserId:int}/MyClaims";

    public static string BuildRoute(int userId) => Route.Replace("{UserId:int}", userId.ToString());
    
    public int UserId { get; set; }
}
