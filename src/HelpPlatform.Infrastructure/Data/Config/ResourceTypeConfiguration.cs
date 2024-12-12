using HelpPlatform.Core.ResourceTypeDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpPlatform.Infrastructure.Data.Config;

public class ResourceTypeConfiguration : IEntityTypeConfiguration<ResourceType>
{
    public void Configure(EntityTypeBuilder<ResourceType> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(DataSchemaConstants.DefaultNameLength)
            .IsRequired();

        builder.Property(p => p.Scale)
            .HasMaxLength(DataSchemaConstants.DefaultScaleLength)
            .IsRequired();
    }
}
