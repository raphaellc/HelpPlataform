using Ardalis.Specification;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;

namespace HelpPlatform.Core.RequestDomain.Specifications;

public sealed class PaginatedDonationRequestsWithUserSpecification : Specification<DonationRequest>
{
    public PaginatedDonationRequestsWithUserSpecification(int skip, int take)
    {
        Query.Include(dr => dr.User).Skip(skip).Take(take);
    }
}
