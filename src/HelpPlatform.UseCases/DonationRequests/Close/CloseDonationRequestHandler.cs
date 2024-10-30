using Ardalis.Result;
using HelpPlatform.Core.DonationRequestDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.Close;

public class CloseDonationRequestHandler(ICloseDonationRequestService service) : ICommandHandler<CloseDonationRequestCommand, Result>
{
    public async Task<Result> Handle(CloseDonationRequestCommand request, CancellationToken cancellationToken)
    {
        return await service.CloseRequest(request.requestId, request.cancellationToken);
    }
}
