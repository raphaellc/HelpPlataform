using HelpPlatform.Core.DonationRequestDomain;

namespace HelpPlatform.Web.DonationRequests.Claims;

public record DonationRequestClaimRecord(
    int Id,
    string? Message,
    int Quantity,
    DateTime CreatedAt,
    DateTime? Deadline,
    DonationRequestClaimStatusEnum Status,
    int UserId,
    int RequestId);
