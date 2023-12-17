using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InformalDocumentService.Data.Models;

namespace InformalDocumentService.Data.ConfigurationProviders
{
    public class AbonentConfiguration : IEntityTypeConfiguration<Abonent>
    {
        public void Configure(EntityTypeBuilder<Abonent> builder)
        {
            builder.ToTable("Abonent");
            builder.HasIndex(e => e.ExternalId).IsUnique();
            builder.Property(e => e.ExternalId).HasComment("Company Id for internal use.");
            builder.Property(e => e.Name).HasComment("Company name.");
        }
    }
}
