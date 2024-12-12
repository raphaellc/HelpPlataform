using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.RejectClaim;

public record RejectDonationRequestClaimCommand(int RequestId, int ClaimId) : ICommand<Result>;
