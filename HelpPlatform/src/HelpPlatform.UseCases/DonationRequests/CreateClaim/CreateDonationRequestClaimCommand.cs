using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.CreateClaim;

public record CreateDonationRequestClaimCommand(
    string? message,
    int userId,
    int requestId,
    int quantity,
    DateTime? deadline) : ICommand<Result>;
