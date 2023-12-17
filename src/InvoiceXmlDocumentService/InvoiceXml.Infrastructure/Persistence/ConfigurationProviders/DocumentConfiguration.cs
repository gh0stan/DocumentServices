using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceXml.Domain.Models;

namespace InvoiceXml.Infrastructure.Persistence.ConfigurationProviders
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Document");
            builder.Property(e => e.Title).HasComment("Document name.");
            builder.Property(e => e.SenderAbonentId).HasComment("Sender company Id.");
            builder.Property(e => e.ReceiverAbonentId).HasComment("Receiver company Id.");
            builder.Property(e => e.DocumentDate).HasComment("Date from the document itself.");
            builder.Property(e => e.Guid).HasComment("Document guid.");
            builder.Property(e => e.InvoiceTotal).HasComment("Total of the invoice.")
                .HasPrecision(18, 2);
            builder.Property(e => e.CreatedBy).HasComment("Created by user id.");

            builder.Property(e => e.CreatedDate).HasComment("Date added into the system.");
            builder.Property(e => e.LastModifiedDate).HasComment("Date of the last status change.");
        }
    }
}
