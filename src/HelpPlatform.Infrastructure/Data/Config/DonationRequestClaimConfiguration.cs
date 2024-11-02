using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpPlatform.Infrastructure.Data.Config;

public class DonationRequestClaimConfiguration : IEntityTypeConfiguration<DonationRequestClaim>
{
    public void Configure(EntityTypeBuilder<DonationRequestClaim> builder)
    {
        builder.Property(drClaim => drClaim.Message)
            .HasMaxLength(DataSchemaConstants.DefaultClaimMessageLength);

        builder.Property(drClaim => drClaim.Status)
            .HasDefaultValue(DataSchemaConstants.DefaultClaimStatus)
            .IsRequired();
    }
}
