using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;

namespace HelpPlatform.Web.DonationRequests.Claims;

public record DonationRequestClaimRecord(
    int Id,
    string? Message,
    int Quantity,
    DateTime CreatedAt,
    DateTime? Deadline,
    DonationRequestStatusEnum Status,
    int UserId,
    int RequestId);
