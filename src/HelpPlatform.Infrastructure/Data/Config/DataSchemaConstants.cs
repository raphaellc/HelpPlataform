using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;

namespace HelpPlatform.Infrastructure.Data.Config;

public static class DataSchemaConstants {
    public const int DefaultNameLength = 100;
    public const int DefaultEmailLength = 100;

    public const DonationRequestStatusEnum DefaultRequestStatus = DonationRequestStatusEnum.Open;
    public const int DefaultLocationLength = 100;
    public const int DefaultDescriptionLength = 500;

    public const DonationRequestClaimStatusEnum DefaultClaimStatus = DonationRequestClaimStatusEnum.Waiting;
    public const int DefaultClaimMessageLength = 500;
    
    public const int DefaultScaleLength = 20;
}
