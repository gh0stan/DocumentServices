using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InformalDocumentService.Data.Models;

namespace InformalDocumentService.Data.ConfigurationProviders
{
    public class AppSettingsConfiguration : IEntityTypeConfiguration<AppSettings>
    {
        public void Configure(EntityTypeBuilder<AppSettings> builder)
        {
            builder.ToTable("AppSettings");
            builder.Property(e => e.Key).HasComment("Property key");
            builder.Property(e => e.Value).HasComment("Property value");
            builder.Property(e => e.Description).HasComment("Description");
        }
    }
}
