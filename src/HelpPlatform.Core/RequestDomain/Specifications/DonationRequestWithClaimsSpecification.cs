using Ardalis.Specification;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;

namespace HelpPlatform.Core.RequestDomain.Specifications;

public sealed class DonationRequestWithClaimsSpecification : Specification<DonationRequest>
{
    public DonationRequestWithClaimsSpecification(int donationRequestId)
    {
        Query
            .Where(dr => dr.Id == donationRequestId)
            .Include(dr => dr.Claims);
    }
}
