using HelpPlatform.Core.DonationRequestDomain;

namespace HelpPlatform.Web.DonationRequests.Claims;

public record MyClaimRecord(
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
    string ResourceScale);
