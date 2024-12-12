using HelpPlatform.Core.UserDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpPlatform.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(DataSchemaConstants.DefaultNameLength)
      .IsRequired();

    builder.Property(p => p.Email)
      .HasMaxLength(DataSchemaConstants.DefaultEmailLength)
      .IsRequired();
    
    builder.HasMany(u => u.DonationRequests)
        .WithOne(dr => dr.User)
        .HasForeignKey(dr => dr.UserId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
