using HelpPlatform.UseCases.DonationRequests;
using HelpPlatform.UseCases.DonationRequests.ListClaim;
using Microsoft.EntityFrameworkCore;

namespace HelpPlatform.Infrastructure.Data.Queries.DonationRequests.Claims;

public class ListClaimsByDonationRequestService(AppDbContext db) : IListClaimsByDonationRequestService
{
    public async Task<IEnumerable<DonationRequestClaimDto>> ListAsync(int requestId)
    {
        var result = await db.Database.SqlQuery<DonationRequestClaimDto>
        (
            $"""
             Select Id, Message, Quantity, CreatedAt, Deadline, Status, UserId, RequestId 
             FROM DonationRequestClaims 
             """
        ).Where(claim => claim.RequestId == requestId).ToListAsync();

        return result;
    }
}
