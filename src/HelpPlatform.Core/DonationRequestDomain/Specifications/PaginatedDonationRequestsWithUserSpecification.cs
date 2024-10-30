using System.Drawing;
using Ardalis.Specification;

namespace HelpPlatform.Core.DonationRequestDomain.Specifications;

public sealed class PaginatedDonationRequestsWithUserSpecification : Specification<DonationRequest>
{
    public PaginatedDonationRequestsWithUserSpecification(int skip, int take)
    {
        Query.Include(dr => dr.User).Skip(skip).Take(take);
    }
}
