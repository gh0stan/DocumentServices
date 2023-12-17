using InvoiceXml.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Infrastructure.Persistence.ConfigurationProviders
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(e => e.ExternalId).HasComment("User Id in the system.");
            builder.Property(e => e.AbonentId).HasComment("Company Id user belongs to.");
            
            builder.Property(e => e.CreatedDate).HasComment("Date added into the system.");
            builder.Property(e => e.LastModifiedDate).HasComment("Date of the last status change.");
        }
    }
}
