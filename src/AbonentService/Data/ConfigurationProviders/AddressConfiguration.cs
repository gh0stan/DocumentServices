using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AbonentService.Data.Models;

namespace AbonentService.Data.ConfigurationProviders
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.Property(e => e.AbonentId).HasComment("Abonent that owns this address.");
            builder.Property(e => e.Name).HasComment("Abonent's name for this address.");

            builder.HasOne(e => e.Abonent)
                    .WithMany(e => e.Addresses)
                    .HasForeignKey(e => e.AbonentId)
                    .IsRequired();
        }
    }
}
