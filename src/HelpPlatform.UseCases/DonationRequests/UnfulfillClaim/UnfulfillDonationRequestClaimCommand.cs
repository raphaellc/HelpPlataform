using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.UnfulfillClaim;

public record UnfulfillDonationRequestClaimCommand(int requestId, int claimId) : ICommand<Result>;
