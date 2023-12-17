using InformalDocumentService.Data.Models;
using InformalDocumentService.Data.ConfigurationProviders;
using Microsoft.EntityFrameworkCore;

namespace InformalDocumentService.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppSettings> AppSettings { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Abonent> Abonents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.ApplyConfiguration(new AppSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new AbonentConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
