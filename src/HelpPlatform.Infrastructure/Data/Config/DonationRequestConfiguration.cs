using HelpPlatform.Core.RequestDomain;
using HelpPlatform.Core.RequestDomain.DonationRequestDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpPlatform.Infrastructure.Data.Config;

public class DonationRequestConfiguration : IEntityTypeConfiguration<DonationRequest>
{
    public void Configure(EntityTypeBuilder<DonationRequest> builder)
    {
        builder.Property(dr => dr.Description)
            .HasMaxLength(DataSchemaConstants.DefaultDescriptionLength);

        builder.Property(dr => dr.Deadline)
            .IsRequired();

        builder.Property(dr => dr.Location)
            .HasMaxLength(DataSchemaConstants.DefaultLocationLength)
            .IsRequired();

        builder.Property(dr => dr.RequestedQuantity)
            .IsRequired();

        builder.Property(dr => dr.FulfilledQuantity)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(dr => dr.Status)
            .HasDefaultValue(DataSchemaConstants.DefaultRequestStatus)
            .IsRequired();

        builder.HasOne(dr => dr.ResourceType)
            .WithMany()
            .HasForeignKey(dr => dr.ResourceTypeId);
        
        builder.HasOne(dr => dr.User)
            .WithMany(u => u.DonationRequests)
            .HasForeignKey(dr => dr.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(dr => dr.Claims)
            .WithOne()
            .HasForeignKey(claim => claim.RequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
