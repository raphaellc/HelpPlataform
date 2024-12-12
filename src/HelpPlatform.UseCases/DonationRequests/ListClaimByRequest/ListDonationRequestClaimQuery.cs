using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.ListClaimByRequest;

public record ListDonationRequestClaimQuery(int? RequestId = null, int? UserId = null) : IQuery<Result<IEnumerable<DonationRequestClaimDto>>>;
