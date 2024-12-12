using Ardalis.Result;

namespace HelpPlatform.Core.DonationRequestDomain.Interfaces;

public interface ICloseDonationRequestService
{
    public Task<Result> CloseRequest(int requestId, CancellationToken cancellationToken);
}
