using Ardalis.Result;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.RequestDomain;

public interface IRequest : IAggregateRoot
{
    int Id { get; }
    Result AcceptClaim(int claimId);
    IReadOnlyCollection<DonationRequestClaim> Claims { get; }
}
