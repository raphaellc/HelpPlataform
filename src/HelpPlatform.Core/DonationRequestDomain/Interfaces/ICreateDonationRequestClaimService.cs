using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface ICreateDonationRequestClaimService
{
    public Task<Result> CreateClaim(string? message, int userId, int requestId, int quantity, DateTime? deadline, CancellationToken cancellationToken);
}
