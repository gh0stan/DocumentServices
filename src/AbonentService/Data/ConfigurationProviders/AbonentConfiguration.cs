using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbonentService.Data.Models;

namespace AbonentService.Data.ConfigurationProviders
{
    public class AbonentConfiguration : IEntityTypeConfiguration<Abonent>
    {
        public void Configure(EntityTypeBuilder<Abonent> builder)
        {
            builder.ToTable("Abonent");
            builder.Property(e => e.Name).HasComment("Company name.");

            builder.HasMany(e => e.Addresses)
                    .WithOne(e => e.Abonent)
                    .HasForeignKey(e => e.AbonentId)
                    .IsRequired();
        }
    }
}
