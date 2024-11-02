using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;

namespace HelpPlatform.UseCases.DonationRequests;

public record DonationRequestClaimDto(
    int Id,
    string? Message,
    int Quantity,
    DateTime CreatedAt,
    DateTime? Deadline,
    DonationRequestStatusEnum Status,
    int UserId,
    int RequestId);
