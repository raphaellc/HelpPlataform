using HelpPlatform.Core.NotificationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpPlatform.Infrastructure.Data.Config;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(n => n.Message)
            .IsRequired()
            .HasMaxLength(DataSchemaConstants.DefaultMessageLength); // Assumindo que você tenha uma constante para o tamanho máximo da mensagem

        builder.Property(n => n.Read)
            .IsRequired();

        builder.Property(n => n.CreatedAt)
            .IsRequired();
        
        builder.Property(n => n.UserId).IsRequired();
         
    }
}