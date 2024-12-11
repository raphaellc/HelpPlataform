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
              Select claim.Id, claim.Message, claim.Quantity, claim.CreatedAt,
                     claim.Deadline, claim.Status, claim.UserId, claim.RequestId,
                     dr.Location, u.Name as RequesterName, u.Email as RequesterEmail, rt.Name as Resource, rt.Scale as ResourceScale
              FROM DonationRequestClaims as claim
              JOIN DonationRequests as dr ON dr.Id = claim.RequestId
              JOIN ResourceTypes as rt ON dr.ResourceTypeId = rt.Id
              JOIN Users as u ON u.Id = dr.UserId
              """
         );

        if (requestId is not null) query = query.Where(claim => claim.RequestId == requestId);
        if (userId is not null) query = query.Where(claim => claim.UserId == userId);

        return await query.ToListAsync();
    }
}
