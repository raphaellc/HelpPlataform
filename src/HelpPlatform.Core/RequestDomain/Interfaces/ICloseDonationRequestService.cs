using Ardalis.Result;

namespace HelpPlatform.Core.RequestDomain.Interfaces;

public interface ICloseDonationRequestService
{
    public Task<Result> CloseRequest(int requestId, CancellationToken cancellationToken);
}
