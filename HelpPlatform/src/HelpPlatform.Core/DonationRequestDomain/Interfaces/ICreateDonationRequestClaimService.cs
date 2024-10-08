using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface ICreateDonationRequestClaimService
{
    public Task<Result> CreateClaim(string? message, int userId, int requestId, DateTime? deadline, CancellationToken cancellationToken);
}
