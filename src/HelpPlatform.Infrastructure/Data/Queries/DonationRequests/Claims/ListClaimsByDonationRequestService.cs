using HelpPlatform.Core.DonationRequestDomain;
using HelpPlatform.UseCases.DonationRequests;
using HelpPlatform.UseCases.DonationRequests.ListClaimByRequest;
using Microsoft.EntityFrameworkCore;

namespace HelpPlatform.Infrastructure.Data.Queries.DonationRequests.Claims;

public class ListClaimsByDonationRequestService(AppDbContext db) : IListClaimsByDonationRequestService
{
    public async Task<IEnumerable<DonationRequestClaimDto>> ListAsync(int? requestId = null, int? userId = null)
    {
         var query = db.Database.SqlQuery<DonationRequestClaimDto>(
             $"""
              Select Id, Message, Quantity, CreatedAt, Deadline, Status, UserId, RequestId 
              FROM DonationRequestClaims 
              """
         );

        if (requestId is not null) query = query.Where(claim => claim.RequestId == requestId);
        if (userId is not null) query = query.Where(claim => claim.UserId == userId);

        return await query.ToListAsync();
    }
}
