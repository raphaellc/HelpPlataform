using Ardalis.Specification;

namespace HelpPlatform.Core.RequestDomain.Specifications;

public sealed class DonationRequestTestSpecification<TRequest> : Specification<TRequest>
    where TRequest : class, IRequest
{
    public DonationRequestTestSpecification(int donationRequestId)
    {
        Query
            .Where(dr => dr.Id == donationRequestId)
            .Include(dr => dr.Claims);
    }
}
