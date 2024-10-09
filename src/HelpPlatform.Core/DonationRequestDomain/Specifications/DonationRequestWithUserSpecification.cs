using Ardalis.Specification;

namespace HelpPlatform.Core.DonationRequestDomain.Specifications;

public sealed class DonationRequestWithUserSpecification : Specification<DonationRequest>
{
    public DonationRequestWithUserSpecification()
    {
        Query.Include(dr => dr.User);
    }
}
