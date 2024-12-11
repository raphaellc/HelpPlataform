using HelpPlatform.Core.DonationRequestDomain;

namespace HelpPlatform.UseCases.DonationRequests;

public record DonationRequestClaimDto(
    int Id,
    string? Message,
    int Quantity,
    DateTime CreatedAt,
    DateTime? Deadline,
    DonationRequestClaimStatusEnum Status,
    int UserId,
    int RequestId,
    string Location,
    string RequesterName,
    string RequesterEmail,
    string Resource,
    string ResourceScale
);
