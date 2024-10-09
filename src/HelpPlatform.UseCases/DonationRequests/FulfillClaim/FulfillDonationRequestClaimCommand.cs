using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.FulfillClaim;

public record FulfillDonationRequestClaimCommand(int requestId, int claimId) : ICommand<Result>;
