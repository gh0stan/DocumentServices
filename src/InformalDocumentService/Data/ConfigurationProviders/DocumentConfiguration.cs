using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InformalDocumentService.Data.Models;

namespace InformalDocumentService.Data.ConfigurationProviders
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Document");
            builder.Property(e => e.Title).HasComment("Title");
            builder.Property(e => e.CreatedOn).HasComment("Date when document was added to system");
            builder.Property(e => e.DocumentDate).HasComment("Document created date");
            builder.Property(e => e.SenderId).HasComment("Sender abonent Id");
            builder.Property(e => e.ReceiverId).HasComment("Receiver abonent Id");
            builder.Property(e => e.Guid).HasComment("Document guid");
        }
    }
}
