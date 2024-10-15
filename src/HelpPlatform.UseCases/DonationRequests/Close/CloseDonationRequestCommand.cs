using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.Close;

public record CloseDonationRequestCommand(int requestId, CancellationToken cancellationToken) : ICommand<Result>;
