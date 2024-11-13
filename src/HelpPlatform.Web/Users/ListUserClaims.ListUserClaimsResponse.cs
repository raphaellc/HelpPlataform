using HelpPlatform.Web.DonationRequests.Claims;

namespace HelpPlatform.Web.Users;

public class ListUserClaimsResponse
{
    public List<DonationRequestClaimRecord> Claims { get; set; } = [];
}
