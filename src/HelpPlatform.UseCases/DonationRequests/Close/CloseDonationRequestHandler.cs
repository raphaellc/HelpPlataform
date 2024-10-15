using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.Close;

public class CloseDonationRequestHandler : ICommandHandler<CloseDonationRequestCommand, Result>
{
    public Task<Result> Handle(CloseDonationRequestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
