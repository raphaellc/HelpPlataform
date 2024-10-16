using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.ListClaim;

public record ListDonationRequestClaimQuery(int RequestId) : IQuery<Result<IEnumerable<DonationRequestClaimDto>>>;
