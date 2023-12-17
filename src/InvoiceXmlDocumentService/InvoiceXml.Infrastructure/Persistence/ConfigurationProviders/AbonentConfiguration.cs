using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoiceXml.Domain.Models;

namespace InvoiceXml.Infrastructure.Persistence.ConfigurationProviders
{
    public class AbonentConfiguration : IEntityTypeConfiguration<Abonent>
    {
        public void Configure(EntityTypeBuilder<Abonent> builder)
        {
            builder.ToTable("Abonent");
            builder.Property(e => e.Name).HasComment("Company name.");
            builder.Property(e => e.ExternalId).HasComment("Company Id in our system.");

            builder.HasMany(e => e.Users)
                    .WithOne(e => e.Abonent)
                    .HasForeignKey(e => e.AbonentId)
                    .IsRequired();
        }
    }
}
