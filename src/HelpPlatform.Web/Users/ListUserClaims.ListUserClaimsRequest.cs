namespace HelpPlatform.Web.Users;

public class ListUserClaimsRequest
{
    public const string Route = "/Users/{UserId:int}/Claims";
    
    public static string BuildRoute(int requestId) => Route
        .Replace("{UserId:int}", requestId.ToString());
    
    public int? UserId { get; set; }
}
