using Ardalis.Result;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.UseCases.DonationRequests.Create;

public record CreateDonationRequestCommand(
    string? Description,
    DateTime Deadline,
    string Location,
    string ResourceType,
    int RequestedQuantity,
    int UserId
    ) : ICommand<Result<int>>;
