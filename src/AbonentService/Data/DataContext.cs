using AbonentService.Data.Models;
using AbonentService.Data.ConfigurationProviders;
using Microsoft.EntityFrameworkCore;

namespace AbonentService.Data
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
        public virtual DbSet<Abonent> Abonents { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.ApplyConfiguration(new AppSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new AbonentConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
