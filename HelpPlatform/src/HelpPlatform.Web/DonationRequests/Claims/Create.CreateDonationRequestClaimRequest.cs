using Microsoft.Build.Framework;

namespace HelpPlatform.Web.DonationRequests.Claims;

public class CreateDonationRequestClaimRequest
{
    public const string Route = "/DonationRequests/{RequestId:int}/Claims";
    
    public static string BuildRoute(int requestId) => Route.Replace("{RequestId:int}", requestId.ToString());

    public string? Message { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int RequestId { get; set; }
    
    public int Quantity { get; set; }
    
    public DateTime? Deadline { get; set; }
}
