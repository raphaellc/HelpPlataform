using Ardalis.Specification;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;

namespace HelpPlatform.Core.RequestDomain.Specifications;

public sealed class DonationRequestWithUserSpecification : Specification<DonationRequest>
{
    public DonationRequestWithUserSpecification()
    {
        Query.Include(dr => dr.User);
    }
}
