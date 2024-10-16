using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.CancelClaim;

public record CancelDonationRequestClaimCommand(int requestId, int claimId) : ICommand<Result>;
