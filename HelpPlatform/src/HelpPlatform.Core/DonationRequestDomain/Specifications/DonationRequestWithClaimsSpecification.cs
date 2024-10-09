using Ardalis.Specification;

namespace HelpPlatform.Core.DonationRequestDomain.Specifications;

public sealed class DonationRequestWithClaimsSpecification : Specification<DonationRequest>
{
    public DonationRequestWithClaimsSpecification(int donationRequestId)
    {
        Query
            .Where(dr => dr.Id == donationRequestId)
            .Include(dr => dr.Claims);
    }
}
