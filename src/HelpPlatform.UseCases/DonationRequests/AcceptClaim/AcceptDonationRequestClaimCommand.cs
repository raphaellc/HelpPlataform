using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.AcceptClaim;

public record AcceptDonationRequestClaimCommand(int RequestId, int ClaimId) : ICommand<Result>;
